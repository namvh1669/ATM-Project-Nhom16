using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using ATM.Common;
using ATM.UI;

namespace ATM.Client.Pages
{
    /// <summary>Thẻ ngân hàng hiển thị số dư (gradient, chip, ẩn/hiện số dư).</summary>
    public class BalanceCard : Control
    {
        private bool _hidden;
        private RectangleF _eye = RectangleF.Empty;

        [Category("ATM"), DefaultValue("")]
        public string CardNumber { get; set; }

        [Category("ATM"), DefaultValue("")]
        public string HolderName { get; set; }

        [Category("ATM"), DefaultValue("")]
        public string ServerName { get; set; }

        [Category("ATM"), DefaultValue(0L)]
        public long Balance { get; set; }

        public BalanceCard()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            Height = 200;
        }

        public void Bind(string card, string holder, string server, long balance)
        {
            CardNumber = card;
            HolderName = holder;
            ServerName = server;
            Balance = balance;
            Invalidate();
        }

        private static string Spaced(string card)
        {
            if (string.IsNullOrEmpty(card)) return "";
            var sb = new System.Text.StringBuilder();
            for (int i = 0; i < card.Length; i++)
            {
                if (i > 0 && i % 4 == 0) sb.Append(' ');
                sb.Append(card[i]);
            }
            return sb.ToString();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.Clear(Theme.SurfaceOf(this));
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            Cursor = _eye.Contains(e.Location) ? Cursors.Hand : Cursors.Default;
            base.OnMouseMove(e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (_eye.Contains(e.Location)) { _hidden = !_hidden; Invalidate(); }
            base.OnMouseClick(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            float s = Theme.Dpi(g);
            var rect = new RectangleF(0.5f, 0.5f, Width - 1.5f, Height - 1.5f);
            float pad = 30f * s;

            // Trong Designer hiển thị dữ liệu mẫu để dễ hình dung
            bool sample = DesignMode && string.IsNullOrEmpty(CardNumber);
            string cardNo = sample ? "1000000001" : CardNumber;
            string holder = sample ? "Nguyễn Văn An" : HolderName;
            string server = sample ? "Server-1" : ServerName;
            long balance = sample ? 15000000 : Balance;

            using (GraphicsPath path = Theme.RoundRect(rect, 24f * s))
            {
                using (var br = new LinearGradientBrush(rect, Color.FromArgb(79, 70, 229), Color.FromArgb(14, 165, 233), 20f))
                    g.FillPath(br, path);

                g.SetClip(path);
                using (var br = new SolidBrush(Color.FromArgb(34, 255, 255, 255)))
                {
                    g.FillEllipse(br, Width * 0.62f, -Height * 0.55f, Height * 1.5f, Height * 1.5f);
                    g.FillEllipse(br, Width * 0.78f, Height * 0.30f, Height * 1.0f, Height * 1.0f);
                }
                g.ResetClip();
            }

            // chip
            var chip = new RectangleF(pad, 24f * s, 46f * s, 34f * s);
            using (GraphicsPath cp = Theme.RoundRect(chip, 7f * s))
            using (var br = new LinearGradientBrush(chip, Color.FromArgb(253, 224, 71), Color.FromArgb(202, 138, 4), 45f))
                g.FillPath(br, cp);
            using (var pen = new Pen(Color.FromArgb(120, 120, 70, 0), 1f))
            {
                g.DrawLine(pen, chip.X, chip.Y + chip.Height / 2, chip.Right, chip.Y + chip.Height / 2);
                g.DrawLine(pen, chip.X + chip.Width / 3, chip.Y, chip.X + chip.Width / 3, chip.Bottom);
                g.DrawLine(pen, chip.X + 2 * chip.Width / 3, chip.Y, chip.X + 2 * chip.Width / 3, chip.Bottom);
            }

            // máy chủ phục vụ (góc phải trên)
            if (!string.IsNullOrEmpty(server))
            {
                string txt = "Phục vụ bởi " + server;
                using (var f = new Font("Segoe UI", 9f, FontStyle.Regular, GraphicsUnit.Point))
                {
                    SizeF sz = g.MeasureString(txt, f);
                    float x = Width - pad - sz.Width;
                    g.DrawString(txt, f, new SolidBrush(Color.FromArgb(220, 255, 255, 255)), x, 30f * s);
                    Icons.Draw(g, IconKind.Server, new RectangleF(x - 26f * s, 28f * s, 20f * s, 20f * s), Color.FromArgb(220, 255, 255, 255), 1.8f);
                }
            }

            // nhãn + nút ẩn/hiện
            using (var f = new Font("Segoe UI", 10f, FontStyle.Regular, GraphicsUnit.Point))
            {
                string lbl = "Số dư khả dụng";
                SizeF sz = g.MeasureString(lbl, f);
                g.DrawString(lbl, f, new SolidBrush(Color.FromArgb(215, 255, 255, 255)), pad, 70f * s);

                string eyeTxt = _hidden ? "Hiện" : "Ẩn";
                using (var ef = new Font("Segoe UI", 8.5f, FontStyle.Bold, GraphicsUnit.Point))
                {
                    SizeF es = g.MeasureString(eyeTxt, ef);
                    _eye = new RectangleF(pad + sz.Width + 8f * s, 70f * s, es.Width + 20f * s, 22f * s);
                    using (GraphicsPath ep = Theme.RoundRect(_eye, 11f * s))
                    {
                        using (var br = new SolidBrush(Color.FromArgb(50, 255, 255, 255))) g.FillPath(br, ep);
                    }
                    g.DrawString(eyeTxt, ef, Brushes.White, _eye.X + 10f * s, _eye.Y + 3f * s);
                }
            }

            // số dư
            string money = _hidden ? "••••••••" : Money.Format(balance);
            using (var f = new Font("Segoe UI Semibold", 30f, FontStyle.Regular, GraphicsUnit.Point))
                g.DrawString(money, f, Brushes.White, pad, 90f * s);

            // số thẻ + chủ thẻ
            float by = Height - 44f * s;
            using (var f = new Font("Consolas", 13f, FontStyle.Bold, GraphicsUnit.Point))
                g.DrawString(Spaced(cardNo), f, new SolidBrush(Color.FromArgb(235, 255, 255, 255)), pad, by);
            if (!string.IsNullOrEmpty(holder))
            {
                using (var f = new Font("Segoe UI", 10f, FontStyle.Bold, GraphicsUnit.Point))
                {
                    string name = holder.ToUpperInvariant();
                    SizeF sz = g.MeasureString(name, f);
                    g.DrawString(name, f, new SolidBrush(Color.FromArgb(235, 255, 255, 255)), Width - pad - sz.Width, by + 4f * s);
                }
            }
        }
    }

    /// <summary>Ô chức năng lớn trên màn hình chính (biểu tượng + tiêu đề + mô tả).</summary>
    public class TileButton : Control
    {
        private bool _hover;
        private bool _down;
        private string _title = "";
        private string _subtitle = "";
        private IconKind _icon = IconKind.None;
        private Color _accent = Theme.Accent;
        private float _titleSize = 12.5f;

        [Category("ATM"), DefaultValue(12.5f), Description("Cỡ chữ tiêu đề (pt).")]
        public float TitleSize { get { return _titleSize; } set { _titleSize = value; Invalidate(); } }

        [Category("ATM"), DefaultValue("")]
        public string Title { get { return _title; } set { _title = value; Invalidate(); } }

        [Category("ATM"), DefaultValue("")]
        public string Subtitle { get { return _subtitle; } set { _subtitle = value; Invalidate(); } }

        [Category("ATM"), DefaultValue(IconKind.None)]
        public IconKind Icon { get { return _icon; } set { _icon = value; Invalidate(); } }

        [Category("ATM"), Description("Màu nhấn của biểu tượng và viền khi rê chuột.")]
        public Color Accent { get { return _accent; } set { _accent = value; Invalidate(); } }
        private bool ShouldSerializeAccent() { return _accent != Theme.Accent; }
        private void ResetAccent() { Accent = Theme.Accent; }

        public TileButton()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.Selectable, false);
            Cursor = Cursors.Hand;
            Height = 120;
        }

