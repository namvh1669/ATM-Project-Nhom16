using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ATM.UI;

namespace ATM.Client.Forms
{
    /// <summary>Logo "ATM" vẽ bằng GDI+: hình vuông bo góc gradient.</summary>
    public class LogoMark : Control
    {
        public LogoMark()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent; // trong suốt để góc bo hòa với nền phía sau (gradient, thanh trên...)
            Size = new Size(44, 44);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            var rect = new RectangleF(0.5f, 0.5f, Width - 1.5f, Height - 1.5f);
            using (GraphicsPath p = Theme.RoundRect(rect, Width * 0.26f))
            using (var br = new LinearGradientBrush(rect, Theme.Accent2, Theme.Accent, 45f))
                g.FillPath(br, p);

            float fs = Math.Max(8f, Width * 0.27f * 72f / g.DpiX * 1.05f);
            using (var f = new Font("Segoe UI", fs, FontStyle.Bold, GraphicsUnit.Point))
            using (var sf = new StringFormat())
            {
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                g.DrawString("ATM", f, Brushes.White, new RectangleF(0, 0, Width, Height), sf);
            }
        }
    }
}
