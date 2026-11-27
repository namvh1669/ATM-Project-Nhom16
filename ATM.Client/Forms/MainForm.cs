using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using ATM.Client.Net;
using ATM.Client.Pages;
using ATM.Common;
using ATM.UI;

namespace ATM.Client.Forms
{
    /// <summary>
    /// Cửa sổ chính: thanh trên cùng (trạng thái kết nối, người dùng, đồng hồ) + vùng hiển thị các trang.
    /// Giữ phiên làm việc (thẻ đang đăng nhập, số dư) và điều hướng giữa các trang.
    /// Giao diện: MainForm.Designer.cs (mở bằng Visual Studio Designer).
    /// </summary>
    public partial class MainForm : Form
    {
        private Control _page;

        public AtmClient Client { get; private set; }
        public ClientConfig Config { get; private set; }
        public string Card { get; private set; }
        public string FullName { get; private set; }
        public long Balance { get; private set; }
        public string CreatedAt { get; private set; }
        public bool Busy { get; private set; }
        public bool LoggedIn { get { return Card != null; } }

        public MainForm(AtmClient client, ClientConfig config)
        {
            InitializeComponent();
            DoubleBuffered = true;
            Client = client;
            Config = config;

            // Cỡ cửa sổ: tối đa 1180x780 nhưng không vượt màn hình nhỏ
            float f;
            using (Graphics g = Graphics.FromHwnd(IntPtr.Zero)) f = Theme.Dpi(g);
            Rectangle wa = Screen.PrimaryScreen.WorkingArea;
            int w = (int)Math.Min(1180, wa.Width / f * 0.94);
            int h = (int)Math.Min(780, wa.Height / f * 0.92);
            ClientSize = new Size(w, h);
            MinimumSize = new Size((int)(Math.Min(980, w)), (int)(Math.Min(620, h)));

            Client.Disconnected += OnClientDisconnected;

            lblClock.Text = DateTime.Now.ToString("dd/MM/yyyy  HH:mm:ss");
            UpdateUserUi();
            UpdateConnectionUi();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            // Mở trang đầu tiên sau khi form đã tự co giãn theo DPI
            ShowLogin(null);
        }

        // ------------------------------------------------------- sự kiện từ Designer

        private void pnlTop_Paint(object sender, PaintEventArgs e)
        {
            using (var pen = new Pen(Theme.Border)) e.Graphics.DrawLine(pen, 0, pnlTop.Height - 1, pnlTop.Width, pnlTop.Height - 1);
        }

        private void pnlTop_Resize(object sender, EventArgs e)
        {
            LayoutTop();
        }

        private void btnConn_Click(object sender, EventArgs e)
        {
            ShowConnectionDialog();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Logout();
        }

        private void timerClock_Tick(object sender, EventArgs e)
        {
            lblClock.Text = DateTime.Now.ToString("dd/MM/yyyy  HH:mm:ss");
        }

        // ------------------------------------------------------------ thanh trên cùng

        /// <summary>Xếp các phần tử bên phải thanh trên cùng từ phải sang trái.</summary>
        private void LayoutTop()
        {
            int h = pnlTop.Height;
            int x = pnlTop.Width - 24;
            Control[] order = { lblClock, pillConn, btnConn, btnLogout, lblUser };
            foreach (Control c in order)
            {
                if (!c.Visible) continue;
                x -= c.Width;
                c.Left = x;
                c.Top = (h - c.Height) / 2;
                x -= 14;
            }
        }

        private void UpdateUserUi()
        {
            lblUser.Visible = LoggedIn;
            btnLogout.Visible = LoggedIn;
            btnConn.Visible = !LoggedIn;
            if (LoggedIn) lblUser.Text = FullName + "  ·  " + Card;
            LayoutTop();
        }

        public void UpdateConnectionUi()
        {
            if (Client.Connected)
            {
                pillConn.PillText = "Đã kết nối • " + Client.ServerName;
                pillConn.PillColor = Theme.Success;
                toolTip.SetToolTip(pillConn, "Kênh mã hóa: RSA-2048 + AES-256-CBC + HMAC-SHA256\nVân tay khóa Server: " + Client.Fingerprint +
                    "\nLoad Balancer: " + Client.Host + ":" + Client.Port);
            }
            else
            {
                pillConn.PillText = "Chưa kết nối";
                pillConn.PillColor = Theme.Danger;
                toolTip.SetToolTip(pillConn, "Nhấn nút Kết nối để thiết lập địa chỉ máy chủ");
            }
            LayoutTop();
        }

        // ---------------------------------------------------------------- điều hướng