        protected override void OnMouseEnter(EventArgs e) { _hover = true; Invalidate(); base.OnMouseEnter(e); }
        protected override void OnMouseLeave(EventArgs e) { _hover = false; _down = false; Invalidate(); base.OnMouseLeave(e); }
        protected override void OnMouseDown(MouseEventArgs e) { _down = true; Invalidate(); base.OnMouseDown(e); }
        protected override void OnMouseUp(MouseEventArgs e) { _down = false; Invalidate(); base.OnMouseUp(e); }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.Clear(Theme.SurfaceOf(this));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            float s = Theme.Dpi(g);
            var rect = new RectangleF(0.5f, 0.5f, Width - 1.5f, Height - 1.5f);
            if (_down) rect.Inflate(-2f * s, -2f * s);

            using (GraphicsPath path = Theme.RoundRect(rect, 18f * s))
            {
                Color fill = _hover ? Color.FromArgb(28, 40, 66) : Theme.Surface;
                using (var br = new SolidBrush(fill)) g.FillPath(br, path);
                if (_hover)
                {
                    g.SetClip(path);
                    using (var br = new LinearGradientBrush(new RectangleF(rect.X, rect.Y, rect.Width, rect.Height * 0.7f),
                        Theme.WithAlpha(_accent, 46), Theme.WithAlpha(_accent, 0), 90f))
                        g.FillRectangle(br, rect.X, rect.Y, rect.Width, rect.Height * 0.7f);
                    g.ResetClip();
                }
                using (var pen = new Pen(_hover ? Theme.WithAlpha(_accent, 190) : Theme.Border, _hover ? 1.6f : 1f)) g.DrawPath(pen, path);
            }

