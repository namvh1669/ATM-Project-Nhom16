using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using ATM.Common;
using ATM.Server.Core;
using ATM.Server.Data;
using ATM.UI;

namespace ATM.Server
{
    /// <summary>
    /// Bảng điều khiển ATM Server: cấu hình, thống kê, nhật ký, danh sách client và tài khoản.
    /// Giao diện nằm trong ServerForm.Designer.cs (mở bằng Visual Studio Designer); file này chỉ chứa phần xử lý.
    /// </summary>
    public partial class ServerForm : Form
    {
        private readonly RoundedButton[] _tabs;
        private readonly Control[] _tabPanels;

        private ServerHost _host;
        private SqliteAtmRepository _repo;
        private FileLogger _logger;
        private int _logLines;
        private int _tick;
        private bool _autoStart;

        public ServerForm(string[] args)
        {
            InitializeComponent();
            DoubleBuffered = true;

            _tabs = new[] { btnTabLog, btnTabClients, btnTabAccounts };
            _tabPanels = new Control[] { cardLog, cardClients, cardAccounts };

            ApplyArgs(args);
            SelectTab(0);
            if (_autoStart) Shown += delegate { StartServer(); };
        }

        // ------------------------------------------------------- sự kiện từ Designer

        private void pnlHeader_Resize(object sender, EventArgs e)
        {
            pillStatus.Left = pnlHeader.Width - pillStatus.Width;
        }

        private void btnToggle_Click(object sender, EventArgs e)
        {
            if (_host != null && _host.IsRunning) StopServer(); else StartServer();
        }

        private void btnTab_Click(object sender, EventArgs e)
        {
            SelectTab(Array.IndexOf(_tabs, (RoundedButton)sender));
        }

        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            OnTick();
        }

        private void SelectTab(int index)
        {
            for (int i = 0; i < _tabs.Length; i++)
            {
                _tabs[i].Kind = i == index ? ButtonKind.Primary : ButtonKind.Ghost;
                _tabPanels[i].Visible = i == index;
            }
            if (index == 1) RefreshClients();
            if (index == 2) RefreshAccounts();
        }

        // --------------------------------------------------------------- arguments

        private void ApplyArgs(string[] args)
        {
            string dbDefault = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "ATMDemo", "atm.db");
            string name = null;
            int port = 0;
            string db = dbDefault;

            for (int i = 0; args != null && i < args.Length; i++)
            {
                string a = args[i].ToLowerInvariant();
                if (a == "--autostart") _autoStart = true;
                else if (a == "--port" && i + 1 < args.Length) int.TryParse(args[++i], out port);
                else if (a == "--name" && i + 1 < args.Length) name = args[++i];
                else if (a == "--db" && i + 1 < args.Length) db = args[++i];
            }

            if (port == 0) port = FindFreePort(9101);
            if (string.IsNullOrEmpty(name)) name = "Server-" + (port - 9100);

