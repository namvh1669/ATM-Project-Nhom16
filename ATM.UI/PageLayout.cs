using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ATM.UI
{
    /// <summary>
    /// Vùng nội dung của một trang (kéo thả trong Designer):
    ///  - MaxContentWidth: khi cửa sổ rộng, nội dung được căn giữa và giới hạn bề rộng (px ở 96 DPI);
    ///  - MinContentHeight: khi cửa sổ thấp hơn mức này thì xuất hiện thanh cuộn thay vì bóp méo giao diện.
    /// Lề trái/phải thực tế được tính lúc chạy, lề trên/dưới lấy từ thuộc tính Padding.
    /// </summary>
    public class PagePanel : Panel
    {
        private int _maxWidth = 1060;
        private int _minHeight;

        [Category("ATM"), DefaultValue(1060), Description("Bề rộng tối đa của nội dung (px ở 96 DPI).")]
        public int MaxContentWidth
        {
            get { return _maxWidth; }
            set { _maxWidth = value; if (IsHandleCreated) ApplySide(); }
        }

        [Category("ATM"), DefaultValue(0), Description("Chiều cao tối thiểu của nội dung (px ở 96 DPI); nhỏ hơn thì có thanh cuộn.")]
        public int MinContentHeight
        {
            get { return _minHeight; }
            set { _minHeight = value; if (IsHandleCreated) ApplyMinHeight(); }
        }

        private float Dpi()
        {
            using (Graphics g = CreateGraphics()) return Theme.Dpi(g);
        }

        private void ApplySide()
        {
            if (DesignMode) return;
            float s = Dpi();
            int max = (int)(_maxWidth * s);
            int side = Math.Max((int)(32 * s), (ClientSize.Width - max) / 2);
            if (Padding.Left != side || Padding.Right != side)
                Padding = new Padding(side, Padding.Top, side, Padding.Bottom);
        }

        private void ApplyMinHeight()
        {
            if (DesignMode || _minHeight <= 0) return;
            AutoScrollMinSize = new Size(0, (int)(_minHeight * Dpi()));
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            ApplyMinHeight();
            ApplySide();
        }

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            if (IsHandleCreated) ApplySide();
        }
    }

    /// <summary>Đầu trang: nút "Quay lại" + tiêu đề + dòng mô tả, thẳng hàng với PagePanel có cùng MaxContentWidth.</summary>
    [DesignerCategory("Code"), DefaultEvent("BackClick")]
    public class PageHeader : UserControl
    {
        private readonly RoundedButton _back = new RoundedButton();
        private readonly Label _title = new Label();
        private readonly Label _sub = new Label();
        private bool _showBack = true;
        private int _maxWidth = 1060;

        public event EventHandler BackClick;

        public PageHeader()
        {
            AutoScaleMode = AutoScaleMode.None;
            Height = 90;

            _back.Text = "←  Quay lại";
            _back.Kind = ButtonKind.Secondary;
            _back.Font = Theme.F(10.5f, true);
            _back.Click += delegate
            {
                EventHandler h = BackClick;
                if (h != null) h(this, EventArgs.Empty);
            };

            _title.AutoSize = true;
            _title.UseMnemonic = false;
            _title.Font = Theme.F(20f, true);
            _title.ForeColor = Theme.Text;

            _sub.AutoSize = true;
            _sub.UseMnemonic = false;
            _sub.Font = Theme.F(10f);
            _sub.ForeColor = Theme.Muted;

            Controls.Add(_back);
            Controls.Add(_title);
            Controls.Add(_sub);
            ApplyLayout();
        }

        [Category("ATM"), DefaultValue(""), Description("Tiêu đề trang.")]
        public string Title
        {
            get { return _title.Text; }
            set { _title.Text = value ?? ""; }
        }

        [Category("ATM"), DefaultValue(""), Description("Dòng mô tả nhỏ bên dưới tiêu đề.")]
        public string Subtitle
        {
            get { return _sub.Text; }
            set { _sub.Text = value ?? ""; }
        }

        [Category("ATM"), DefaultValue(true), Description("Hiện nút Quay lại.")]
        public bool ShowBack
        {
            get { return _showBack; }
            set { _showBack = value; _back.Visible = value; ApplyLayout(); }
        }

        [Category("ATM"), DefaultValue(1060), Description("Phải giống MaxContentWidth của PagePanel để thẳng hàng.")]
        public int MaxContentWidth
        {
            get { return _maxWidth; }
            set { _maxWidth = value; ApplyLayout(); }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IButtonControl BackButton
        {
            get { return _back; }
        }

        private void ApplyLayout()
        {
            float s;
            using (Graphics g = CreateGraphics()) s = Theme.Dpi(g);
            int side = DesignMode ? (int)(40 * s) : Math.Max((int)(32 * s), (Width - (int)(_maxWidth * s)) / 2);
            int offset = _showBack ? (int)(164 * s) : 0;

            _back.SetBounds(side, (int)(24 * s), (int)(140 * s), (int)(42 * s));
            _title.Location = new Point(side + offset, (int)(14 * s));
            _sub.Location = new Point(side + offset + (int)(2 * s), (int)(52 * s));
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            ApplyLayout();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ApplyLayout();
        }
    }
}