            float d = Math.Min(54f * s, Height * 0.40f);
            float x = 20f * s;
            var fl = TextFormatFlags.Left | TextFormatFlags.NoPadding | TextFormatFlags.NoPrefix | TextFormatFlags.EndEllipsis | TextFormatFlags.SingleLine;
            Font tf = Theme.F(_titleSize, true);
            int titleH = tf.Height + 2;
            int subH = Theme.F(9.5f).Height + 2;
            float contentH = d + 8f * s + titleH + 2f * s + subH;
            float y = Math.Max(10f * s, (Height - contentH) / 2f);
            using (var br = new SolidBrush(Theme.WithAlpha(_accent, 44))) g.FillEllipse(br, x, y, d, d);
            Icons.Draw(g, _icon, new RectangleF(x + d * 0.23f, y + d * 0.23f, d * 0.54f, d * 0.54f), _accent, 2.2f);

            int tx = (int)x;
            int ty = (int)(y + d + 8f * s);
            TextRenderer.DrawText(g, _title, tf, new Rectangle(tx, ty, Width - tx - 10, titleH), Theme.Text, fl);
            TextRenderer.DrawText(g, _subtitle, Theme.F(9.5f), new Rectangle(tx, ty + titleH + (int)(2 * s), Width - tx - 10, subH), Theme.Muted, fl);
        }
    }

    /// <summary>Danh sách "nhãn - giá trị" có đường kẻ ngăn cách.</summary>
    public class InfoList : Control
    {
        private readonly List<KeyValuePair<string, string>> _rows = new List<KeyValuePair<string, string>>();

        public InfoList()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
        }

        public void SetRows(IEnumerable<KeyValuePair<string, string>> rows)
        {
            _rows.Clear();
            _rows.AddRange(rows);
            Invalidate();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.Clear(Theme.SurfaceOf(this));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            float s = Theme.Dpi(g);
            int n = Math.Max(1, _rows.Count);
            float rowH = Math.Min(46f * s, Height / (float)n);
            var fl = TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding | TextFormatFlags.NoPrefix | TextFormatFlags.EndEllipsis | TextFormatFlags.SingleLine;
            for (int i = 0; i < _rows.Count; i++)
            {
                int y = (int)(i * rowH);
                int h = (int)rowH;
                int split = (int)(Width * 0.38f);
                TextRenderer.DrawText(g, _rows[i].Key, Theme.F(10f), new Rectangle(0, y, split, h), Theme.Muted, fl | TextFormatFlags.Left);
                TextRenderer.DrawText(g, _rows[i].Value, Theme.F(10.5f, true), new Rectangle(split, y, Width - split, h), Theme.Text, fl | TextFormatFlags.Left);
                if (i < _rows.Count - 1)
                    using (var pen = new Pen(Theme.Border)) g.DrawLine(pen, 0, y + h - 1, Width, y + h - 1);
            }
        }
    }
}
