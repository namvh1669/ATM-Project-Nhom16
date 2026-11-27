using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Windows.Forms;
using ATM.Common;
using ATM.LoadBalancer.Core;
using ATM.UI;

namespace ATM.LoadBalancer
{
    /// <summary>
    /// Bảng điều khiển Load Balancer: cấu hình, theo dõi tình trạng từng Server, nhật ký.
    /// Giao diện nằm trong LbForm.Designer.cs (mở bằng Visual Studio Designer); file này chỉ chứa phần xử lý.
    /// </summary>
    public partial class LbForm : Form
    {
        private readonly FileLogger _logger = new FileLogger("loadbalancer");
        private readonly BalancerService _lb;
        private readonly LbConfig _config;
        private int _logLines;
        private bool _autoStart;

        public LbForm(string[] args)
        {
            InitializeComponent();
            DoubleBuffered = true;

            _config = LbConfig.Load();
            for (int i = 0; args != null && i < args.Length; i++)
            {
                string a = args[i].ToLowerInvariant();
                int p;
                if (a == "--autostart") _autoStart = true;
                else if (a == "--port" && i + 1 < args.Length && int.TryParse(args[++i], out p)) _config.ListenPort = p;
            }

            _lb = new BalancerService(_logger);
            _lb.Algorithm = _config.Algorithm == "LeastConnections" ? LbAlgorithm.LeastConnections : LbAlgorithm.RoundRobin;
            foreach (BackendConfig b in _config.Backends) _lb.AddBackend(b.Name, b.Host, b.Port);
            _logger.LineWritten += OnLogLine;

            txtPort.Text = _config.ListenPort.ToString();
            ShowAlgorithm();
            ShowLanIps();
            RefreshBackends();

            if (_autoStart) Shown += delegate { StartLb(); };
        }

        // ------------------------------------------------------- sự kiện từ Designer

        private void pnlHeader_Resize(object sender, EventArgs e)
        {
            pillStatus.Location = new Point(pnlHeader.Width - pillStatus.Width, 8);
        }

        private void btnToggle_Click(object sender, EventArgs e)
        {
            if (_lb.IsRunning) StopLb(); else StartLb();
        }

        private void btnRr_Click(object sender, EventArgs e)
        {
            _lb.Algorithm = LbAlgorithm.RoundRobin;
            ShowAlgorithm();
            SaveConfig();
        }

        private void btnLc_Click(object sender, EventArgs e)
        {
            _lb.Algorithm = LbAlgorithm.LeastConnections;
            ShowAlgorithm();
            SaveConfig();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddBackend();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            RemoveSelected();
        }

        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            OnTick();
        }

        // ------------------------------------------------------------ hiển thị

        private void ShowAlgorithm()
        {
            bool rr = _lb.Algorithm == LbAlgorithm.RoundRobin;
            btnRr.Kind = rr ? ButtonKind.Primary : ButtonKind.Secondary;
            btnLc.Kind = rr ? ButtonKind.Secondary : ButtonKind.Primary;
        }

