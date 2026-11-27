using System;
using System.Windows.Forms;
using ATM.Client.Forms;
using ATM.Common;
using ATM.UI;

namespace ATM.Client.Pages
{
    /// <summary>[Rubric: Sign in] Màn hình đăng nhập. Giao diện: LoginPage.Designer.cs.</summary>
    public partial class LoginPage : PageBase
    {
        private readonly bool _prefilled;

        public LoginPage(MainForm app, string prefillCard) : base(app)
        {
            InitializeComponent();
            DefaultButton = btnLogin;

            lblPinCaption.Text = "Mã PIN (" + Limits.PinLength + " chữ số)";
            pinDots.PinLength = Limits.PinLength;
            txtCard.Text = prefillCard ?? "";
            _prefilled = !string.IsNullOrEmpty(prefillCard);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (_prefilled) pinDots.Focus(); else txtCard.Inner.Focus();
        }

        // ------------------------------------------------------- sự kiện từ Designer

        private void brandPanel_DemoClick(object sender, EventArgs e)
        {
            txtCard.Text = "1000000001";
            pinDots.Clear();
            foreach (char c in "123456") pinDots.Append(c);
            pinDots.Focus();
        }

        private void numPad_KeyPressed(object sender, NumPadKeyEventArgs e)
        {
            if (e.Key == "C") pinDots.Clear();
            else if (e.Key == "←") pinDots.Backspace();
            else pinDots.Append(e.Key[0]);
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            App.ShowRegister();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            if (App.Busy) return;
            string card = txtCard.Text.Trim();
            string pin = pinDots.Value;

            if (card.Length < 6)
            {
                Dialogs.Warning(App, "Thiếu số thẻ", "Vui lòng nhập số thẻ (10 chữ số).");
                txtCard.Inner.Focus();
                return;
            }
            if (pin.Length != Limits.PinLength)
            {
                Dialogs.Warning(App, "Thiếu mã PIN", "Vui lòng nhập đủ " + Limits.PinLength + " chữ số mã PIN.");
                pinDots.Focus();
                return;
            }

            if (!await App.EnsureConnectedAsync()) return;
            Response res = await CallAsync(new Request { Command = Commands.Login, Card = card, Pin = pin });
            if (res == null) return;

            if (!res.Ok)
            {
                pinDots.Clear();
                Dialogs.Error(App, res.Code == "LOCKED" ? "Thẻ bị khóa tạm thời" : "Đăng nhập thất bại", res.Message);
                pinDots.Focus();
                return;
            }

            App.Config.LastCard = card;
            App.Config.Save();
            App.SetSession(res);
            App.ShowHome();
        }
    }
}
