using System;
using System.Windows.Forms;
using ATM.Client.Forms;
using ATM.Common;
using ATM.UI;

namespace ATM.Client.Pages
{
    /// <summary>Đổi mã PIN (cần nhập PIN hiện tại). Giao diện: ChangePinPage.Designer.cs.</summary>
    public partial class ChangePinPage : PageBase
    {
        public ChangePinPage(MainForm app) : base(app)
        {
            InitializeComponent();
            DefaultButton = btnUpdate;

            txtNew.Caption = "Mã PIN mới (" + Limits.PinLength + " chữ số)";
            txtOld.MaxLength = Limits.PinLength;
            txtNew.MaxLength = Limits.PinLength;
            txtNew2.MaxLength = Limits.PinLength;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            txtOld.Inner.Focus();
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (App.Busy) return;
            if (txtOld.Text.Length != Limits.PinLength || txtNew.Text.Length != Limits.PinLength)
            {
                Dialogs.Warning(App, "Thiếu thông tin", "Mã PIN gồm đúng " + Limits.PinLength + " chữ số.");
                return;
            }
            if (txtNew.Text != txtNew2.Text)
            {
                Dialogs.Warning(App, "Mã PIN không khớp", "Hai lần nhập mã PIN mới chưa giống nhau.");
                txtNew2.Text = "";
                txtNew2.Inner.Focus();
                return;
            }
            if (txtNew.Text == txtOld.Text)
            {
                Dialogs.Warning(App, "Mã PIN trùng", "Mã PIN mới phải khác mã PIN hiện tại.");
                return;
            }

            Response res = await CallAsync(new Request { Command = Commands.ChangePin, Pin = txtOld.Text, NewPin = txtNew.Text });
            if (res == null) return;
            if (!res.Ok)
            {
                Dialogs.Error(App, "Đổi PIN thất bại", res.Message);
                txtOld.Text = "";
                txtOld.Inner.Focus();
                return;
            }
            Dialogs.Success(App, "Đổi mã PIN thành công", "Từ lần đăng nhập sau, hãy dùng mã PIN mới.");
            App.ShowHome();
        }
    }
}