        private void ShowLanIps()
        {
            var ips = new List<string>();
            try
            {
                foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (ni.OperationalStatus != OperationalStatus.Up || ni.NetworkInterfaceType == NetworkInterfaceType.Loopback) continue;
                    foreach (UnicastIPAddressInformation ua in ni.GetIPProperties().UnicastAddresses)
                        if (ua.Address.AddressFamily == AddressFamily.InterNetwork) ips.Add(ua.Address.ToString());
                }
            }
            catch { }
            lblIp.Text = ips.Count == 0
                ? "Không tìm thấy IP mạng. Demo LAN: nhập IP của máy này vào ô \"Kết nối\" của Client."
                : "IP của máy này (nhập vào Client khi demo LAN): " + string.Join("  •  ", ips.ToArray());
        }

        // ------------------------------------------------------------ hành động

        private void StartLb()
        {
            int port;
            if (!int.TryParse(txtPort.Text, out port) || port < 1024 || port > 65535)
            {
                Dialogs.Error(this, "Cổng không hợp lệ", "Vui lòng nhập cổng trong khoảng 1024 - 65535.");
                return;
            }
            try
            {
                _lb.Start(port);
            }
            catch (SocketException ex)
            {
                Dialogs.Error(this, "Không mở được cổng " + port, "Cổng đang được sử dụng bởi chương trình khác.\n\n" + ex.Message);
                return;
            }
            _config.ListenPort = port;
            SaveConfig();
            txtPort.Enabled = false;
            btnToggle.Text = "Dừng";
            btnToggle.Kind = ButtonKind.Danger;
            pillStatus.PillText = "Đang chạy • cổng " + port;
            pillStatus.PillColor = Theme.Success;
            pillStatus.Left = pnlHeader.Width - pillStatus.Width;
        }

        private void StopLb()
        {
            _lb.Stop();
            txtPort.Enabled = true;
            btnToggle.Text = "Khởi động";
            btnToggle.Kind = ButtonKind.Primary;
            pillStatus.PillText = "Đã dừng";
            pillStatus.PillColor = Theme.Muted;
            pillStatus.Left = pnlHeader.Width - pillStatus.Width;
        }

        private void AddBackend()
        {
            string name = txtName.Text.Trim();
            string host = txtHost.Text.Trim();
            int port;
            if (name.Length == 0 || host.Length == 0 || !int.TryParse(txtBport.Text, out port) || port < 1 || port > 65535)
            {
                Dialogs.Warning(this, "Thiếu thông tin", "Nhập đủ Tên, Địa chỉ IP/host và Cổng hợp lệ của ATM Server.");
                return;
            }
            _lb.AddBackend(name, host, port);
            txtName.Text = "";
            txtHost.Text = "";
            txtBport.Text = "";
            SaveConfig();
            RefreshBackends();
        }

        private void RemoveSelected()
        {
            if (gridBackends.CurrentRow == null) return;
            Backend b = gridBackends.CurrentRow.Tag as Backend;
            if (b == null) return;
            _lb.RemoveBackend(b);
            SaveConfig();
            RefreshBackends();
        }

        private void SaveConfig()
        {
            _config.Algorithm = _lb.Algorithm.ToString();
            _config.Backends.Clear();
            foreach (Backend b in _lb.GetBackends())
                _config.Backends.Add(new BackendConfig { Name = b.Name, Host = b.Host, Port = b.Port });
            _config.Save();
        }

        // ------------------------------------------------------------------ làm mới

        private void OnTick()
        {
            RefreshBackends();
            Backend[] list = _lb.GetBackends();
            int alive = 0;
            foreach (Backend b in list) if (b.Alive) alive++;
            stAlive.Value = alive + " / " + list.Length;

            if (_lb.IsRunning)
            {
                stActive.Value = _lb.ActiveConnections.ToString();
                stTotal.Value = _lb.TotalConnections.ToString();
                TimeSpan up = DateTime.Now - _lb.StartedAt;
                stUptime.Value = string.Format("{0:00}:{1:00}:{2:00}", (int)up.TotalHours, up.Minutes, up.Seconds);
            }
        }

        private void RefreshBackends()
        {
            Backend[] list = _lb.GetBackends();
            bool rebuild = gridBackends.Rows.Count != list.Length;
            if (!rebuild)
            {
                for (int i = 0; i < list.Length; i++)
                    if (!object.ReferenceEquals(gridBackends.Rows[i].Tag, list[i])) { rebuild = true; break; }
            }

            if (rebuild)
            {
                gridBackends.Rows.Clear();
                foreach (Backend b in list)
                {
                    int idx = gridBackends.Rows.Add();
                    gridBackends.Rows[idx].Tag = b;
                }
            }

            for (int i = 0; i < list.Length; i++)
            {
                Backend b = list[i];
                DataGridViewRow r = gridBackends.Rows[i];
                r.Cells[0].Value = b.Name;
                r.Cells[1].Value = b.Address;
                r.Cells[2].Value = b.Alive ? "● UP" : "● DOWN";
                r.Cells[2].Style.ForeColor = b.Alive ? Theme.Success : Theme.Danger;
                r.Cells[2].Style.SelectionForeColor = r.Cells[2].Style.ForeColor;
                r.Cells[3].Value = b.Active;
                r.Cells[4].Value = b.Total;
                r.Cells[5].Value = b.LastCheck == DateTime.MinValue ? "-" : b.LastCheck.ToString("HH:mm:ss");
            }
        }

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
            else if (line.Contains("→ UP")) c = Theme.Success;
            else if (line.Contains("→ ")) c = Theme.Accent;

            rtbLog.SelectionStart = rtbLog.TextLength;
            rtbLog.SelectionLength = 0;
            rtbLog.SelectionColor = c;
            rtbLog.SelectedText = line + "\n";
            rtbLog.SelectionStart = rtbLog.TextLength;
            rtbLog.ScrollToCaret();

            if (++_logLines > 1200)
            {
                rtbLog.Select(0, rtbLog.GetFirstCharIndexFromLine(400));
                rtbLog.SelectedText = "";
                _logLines -= 400;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            timerRefresh.Stop();
            _lb.Stop();
            SaveConfig();
            base.OnFormClosing(e);
        }
    }
}
