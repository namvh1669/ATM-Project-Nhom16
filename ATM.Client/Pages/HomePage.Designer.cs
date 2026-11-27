namespace ATM.Client.Pages
{
    partial class HomePage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pagePanel = new ATM.UI.PagePanel();
            this.tblHome = new System.Windows.Forms.TableLayoutPanel();
            this.balanceCard = new ATM.Client.Pages.BalanceCard();
            this.tblTiles = new System.Windows.Forms.TableLayoutPanel();
            this.tileQuick = new ATM.Client.Pages.TileButton();
            this.tileWithdraw = new ATM.Client.Pages.TileButton();
            this.tileDeposit = new ATM.Client.Pages.TileButton();
            this.tileTransfer = new ATM.Client.Pages.TileButton();
            this.tileBalance = new ATM.Client.Pages.TileButton();
            this.tileHistory = new ATM.Client.Pages.TileButton();
            this.tileChangePin = new ATM.Client.Pages.TileButton();
            this.tileExit = new ATM.Client.Pages.TileButton();
            this.pageHeader = new ATM.UI.PageHeader();
            this.pagePanel.SuspendLayout();
            this.tblHome.SuspendLayout();
            this.tblTiles.SuspendLayout();
            this.SuspendLayout();
            // 
            // pagePanel
            // 
            this.pagePanel.AutoScroll = true;
            this.pagePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pagePanel.MaxContentWidth = 1060;
            this.pagePanel.MinContentHeight = 560;
            this.pagePanel.Padding = new System.Windows.Forms.Padding(40, 4, 40, 24);
            this.pagePanel.Name = "pagePanel";
            this.pagePanel.Controls.Add(this.tblHome);
            // 
            // tblHome
            // 
            this.tblHome.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblHome.Name = "tblHome";
            this.tblHome.ColumnCount = 1;
            this.tblHome.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblHome.RowCount = 2;
            this.tblHome.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 206F));
            this.tblHome.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblHome.Controls.Add(this.balanceCard, 0, 0);
            this.tblHome.Controls.Add(this.tblTiles, 0, 1);
            // 
            // balanceCard
            // 
            this.balanceCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.balanceCard.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.balanceCard.Name = "balanceCard";
            // 
            // tblTiles
            // 
            this.tblTiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblTiles.Name = "tblTiles";
            this.tblTiles.ColumnCount = 4;
            this.tblTiles.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblTiles.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblTiles.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblTiles.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblTiles.RowCount = 2;
            this.tblTiles.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblTiles.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblTiles.Controls.Add(this.tileQuick, 0, 0);
            this.tblTiles.Controls.Add(this.tileWithdraw, 1, 0);
            this.tblTiles.Controls.Add(this.tileDeposit, 2, 0);
            this.tblTiles.Controls.Add(this.tileTransfer, 3, 0);
            this.tblTiles.Controls.Add(this.tileBalance, 0, 1);
            this.tblTiles.Controls.Add(this.tileHistory, 1, 1);
            this.tblTiles.Controls.Add(this.tileChangePin, 2, 1);
            this.tblTiles.Controls.Add(this.tileExit, 3, 1);
            // 
            // tileQuick
            // 
            this.tileQuick.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tileQuick.Icon = ATM.UI.IconKind.Bolt;
            this.tileQuick.Margin = new System.Windows.Forms.Padding(0, 6, 12, 6);
            this.tileQuick.Subtitle = "Chọn nhanh mệnh giá";
            this.tileQuick.Title = "Rút tiền nhanh";
            this.tileQuick.Name = "tileQuick";
            this.tileQuick.Click += new System.EventHandler(this.tileQuick_Click);
            // 
            // tileWithdraw
            // 
            this.tileWithdraw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tileWithdraw.Icon = ATM.UI.IconKind.Withdraw;
            this.tileWithdraw.Margin = new System.Windows.Forms.Padding(0, 6, 12, 6);
            this.tileWithdraw.Subtitle = "Nhập số tiền tùy chọn";
            this.tileWithdraw.Title = "Rút tiền";
            this.tileWithdraw.Accent = System.Drawing.Color.FromArgb(99, 102, 241);
            this.tileWithdraw.Name = "tileWithdraw";
            this.tileWithdraw.Click += new System.EventHandler(this.tileWithdraw_Click);
            // 
            // tileDeposit
            // 
            this.tileDeposit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tileDeposit.Icon = ATM.UI.IconKind.Deposit;
            this.tileDeposit.Margin = new System.Windows.Forms.Padding(0, 6, 12, 6);
            this.tileDeposit.Subtitle = "Nạp tiền vào tài khoản";
            this.tileDeposit.Title = "Nộp tiền";
            this.tileDeposit.Accent = System.Drawing.Color.FromArgb(34, 197, 94);
            this.tileDeposit.Name = "tileDeposit";
            this.tileDeposit.Click += new System.EventHandler(this.tileDeposit_Click);
            // 
            // tileTransfer
            // 
            this.tileTransfer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tileTransfer.Icon = ATM.UI.IconKind.Transfer;
            this.tileTransfer.Margin = new System.Windows.Forms.Padding(0, 6, 0, 6);
            this.tileTransfer.Subtitle = "Tới thẻ ATM khác";
            this.tileTransfer.Title = "Chuyển khoản";
            this.tileTransfer.Accent = System.Drawing.Color.FromArgb(245, 158, 11);
            this.tileTransfer.Name = "tileTransfer";
            this.tileTransfer.Click += new System.EventHandler(this.tileTransfer_Click);
            // 
            // tileBalance
            // 
            this.tileBalance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tileBalance.Icon = ATM.UI.IconKind.Wallet;
            this.tileBalance.Margin = new System.Windows.Forms.Padding(0, 6, 12, 6);
            this.tileBalance.Subtitle = "Thông tin tài khoản";
            this.tileBalance.Title = "Xem số dư";
            this.tileBalance.Name = "tileBalance";
            this.tileBalance.Click += new System.EventHandler(this.tileBalance_Click);
            // 
            // tileHistory
            // 
            this.tileHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tileHistory.Icon = ATM.UI.IconKind.History;
            this.tileHistory.Margin = new System.Windows.Forms.Padding(0, 6, 12, 6);
            this.tileHistory.Subtitle = "Sao kê & xuất file";
            this.tileHistory.Title = "Lịch sử giao dịch";
            this.tileHistory.Accent = System.Drawing.Color.FromArgb(99, 102, 241);
            this.tileHistory.Name = "tileHistory";
            this.tileHistory.Click += new System.EventHandler(this.tileHistory_Click);
            // 
            // tileChangePin
            // 
            this.tileChangePin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tileChangePin.Icon = ATM.UI.IconKind.Lock;
            this.tileChangePin.Margin = new System.Windows.Forms.Padding(0, 6, 12, 6);
            this.tileChangePin.Subtitle = "Tăng cường bảo mật";
            this.tileChangePin.Title = "Đổi mã PIN";
            this.tileChangePin.Accent = System.Drawing.Color.FromArgb(245, 158, 11);
            this.tileChangePin.Name = "tileChangePin";
            this.tileChangePin.Click += new System.EventHandler(this.tileChangePin_Click);
            // 
            // tileExit
            // 
            this.tileExit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tileExit.Icon = ATM.UI.IconKind.Power;
            this.tileExit.Margin = new System.Windows.Forms.Padding(0, 6, 0, 6);
            this.tileExit.Subtitle = "Đăng xuất & rút thẻ";
            this.tileExit.Title = "Thoát";
            this.tileExit.Accent = System.Drawing.Color.FromArgb(239, 68, 68);
            this.tileExit.Name = "tileExit";
            this.tileExit.Click += new System.EventHandler(this.tileExit_Click);
            // 
            // pageHeader
            // 
            this.pageHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pageHeader.MaxContentWidth = 1060;
            this.pageHeader.Size = new System.Drawing.Size(1100, 90);
            this.pageHeader.Subtitle = "Chọn giao dịch bạn muốn thực hiện";
            this.pageHeader.Title = "Xin chào";
            this.pageHeader.ShowBack = false;
            this.pageHeader.Name = "pageHeader";
            // 
            // HomePage
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(10, 16, 30);
            this.Size = new System.Drawing.Size(1100, 640);
            this.Name = "HomePage";
            this.Controls.Add(this.pagePanel);
            this.Controls.Add(this.pageHeader);
            this.ResumeLayout(false);
            this.tblTiles.ResumeLayout(false);
            this.tblHome.ResumeLayout(false);
            this.pagePanel.ResumeLayout(false);
            this.tblTiles.PerformLayout();
            this.tblHome.PerformLayout();
            this.PerformLayout();

        }

        #endregion

        private ATM.UI.PagePanel pagePanel;
        private System.Windows.Forms.TableLayoutPanel tblHome;
        private ATM.Client.Pages.BalanceCard balanceCard;
        private System.Windows.Forms.TableLayoutPanel tblTiles;
        private ATM.Client.Pages.TileButton tileQuick;
        private ATM.Client.Pages.TileButton tileWithdraw;
        private ATM.Client.Pages.TileButton tileDeposit;
        private ATM.Client.Pages.TileButton tileTransfer;
        private ATM.Client.Pages.TileButton tileBalance;
        private ATM.Client.Pages.TileButton tileHistory;
        private ATM.Client.Pages.TileButton tileChangePin;
        private ATM.Client.Pages.TileButton tileExit;
        private ATM.UI.PageHeader pageHeader;
    }
}
