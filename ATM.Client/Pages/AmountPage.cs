using System;
using System.Windows.Forms;
using ATM.Client.Forms;
using ATM.Common;
using ATM.UI;

namespace ATM.Client.Pages
{
    public enum AmountMode
    {
        Withdraw,
        Deposit
    }

    /// <summary>Nhập số tiền tùy chọn (rút hoặc nộp) bằng bàn phím số ảo hoặc bàn phím máy tính. Giao diện: AmountPage.Designer.cs.</summary>
    public partial class AmountPage : PageBase
    {
        private readonly AmountMode _mode;

        public AmountPage(MainForm app, AmountMode mode) : base(app)
        {
            _mode = mode;
            InitializeComponent();
            DefaultButton = btnSubmit;

            bool withdraw = mode == AmountMode.Withdraw;
            long max = withdraw ? Limits.MaxWithdraw : Limits.MaxDeposit;

            pageHeader.Title = withdraw ? "Rút tiền" : "Nộp tiền";
            pageHeader.Subtitle = withdraw
                ? "Nhập số tiền cần rút - số dư hiện tại: " + Money.Format(App.Balance)
                : "Nhập số tiền cần nộp vào tài khoản";
            btnSubmit.Text = withdraw ? "Rút tiền" : "Nộp tiền";
            btnSubmit.Icon = withdraw ? IconKind.Withdraw : IconKind.Deposit;
            btnSubmit.Kind = withdraw ? ButtonKind.Primary : ButtonKind.Success;
            lblHint.Text = "Bội số của " + Money.Format(Limits.AmountStep) + ", tối đa " + Money.Format(max) + " mỗi giao dịch.";

            txtAmount.Inner.Font = Theme.F(16f, true);
            txtAmount.MaxLength = 10;

            chip1.Tag = 100000L;
            chip2.Tag = 200000L;
            chip3.Tag = 500000L;
            chip4.Tag = 1000000L;

            UpdatePreview();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            txtAmount.Inner.Focus();
        }

        // ------------------------------------------------------- sự kiện từ Designer

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
            UpdatePreview();
        }

        private void chip_Click(object sender, EventArgs e)
        {
            long v = ParseAmount() + (long)((Control)sender).Tag;
            long max = _mode == AmountMode.Withdraw ? Limits.MaxWithdraw : Limits.MaxDeposit;
            if (v > max) v = max;
            txtAmount.Text = v.ToString();
        }

        private void numPad_KeyPressed(object sender, NumPadKeyEventArgs e)
        {
            string t = txtAmount.Text;
            string k = e.Key;
            if (k == "C") txtAmount.Text = "";
            else if (k == "←") txtAmount.Text = t.Length > 0 ? t.Substring(0, t.Length - 1) : t;
            else if (t.Length < txtAmount.MaxLength && !(t.Length == 0 && k == "0")) txtAmount.Text = t + k;
        }

        private async void btnSubmit_Click(object sender, EventArgs e)
        {
            if (App.Busy) return;
            long v = ParseAmount();
            string err = Validate(v);
            if (err != null)
            {
                Dialogs.Warning(App, "Số tiền chưa hợp lệ", err);
                return;
            }
            if (_mode == AmountMode.Withdraw) await DoWithdraw(v);
            else await DoDeposit(v);
        }

        // ------------------------------------------------------------- xử lý

        private long ParseAmount()
        {
            long v;
            return long.TryParse(txtAmount.Text, out v) ? v : 0;
        }

        private string Validate(long v)
        {
            long max = _mode == AmountMode.Withdraw ? Limits.MaxWithdraw : Limits.MaxDeposit;
            if (v <= 0) return "Vui lòng nhập số tiền.";
            if (v % Limits.AmountStep != 0) return "Số tiền phải là bội số của " + Money.Format(Limits.AmountStep) + ".";
            if (v > max) return "Số tiền tối đa mỗi giao dịch là " + Money.Format(max) + ".";
            if (_mode == AmountMode.Withdraw && v > App.Balance) return "Số dư không đủ (" + Money.Format(App.Balance) + ").";
            return null;
        }

        private void UpdatePreview()
        {
            long v = ParseAmount();
            lblPreview.Text = v > 0 ? "= " + Money.Format(v) : "";
            string err = v > 0 ? Validate(v) : null;
            lblPreview.ForeColor = err == null ? Theme.Accent : Theme.Warning;
            btnSubmit.Enabled = v > 0;

            long after = _mode == AmountMode.Withdraw ? App.Balance - v : App.Balance + v;
            lblAfter.Text = v > 0 ? "Số dư sau giao dịch:  " + Money.Format(Math.Max(0, after)) : "Số dư hiện tại:  " + Money.Format(App.Balance);
            lblAfter.ForeColor = (_mode == AmountMode.Withdraw && after < 0) ? Theme.Danger : Theme.Muted;
        }
    }
}
