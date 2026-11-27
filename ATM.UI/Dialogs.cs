using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ATM.UI
{
    public enum DialogKind
    {
        Info, Success, Warning, Error, Question
    }

    /// <summary>Các hộp thoại thông báo / xác nhận / biên lai theo phong cách của ứng dụng.</summary>
    public static class Dialogs
    {
        public static KeyValuePair<string, string> Row(string key, string value)
        {
            return new KeyValuePair<string, string>(key, value);
        }

        public static void Info(IWin32Window owner, string title, string message)
        {
            Run(owner, DialogKind.Info, title, message, null, false, "Đóng");
        }

        public static void Success(IWin32Window owner, string title, string message)
        {
            Run(owner, DialogKind.Success, title, message, null, false, "Đóng");
        }

        public static void Warning(IWin32Window owner, string title, string message)
        {
            Run(owner, DialogKind.Warning, title, message, null, false, "Đã hiểu");
        }

        public static void Error(IWin32Window owner, string title, string message)
        {
            Run(owner, DialogKind.Error, title, message, null, false, "Đóng");
        }

        public static bool Confirm(IWin32Window owner, string title, string message, IList<KeyValuePair<string, string>> rows, string okText)
        {
            return Run(owner, DialogKind.Question, title, message, rows, true, okText);
        }

        /// <summary>Biên lai giao dịch thành công.</summary>
        public static void Receipt(IWin32Window owner, string title, string message, IList<KeyValuePair<string, string>> rows)
        {
            Run(owner, DialogKind.Success, title, message, rows, false, "Hoàn tất");
        }

        private static bool Run(IWin32Window owner, DialogKind kind, string title, string message,
            IList<KeyValuePair<string, string>> rows, bool confirm, string okText)
        {
            using (var dlg = new MessageDialog(kind, title, message, rows, confirm, okText))
            {
                return dlg.ShowDialog(owner) == DialogResult.OK;
            }
        }
    }

    internal sealed class MessageDialog : Form
    {
        private readonly DialogKind _kind;
        private readonly float _s;
        private readonly int _iconTop;
        private readonly int _iconSize;

        public MessageDialog(DialogKind kind, string title, string message, IList<KeyValuePair<string, string>> rows, bool confirm, string okText)
        {
            _kind = kind;
            AutoScaleMode = AutoScaleMode.None;
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterParent;
            ShowInTaskbar = false;
            BackColor = Theme.Surface;
            DoubleBuffered = true;
            KeyPreview = true;

            using (Graphics g = CreateGraphics()) _s = Theme.Dpi(g);
            int W = (int)(460 * _s);
            int pad = (int)(30 * _s);
            int inner = W - 2 * pad;
            _iconSize = (int)(68 * _s);
            _iconTop = (int)(30 * _s);
            int y = _iconTop + _iconSize + (int)(18 * _s);

            Font tf = Theme.F(16f, true);
            Font mf = Theme.F(10.5f);
            var flags = TextFormatFlags.WordBreak | TextFormatFlags.NoPadding | TextFormatFlags.HorizontalCenter;

            Size ts = TextRenderer.MeasureText(title, tf, new Size(inner, 0), flags);
            AddLabel(title, tf, Theme.Text, pad, y, inner, ts.Height + 2);
            y += ts.Height + (int)(8 * _s);

            if (!string.IsNullOrEmpty(message))
            {
                Size ms = TextRenderer.MeasureText(message, mf, new Size(inner, 0), flags);
                AddLabel(message, mf, Theme.Muted, pad, y, inner, ms.Height + 2);
                y += ms.Height + (int)(16 * _s);
            }

            if (rows != null && rows.Count > 0)
            {
                int rowH = (int)(36 * _s);
                var panel = new RoundedPanel();
                panel.Radius = 12;
                panel.FillColor = Theme.Surface2;
                panel.BorderColor = Theme.Border;
                panel.SetBounds(pad, y, inner, rows.Count * rowH + (int)(16 * _s));
                int ry = (int)(8 * _s);
                foreach (KeyValuePair<string, string> kv in rows)
                {
                    var k = new Label();
                    k.Text = kv.Key;
                    k.Font = Theme.F(10f);
                    k.ForeColor = Theme.Muted;
                    k.BackColor = Color.Transparent;
                    k.TextAlign = ContentAlignment.MiddleLeft;
                    k.UseMnemonic = false;
                    k.SetBounds((int)(16 * _s), ry, (int)(inner * 0.42), rowH);
                    var v = new Label();
                    v.Text = kv.Value;
                    v.Font = Theme.F(10.5f, true);
                    v.ForeColor = Theme.Text;
                    v.BackColor = Color.Transparent;
                    v.TextAlign = ContentAlignment.MiddleRight;
                    v.UseMnemonic = false;
                    v.AutoEllipsis = true;
                    int vx = (int)(16 * _s) + (int)(inner * 0.42);
                    v.SetBounds(vx, ry, inner - vx - (int)(16 * _s), rowH);
                    panel.Controls.Add(k);
                    panel.Controls.Add(v);
                    ry += rowH;
                }
                Controls.Add(panel);
                y += panel.Height + (int)(18 * _s);
            }

            int bh = (int)(48 * _s);
            var ok = new RoundedButton();
            ok.Text = okText;
            ok.Kind = kind == DialogKind.Error ? ButtonKind.Danger : (kind == DialogKind.Success ? ButtonKind.Success : ButtonKind.Primary);
            ok.DialogResult = DialogResult.OK;
            if (confirm)
            {
                int gap = (int)(12 * _s);
                int bw = (inner - gap) / 2;
                var cancel = new RoundedButton();
                cancel.Text = "Hủy";
                cancel.Kind = ButtonKind.Secondary;
                cancel.DialogResult = DialogResult.Cancel;
                cancel.SetBounds(pad, y, bw, bh);
                ok.SetBounds(pad + bw + gap, y, bw, bh);
                Controls.Add(cancel);
                CancelButton = cancel;
            }
            else
            {
                ok.SetBounds(pad, y, inner, bh);
                CancelButton = ok;
            }
            Controls.Add(ok);
            AcceptButton = ok;
            y += bh + (int)(28 * _s);

            ClientSize = new Size(W, y);
            ApplyRegion();
        }

        private void AddLabel(string text, Font f, Color c, int x, int y, int w, int h)
        {
            var l = new Label();
            l.Text = text;
            l.Font = f;
            l.ForeColor = c;
            l.BackColor = Color.Transparent;
            l.TextAlign = ContentAlignment.TopCenter;
            l.UseMnemonic = false;
            l.SetBounds(x, y, w, h);
            Controls.Add(l);
        }

        private void ApplyRegion()
        {
            using (GraphicsPath p = Theme.RoundRect(new RectangleF(0, 0, Width, Height), 20 * _s))
                Region = new Region(p);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (IsHandleCreated) ApplyRegion();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Color c;
            IconKind icon;
            switch (_kind)
            {
                case DialogKind.Success: c = Theme.Success; icon = IconKind.Check; break;
                case DialogKind.Error: c = Theme.Danger; icon = IconKind.Cross; break;
                case DialogKind.Warning: c = Theme.Warning; icon = IconKind.Warning; break;
                case DialogKind.Question: c = Theme.Accent; icon = IconKind.Shield; break;
                default: c = Theme.Accent; icon = IconKind.Info; break;
            }

            float x = (Width - _iconSize) / 2f;
            using (var br = new SolidBrush(Theme.WithAlpha(c, 30)))
                g.FillEllipse(br, x - 8 * _s, _iconTop - 8 * _s, _iconSize + 16 * _s, _iconSize + 16 * _s);
            using (var br = new SolidBrush(Theme.WithAlpha(c, 60))) g.FillEllipse(br, x, _iconTop, _iconSize, _iconSize);
            float pad = _iconSize * 0.24f;
            Icons.Draw(g, icon, new RectangleF(x + pad, _iconTop + pad, _iconSize - 2 * pad, _iconSize - 2 * pad), c, 3f);

            using (GraphicsPath p = Theme.RoundRect(new RectangleF(0.5f, 0.5f, Width - 2, Height - 2), 20 * _s))
            using (var pen = new Pen(Theme.Border, 1.5f))
                g.DrawPath(pen, p);
        }
    }
}
