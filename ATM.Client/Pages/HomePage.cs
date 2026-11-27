using System;
using System.Windows.Forms;
using ATM.Client.Forms;
using ATM.Client.Net;
using ATM.Common;

namespace ATM.Client.Pages
{
    /// <summary>Màn hình chính kiểu ATM: thẻ số dư + các ô chức năng lớn. Giao diện: HomePage.Designer.cs.</summary>
    public partial class HomePage : PageBase
    {
        public HomePage(MainForm app) : base(app)
        {
            InitializeComponent();
            pageHeader.Title = "Xin chào, " + App.FullName;
            balanceCard.Bind(App.Card, App.FullName, App.Client.ServerName, App.Balance);
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            // Đồng bộ lại số dư mới nhất (ví dụ vừa có người chuyển tiền tới)
            try
            {
                Response r = await App.Client.SendAsync(new Request { Command = Commands.Balance });
                if (r == null || IsDisposed) return;
                if (r.Code == AtmClient.ConnectionLostCode)
                {
                    App.OnConnectionLost(r.Message);
                    return;
                }
                if (r.Ok)
                {
                    App.UpdateBalance(r.Balance);
                    balanceCard.Bind(App.Card, App.FullName, App.Client.ServerName, r.Balance);
                }
            }
            catch (Exception) { }
        }

        // ------------------------------------------------------- sự kiện từ Designer

        private void tileQuick_Click(object sender, EventArgs e) { App.Go(new QuickWithdrawPage(App)); }
        private void tileWithdraw_Click(object sender, EventArgs e) { App.Go(new AmountPage(App, AmountMode.Withdraw)); }
        private void tileDeposit_Click(object sender, EventArgs e) { App.Go(new AmountPage(App, AmountMode.Deposit)); }
        private void tileTransfer_Click(object sender, EventArgs e) { App.Go(new TransferPage(App)); }
        private void tileBalance_Click(object sender, EventArgs e) { App.Go(new BalancePage(App)); }
        private void tileHistory_Click(object sender, EventArgs e) { App.Go(new HistoryPage(App)); }
        private void tileChangePin_Click(object sender, EventArgs e) { App.Go(new ChangePinPage(App)); }
        private void tileExit_Click(object sender, EventArgs e) { App.Logout(); }
    }
}
