namespace ATM.Client.Pages
{
    partial class HistoryPage
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
            this.cardGrid = new ATM.UI.RoundedPanel();
            this.gridHistory = new ATM.UI.ThemedGrid();
            this.colTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBalance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNote = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colServer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlToolbar = new System.Windows.Forms.Panel();
            this.btnRefresh = new ATM.UI.RoundedButton();
            this.btnExport = new ATM.UI.RoundedButton();
            this.lblCount = new System.Windows.Forms.Label();
            this.pageHeader = new ATM.UI.PageHeader();
            ((System.ComponentModel.ISupportInitialize)(this.gridHistory)).BeginInit();
            this.pagePanel.SuspendLayout();
            this.cardGrid.SuspendLayout();
            this.pnlToolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // pagePanel
            // 
            this.pagePanel.AutoScroll = true;
            this.pagePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pagePanel.MaxContentWidth = 1100;
            this.pagePanel.MinContentHeight = 420;
            this.pagePanel.Padding = new System.Windows.Forms.Padding(40, 4, 40, 24);
            this.pagePanel.Name = "pagePanel";
            this.pagePanel.Controls.Add(this.cardGrid);
            this.pagePanel.Controls.Add(this.pnlToolbar);
            // 
            // cardGrid
            // 
            this.cardGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardGrid.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cardGrid.Name = "cardGrid";
            this.cardGrid.Controls.Add(this.gridHistory);
            // 
            // gridHistory
            // 
            this.gridHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridHistory.Name = "gridHistory";
            this.gridHistory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTime,
            this.colType,
            this.colAmount,
            this.colBalance,
            this.colNote,
            this.colServer});
            // 
            // colTime
            // 
            this.colTime.HeaderText = "Thời gian";
            this.colTime.Name = "colTime";
            this.colTime.ReadOnly = true;
            this.colTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colTime.FillWeight = 20F;
            // 
            // colType
            // 
            this.colType.HeaderText = "Loại giao dịch";
            this.colType.Name = "colType";
            this.colType.ReadOnly = true;
            this.colType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colType.FillWeight = 19F;
            // 
            // colAmount
            // 
            this.colAmount.HeaderText = "Số tiền";
            this.colAmount.Name = "colAmount";
            this.colAmount.ReadOnly = true;
            this.colAmount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colAmount.FillWeight = 17F;
            // 
            // colBalance
            // 
            this.colBalance.HeaderText = "Số dư sau GD";
            this.colBalance.Name = "colBalance";
            this.colBalance.ReadOnly = true;
            this.colBalance.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colBalance.FillWeight = 17F;
            // 
            // colNote
            // 
            this.colNote.HeaderText = "Ghi chú";
            this.colNote.Name = "colNote";
            this.colNote.ReadOnly = true;
            this.colNote.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colNote.FillWeight = 29F;
            // 
            // colServer
            // 
            this.colServer.HeaderText = "Xử lý bởi";
            this.colServer.Name = "colServer";
            this.colServer.ReadOnly = true;
            this.colServer.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colServer.FillWeight = 12F;
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolbar.Size = new System.Drawing.Size(1000, 58);
            this.pnlToolbar.Name = "pnlToolbar";
            this.pnlToolbar.Controls.Add(this.btnRefresh);
            this.pnlToolbar.Controls.Add(this.btnExport);
            this.pnlToolbar.Controls.Add(this.lblCount);
            this.pnlToolbar.Resize += new System.EventHandler(this.pnlToolbar_Resize);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Icon = ATM.UI.IconKind.History;
            this.btnRefresh.Kind = ATM.UI.ButtonKind.Secondary;
            this.btnRefresh.Location = new System.Drawing.Point(0, 4);
            this.btnRefresh.Size = new System.Drawing.Size(150, 44);
            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnExport
            // 
            this.btnExport.Icon = ATM.UI.IconKind.Deposit;
            this.btnExport.Location = new System.Drawing.Point(164, 4);
            this.btnExport.Size = new System.Drawing.Size(220, 44);
            this.btnExport.Text = "Xuất sao kê (CSV)";
            this.btnExport.Name = "btnExport";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // lblCount
            // 
            this.lblCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCount.BackColor = System.Drawing.Color.Transparent;
            this.lblCount.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblCount.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.lblCount.Location = new System.Drawing.Point(700, 4);
            this.lblCount.Size = new System.Drawing.Size(300, 44);
            this.lblCount.Text = "5 giao dịch";
            this.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblCount.UseMnemonic = false;
            this.lblCount.Name = "lblCount";
            // 
            // pageHeader
            // 
            this.pageHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pageHeader.MaxContentWidth = 1100;
            this.pageHeader.Size = new System.Drawing.Size(1100, 90);
            this.pageHeader.Subtitle = "50 giao dịch gần nhất";
            this.pageHeader.Title = "Lịch sử giao dịch";
            this.pageHeader.Name = "pageHeader";
            // 
            // HistoryPage
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(10, 16, 30);
            this.Size = new System.Drawing.Size(1100, 640);
            this.Name = "HistoryPage";
            this.Controls.Add(this.pagePanel);
            this.Controls.Add(this.pageHeader);
            this.ResumeLayout(false);
            this.pnlToolbar.ResumeLayout(false);
            this.cardGrid.ResumeLayout(false);
            this.pagePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridHistory)).EndInit();
            this.PerformLayout();

        }

        #endregion

        private ATM.UI.PagePanel pagePanel;
        private ATM.UI.RoundedPanel cardGrid;
        private ATM.UI.ThemedGrid gridHistory;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBalance;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNote;
        private System.Windows.Forms.DataGridViewTextBoxColumn colServer;
        private System.Windows.Forms.Panel pnlToolbar;
        private ATM.UI.RoundedButton btnRefresh;
        private ATM.UI.RoundedButton btnExport;
        private System.Windows.Forms.Label lblCount;
        private ATM.UI.PageHeader pageHeader;
    }
}