            txtName.Text = name;
            txtPort.Text = port.ToString();
            txtDb.Text = db;
        }

        /// <summary>Tìm cổng trống đầu tiên từ 'start' để chạy nhiều Server cùng lúc dễ dàng.</summary>
        private static int FindFreePort(int start)
        {
            for (int p = start; p < start + 50; p++)
            {
                try
                {
                    var l = new TcpListener(IPAddress.Any, p);
                    l.Start();
                    l.Stop();
                    return p;
                }
                catch (SocketException) { }
            }
            return start;
        }

        // ------------------------------------------------------------ start / stop

        private void StartServer()
        {
            int port;
            if (!int.TryParse(txtPort.Text, out port) || port < 1024 || port > 65535)
            {
                Dialogs.Error(this, "Cổng không hợp lệ", "Vui lòng nhập cổng trong khoảng 1024 - 65535.");
                return;
            }
            string name = txtName.Text.Trim();
            if (name.Length == 0)
            {
                Dialogs.Error(this, "Thiếu tên Server", "Vui lòng đặt tên cho Server (ví dụ Server-1).");
                return;
            }

            try
            {
                _repo = new SqliteAtmRepository(txtDb.Text.Trim());
                _repo.Initialize();

                string safe = name;
                foreach (char c in Path.GetInvalidFileNameChars()) safe = safe.Replace(c, '_');
                _logger = new FileLogger("server-" + safe);
                _logger.LineWritten += OnLogLine;

                _host = new ServerHost(name, port, _repo, _logger);
                _host.Start();
            }
            catch (SocketException ex)
            {
                _host = null;
                Dialogs.Error(this, "Không mở được cổng " + port,
                    "Cổng đang được chương trình khác sử dụng (có thể là một Server khác).\nHãy đổi sang cổng khác.\n\n" + ex.Message);
                return;
            }
            catch (Exception ex)
            {
                _host = null;
                Dialogs.Error(this, "Không thể khởi động Server", ex.Message);
                return;
            }

            SetRunningUi(true);
            _logger.Info("Cơ sở dữ liệu: " + _repo.DbPath);
            _logger.Info("File nhật ký lưu tại: " + _logger.Directory);
        }

        private void StopServer()
        {
            if (_host != null) _host.Stop();
            SetRunningUi(false);
        }

        private void SetRunningUi(bool running)
        {
            txtName.Enabled = !running;
            txtPort.Enabled = !running;
            txtDb.Enabled = !running;
            btnToggle.Text = running ? "Dừng Server" : "Khởi động";
            btnToggle.Kind = running ? ButtonKind.Danger : ButtonKind.Primary;
            pillStatus.PillText = running ? "Đang chạy • cổng " + txtPort.Text : "Đã dừng";
            pillStatus.PillColor = running ? Theme.Success : Theme.Muted;
            pillStatus.Left = pnlHeader.Width - pillStatus.Width;
            Text = running ? "ATM Server - " + txtName.Text + " (:" + txtPort.Text + ")" : "ATM Server";
        }

        // ------------------------------------------------------------------- log

        private void OnLogLine(string line)
        {
            try
            {
                if (IsDisposed || !IsHandleCreated) return;
                BeginInvoke(new Action<string>(AppendLog), line);
            }
            catch (InvalidOperationException) { }
        }

        private void AppendLog(string line)
        {
            Color c = Theme.Text;
            if (line.Contains("[ERROR]")) c = Theme.Danger;
            else if (line.Contains("[WARN]")) c = Theme.Warning;
            else if (line.Contains("[LOGIN]") || line.Contains("[REGISTER]") || line.Contains("[PIN]")) c = Theme.Accent;
            else if (line.Contains("[WITHDRAW]") || line.Contains("[DEPOSIT]") || line.Contains("[TRANSFER]")) c = Theme.Success;
            else if (line.Contains("Client #")) c = Theme.Muted;

            rtbLog.SelectionStart = rtbLog.TextLength;
            rtbLog.SelectionLength = 0;
            rtbLog.SelectionColor = c;
            rtbLog.SelectedText = line + "\n";
            rtbLog.SelectionStart = rtbLog.TextLength;
            rtbLog.ScrollToCaret();

            if (++_logLines > 1500)
            {
                rtbLog.Select(0, rtbLog.GetFirstCharIndexFromLine(500));
                rtbLog.SelectedText = "";
                _logLines -= 500;
            }
        }

        // ------------------------------------------------------------------ timer

        private void OnTick()
        {
            _tick++;
            if (_host != null && _host.IsRunning)
            {
                stClients.Value = _host.ActiveClients.ToString();
                stConn.Value = _host.TotalConnections.ToString();
                stReq.Value = _host.TotalRequests.ToString();
                TimeSpan up = DateTime.Now - _host.StartedAt;
                stUptime.Value = string.Format("{0:00}:{1:00}:{2:00}", (int)up.TotalHours, up.Minutes, up.Seconds);
            }
            if (_tabPanels[1].Visible) RefreshClients();
            if (_tabPanels[2].Visible && _tick % 3 == 0) RefreshAccounts();
        }

        private void RefreshClients()
        {
            gridClients.Rows.Clear();
            if (_host == null) return;
            foreach (SessionState s in _host.GetSessions())
                gridClients.Rows.Add(s.Id, s.Remote, s.IsLoggedIn ? s.Card + " - " + s.FullName : "(chưa đăng nhập)", s.ConnectedAt.ToString("HH:mm:ss"));
        }

        private void RefreshAccounts()
        {
            if (_repo == null) return;
            try
            {
                var list = _repo.ListAccounts();
                int first = gridAccounts.FirstDisplayedScrollingRowIndex;
                gridAccounts.Rows.Clear();
                foreach (Account a in list)
                {
                    bool locked = a.LockedUntil.HasValue && a.LockedUntil.Value > DateTime.Now;
                    gridAccounts.Rows.Add(a.CardNumber, a.FullName, Money.Format(a.Balance), a.FailedAttempts,
                        locked ? "Đang khóa" : "Bình thường", a.CreatedAt);
                }
                if (first > 0 && first < gridAccounts.Rows.Count) gridAccounts.FirstDisplayedScrollingRowIndex = first;
            }
            catch (Exception ex)
            {
                if (_logger != null) _logger.Warn("Không đọc được danh sách tài khoản: " + ex.Message);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            timerRefresh.Stop();
            if (_host != null) _host.Stop();
            base.OnFormClosing(e);
        }
    }
}
