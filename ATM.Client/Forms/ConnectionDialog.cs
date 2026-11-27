using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Windows.Forms;
using ATM.Client.Net;
using ATM.UI;

namespace ATM.Client.Forms
{
    /// <summary>
    /// [Rubric: Demo via LAN / Internet] Cho phép nhập địa chỉ Load Balancer (IP LAN, IP/tên miền Internet)
    /// và kiểm tra kết nối ngay trước khi lưu. Giao diện: ConnectionDialog.Designer.cs.
    /// </summary>
    public partial class ConnectionDialog : Form
    {
        public string HostValue { get { return txtHost.Text.Trim(); } }
        public int PortValue { get { int p; int.TryParse(txtPort.Text, out p); return p; } }

        public ConnectionDialog(string host, int port)
        {
            InitializeComponent();
            DoubleBuffered = true;
            txtHost.Text = host;
            txtPort.Text = port.ToString();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            float s;
            using (Graphics g = CreateGraphics()) s = Theme.Dpi(g);
            using (GraphicsPath p = Theme.RoundRect(new RectangleF(0, 0, Width, Height), 20 * s))
                Region = new Region(p);
        }

        private bool ValidateInput(out string host, out int port)
        {
            host = HostValue;
            port = PortValue;
            if (host.Length == 0 || port < 1 || port > 65535)
            {
                SetResult("Vui lòng nhập địa chỉ và cổng hợp lệ (1 - 65535).", Theme.Warning);
                return false;
            }
            return true;
        }

        private void SetResult(string text, Color color)
        {
            lblResult.Text = text;
            lblResult.ForeColor = color;
        }

        // ------------------------------------------------------- sự kiện từ Designer

        private async void btnTest_Click(object sender, EventArgs e)
        {
            string host;
            int port;
            if (!ValidateInput(out host, out port)) return;

            btnTest.Enabled = false;
            btnSave.Enabled = false;
            SetResult("Đang kiểm tra kết nối...", Theme.Muted);
            try
            {
                var sw = Stopwatch.StartNew();
                string result = await Task.Run(delegate ()
                {
                    using (var c = new AtmClient())
                    {
                        string err = c.TryConnect(host, port, 4000);
                        return err == null ? "OK|" + c.ServerName : "ERR|" + err;
                    }
                });
                sw.Stop();
                if (result.StartsWith("OK|"))
                    SetResult("Kết nối thành công • được phục vụ bởi " + result.Substring(3) + " • " + sw.ElapsedMilliseconds + " ms", Theme.Success);
                else
                    SetResult(result.Substring(4), Theme.Danger);
            }
            finally
            {
                btnTest.Enabled = true;
                btnSave.Enabled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string host;
            int port;
            if (!ValidateInput(out host, out port)) return;
            DialogResult = DialogResult.OK;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            float s = Theme.Dpi(g);
            using (GraphicsPath p = Theme.RoundRect(new RectangleF(0.5f, 0.5f, Width - 2, Height - 2), 20 * s))
            using (var pen = new Pen(Theme.Border, 1.5f))
                g.DrawPath(pen, p);
        }
    }
}
