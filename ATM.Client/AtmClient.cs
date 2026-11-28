using System;
using System.IO;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using ATM.Common;

namespace ATM.Client.Net
{
    public class ConnectionLostException : Exception
    {
        public ConnectionLostException(string message) : base(message) { }
    }

    /// <summary>
    /// [Rubric: Socket Logic + Thread + Cryptography]
    /// Kết nối TCP tới Load Balancer (hoặc trực tiếp tới Server), thiết lập kênh mã hóa,
    /// gửi yêu cầu / nhận phản hồi. Một luồng nền gửi PING định kỳ để giữ kết nối và phát hiện Server chết.
    /// </summary>
    public sealed class AtmClient : IDisposable
    {
        private readonly object _lock = new object();
        private TcpClient _tcp;
        private SecureChannel _channel;
        private Thread _heartbeat;
        private volatile bool _stopHeartbeat;

        /// <summary>Mã lỗi trong Response khi SendAsync mất kết nối (không ném ngoại lệ qua Task.Run).</summary>
        public const string ConnectionLostCode = "CONNECTION_LOST";

        public string Host { get; private set; }
        public int Port { get; private set; }
        public string ServerName { get; private set; }
        public string Fingerprint { get; private set; }
        public bool Connected { get { return _channel != null; } }

        /// <summary>Phát ra từ luồng nền khi mất kết nối.</summary>
        public event Action<string> Disconnected;

        public void Connect(string host, int port, int timeoutMs)
        {
            lock (_lock)
            {
                Disconnect(false);

                var tcp = new TcpClient();
                try
                {
                    var task = tcp.ConnectAsync(host, port);
                    if (!task.Wait(timeoutMs)) throw new TimeoutException("Hết thời gian chờ kết nối tới " + host + ":" + port);
                    tcp.NoDelay = true;
                    tcp.ReceiveTimeout = 15000;
                    tcp.SendTimeout = 15000;

                    NetworkStream ns = tcp.GetStream();
                    SecureChannel ch = SecureChannel.ConnectAsClient(ns);
                    Response hello = ch.Receive<Response>();

                    _tcp = tcp;
                    _channel = ch;
                    Host = host;
                    Port = port;
                    ServerName = hello.ServerName;
                    Fingerprint = ch.PeerFingerprint;
                }
                catch (AggregateException ex)
                {
                    try { tcp.Close(); } catch { }
                    Exception inner = ex.InnerException ?? ex;
                    throw new IOException(inner.Message, inner);
                }
                catch
                {
                    try { tcp.Close(); } catch { }
                    throw;
                }
            }
            StartHeartbeat();
        }

        /// <summary>
        /// Kết nối nhưng KHÔNG ném ngoại lệ ra ngoài: trả về null nếu thành công, ngược lại trả về thông báo lỗi dễ hiểu.
        /// (Dùng cho giao diện - tránh việc trình gỡ lỗi Visual Studio dừng ở ngoại lệ đã được xử lý.)
        /// </summary>
        public string TryConnect(string host, int port, int timeoutMs)
        {
            try
            {
                Connect(host, port, timeoutMs);
                return null;
            }
            catch (Exception ex)
            {
                return Describe(ex, host, port);
            }
        }

        public Task<string> TryConnectAsync(string host, int port, int timeoutMs)
        {
            return Task.Run(delegate () { return TryConnect(host, port, timeoutMs); });
        }

