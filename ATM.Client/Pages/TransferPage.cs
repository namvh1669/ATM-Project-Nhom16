using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using ATM.Client.Forms;
using ATM.Common;
using ATM.UI;

namespace ATM.Client.Pages
{
    /// <summary>Chuyển khoản tới thẻ khác: tra cứu tên người nhận trước khi chuyển (giao dịch nguyên tử trên Server).</summary>
    public partial class TransferPage : PageBase
    {
        private string _verifiedCard;
        private string _verifiedName;

        public TransferPage(MainForm app) : base(app)
        {
            InitializeComponent();
            DefaultButton = btnSend;
            pageHeader.Subtitle = "Số dư hiện tại: " + Money.Format(App.Balance);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            txtTo.Inner.Focus();
        }

        // ------------------------------------------------------- sự kiện từ Designer

        private void txtTo_TextChanged(object sender, EventArgs e)
        {
            if (txtTo.Text != _verifiedCard)
            {
                lblRecipient.Text = "";
                _verifiedCard = null;
            }
        }

        private async void btnLookup_Click(object sender, EventArgs e)
        {
            await LookupAsync();
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            await SendAsync();
        }

        // ------------------------------------------------------------- xử lý

        private async Task<bool> LookupAsync()
        {
            string to = txtTo.Text.Trim();
            if (to.Length < 6)
            {
                Dialogs.Warning(App, "Thiếu số thẻ", "Vui lòng nhập số thẻ người nhận.");
                return false;
            }
            if (to == App.Card)
            {
                Dialogs.Warning(App, "Không hợp lệ", "Bạn không thể chuyển khoản cho chính mình.");
                return false;
            }
            Response res = await CallAsync(new Request { Command = Commands.Lookup, ToCard = to });
            if (res == null) return false;
            if (!res.Ok)
            {
                lblRecipient.ForeColor = Theme.Danger;
                lblRecipient.Text = res.Message;
                _verifiedCard = null;
                return false;
            }
            _verifiedCard = to;
            _verifiedName = res.FullName;
            lblRecipient.ForeColor = Theme.Success;
            lblRecipient.Text = "✓  " + res.FullName;
            return true;
        }

        private async Task SendAsync()
        {
            if (App.Busy) return;
            long amount;
            if (!long.TryParse(txtAmount.Text, out amount) || amount <= 0)
            {
                Dialogs.Warning(App, "Thiếu số tiền", "Vui lòng nhập số tiền cần chuyển.");
                txtAmount.Inner.Focus();
                return;
            }
            if (amount % Limits.AmountStep != 0)
            {
                Dialogs.Warning(App, "Số tiền chưa hợp lệ", "Số tiền phải là bội số của " + Money.Format(Limits.AmountStep) + ".");
                return;
            }
            if (amount > Limits.MaxTransfer)
            {
                Dialogs.Warning(App, "Vượt hạn mức", "Tối đa " + Money.Format(Limits.MaxTransfer) + " mỗi giao dịch.");
                return;
            }
            if (amount > App.Balance)
            {
                Dialogs.Warning(App, "Số dư không đủ", "Số dư hiện tại: " + Money.Format(App.Balance));
                return;
            }

            if (_verifiedCard == null || _verifiedCard != txtTo.Text.Trim())
            {
                if (!await LookupAsync()) return;
            }

            string note = txtNote.Text.Trim();
            var rows = new List<KeyValuePair<string, string>>();
            rows.Add(Row("Người nhận", _verifiedName));
            rows.Add(Row("Số thẻ", _verifiedCard));
            rows.Add(Row("Số tiền", Money.Format(amount)));
            rows.Add(Row("Nội dung", note.Length == 0 ? "Chuyển khoản" : note));
            if (!Dialogs.Confirm(App, "Xác nhận chuyển khoản", "Vui lòng kiểm tra kỹ thông tin người nhận.", rows, "Chuyển tiền")) return;

            Response res = await CallAsync(new Request { Command = Commands.Transfer, ToCard = _verifiedCard, Amount = amount, Note = note });
            if (res == null) return;
            if (!res.Ok)
            {
                Dialogs.Error(App, "Chuyển khoản thất bại", res.Message);
                return;
            }
            App.UpdateBalance(res.Balance);
            var receipt = new List<KeyValuePair<string, string>>();
            receipt.Add(Row("Người nhận", res.FullName));
            receipt.Add(Row("Số tiền", Money.Format(amount)));
            receipt.Add(Row("Số dư còn lại", Money.Format(res.Balance)));
            receipt.Add(Row("Thời gian", res.Time));
            receipt.Add(Row("Xử lý bởi", res.ServerName));
            Dialogs.Receipt(App, "Chuyển khoản thành công", "Giao dịch đã được ghi nhận.", receipt);
            App.ShowHome();
        }
    }
}
