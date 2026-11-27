using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ATM.Client.Forms;
using ATM.Common;
using ATM.UI;

namespace ATM.Client.Pages
{
    /// <summary>Xem số dư và thông tin tài khoản / kênh truyền bảo mật. Giao diện: BalancePage.Designer.cs.</summary>
    public partial class BalancePage : PageBase
    {
        public BalancePage(MainForm app) : base(app)
        {
            InitializeComponent();
            balanceCard.Bind(App.Card, App.FullName, App.Client.ServerName, App.Balance);
            ShowInfo(App.Balance, App.CreatedAt);
        }

        private void ShowInfo(long balance, string created)
        {
            var rows = new List<KeyValuePair<string, string>>();
            rows.Add(Row("Chủ thẻ", App.FullName));
            rows.Add(Row("Số thẻ", App.Card));
            rows.Add(Row("Số dư khả dụng", Money.Format(balance)));
            rows.Add(Row("Ngày mở thẻ", created ?? "-"));
            rows.Add(Row("Máy chủ đang phục vụ", App.Client.ServerName ?? "-"));
            rows.Add(Row("Kênh truyền", "RSA-2048 + AES-256-CBC + HMAC-SHA256"));
            rows.Add(Row("Vân tay khóa Server", App.Client.Fingerprint ?? "-"));
            infoList.SetRows(rows);
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Response r = await CallAsync(new Request { Command = Commands.Balance });
            if (r == null || IsDisposed) return;
            if (r.Ok)
            {
                App.UpdateBalance(r.Balance);
                balanceCard.Bind(App.Card, App.FullName, App.Client.ServerName, r.Balance);
                ShowInfo(r.Balance, r.CreatedAt);
            }
            else Dialogs.Error(App, "Không lấy được số dư", r.Message);
        }
    }
}