        public void Navigate(Control page)
        {
            Control old = _page;
            _page = page;

            // Trang tạo lúc chạy không được form tự co giãn DPI, nên co giãn thủ công một lần tại đây
            float f;
            using (Graphics g = CreateGraphics()) f = Theme.Dpi(g);
            if (Math.Abs(f - 1f) > 0.01f) page.Scale(new SizeF(f, f));

            page.Dock = DockStyle.Fill;
            pnlHost.Controls.Add(page);
            if (old != null)
            {
                pnlHost.Controls.Remove(old);
                old.Dispose();
            }
            var pb = page as PageBase;
            AcceptButton = pb != null ? pb.DefaultButton : null;
            CancelButton = pb != null ? pb.BackButton : null;
        }

        public void ShowLogin(string prefillCard)
        {
            ClearSession();
            Navigate(new LoginPage(this, string.IsNullOrEmpty(prefillCard) ? Config.LastCard : prefillCard));
        }

        public void ShowRegister()
        {
            Navigate(new RegisterPage(this));
        }

        public void ShowHome()
        {
            Navigate(new HomePage(this));
        }

        public void Go(Control page)
        {
            Navigate(page);
        }

        // ------------------------------------------------------------------- phiên

        public void SetSession(Response r)
        {
            Card = r.Card;
            FullName = r.FullName;
            Balance = r.Balance;
            CreatedAt = r.CreatedAt;
            UpdateUserUi();
            UpdateConnectionUi();
        }

        public void UpdateBalance(long balance)
        {
            Balance = balance;
        }

        private void ClearSession()
        {
            Card = null;
            FullName = null;
            Balance = 0;
            CreatedAt = null;
            UpdateUserUi();
        }

        public async void Logout()
        {
            if (Busy) return;
            SetBusy(true);
            try
            {
                if (Client.Connected) await Client.SendAsync(new Request { Command = Commands.Logout });
            }
            catch (Exception) { }
            finally { SetBusy(false); }
            ShowLogin(null);
        }

        public void SetBusy(bool busy)
        {
            Busy = busy;
            busyStrip.Active = busy;
            UseWaitCursor = busy;
        }

        // ------------------------------------------------------------------ kết nối

        public async Task<bool> EnsureConnectedAsync()
        {
            if (Client.Connected) return true;

            SetBusy(true);
            pillConn.PillText = "Đang kết nối...";
            pillConn.PillColor = Theme.Warning;
            LayoutTop();
            try
            {
                string err = await Client.TryConnectAsync(Config.Host, Config.Port, 4000);
                UpdateConnectionUi();
                if (err == null) return true;

                SetBusy(false);
                Dialogs.Error(this, "Không kết nối được máy chủ",
                    err + "\n\n" +
                    "Cần chạy trước: ATM Server (nút Khởi động) và Load Balancer (nút Khởi động), " +
                    "hoặc dùng Chay-Demo.bat.\n" +
                    "Kiểm tra địa chỉ trong nút \"Kết nối\" (" + Config.Host + ":" + Config.Port + ") và tường lửa khi demo LAN / Internet.");
                return false;
            }
            finally
            {
                SetBusy(false);
            }
        }

        public void ShowConnectionDialog()
        {
            using (var dlg = new ConnectionDialog(Config.Host, Config.Port))
            {
                if (dlg.ShowDialog(this) != DialogResult.OK) return;
                bool changed = dlg.HostValue != Config.Host || dlg.PortValue != Config.Port;
                Config.Host = dlg.HostValue;
                Config.Port = dlg.PortValue;
                Config.Save();
                if (changed) Client.Disconnect();
                UpdateConnectionUi();
            }
        }

        private void OnClientDisconnected(string reason)
        {
            try
            {
                if (IsDisposed || !IsHandleCreated) return;
                BeginInvoke(new Action<string>(OnConnectionLost), reason);
            }
            catch (InvalidOperationException) { }
        }

        /// <summary>Gọi khi mất kết nối: quay về màn hình đăng nhập, lần đăng nhập sau Load Balancer sẽ chọn Server khả dụng.</summary>
        public void OnConnectionLost(string reason)
        {
            UpdateConnectionUi();
            if (!LoggedIn) return;
            SetBusy(false);
            ShowLogin(null);
            Dialogs.Warning(this, "Mất kết nối máy chủ",
                (string.IsNullOrEmpty(reason) ? "Kết nối tới máy chủ đã bị gián đoạn." : reason) +
                "\n\nVui lòng đăng nhập lại. Hệ thống sẽ tự chọn một Server đang hoạt động.");
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            timerClock.Stop();
            Client.Disconnected -= OnClientDisconnected;
            Client.Disconnect();
            base.OnFormClosing(e);
        }
    }
}
