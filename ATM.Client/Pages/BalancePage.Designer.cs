namespace ATM.Client.Pages
{
    partial class BalancePage
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
            this.tblBalance = new System.Windows.Forms.TableLayoutPanel();
            this.balanceCard = new ATM.Client.Pages.BalanceCard();
            this.pnlInfo = new ATM.UI.RoundedPanel();
            this.infoList = new ATM.Client.Pages.InfoList();
            this.pageHeader = new ATM.UI.PageHeader();
            this.pagePanel.SuspendLayout();
            this.tblBalance.SuspendLayout();
            this.pnlInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // pagePanel
            // 
            this.pagePanel.AutoScroll = true;
            this.pagePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pagePanel.MaxContentWidth = 820;
            this.pagePanel.MinContentHeight = 570;
            this.pagePanel.Padding = new System.Windows.Forms.Padding(40, 4, 40, 24);
            this.pagePanel.Name = "pagePanel";
            this.pagePanel.Controls.Add(this.tblBalance);
            // 
            // tblBalance
            // 
            this.tblBalance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblBalance.Name = "tblBalance";
            this.tblBalance.ColumnCount = 1;
            this.tblBalance.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblBalance.RowCount = 2;
            this.tblBalance.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 206F));
            this.tblBalance.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblBalance.Controls.Add(this.balanceCard, 0, 0);
            this.tblBalance.Controls.Add(this.pnlInfo, 0, 1);
            // 
            // balanceCard
            // 
            this.balanceCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.balanceCard.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.balanceCard.Name = "balanceCard";
            // 
            // pnlInfo
            // 
            this.pnlInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInfo.Margin = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.pnlInfo.Padding = new System.Windows.Forms.Padding(26, 12, 26, 12);
            this.pnlInfo.Name = "pnlInfo";
            this.pnlInfo.Controls.Add(this.infoList);
            // 
            // infoList
            // 
            this.infoList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.infoList.Name = "infoList";
            // 
            // pageHeader
            // 
            this.pageHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pageHeader.MaxContentWidth = 820;
            this.pageHeader.Size = new System.Drawing.Size(1100, 90);
            this.pageHeader.Subtitle = "Dữ liệu được lấy trực tiếp từ máy chủ";
            this.pageHeader.Title = "Số dư & thông tin tài khoản";
            this.pageHeader.Name = "pageHeader";
            // 
            // BalancePage
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(10, 16, 30);
            this.Size = new System.Drawing.Size(1100, 640);
            this.Name = "BalancePage";
            this.Controls.Add(this.pagePanel);
            this.Controls.Add(this.pageHeader);
            this.ResumeLayout(false);
            this.pnlInfo.ResumeLayout(false);
            this.tblBalance.ResumeLayout(false);
            this.pagePanel.ResumeLayout(false);
            this.tblBalance.PerformLayout();
            this.PerformLayout();

        }

        #endregion

        private ATM.UI.PagePanel pagePanel;
        private System.Windows.Forms.TableLayoutPanel tblBalance;
        private ATM.Client.Pages.BalanceCard balanceCard;
        private ATM.UI.RoundedPanel pnlInfo;
        private ATM.Client.Pages.InfoList infoList;
        private ATM.UI.PageHeader pageHeader;
    }
}
