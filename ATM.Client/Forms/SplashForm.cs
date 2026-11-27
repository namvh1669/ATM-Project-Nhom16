using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;
using ATM.Client.Net;
using ATM.UI;

namespace ATM.Client.Forms
{
    /// <summary>
    /// Màn hình chào (Splash): logo + thanh tiến trình. Một luồng (Thread) nền thực hiện các bước khởi động,
    /// trong đó có bước thật sự kết nối tới Load Balancer và thiết lập kênh mã hóa.
    /// Giao diện: SplashForm.Designer.cs (bố cục) + phần vẽ nền gradient trong file này.
    /// </summary>
    public partial class SplashForm : Form
    {
        private readonly AtmClient _client;
        private readonly ClientConfig _config;

        public SplashForm(AtmClient client, ClientConfig config)
        {
            InitializeComponent();
            DoubleBuffered = true;
            _client = client;
            _config = config;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            float s;
            using (Graphics g = CreateGraphics()) s = Theme.Dpi(g);
            using (GraphicsPath p = Theme.RoundRect(new RectangleF(0, 0, Width, Height), 24 * s))
                Region = new Region(p);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            var t = new Thread(LoadWorker);
            t.IsBackground = true;
            t.Name = "Splash-Loader";
            t.Start();
        }

        private void LoadWorker()
        {
            Step("Đang khởi tạo ứng dụng...", 12, 450);
            Step("Đang đọc cấu hình kết nối...", 30, 450);
            Step("Đang kết nối tới Load Balancer " + _config.Host + ":" + _config.Port + " ...", 48, 150);

            string err = _client.TryConnect(_config.Host, _config.Port, 3000);
            string result = err == null
                ? "Kênh mã hóa RSA-2048 + AES-256 đã sẵn sàng • " + _client.ServerName
                : "Chưa kết nối được máy chủ - bạn có thể thiết lập lại ở màn hình đăng nhập";
            Step(result, 85, 700);
            Step("Sẵn sàng!", 100, 350);

            try
            {
                BeginInvoke(new Action(delegate { DialogResult = DialogResult.OK; Close(); }));
            }
            catch (InvalidOperationException) { }
        }

        private void Step(string text, int percent, int delayMs)
        {
            try
            {
                BeginInvoke(new Action(delegate { lblStatus.Text = text; AnimateTo(percent); }));
            }
            catch (InvalidOperationException) { }
            Thread.Sleep(delayMs);
        }

        private void AnimateTo(int percent)
        {
            var t = new System.Windows.Forms.Timer();
            t.Interval = 15;
            t.Tick += delegate
            {
                if (progress.Value >= percent) { t.Stop(); t.Dispose(); return; }
                progress.Value = progress.Value + 1;
            };
            t.Start();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            var rect = new Rectangle(0, 0, Width, Height);
            using (var br = new LinearGradientBrush(rect, Color.FromArgb(10, 16, 30), Color.FromArgb(28, 36, 92), 60f))
                g.FillRectangle(br, rect);
            using (var br = new SolidBrush(Theme.WithAlpha(Theme.Accent, 22)))
                g.FillEllipse(br, Width * 0.55f, -Height * 0.35f, Height * 1.1f, Height * 1.1f);
            using (var br = new SolidBrush(Theme.WithAlpha(Theme.Accent2, 26)))
                g.FillEllipse(br, -Width * 0.12f, Height * 0.55f, Height * 0.9f, Height * 0.9f);
            using (var pen = new Pen(Theme.Border, 1.5f))
                g.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);
        }
    }
}
