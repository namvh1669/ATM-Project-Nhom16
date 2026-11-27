using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using ATM.Common;

namespace ATM.LoadBalancer.Core
{
    public enum LbAlgorithm
    {
        RoundRobin,
        LeastConnections
    }

    public sealed class Backend
    {
        internal int ActiveCount;
        internal long TotalCount;
        internal int FailCount;

        public string Name { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public volatile bool Alive;
        public DateTime LastCheck;

        public int Active { get { return Volatile.Read(ref ActiveCount); } }
        public long Total { get { return Interlocked.Read(ref TotalCount); } }
        public string Address { get { return Host + ":" + Port; } }
    }

    /// <summary>
    /// [Rubric: Load Blancing + Multi Server + Thread + Socket Logic]
    /// TCP reverse-proxy: Client chỉ biết địa chỉ Load Balancer. Mỗi kết nối mới được chuyển tới một
    /// ATM Server khả dụng theo thuật toán Round-Robin hoặc Least-Connections.
    ///  - Health check: luồng nền kiểm tra từng Server mỗi 2 giây (kết nối + nhận gói chào của ATM Server).
    ///  - Failover: nếu Server được chọn không kết nối được, tự động thử Server khác.
    ///  - Dữ liệu được chuyển tiếp nguyên dạng (đã mã hóa đầu cuối giữa Client và Server).
    /// </summary>
    public sealed class BalancerService
    {
        private readonly object _lock = new object();
        private readonly List<Backend> _backends = new List<Backend>();
        private readonly FileLogger _log;

        private TcpListener _listener;
        private Thread _acceptThread;
        private Thread _healthThread;
        private volatile bool _running;
        private int _rr;
        private int _activeConnections;
        private long _totalConnections;

        public LbAlgorithm Algorithm { get; set; }
        public int ListenPort { get; private set; }
        public bool IsRunning { get { return _running; } }
        public DateTime StartedAt { get; private set; }
        public int ActiveConnections { get { return Volatile.Read(ref _activeConnections); } }
        public long TotalConnections { get { return Interlocked.Read(ref _totalConnections); } }

        public BalancerService(FileLogger log)
        {
            _log = log;
            Algorithm = LbAlgorithm.RoundRobin;
        }

        // --------------------------------------------------------------- backends

        public Backend AddBackend(string name, string host, int port)
        {
            var b = new Backend { Name = name, Host = host, Port = port };
            lock (_lock) _backends.Add(b);
            _log.Info("Thêm backend " + name + " (" + b.Address + ")");
            return b;
        }

        public void RemoveBackend(Backend b)
        {
            lock (_lock) _backends.Remove(b);
            _log.Info("Xóa backend " + b.Name + " (" + b.Address + ")");
        }

        public Backend[] GetBackends()
        {
            lock (_lock) return _backends.ToArray();
        }

        // ------------------------------------------------------------- start/stop

        public void Start(int port)
        {
            if (_running) return;
            ListenPort = port;
            _listener = new TcpListener(IPAddress.Any, port);
            _listener.Start(200);
            _running = true;
            StartedAt = DateTime.Now;

            foreach (Backend b in GetBackends()) Probe(b);

            _acceptThread = new Thread(AcceptLoop);
            _acceptThread.IsBackground = true;
            _acceptThread.Name = "LB-Accept";
            _acceptThread.Start();

            _healthThread = new Thread(HealthLoop);
            _healthThread.IsBackground = true;
            _healthThread.Name = "LB-Health";
            _healthThread.Start();

            _log.Info("Load Balancer lắng nghe cổng " + port + " - thuật toán " + Algorithm);
        }

        public void Stop()
        {
            if (!_running) return;
            _running = false;
            try { _listener.Stop(); } catch { }
            _log.Info("Load Balancer đã dừng.");
        }

        // --------------------------------------------------------------- chọn backend

