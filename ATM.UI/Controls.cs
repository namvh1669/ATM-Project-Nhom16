using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ATM.UI
{
    // ====================================================================== RoundedPanel
    /// <summary>Panel bo góc, có thể tô gradient. Các control con nên đặt BackColor = Transparent.</summary>
    public class RoundedPanel : Panel
    {
        private Color _fill = Theme.Surface;
        private Color _border = Theme.Border;
        private Color _gradientTo = Color.Empty;
        private int _radius = 16;

        [Category("ATM"), DefaultValue(16), Description("Bán kính bo góc (px ở 96 DPI).")]
        public int Radius { get { return _radius; } set { _radius = value; Invalidate(); } }

        [Category("ATM"), Description("Màu nền của panel.")]
        public Color FillColor { get { return _fill; } set { _fill = value; Invalidate(); } }
        private bool ShouldSerializeFillColor() { return _fill != Theme.Surface; }
        private void ResetFillColor() { FillColor = Theme.Surface; }

        [Category("ATM"), Description("Màu viền (Empty = không viền).")]
        public Color BorderColor { get { return _border; } set { _border = value; Invalidate(); } }
        private bool ShouldSerializeBorderColor() { return _border != Theme.Border; }
        private void ResetBorderColor() { BorderColor = Theme.Border; }

        [Category("ATM"), Description("Nếu khác Empty, panel tô gradient từ FillColor sang màu này.")]
        public Color GradientTo { get { return _gradientTo; } set { _gradientTo = value; Invalidate(); } }
        private bool ShouldSerializeGradientTo() { return _gradientTo != Color.Empty; }
        private void ResetGradientTo() { GradientTo = Color.Empty; }

        public RoundedPanel()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
        }

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
            if (rect.Width < 2 || rect.Height < 2) return;
            using (GraphicsPath path = Theme.RoundRect(rect, _radius * s))
            {
                if (_gradientTo != Color.Empty)
                {
                    using (var br = new LinearGradientBrush(rect, _fill, _gradientTo, 35f)) g.FillPath(br, path);
                }
                else
                {
                    using (var br = new SolidBrush(_fill)) g.FillPath(br, path);
                }
                if (_border != Color.Empty)
                    using (var pen = new Pen(_border, 1f)) g.DrawPath(pen, path);
            }
        }
    }

    // ===================================================================== RoundedButton
    public enum ButtonKind
    {
        Primary, Secondary, Success, Danger, Ghost
    }

    public class RoundedButton : Button
    {
        private bool _hover;
        private bool _down;
        private ButtonKind _kind = ButtonKind.Primary;
        private IconKind _icon = IconKind.None;
        private int _radius = 12;
        private bool _transparent;

        [Category("ATM"), DefaultValue(ButtonKind.Primary), Description("Kiểu nút: Primary (gradient), Secondary, Success, Danger, Ghost.")]
        public ButtonKind Kind { get { return _kind; } set { _kind = value; Invalidate(); } }

        [Category("ATM"), DefaultValue(IconKind.None), Description("Biểu tượng vẽ bên trái chữ.")]
        public IconKind Icon { get { return _icon; } set { _icon = value; Invalidate(); } }

        [Category("ATM"), DefaultValue(12), Description("Bán kính bo góc (px ở 96 DPI).")]
        public int Radius { get { return _radius; } set { _radius = value; Invalidate(); } }

        /// <summary>true: nền nút trong suốt (dùng khi đặt trên panel gradient).</summary>
        [Category("ATM"), DefaultValue(false)]
        public bool TransparentBackground
        {
            get { return _transparent; }
            set
            {
                _transparent = value;
                Invalidate();
            }
        }

        /// <summary>false: bấm vào nút không chuyển focus khỏi control khác (dùng cho bàn phím số ảo).</summary>
        [Category("ATM"), DefaultValue(true)]
        public bool Focusable
        {
            get { return GetStyle(ControlStyles.Selectable); }
            set { SetStyle(ControlStyles.Selectable, value); TabStop = value; }
        }

        public RoundedButton()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            Font = Theme.F(11f, true);
            ForeColor = Theme.Text;
            Cursor = Cursors.Hand;
            Height = 46;
            TabStop = true;
        }

        protected override bool ShowFocusCues { get { return false; } }

        /// <summary>Vẽ lại phần nền (kể cả gradient) của control cha vào đúng vùng của nút để góc bo không bị đen.</summary>
        private void PaintParentBackground(Graphics g)
        {
            Control p = Parent;
            if (p == null)
            {
                g.Clear(Theme.Bg);
                return;
            }
            GraphicsState state = g.Save();
            g.TranslateTransform(-Left, -Top);
            g.SetClip(new Rectangle(Left, Top, Width, Height));
            using (var pe = new PaintEventArgs(g, new Rectangle(Left, Top, Width, Height)))
            {
                InvokePaintBackground(p, pe);
                InvokePaint(p, pe);
            }
            g.Restore(state);
        }

        protected override void OnMouseEnter(EventArgs e) { _hover = true; Invalidate(); base.OnMouseEnter(e); }
        protected override void OnMouseLeave(EventArgs e) { _hover = false; _down = false; Invalidate(); base.OnMouseLeave(e); }
        protected override void OnMouseDown(MouseEventArgs e) { _down = true; Invalidate(); base.OnMouseDown(e); }
        protected override void OnMouseUp(MouseEventArgs e) { _down = false; Invalidate(); base.OnMouseUp(e); }
        protected override void OnEnabledChanged(EventArgs e) { Invalidate(); base.OnEnabledChanged(e); }
        protected override void OnTextChanged(EventArgs e) { Invalidate(); base.OnTextChanged(e); }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (_transparent) PaintParentBackground(g); else g.Clear(Theme.SurfaceOf(this));
            g.SmoothingMode = SmoothingMode.AntiAlias;
            float s = Theme.Dpi(g);
            var rect = new RectangleF(0.5f, 0.5f, Width - 1.5f, Height - 1.5f);
            if (_down) rect.Inflate(-1f * s, -1f * s);

            Color textColor = ForeColor;
            using (GraphicsPath path = Theme.RoundRect(rect, _radius * s))
            {
                if (!Enabled)
                {
                    using (var br = new SolidBrush(Theme.Surface2)) g.FillPath(br, path);
                    textColor = Theme.WithAlpha(Theme.Muted, 160);
                }
                else if (_kind == ButtonKind.Primary)
                {
                    Color c1 = _hover ? Theme.Lighten(Theme.Accent2, 0.15f) : Theme.Accent2;
                    Color c2 = _hover ? Theme.Lighten(Theme.Accent, 0.12f) : Theme.Accent;
                    using (var br = new LinearGradientBrush(rect, c1, c2, 0f)) g.FillPath(br, path);
                    textColor = Color.White;
                }
                else if (_kind == ButtonKind.Success || _kind == ButtonKind.Danger)
                {
                    Color b = _kind == ButtonKind.Success ? Theme.Success : Theme.Danger;
                    using (var br = new SolidBrush(_hover ? Theme.Lighten(b, 0.12f) : b)) g.FillPath(br, path);
                    textColor = Color.White;
                }
                else if (_kind == ButtonKind.Secondary)
                {
                    using (var br = new SolidBrush(_hover ? Color.FromArgb(40, 56, 90) : Theme.Surface2)) g.FillPath(br, path);
                    using (var pen = new Pen(_hover ? Theme.Accent : Theme.Border, 1f)) g.DrawPath(pen, path);
                }
                else
                {
                    if (_hover) using (var br = new SolidBrush(Theme.Surface2)) g.FillPath(br, path);
                    if (ForeColor == Theme.Text) textColor = _hover ? Theme.Text : Theme.Muted;
                }
            }

            var flags = TextFormatFlags.NoPadding | TextFormatFlags.NoPrefix | TextFormatFlags.EndEllipsis;
            Size ts = TextRenderer.MeasureText(g, Text, Font, new Size(int.MaxValue, int.MaxValue), flags);
            float iconSize = _icon == IconKind.None ? 0 : Math.Min(Height * 0.46f, 26f * s);
            float gap = _icon == IconKind.None || Text.Length == 0 ? 0 : 8f * s;
            float total = iconSize + gap + ts.Width;
            float x = (Width - total) / 2f;
            if (_icon != IconKind.None)
            {
                Icons.Draw(g, _icon, new RectangleF(x, (Height - iconSize) / 2f, iconSize, iconSize), textColor, 2f);
                x += iconSize + gap;
            }
            var tr = new Rectangle((int)Math.Round(x), 0, ts.Width + 2, Height);
            TextRenderer.DrawText(g, Text, Font, tr, textColor, flags | TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
        }
    }

    // =================================================================== ModernTextBox
    /// <summary>Ô nhập liệu bo góc có tiêu đề nhỏ phía trên, viền sáng khi được focus.</summary>
    [DesignerCategory("Code")]
    public class ModernTextBox : UserControl
    {
        private readonly TextBox _tb = new TextBox();
        private bool _focused;
        private bool _numbersOnly;
        private bool _fixing;
        private string _caption = "";

        public ModernTextBox()
        {
            AutoScaleMode = AutoScaleMode.None;
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            Height = 76;
            _tb.BorderStyle = BorderStyle.None;
            _tb.BackColor = Theme.Surface2;
            _tb.ForeColor = Theme.Text;
            _tb.Font = Theme.F(12.5f);
            _tb.Enter += delegate { _focused = true; Invalidate(); };
            _tb.Leave += delegate { _focused = false; Invalidate(); };
            _tb.TextChanged += TbTextChanged;
            _tb.KeyPress += TbKeyPress;
            Controls.Add(_tb);
        }

        private string _placeholder = "";

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TextBox Inner { get { return _tb; } }

        [Category("ATM"), DefaultValue(""), Description("Tiêu đề nhỏ hiển thị phía trên ô nhập.")]
        public string Caption { get { return _caption; } set { _caption = value ?? ""; PerformLayout(); Invalidate(); } }

        [Category("ATM"), DefaultValue(false), Description("Ẩn ký tự nhập (mã PIN).")]
        public bool Password { get { return _tb.UseSystemPasswordChar; } set { _tb.UseSystemPasswordChar = value; } }

        [Category("ATM"), DefaultValue(false)]
        public bool ReadOnlyBox { get { return _tb.ReadOnly; } set { _tb.ReadOnly = value; } }

        [Category("ATM"), DefaultValue(32767)]
        public int MaxLength { get { return _tb.MaxLength; } set { _tb.MaxLength = value; } }

        [Category("ATM"), DefaultValue(false), Description("Chỉ cho phép nhập chữ số.")]
        public bool NumbersOnly { get { return _numbersOnly; } set { _numbersOnly = value; } }

        [Category("ATM"), DefaultValue(""), Description("Chữ gợi ý khi ô còn trống.")]
        public string Placeholder
        {
            get { return _placeholder; }
            set
            {
                _placeholder = value ?? "";
#if NETCOREAPP
                _tb.PlaceholderText = _placeholder;
#endif
            }
        }

        [Browsable(true), DefaultValue(""), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get { return _tb.Text; }
            set { _tb.Text = value ?? ""; }
        }

        private void TbTextChanged(object sender, EventArgs e)
        {
            if (_numbersOnly && !_fixing)
            {
                string t = _tb.Text;
                var sb = new System.Text.StringBuilder();
                foreach (char c in t) if (c >= '0' && c <= '9') sb.Append(c);
                if (sb.Length != t.Length)
                {
                    _fixing = true;
                    _tb.Text = sb.ToString();
                    _tb.SelectionStart = _tb.Text.Length;
                    _fixing = false;
                }
            }
            OnTextChanged(EventArgs.Empty);
        }

        private void TbKeyPress(object sender, KeyPressEventArgs e)
        {
            if (_numbersOnly && !char.IsControl(e.KeyChar) && (e.KeyChar < '0' || e.KeyChar > '9')) e.Handled = true;
        }

        private int CaptionHeight()
        {
            return _caption.Length == 0 ? 0 : Theme.F(9.5f).Height + 6;
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            base.OnLayout(e);
            int capH = CaptionHeight();
            int boxH = Height - capH - 2;
            int pad = 14;
            _tb.Width = Math.Max(10, Width - pad * 2);
            _tb.Left = pad;
            _tb.Top = capH + Math.Max(0, (boxH - _tb.Height) / 2);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            _tb.Focus();
            base.OnMouseDown(e);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            _tb.Focus();
            base.OnGotFocus(e);
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            _tb.Enabled = Enabled;
            Invalidate();
            base.OnEnabledChanged(e);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.Clear(Theme.SurfaceOf(this));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            float s = Theme.Dpi(g);
            int capH = CaptionHeight();
            if (capH > 0)
                TextRenderer.DrawText(g, _caption, Theme.F(9.5f), new Rectangle(2, 0, Width - 4, capH), Theme.Muted,
                    TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding | TextFormatFlags.NoPrefix);

            var rect = new RectangleF(0.5f, capH + 0.5f, Width - 1.5f, Height - capH - 2.5f);
            using (GraphicsPath path = Theme.RoundRect(rect, 12 * s))
            {
                using (var br = new SolidBrush(Theme.Surface2)) g.FillPath(br, path);
                using (var pen = new Pen(_focused ? Theme.Accent : Theme.Border, _focused ? 1.8f : 1f)) g.DrawPath(pen, path);
            }
        }
    }

    // ======================================================================== PinDots
    /// <summary>Hiển thị mã PIN dạng các chấm tròn (giống ATM thật). Nhận phím số khi đang focus.</summary>
    public class PinDots : Control
    {
        private string _value = "";
        private bool _focused;
        private int _length = 6;

        public event EventHandler Changed;

        [Category("ATM"), DefaultValue(6), Description("Số chữ số của mã PIN.")]
        public int PinLength { get { return _length; } set { _length = value; Invalidate(); } }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Value { get { return _value; } }

        public PinDots()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.Selectable, true);
            TabStop = true;
            Height = 60;
            Cursor = Cursors.IBeam;
        }

        public void Append(char c)
        {
            if (c < '0' || c > '9' || _value.Length >= _length) return;
            _value += c;
            Raise();
        }

        public void Backspace()
        {
            if (_value.Length == 0) return;
            _value = _value.Substring(0, _value.Length - 1);
            Raise();
        }

        public void Clear()
        {
            _value = "";
            Raise();
        }

        private void Raise()
        {
            Invalidate();
            EventHandler h = Changed;
            if (h != null) h(this, EventArgs.Empty);
        }

        protected override void OnMouseDown(MouseEventArgs e) { Focus(); base.OnMouseDown(e); }
        protected override void OnGotFocus(EventArgs e) { _focused = true; Invalidate(); base.OnGotFocus(e); }
        protected override void OnLostFocus(EventArgs e) { _focused = false; Invalidate(); base.OnLostFocus(e); }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (e.KeyChar >= '0' && e.KeyChar <= '9') { Append(e.KeyChar); e.Handled = true; }
            else if (e.KeyChar == (char)Keys.Back) { Backspace(); e.Handled = true; }
            base.OnKeyPress(e);
        }

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
            using (GraphicsPath path = Theme.RoundRect(rect, 12 * s))
            {
                using (var br = new SolidBrush(Theme.Surface2)) g.FillPath(br, path);
                using (var pen = new Pen(_focused ? Theme.Accent : Theme.Border, _focused ? 1.8f : 1f)) g.DrawPath(pen, path);
            }

            float d = 14f * s;
            float gap = 18f * s;
            float total = _length * d + (_length - 1) * gap;
            float x = (Width - total) / 2f;
            float y = (Height - d) / 2f;
            for (int i = 0; i < _length; i++)
            {
                if (i < _value.Length)
                {
                    using (var br = new LinearGradientBrush(new RectangleF(x, y, d, d), Theme.Accent, Theme.Accent2, 45f))
                        g.FillEllipse(br, x, y, d, d);
                }
                else
                {
                    using (var pen = new Pen(Theme.Border, 1.6f * s)) g.DrawEllipse(pen, x, y, d, d);
                }
                x += d + gap;
            }
        }
    }

    // ======================================================================== NumPad
    public class NumPadKeyEventArgs : EventArgs
    {
        public string Key { get; private set; }
        public NumPadKeyEventArgs(string key) { Key = key; }
    }

    public delegate void NumPadKeyEventHandler(object sender, NumPadKeyEventArgs e);

    /// <summary>Bàn phím số ảo 3x4 giống máy ATM. Phát sự kiện KeyPressed với Key = "0".."9", "C", "←".</summary>
    [DesignerCategory("Code"), DefaultEvent("KeyPressed")]
    public class NumPad : UserControl
    {
        public event NumPadKeyEventHandler KeyPressed;

        public NumPad()
        {
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.Transparent;
            Size = new Size(240, 280);

            var table = new TableLayoutPanel();
            table.Dock = DockStyle.Fill;
            table.BackColor = Color.Transparent;
            table.ColumnCount = 3;
            table.RowCount = 4;
            for (int i = 0; i < 3; i++) table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / 3f));
            for (int i = 0; i < 4; i++) table.RowStyles.Add(new RowStyle(SizeType.Percent, 25f));

            string[] keys = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "C", "0", "←" };
            for (int i = 0; i < keys.Length; i++)
            {
                string k = keys[i];
                var b = new RoundedButton();
                b.Text = k;
                b.Kind = ButtonKind.Secondary;
                b.Dock = DockStyle.Fill;
                b.Margin = new Padding(5);
                b.Font = Theme.F(17f, true);
                b.Focusable = false;
                if (k == "C") b.ForeColor = Theme.Warning;
                if (k == "←") b.ForeColor = Theme.Muted;
                b.Click += delegate
                {
                    NumPadKeyEventHandler h = KeyPressed;
                    if (h != null) h(this, new NumPadKeyEventArgs(k));
                };
                table.Controls.Add(b, i % 3, i / 3);
            }
            Controls.Add(table);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.Clear(Theme.SurfaceOf(this));
        }
    }

    // ========================================================== GradientProgressBar
    public class GradientProgressBar : Control
    {
        private int _value;

        [Category("ATM"), DefaultValue(0), Description("Giá trị 0-100.")]
        public int Value
        {
            get { return _value; }
            set { _value = Math.Max(0, Math.Min(100, value)); Invalidate(); }
        }

        public GradientProgressBar()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            Height = 10;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.Clear(Theme.SurfaceOf(this));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            var track = new RectangleF(0, 0, Width - 1, Height - 1);
            using (GraphicsPath p = Theme.RoundRect(track, Height / 2f))
            using (var br = new SolidBrush(Theme.WithAlpha(Color.White, 28)))
                g.FillPath(br, p);

            float w = (Width - 1) * _value / 100f;
            if (w < Height) w = _value == 0 ? 0 : Height;
            if (w <= 0) return;
            var fill = new RectangleF(0, 0, w, Height - 1);
            using (GraphicsPath p = Theme.RoundRect(fill, Height / 2f))
            using (var br = new LinearGradientBrush(new RectangleF(0, 0, Math.Max(w, 2), Height), Theme.Accent2, Theme.Accent, 0f))
                g.FillPath(br, p);
        }
    }

    // ==================================================================== BusyStrip
    /// <summary>Vạch mỏng chạy qua lại khi đang xử lý yêu cầu mạng.</summary>
    public class BusyStrip : Control
    {
        private readonly Timer _timer = new Timer();
        private bool _active;
        private float _pos;

        [Category("ATM"), DefaultValue(false), Description("Đang xử lý: vạch chạy qua lại.")]
        public bool Active
        {
            get { return _active; }
            set
            {
                _active = value;
                if (value) _timer.Start(); else _timer.Stop();
                Invalidate();
            }
        }

        public BusyStrip()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            Height = 3;
            _timer.Interval = 20;
            _timer.Tick += delegate
            {
                _pos += 0.018f;
                if (_pos > 1.3f) _pos = -0.3f;
                Invalidate();
            };
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) _timer.Dispose();
            base.Dispose(disposing);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(Theme.Bg);
            if (!_active) return;
            int w = Math.Max(40, Width / 4);
            int x = (int)(_pos * Width);
            var r = new Rectangle(x, 0, w, Height);
            using (var br = new LinearGradientBrush(new Rectangle(x - 1, 0, w + 2, Height), Theme.Accent2, Theme.Accent, 0f))
                e.Graphics.FillRectangle(br, r);
        }
    }

    // ===================================================================== StatCard
    public class StatCard : Control
    {
        private string _title = "";
        private string _value = "-";
        private Color _accent = Theme.Accent;
        private IconKind _icon = IconKind.None;

        [Category("ATM"), DefaultValue("")]
        public string Title { get { return _title; } set { _title = value; Invalidate(); } }

        [Category("ATM"), DefaultValue("-")]
        public string Value { get { return _value; } set { if (_value != value) { _value = value; Invalidate(); } } }

        [Category("ATM"), Description("Màu nhấn của biểu tượng.")]
        public Color Accent { get { return _accent; } set { _accent = value; Invalidate(); } }
        private bool ShouldSerializeAccent() { return _accent != Theme.Accent; }
        private void ResetAccent() { Accent = Theme.Accent; }

        [Category("ATM"), DefaultValue(IconKind.None)]
        public IconKind Icon { get { return _icon; } set { _icon = value; Invalidate(); } }

        public StatCard()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            Height = 86;
        }

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
            using (GraphicsPath path = Theme.RoundRect(rect, 14 * s))
            {
                using (var br = new SolidBrush(Theme.Surface)) g.FillPath(br, path);
                using (var pen = new Pen(Theme.Border, 1f)) g.DrawPath(pen, path);
            }

            float ic = 40f * s;
            float pad = 16f * s;
            var circle = new RectangleF(pad, (Height - ic) / 2f, ic, ic);
            using (var br = new SolidBrush(Theme.WithAlpha(_accent, 38))) g.FillEllipse(br, circle);
            var inner = new RectangleF(circle.X + ic * 0.24f, circle.Y + ic * 0.24f, ic * 0.52f, ic * 0.52f);
            Icons.Draw(g, _icon, inner, _accent, 2f);

            int tx = (int)(pad + ic + 14 * s);
            var fl = TextFormatFlags.Left | TextFormatFlags.NoPadding | TextFormatFlags.NoPrefix | TextFormatFlags.EndEllipsis;
            TextRenderer.DrawText(g, _title, Theme.F(9.5f), new Rectangle(tx, (int)(Height * 0.17f), Width - tx - 8, (int)(Height * 0.3f)), Theme.Muted, fl | TextFormatFlags.VerticalCenter);
            TextRenderer.DrawText(g, _value, Theme.F(18f, true), new Rectangle(tx, (int)(Height * 0.44f), Width - tx - 8, (int)(Height * 0.42f)), Theme.Text, fl | TextFormatFlags.VerticalCenter);
        }
    }

    // =================================================================== StatusPill
    public class StatusPill : Control
    {
        private string _text = "";
        private Color _color = Theme.Muted;

        [Category("ATM"), Description("Màu của chấm và viền.")]
        public Color PillColor { get { return _color; } set { _color = value; Invalidate(); } }
        private bool ShouldSerializePillColor() { return _color != Theme.Muted; }
        private void ResetPillColor() { PillColor = Theme.Muted; }

        [Category("ATM"), DefaultValue(""), Description("Nội dung hiển thị (độ rộng tự co theo chữ).")]
        public string PillText
        {
            get { return _text; }
            set
            {
                _text = value ?? "";
                Size sz = TextRenderer.MeasureText(_text, Theme.F(9.5f, true), new Size(int.MaxValue, int.MaxValue), TextFormatFlags.NoPadding | TextFormatFlags.NoPrefix);
                Width = sz.Width + 44;
                Invalidate();
            }
        }

        public StatusPill()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            Height = 30;
        }

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
            using (GraphicsPath p = Theme.RoundRect(rect, Height / 2f))
            {
                using (var br = new SolidBrush(Theme.WithAlpha(_color, 36))) g.FillPath(br, p);
                using (var pen = new Pen(Theme.WithAlpha(_color, 120), 1f)) g.DrawPath(pen, p);
            }
            float d = 8f * s;
            using (var br = new SolidBrush(_color)) g.FillEllipse(br, 14f * s, (Height - d) / 2f, d, d);
            TextRenderer.DrawText(g, _text, Theme.F(9.5f, true), new Rectangle((int)(28 * s), 0, Width - (int)(28 * s), Height), _color,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding | TextFormatFlags.NoPrefix);
        }
    }

    // ==================================================================== ThemedGrid
    /// <summary>DataGridView đã áp sẵn giao diện tối của ứng dụng (thêm cột trực tiếp trong Designer).</summary>
    public class ThemedGrid : DataGridView
    {
        public ThemedGrid()
        {
            Theme.StyleGrid(this);
        }
    }
}
