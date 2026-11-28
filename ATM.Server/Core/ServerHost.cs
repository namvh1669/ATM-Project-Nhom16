using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Threading;
using ATM.Common;
using ATM.Server.Data;

namespace ATM.Server.Core
{
    /// <summary>
    /// [Rubric: Socket Logic + Thread + Multi Client]
    /// TCP Server: một luồng (Thread) lắng nghe Accept, mỗi Client được phục vụ bởi một luồng riêng
    /// nên nhiều Client có thể giao dịch đồng thời. Mọi dữ liệu đi qua SecureChannel (RSA + AES + HMAC).
    /// </summary>
    public sealed class ServerHost
    {
        private const int HandshakeTimeoutMs = 10000;
        private const int IdleTimeoutMs = 5 * 60 * 1000;

        private readonly AtmService _service;
        private readonly FileLogger _log;
        private readonly ConcurrentDictionary<int, SessionState> _sessions = new ConcurrentDictionary<int, SessionState>();

        private TcpListener _listener;
        private Thread _acceptThread;
        private RSA _rsa;
        private volatile bool _running;
        private int _nextId;
        private long _totalConnections;
        private long _totalRequests;

        public string Name { get; private set; }
        public int Port { get; private set; }
        public bool IsRunning { get { return _running; } }
        public DateTime StartedAt { get; private set; }
        public int ActiveClients { get { return _sessions.Count; } }
        public long TotalConnections { get { return Interlocked.Read(ref _totalConnections); } }
        public long TotalRequests { get { return Interlocked.Read(ref _totalRequests); } }

        public ServerHost(string name, int port, IAtmRepository repo, FileLogger log)
        {
            Name = name;
            Port = port;
            _log = log;
            _service = new AtmService(repo, name, delegate (string m) { _log.Info(m); });
        }

        public void Start()
        {
            if (_running) return;
            _rsa = RSA.Create(2048);
            _listener = new TcpListener(IPAddress.Any, Port);
            _listener.Start(100);
            _running = true;
            StartedAt = DateTime.Now;

            _acceptThread = new Thread(AcceptLoop);
            _acceptThread.IsBackground = true;
            _acceptThread.Name = "Accept-" + Name;
            _acceptThread.Start();
            _log.Info("Server '" + Name + "' đang lắng nghe tại cổng " + Port + " (RSA-2048 sẵn sàng).");
        }

        public void Stop()
        {
            if (!_running) return;
            _running = false;
            try { _listener.Stop(); } catch { }
            foreach (SessionState s in _sessions.Values)
            {
                try { s.Tcp.Close(); } catch { }
            }
            if (_rsa != null) _rsa.Dispose();
            _log.Info("Server '" + Name + "' đã dừng.");
        }

        public SessionState[] GetSessions()
        {
            var arr = new List<SessionState>(_sessions.Values);
            arr.Sort(delegate (SessionState a, SessionState b) { return a.Id.CompareTo(b.Id); });
            return arr.ToArray();
        }

        // ---------------------------------------------------------------- threads

        private void AcceptLoop()
        {
            while (_running)
            {
                try
                {
                    TcpClient tcp = _listener.AcceptTcpClient();
                    var t = new Thread(delegate () { HandleClient(tcp); });
                    t.IsBackground = true;
                    t.Name = "Client-" + Name;
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

        private void HandleClient(TcpClient tcp)
        {
            SessionState session = new SessionState();
            session.Tcp = tcp;
            bool registered = false;
            string reason = "ngắt kết nối";

            try
            {
                session.Remote = tcp.Client.RemoteEndPoint.ToString();
                tcp.NoDelay = true;
                tcp.ReceiveTimeout = HandshakeTimeoutMs;

                using (NetworkStream ns = tcp.GetStream())
                {
                    // Bắt tay mã hóa. Nếu Client (ví dụ health-check của Load Balancer) đóng ngay thì thoát im lặng.
                    SecureChannel ch = SecureChannel.AcceptAsServer(ns, _rsa);
                    tcp.ReceiveTimeout = IdleTimeoutMs;

                    session.Id = Interlocked.Increment(ref _nextId);
                    _sessions[session.Id] = session;
                    registered = true;
                    Interlocked.Increment(ref _totalConnections);
                    _log.Info("Client #" + session.Id + " kết nối từ " + session.Remote + " (kênh mã hóa đã thiết lập)");

                    var hello = Response.Success("READY");
                    hello.ServerName = Name;
                    ch.Send(hello);

                    while (_running)
                    {
                        Request req = ch.Receive<Request>();
                        Interlocked.Increment(ref _totalRequests);
                        Response res = _service.Handle(req, session);
                        res.ServerName = Name;
                        ch.Send(res);
                    }
                }
            }
            catch (EndOfStreamException) { }
            catch (IOException) { reason = "mất kết nối / quá thời gian chờ"; }
            catch (CryptographicException ex)
            {
                reason = "lỗi bảo mật";
                if (registered) _log.Warn("Client #" + session.Id + " bị ngắt: " + ex.Message);
            }
            catch (Exception ex)
            {
                if (_running && registered) _log.Error("Client #" + session.Id + ": " + ex.Message);
            }
            finally
            {
                if (registered)
                {
                    SessionState removed;
                    _sessions.TryRemove(session.Id, out removed);
                    _log.Info("Client #" + session.Id + " " + reason + " (" + session.Remote + ")");
                }
                try { tcp.Close(); } catch { }
            }
        }
    }
}
