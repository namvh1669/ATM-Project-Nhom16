using System;
using System.Windows.Forms;
using ATM.Client.Forms;
using ATM.Common;
using ATM.UI;

namespace ATM.Client.Pages
{
    /// <summary>Rút nhanh: chọn một trong các mệnh giá có sẵn (Limits.QuickAmounts).</summary>
    public partial class QuickWithdrawPage : PageBase
    {
        public QuickWithdrawPage(MainForm app) : base(app)
        {
            InitializeComponent();
            pageHeader.Subtitle = "Chọn mệnh giá - số dư hiện tại: " + Money.Format(App.Balance);

            // Gán mệnh giá cho 6 ô (nguồn duy nhất: Limits.QuickAmounts)
            TileButton[] tiles = { tile1, tile2, tile3, tile4, tile5, tile6 };
            long[] amounts = Limits.QuickAmounts;
            for (int i = 0; i < tiles.Length && i < amounts.Length; i++)
            {
                tiles[i].Tag = amounts[i];
                tiles[i].Title = Money.Format(amounts[i]);
                bool tooMuch = amounts[i] > App.Balance;
                tiles[i].Subtitle = tooMuch ? "Vượt quá số dư" : "Nhấn để rút ngay";
                tiles[i].Accent = tooMuch ? Theme.Muted : Theme.Accent;
            }
        }

        private async void tile_Click(object sender, EventArgs e)
        {
            await DoWithdraw((long)((Control)sender).Tag);
        }
    }
}
