using System;
using System.Net.Sockets;

namespace ATM.Server.Core
{
    /// <summary>Trạng thái của MỘT kết nối Client (đăng nhập hay chưa, thẻ nào...).</summary>
    public sealed class SessionState
    {
        public int Id;
        public string Remote;
        public DateTime ConnectedAt = DateTime.Now;
        public string Card;
        public string FullName;
        public TcpClient Tcp;

        public bool IsLoggedIn { get { return Card != null; } }
    }
}