        /// <summary>Chọn một backend còn sống (chưa thử) và đặt chỗ 1 kết nối đang hoạt động.</summary>
        private Backend Pick(HashSet<Backend> tried)
        {
            lock (_lock)
            {
                int n = _backends.Count;
                if (n == 0) return null;
                Backend chosen = null;

                if (Algorithm == LbAlgorithm.RoundRobin)
                {
                    for (int i = 0; i < n; i++)
                    {
                        int idx = (_rr + i) % n;
                        Backend b = _backends[idx];
                        if (b.Alive && !tried.Contains(b))
                        {
                            chosen = b;
                            _rr = idx + 1;
                            break;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < n; i++)
                    {
                        int idx = (_rr + i) % n; // xoay vòng điểm bắt đầu để phá thế hòa
                        Backend b = _backends[idx];
                        if (!b.Alive || tried.Contains(b)) continue;
                        if (chosen == null || b.Active < chosen.Active) chosen = b;
                    }
                    if (chosen != null) _rr = _backends.IndexOf(chosen) + 1;
                }

                if (chosen != null) Interlocked.Increment(ref chosen.ActiveCount);
                return chosen;
            }
        }

        // ---------------------------------------------------------------- threads

        private void AcceptLoop()
        {
            while (_running)
            {
                try
                {
                    TcpClient client = _listener.AcceptTcpClient();
                    var t = new Thread(delegate () { HandleClient(client); });
                    t.IsBackground = true;
                    t.Name = "LB-Conn";
                    t.Start();
                }
                catch (Exception ex)
                {
                    if (!_running) break;
                    _log.Warn("Accept lỗi: " + ex.Message);
                    Thread.Sleep(100);
                }
            }
        }

        private void HandleClient(TcpClient client)
        {
            string remote = "?";
            try { remote = client.Client.RemoteEndPoint.ToString(); } catch { }

            Backend chosen = null;
            TcpClient upstream = null;
            var tried = new HashSet<Backend>();

            try
            {
                while (true)
                {
                    chosen = Pick(tried);
                    if (chosen == null) break;
                    tried.Add(chosen);

                    upstream = TryConnect(chosen.Host, chosen.Port, 2000);
                    if (upstream != null) break;

                    // Failover: backend này không kết nối được -> đánh dấu chết và thử backend khác
                    Interlocked.Decrement(ref chosen.ActiveCount);
                    chosen.FailCount = 2;
                    if (chosen.Alive)
                    {
                        chosen.Alive = false;
                        _log.Warn("Backend " + chosen.Name + " (" + chosen.Address + ") không phản hồi -> DOWN, thử server khác");
                    }
                    chosen = null;
                }

                if (upstream == null)
                {
                    _log.Error("Không có ATM Server nào khả dụng cho " + remote);
                    return;
                }

                Interlocked.Increment(ref chosen.TotalCount);
                Interlocked.Increment(ref _totalConnections);
                Interlocked.Increment(ref _activeConnections);
                _log.Info(remote + " → " + chosen.Name + " (" + chosen.Address + ") [" + Algorithm + "]");

                try
                {
                    client.NoDelay = true;
                    Pipe(client, upstream);
                }
                finally
                {
                    Interlocked.Decrement(ref _activeConnections);
                    Interlocked.Decrement(ref chosen.ActiveCount);
                    _log.Info(remote + " ngắt kết nối khỏi " + chosen.Name);
                }
            }
            finally
            {
                Close(client);
                Close(upstream);
            }
        }

        /// <summary>Chuyển tiếp hai chiều bằng 2 luồng. Một chiều kết thúc thì đóng cả hai.</summary>
        private static void Pipe(TcpClient a, TcpClient b)
        {
            var t = new Thread(delegate () { Pump(a, b); });
            t.IsBackground = true;
            t.Name = "LB-Pump";
            t.Start();
            Pump(b, a);
            t.Join(2000);
        }

        private static void Pump(TcpClient from, TcpClient to)
        {
            try
            {
                Stream src = from.GetStream();
                Stream dst = to.GetStream();
                byte[] buf = new byte[16384];
                int n;
                while ((n = src.Read(buf, 0, buf.Length)) > 0)
                {
                    dst.Write(buf, 0, n);
                    dst.Flush();
                }
            }
            catch
            {
                // một đầu đã đóng
            }
            finally
            {
                Close(from);
                Close(to);
            }
        }

        private static void Close(TcpClient c)
        {
            if (c == null) return;
            try { c.Close(); } catch { }
        }

        private static TcpClient TryConnect(string host, int port, int timeoutMs)
        {
            var c = new TcpClient();
            try
            {
                var task = c.ConnectAsync(host, port);
                if (!task.Wait(timeoutMs))
                {
                    Close(c);
                    return null;
                }
                c.NoDelay = true;
                return c;
            }
            catch
            {
                Close(c);
                return null;
            }
        }

        // ------------------------------------------------------------ health check

        private void HealthLoop()
        {
            while (_running)
            {
                foreach (Backend b in GetBackends())
                {
                    if (!_running) return;
                    Probe(b);
                }
                for (int i = 0; i < 20 && _running; i++) Thread.Sleep(100); // 2 giây
            }
        }

        /// <summary>Server "sống" khi nhận được 4 byte đầu của gói chào (khung chứa khóa RSA).</summary>
        private void Probe(Backend b)
        {
            bool alive = false;
            TcpClient c = TryConnect(b.Host, b.Port, 1000);
            if (c != null)
            {
                try
                {
                    c.ReceiveTimeout = 1500;
                    byte[] hdr = new byte[4];
                    alive = c.GetStream().Read(hdr, 0, 4) == 4;
                }
                catch
                {
                    alive = false;
                }
                finally
                {
                    Close(c);
                }
            }

            b.LastCheck = DateTime.Now;
            if (alive)
            {
                b.FailCount = 0;
                if (!b.Alive)
                {
                    b.Alive = true;
                    _log.Info("Backend " + b.Name + " (" + b.Address + ") → UP");
                }
            }
            else
            {
                // Chỉ đánh dấu DOWN sau 2 lần thăm dò thất bại liên tiếp để tránh chập chờn khi máy tải nặng
                b.FailCount++;
                if (b.Alive && b.FailCount >= 2)
                {
                    b.Alive = false;
                    _log.Warn("Backend " + b.Name + " (" + b.Address + ") → DOWN");
                }
            }
        }
    }
}
