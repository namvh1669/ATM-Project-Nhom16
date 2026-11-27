using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ATM.Client.Forms;
using ATM.Common;
using ATM.UI;

namespace ATM.Client.Pages
{
    /// <summary>
    /// Lịch sử giao dịch (bảng) + [Rubric: I/O File] xuất sao kê ra file CSV (mở được bằng Excel).
    /// Giao diện: HistoryPage.Designer.cs.
    /// </summary>
    public partial class HistoryPage : PageBase
    {
        private List<TransactionInfo> _data = new List<TransactionInfo>();

        public HistoryPage(MainForm app) : base(app)
        {
            InitializeComponent();
            pageHeader.Subtitle = "50 giao dịch gần nhất của thẻ " + App.Card;
            colAmount.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colBalance.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await LoadAsync();
        }

        // ------------------------------------------------------- sự kiện từ Designer

        private void pnlToolbar_Resize(object sender, EventArgs e)
        {
            lblCount.Left = pnlToolbar.Width - lblCount.Width;
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadAsync();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            Export();
        }

        // ------------------------------------------------------------- xử lý

        private async Task LoadAsync()
        {
            Response r = await CallAsync(new Request { Command = Commands.History, Count = 50 });
            if (r == null || IsDisposed) return;
            if (!r.Ok)
            {
                Dialogs.Error(App, "Không tải được lịch sử", r.Message);
                return;
            }
            _data = r.History ?? new List<TransactionInfo>();
            gridHistory.Rows.Clear();
            foreach (TransactionInfo t in _data)
            {
                bool debit = TxTypes.IsDebit(t.Type);
                int i = gridHistory.Rows.Add(t.CreatedAt, TxTypes.Display(t.Type),
                    (debit ? "- " : "+ ") + Money.Format(t.Amount), Money.Format(t.BalanceAfter), t.Note, t.ServerName);
                gridHistory.Rows[i].Cells[2].Style.ForeColor = debit ? Theme.Danger : Theme.Success;
            }
            gridHistory.ClearSelection();
            lblCount.Text = _data.Count + " giao dịch";
        }

        private static string Csv(string s)
        {
            s = s ?? "";
            return "\"" + s.Replace("\"", "\"\"") + "\"";
        }

        private void Export()
        {
            if (_data.Count == 0)
            {
                Dialogs.Info(App, "Chưa có dữ liệu", "Không có giao dịch nào để xuất.");
                return;
            }
            using (var dlg = new SaveFileDialog())
            {
                dlg.Title = "Lưu sao kê giao dịch";
                dlg.Filter = "CSV (*.csv)|*.csv|Văn bản (*.txt)|*.txt";
                dlg.FileName = "SaoKe_" + App.Card + "_" + DateTime.Now.ToString("yyyyMMdd_HHmm");
                if (dlg.ShowDialog(App) != DialogResult.OK) return;

                try
                {
                    var sb = new StringBuilder();
                    sb.AppendLine("Sao kê tài khoản," + Csv(App.FullName) + "," + Csv(App.Card));
                    sb.AppendLine("Xuất lúc," + Csv(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")));
                    sb.AppendLine();
                    sb.AppendLine("Mã GD,Thời gian,Loại giao dịch,Số tiền (VND),Số dư sau (VND),Ghi chú,Máy chủ");
                    foreach (TransactionInfo t in _data)
                    {
                        long signed = TxTypes.IsDebit(t.Type) ? -t.Amount : t.Amount;
                        sb.AppendLine(t.Id + "," + Csv(t.CreatedAt) + "," + Csv(TxTypes.Display(t.Type)) + "," + signed + "," + t.BalanceAfter + "," + Csv(t.Note) + "," + Csv(t.ServerName));
                    }
                    File.WriteAllText(dlg.FileName, sb.ToString(), new UTF8Encoding(true)); // BOM để Excel đọc đúng tiếng Việt
                    Dialogs.Success(App, "Đã xuất sao kê", "Đã lưu " + _data.Count + " giao dịch vào:\n" + dlg.FileName);
                }
                catch (Exception ex)
                {
                    Dialogs.Error(App, "Không ghi được file", ex.Message);
                }
            }
        }
    }
}
