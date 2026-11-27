using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ATM.Client.Forms;
using ATM.Common;
using ATM.UI;

namespace ATM.Client.Pages
{
    /// <summary>[Rubric: Sign up] Đăng ký thẻ mới: Server cấp số thẻ, mã PIN được băm PBKDF2 trước khi lưu.</summary>
    public partial class RegisterPage : PageBase
    {
        public RegisterPage(MainForm app) : base(app)
        {
            InitializeComponent();
            DefaultButton = btnRegister;

            txtPin.Caption = "Mã PIN mới (" + Limits.PinLength + " chữ số)";
            txtPin.MaxLength = Limits.PinLength;
            txtPin2.MaxLength = Limits.PinLength;
        }

        protected override void OnBack()
        {
            App.ShowLogin(null);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            txtName.Inner.Focus();
        }

        private async void btnRegister_Click(object sender, EventArgs e)
        {
            if (App.Busy) return;
            string name = txtName.Text.Trim();
            if (name.Length < 2)
            {
                Dialogs.Warning(App, "Thiếu họ tên", "Vui lòng nhập họ và tên (ít nhất 2 ký tự).");
                txtName.Inner.Focus();
                return;
            }
            if (txtPin.Text.Length != Limits.PinLength)
            {
                Dialogs.Warning(App, "Mã PIN chưa hợp lệ", "Mã PIN phải gồm đúng " + Limits.PinLength + " chữ số.");
                txtPin.Inner.Focus();
                return;
            }
            if (txtPin.Text != txtPin2.Text)
            {
                Dialogs.Warning(App, "Mã PIN không khớp", "Hai lần nhập mã PIN chưa giống nhau.");
                txtPin2.Text = "";
                txtPin2.Inner.Focus();
                return;
            }

            if (!await App.EnsureConnectedAsync()) return;
            Response res = await CallAsync(new Request { Command = Commands.Register, FullName = name, Pin = txtPin.Text });
            if (res == null) return;
            if (!res.Ok)
            {
                Dialogs.Error(App, "Đăng ký thất bại", res.Message);
                return;
            }

            var rows = new List<KeyValuePair<string, string>>();
            rows.Add(Row("Số thẻ", res.Card));
            rows.Add(Row("Chủ thẻ", res.FullName));
            rows.Add(Row("Số dư ban đầu", Money.Format(0)));
            Dialogs.Receipt(App, "Đăng ký thành công", "Hãy ghi nhớ số thẻ và mã PIN để đăng nhập.", rows);
            App.ShowLogin(res.Card);
        }
    }
}
