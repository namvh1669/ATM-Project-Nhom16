using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using ATM.Client.Forms;
using ATM.Client.Net;
using ATM.Common;
using ATM.UI;

namespace ATM.Client.Pages
{
    /// <summary>
    /// Lớp nền cho mọi trang (mỗi trang là một UserControl có file .Designer.cs riêng):
    ///  - giữ tham chiếu tới cửa sổ chính (App) để điều hướng / lấy phiên đăng nhập;
    ///  - tự nối nút "Quay lại" của PageHeader (nếu trang có);
    ///  - các hàm gọi Server dùng chung (CallAsync, DoWithdraw, DoDeposit).
    /// Có constructor không tham số để Visual Studio Designer dựng được các trang kế thừa.
    /// </summary>
    public class PageBase : UserControl
    {
        private PageHeader _header;

        protected MainForm App { get; private set; }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IButtonControl DefaultButton { get; protected set; }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IButtonControl BackButton
        {
            get { return _header != null && _header.ShowBack ? _header.BackButton : null; }
        }

        public PageBase()
        {
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Theme.Bg;
            DoubleBuffered = true;
        }

        protected PageBase(MainForm app) : this()
        {
            App = app;
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            var h = e.Control as PageHeader;
            if (h != null)
            {
                _header = h;
                h.BackClick += delegate { OnBack(); };
            }
        }

        protected virtual void OnBack()
        {
            if (App != null) App.ShowHome();
        }

        protected static KeyValuePair<string, string> Row(string k, string v)
        {
            return new KeyValuePair<string, string>(k, v);
        }

        // ------------------------------------------------------ gọi Server dùng chung

        /// <summary>Gửi yêu cầu lên Server (không chặn giao diện). Trả về null nếu bận hoặc mất kết nối.</summary>
        protected async Task<Response> CallAsync(Request req)
        {
            if (App.Busy) return null;
            App.SetBusy(true);
            try
            {
                Response res = await App.Client.SendAsync(req);
                if (res.Code == AtmClient.ConnectionLostCode)
                {
                    App.SetBusy(false);
                    App.OnConnectionLost(res.Message);
                    return null;
                }
                return res;
            }
            catch (Exception ex)
            {
                App.SetBusy(false);
                Dialogs.Error(App, "Có lỗi xảy ra", ex.Message);
                return null;
            }
            finally
            {
                App.SetBusy(false);
            }
        }

        /// <summary>Quy trình rút tiền: xác nhận → gửi lệnh → biên lai → về màn hình chính.</summary>
        protected async Task DoWithdraw(long amount)
        {
            var rows = new List<KeyValuePair<string, string>>();
            rows.Add(Row("Số tiền rút", Money.Format(amount)));
            rows.Add(Row("Phí giao dịch", "0 ₫"));
            rows.Add(Row("Số dư hiện tại", Money.Format(App.Balance)));
            if (!Dialogs.Confirm(App, "Xác nhận rút tiền", "Bạn có chắc muốn thực hiện giao dịch này?", rows, "Rút tiền")) return;

            Response res = await CallAsync(new Request { Command = Commands.Withdraw, Amount = amount });
            if (res == null) return;
            if (!res.Ok)
            {
                Dialogs.Error(App, "Rút tiền thất bại", res.Message);
                return;
            }
            App.UpdateBalance(res.Balance);
            var receipt = new List<KeyValuePair<string, string>>();
            receipt.Add(Row("Số tiền", Money.Format(amount)));
            receipt.Add(Row("Số dư còn lại", Money.Format(res.Balance)));
            receipt.Add(Row("Thời gian", res.Time));
            receipt.Add(Row("Xử lý bởi", res.ServerName));
            Dialogs.Receipt(App, "Rút tiền thành công", "Vui lòng nhận tiền và giữ lại biên lai.", receipt);
            App.ShowHome();
        }

        protected async Task DoDeposit(long amount)
        {
            var rows = new List<KeyValuePair<string, string>>();
            rows.Add(Row("Số tiền nộp", Money.Format(amount)));
            rows.Add(Row("Số dư hiện tại", Money.Format(App.Balance)));
            if (!Dialogs.Confirm(App, "Xác nhận nộp tiền", "Bạn có chắc muốn nộp số tiền này vào tài khoản?", rows, "Nộp tiền")) return;

            Response res = await CallAsync(new Request { Command = Commands.Deposit, Amount = amount });
            if (res == null) return;
            if (!res.Ok)
            {
                Dialogs.Error(App, "Nộp tiền thất bại", res.Message);
                return;
            }
            App.UpdateBalance(res.Balance);
            var receipt = new List<KeyValuePair<string, string>>();
            receipt.Add(Row("Số tiền", Money.Format(amount)));
            receipt.Add(Row("Số dư mới", Money.Format(res.Balance)));
            receipt.Add(Row("Thời gian", res.Time));
            receipt.Add(Row("Xử lý bởi", res.ServerName));
            Dialogs.Receipt(App, "Nộp tiền thành công", "Số tiền đã được cộng vào tài khoản của bạn.", receipt);
            App.ShowHome();
        }
    }
}
