using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using ATM.Client.Forms;
using ATM.UI;

namespace ATM.Client.Pages
{
    /// <summary>
    /// Khung giới thiệu bên trái màn hình đăng nhập (logo, tiêu đề tự co cỡ chữ, các điểm nổi bật, nút điền nhanh tài khoản demo).
    /// Là control tự vẽ nên giữ bằng code; trong Designer chỉ cần kéo vào và đặt Dock/Margin.
    /// </summary>
    [DefaultEvent("DemoClick")]
    public class BrandPanel : RoundedPanel
    {
        private readonly RoundedButton _demo = new RoundedButton();
        private readonly LogoMark _logo = new LogoMark();

        /// <summary>Phát ra khi người dùng bấm "Điền nhanh tài khoản demo".</summary>
        public event EventHandler DemoClick;

        public BrandPanel()
        {
            Radius = 24;
            FillColor = Color.FromArgb(37, 42, 120);
            GradientTo = Color.FromArgb(11, 22, 52);
            BorderColor = Theme.Border;

            _logo.Size = new Size(54, 54);
            _logo.Location = new Point(40, 38);
            Controls.Add(_logo);

            _demo.Text = "Điền nhanh tài khoản demo";
            _demo.TransparentBackground = true;
            _demo.Kind = ButtonKind.Secondary;
            _demo.Icon = IconKind.Bolt;
            _demo.Size = new Size(280, 44);
            _demo.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            _demo.Location = new Point(40, 300);
            _demo.Click += delegate
            {
                EventHandler h = DemoClick;
                if (h != null) h(this, EventArgs.Empty);
            };
            Controls.Add(_demo);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            float s;
            using (Graphics g = CreateGraphics()) s = Theme.Dpi(g);
            _demo.Top = Height - _demo.Height - (int)(34 * s);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            float s = Theme.Dpi(g);
            float x = 40f * s;
            float w = Math.Max(60f * s, Width - 2 * x);
            float limit = _demo.Top - 18f * s;

            using (var f = new Font("Segoe UI Semibold", 17f, FontStyle.Regular, GraphicsUnit.Point))
                g.DrawString("ATM Banking", f, Brushes.White, x + 68f * s, 50f * s);

            // Tiêu đề lớn: giảm dần cỡ chữ cho đến khi dòng dài nhất vừa bề rộng khung
            string l1 = "Giao dịch ATM";
            string l2 = "an toàn và nhanh chóng";
            float y = 126f * s;
            using (var sf = new StringFormat(StringFormat.GenericTypographic))
            {
                float size = 25f;
                Font hf = new Font("Segoe UI Semibold", size, FontStyle.Regular, GraphicsUnit.Point);
                while (size > 13f)
                {
                    float mw = Math.Max(g.MeasureString(l1, hf, 10000, sf).Width, g.MeasureString(l2, hf, 10000, sf).Width);
                    if (mw <= w) break;
                    size -= 1f;
                    hf.Dispose();
                    hf = new Font("Segoe UI Semibold", size, FontStyle.Regular, GraphicsUnit.Point);
                }
                float lh = hf.GetHeight(g) * 1.12f;
                g.DrawString(l1, hf, Brushes.White, x, y, sf);
                g.DrawString(l2, hf, Brushes.White, x, y + lh, sf);
                y += 2 * lh + 16f * s;
                hf.Dispose();
            }

            // Mô tả: chỉ vẽ khi còn đủ chỗ
            string desc = "Rút tiền, nộp tiền, chuyển khoản qua mạng với kênh truyền được mã hóa đầu cuối.";
            using (var f = new Font("Segoe UI", 10.5f, FontStyle.Regular, GraphicsUnit.Point))
            using (var br = new SolidBrush(Color.FromArgb(200, 226, 232, 240)))
            {
                SizeF ds = g.MeasureString(desc, f, (int)w);
                if (y + ds.Height < limit)
                {
                    g.DrawString(desc, f, br, new RectangleF(x, y, w, ds.Height + 4f * s));
                    y += ds.Height + 24f * s;
                }
            }

            // Ba điểm nổi bật: vẽ lần lượt, hết chỗ thì dừng
            IconKind[] icons = { IconKind.Shield, IconKind.Server, IconKind.Lock };
            string[] texts = { "Mã hóa RSA + AES + HMAC đầu cuối", "Nhiều máy chủ, tự động cân bằng tải", "Khóa thẻ khi nhập sai PIN 3 lần" };
            using (var f = new Font("Segoe UI", 10.5f, FontStyle.Regular, GraphicsUnit.Point))
            using (var sf = new StringFormat())
            {
                sf.Trimming = StringTrimming.EllipsisCharacter;
                sf.FormatFlags = StringFormatFlags.NoWrap;
                for (int i = 0; i < 3; i++)
                {
                    if (y + 36f * s > limit) break;
                    using (var br = new SolidBrush(Color.FromArgb(40, 255, 255, 255)))
                        g.FillEllipse(br, x, y, 34f * s, 34f * s);
                    Icons.Draw(g, icons[i], new RectangleF(x + 8f * s, y + 8f * s, 18f * s, 18f * s), Theme.Accent, 2f);
                    float tx = x + 46f * s;
                    g.DrawString(texts[i], f, Brushes.White, new RectangleF(tx, y + 6f * s, Math.Max(10f, Width - tx - 24f * s), 26f * s), sf);
                    y += 46f * s;
                }
            }
        }
    }
}