        /// <summary>Chuyển ngoại lệ mạng thành câu tiếng Việt chỉ rõ nguyên nhân thường gặp.</summary>
        public static string Describe(Exception ex, string host, int port)
        {
            string addr = host + ":" + port;
            for (Exception e = ex; e != null; e = e.InnerException)
            {
                var se = e as SocketException;
                if (se != null)
                {
                    switch (se.SocketErrorCode)
                    {
                        case SocketError.ConnectionRefused:
                            return "Máy chủ từ chối kết nối tới " + addr + ". Load Balancer chưa chạy (hãy bấm Khởi động) hoặc sai địa chỉ / cổng.";
                        case SocketError.HostNotFound:
                        case SocketError.NoData:
                        case SocketError.TryAgain:
                            return "Không tìm thấy máy chủ \"" + host + "\". Hãy kiểm tra lại địa chỉ.";
                        case SocketError.TimedOut:
                            return "Hết thời gian chờ khi kết nối tới " + addr + ". Kiểm tra IP, mạng và tường lửa.";
                        case SocketError.NetworkUnreachable:
                        case SocketError.HostUnreachable:
                            return "Không có đường mạng tới " + host + ". Kiểm tra kết nối mạng / IP.";
                    }
                }
                if (e is TimeoutException)
                    return "Hết thời gian chờ khi kết nối tới " + addr + ". Kiểm tra IP, mạng và tường lửa.";
            }
            if (ex is EndOfStreamException)
                return "Đã kết nối tới " + addr + " nhưng bị đóng ngay. Thường do chưa có ATM Server nào đang chạy phía sau Load Balancer " +
                       "(hãy bấm Khởi động ở cửa sổ ATM Server).";
            if (ex is InvalidDataException)
                return "Địa chỉ " + addr + " không phải ATM Server / Load Balancer (sai giao thức).";
            return ex.InnerException != null ? ex.InnerException.Message : ex.Message;
        }

        /// <summary>Gửi một yêu cầu và chờ phản hồi (đồng bộ, an toàn đa luồng).</summary>
        public Response Send(Request request)
        {
            lock (_lock)
            {
                if (_channel == null) throw new ConnectionLostException("Chưa kết nối tới máy chủ.");
                try
                {
                    _channel.Send(request);
                    Response res = _channel.Receive<Response>();
                    if (!string.IsNullOrEmpty(res.ServerName)) ServerName = res.ServerName;
                    return res;
                }
                catch (Exception ex)
                {
                    if (ex is IOException || ex is SocketException || ex is ObjectDisposedException ||
                        ex is CryptographicException || ex is InvalidDataException)
                    {
                        string last = ServerName;
                        Disconnect(false);
                        RaiseDisconnected("Mất kết nối với " + (string.IsNullOrEmpty(last) ? "máy chủ" : last));
                        throw new ConnectionLostException("Mất kết nối với " + (string.IsNullOrEmpty(last) ? "máy chủ" : last) + ".");
                    }
                    throw;
                }
            }
        }

        /// <summary>
        /// Gửi không chặn giao diện. Mất kết nối được trả về dưới dạng Response có Code = ConnectionLostCode
        /// (không ném ngoại lệ qua ranh giới Task.Run).
        /// </summary>
        public Task<Response> SendAsync(Request request)
        {
            return Task.Run(delegate ()
            {
                try
                {
                    return Send(request);
                }
                catch (ConnectionLostException ex)
                {
                    return Response.Fail(ex.Message, ConnectionLostCode);
                }
            });
        }

        public void Disconnect()
        {
            lock (_lock) Disconnect(true);
        }

        private void Disconnect(bool stopHeartbeat)
        {
            if (stopHeartbeat) _stopHeartbeat = true;
            _channel = null;
            if (_tcp != null)
            {
                try { _tcp.Close(); } catch { }
                _tcp = null;
            }
        }

        private void RaiseDisconnected(string reason)
        {
            Action<string> h = Disconnected;
            if (h != null)
            {
                try { h(reason); } catch { }
            }
        }

        private void StartHeartbeat()
        {
            _stopHeartbeat = false;
            if (_heartbeat != null && _heartbeat.IsAlive) return;
            _heartbeat = new Thread(HeartbeatLoop);
            _heartbeat.IsBackground = true;
            _heartbeat.Name = "ATM-Heartbeat";
            _heartbeat.Start();
        }

        private void HeartbeatLoop()
        {
            while (!_stopHeartbeat)
            {
                for (int i = 0; i < 100 && !_stopHeartbeat; i++) Thread.Sleep(100); // 10 giây
                if (_stopHeartbeat || !Connected) continue;
                try
                {
                    Send(new Request { Command = Commands.Ping });
                }
                catch
                {
                    // Send() đã tự phát sự kiện Disconnected
                }
            }
        }

        public void Dispose()
        {
            Disconnect();
        }
    }
}
