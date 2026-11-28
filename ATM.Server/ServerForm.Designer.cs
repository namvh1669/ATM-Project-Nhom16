namespace ATM.Server
{
    partial class ServerForm
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
            this.components = new System.ComponentModel.Container();
            this.tblRoot = new System.Windows.Forms.TableLayoutPanel();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSub = new System.Windows.Forms.Label();
            this.pillStatus = new ATM.UI.StatusPill();
            this.pnlConfig = new ATM.UI.RoundedPanel();
            this.tblConfig = new System.Windows.Forms.TableLayoutPanel();
            this.txtName = new ATM.UI.ModernTextBox();
            this.txtPort = new ATM.UI.ModernTextBox();
            this.txtDb = new ATM.UI.ModernTextBox();
            this.btnToggle = new ATM.UI.RoundedButton();
            this.tblStats = new System.Windows.Forms.TableLayoutPanel();
            this.stClients = new ATM.UI.StatCard();
            this.stConn = new ATM.UI.StatCard();
            this.stReq = new ATM.UI.StatCard();
            this.stUptime = new ATM.UI.StatCard();
            this.flowTabs = new System.Windows.Forms.FlowLayoutPanel();
            this.btnTabLog = new ATM.UI.RoundedButton();
            this.btnTabClients = new ATM.UI.RoundedButton();
            this.btnTabAccounts = new ATM.UI.RoundedButton();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.cardLog = new ATM.UI.RoundedPanel();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.cardClients = new ATM.UI.RoundedPanel();
            this.gridClients = new ATM.UI.ThemedGrid();
            this.colClientId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colClientRemote = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colClientCard = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colClientTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cardAccounts = new ATM.UI.RoundedPanel();
            this.gridAccounts = new ATM.UI.ThemedGrid();
            this.colAccCard = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAccName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAccBalance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAccFailed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAccStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAccCreated = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timerRefresh = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gridClients)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridAccounts)).BeginInit();
            this.tblRoot.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.pnlConfig.SuspendLayout();
            this.tblConfig.SuspendLayout();
            this.tblStats.SuspendLayout();
            this.flowTabs.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.cardLog.SuspendLayout();
            this.cardClients.SuspendLayout();
            this.cardAccounts.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblRoot
            // 
            this.tblRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblRoot.Padding = new System.Windows.Forms.Padding(26, 20, 26, 20);
            this.tblRoot.Name = "tblRoot";
            this.tblRoot.ColumnCount = 1;
            this.tblRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblRoot.RowCount = 5;
            this.tblRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tblRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 112F));
            this.tblRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.tblRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblRoot.Controls.Add(this.pnlHeader, 0, 0);
            this.tblRoot.Controls.Add(this.pnlConfig, 0, 1);
            this.tblRoot.Controls.Add(this.tblStats, 0, 2);
            this.tblRoot.Controls.Add(this.flowTabs, 0, 3);
            this.tblRoot.Controls.Add(this.pnlContent, 0, 4);
            // 
            // pnlHeader
            // 
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.lblSub);
            this.pnlHeader.Controls.Add(this.pillStatus);
            this.pnlHeader.Resize += new System.EventHandler(this.pnlHeader_Resize);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Text = "ATM Server";
            this.lblTitle.UseMnemonic = false;
            this.lblTitle.Name = "lblTitle";
            // 
            // lblSub
            // 
            this.lblSub.AutoSize = true;
            this.lblSub.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSub.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.lblSub.Location = new System.Drawing.Point(2, 40);
            this.lblSub.Text = "Máy chủ xử lý giao dịch • nhiều Server có thể chạy song song, dùng chung một cơ sở dữ liệu";
            this.lblSub.UseMnemonic = false;
            this.lblSub.Name = "lblSub";
            // 
            // pillStatus
            // 
            this.pillStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pillStatus.Location = new System.Drawing.Point(860, 8);
            this.pillStatus.PillText = "Đã dừng";
            this.pillStatus.Name = "pillStatus";
            // 
            // pnlConfig
            // 
            this.pnlConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlConfig.Padding = new System.Windows.Forms.Padding(16, 10, 16, 10);
            this.pnlConfig.Name = "pnlConfig";
            this.pnlConfig.Controls.Add(this.tblConfig);
            // 
            // tblConfig
            // 
            this.tblConfig.BackColor = System.Drawing.Color.Transparent;
            this.tblConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblConfig.Name = "tblConfig";
            this.tblConfig.ColumnCount = 4;
            this.tblConfig.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22F));
            this.tblConfig.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tblConfig.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tblConfig.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24F));
            this.tblConfig.RowCount = 1;
            this.tblConfig.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblConfig.Controls.Add(this.txtName, 0, 0);
            this.tblConfig.Controls.Add(this.txtPort, 1, 0);
            this.tblConfig.Controls.Add(this.txtDb, 2, 0);
            this.tblConfig.Controls.Add(this.btnToggle, 3, 0);
            // 
            // txtName
            // 
            this.txtName.Caption = "Tên Server";
            this.txtName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtName.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.txtName.Name = "txtName";
            // 
            // txtPort
            // 
            this.txtPort.Caption = "Cổng (port)";
            this.txtPort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPort.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.txtPort.MaxLength = 5;
            this.txtPort.NumbersOnly = true;
            this.txtPort.Name = "txtPort";
            // 
            // txtDb
            // 
            this.txtDb.Caption = "File cơ sở dữ liệu SQLite (dùng chung giữa các Server)";
            this.txtDb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDb.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.txtDb.Name = "txtDb";
            // 
            // btnToggle
            // 
            this.btnToggle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnToggle.Icon = ATM.UI.IconKind.Power;
            this.btnToggle.Margin = new System.Windows.Forms.Padding(6, 0, 6, 4);
            this.btnToggle.Text = "Khởi động";
            this.btnToggle.Name = "btnToggle";
            this.btnToggle.Click += new System.EventHandler(this.btnToggle_Click);
            // 
            // tblStats
            // 
            this.tblStats.BackColor = System.Drawing.Color.Transparent;
            this.tblStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblStats.Name = "tblStats";
            this.tblStats.ColumnCount = 4;
            this.tblStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblStats.RowCount = 1;
            this.tblStats.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblStats.Controls.Add(this.stClients, 0, 0);
            this.tblStats.Controls.Add(this.stConn, 1, 0);
            this.tblStats.Controls.Add(this.stReq, 2, 0);
            this.tblStats.Controls.Add(this.stUptime, 3, 0);
            // 
            // stClients
            // 
            this.stClients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stClients.Icon = ATM.UI.IconKind.Server;
            this.stClients.Margin = new System.Windows.Forms.Padding(6, 14, 6, 6);
            this.stClients.Title = "Client đang kết nối";
            this.stClients.Value = "0";
            this.stClients.Name = "stClients";
            // 
            // stConn
            // 
            this.stConn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stConn.Icon = ATM.UI.IconKind.Transfer;
            this.stConn.Margin = new System.Windows.Forms.Padding(6, 14, 6, 6);
            this.stConn.Title = "Tổng lượt kết nối";
            this.stConn.Value = "0";
            this.stConn.Accent = System.Drawing.Color.FromArgb(99, 102, 241);
            this.stConn.Name = "stConn";
            // 
            // stReq
            // 
            this.stReq.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stReq.Icon = ATM.UI.IconKind.Bolt;
            this.stReq.Margin = new System.Windows.Forms.Padding(6, 14, 6, 6);
            this.stReq.Title = "Tổng số yêu cầu";
            this.stReq.Value = "0";
            this.stReq.Accent = System.Drawing.Color.FromArgb(245, 158, 11);
            this.stReq.Name = "stReq";
            // 
            // stUptime
            // 
            this.stUptime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stUptime.Icon = ATM.UI.IconKind.History;
            this.stUptime.Margin = new System.Windows.Forms.Padding(6, 14, 6, 6);
            this.stUptime.Title = "Thời gian hoạt động";
            this.stUptime.Value = "0";
            this.stUptime.Accent = System.Drawing.Color.FromArgb(34, 197, 94);
            this.stUptime.Name = "stUptime";
            // 
            // flowTabs
            // 
            this.flowTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowTabs.WrapContents = false;
            this.flowTabs.Name = "flowTabs";
            this.flowTabs.Controls.Add(this.btnTabLog);
            this.flowTabs.Controls.Add(this.btnTabClients);
            this.flowTabs.Controls.Add(this.btnTabAccounts);
            // 
            // btnTabLog
            // 
            this.btnTabLog.Kind = ATM.UI.ButtonKind.Ghost;
            this.btnTabLog.Margin = new System.Windows.Forms.Padding(0, 8, 10, 0);
            this.btnTabLog.Size = new System.Drawing.Size(200, 42);
            this.btnTabLog.Text = "Nhật ký hoạt động";
            this.btnTabLog.Name = "btnTabLog";
            this.btnTabLog.Click += new System.EventHandler(this.btnTab_Click);
            // 
            // btnTabClients
            // 
            this.btnTabClients.Kind = ATM.UI.ButtonKind.Ghost;
            this.btnTabClients.Margin = new System.Windows.Forms.Padding(0, 8, 10, 0);
            this.btnTabClients.Size = new System.Drawing.Size(200, 42);
            this.btnTabClients.Text = "Client đang kết nối";
            this.btnTabClients.Name = "btnTabClients";
            this.btnTabClients.Click += new System.EventHandler(this.btnTab_Click);
            // 
            // btnTabAccounts
            // 
            this.btnTabAccounts.Kind = ATM.UI.ButtonKind.Ghost;
            this.btnTabAccounts.Margin = new System.Windows.Forms.Padding(0, 8, 10, 0);
            this.btnTabAccounts.Size = new System.Drawing.Size(200, 42);
            this.btnTabAccounts.Text = "Tài khoản & số dư";
            this.btnTabAccounts.Name = "btnTabAccounts";
            this.btnTabAccounts.Click += new System.EventHandler(this.btnTab_Click);
            // 
            // pnlContent
            // 
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Controls.Add(this.cardLog);
            this.pnlContent.Controls.Add(this.cardClients);
            this.pnlContent.Controls.Add(this.cardAccounts);
            // 
            // cardLog
            // 
            this.cardLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardLog.Padding = new System.Windows.Forms.Padding(14, 14, 14, 14);
            this.cardLog.Name = "cardLog";
            this.cardLog.Controls.Add(this.rtbLog);
            // 
            // rtbLog
            // 
            this.rtbLog.BackColor = System.Drawing.Color.FromArgb(20, 29, 49);
            this.rtbLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbLog.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rtbLog.ForeColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.rtbLog.ReadOnly = true;
            this.rtbLog.WordWrap = false;
            this.rtbLog.Name = "rtbLog";
            // 
            // cardClients
            // 
            this.cardClients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardClients.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cardClients.Name = "cardClients";
            this.cardClients.Controls.Add(this.gridClients);
            // 
            // gridClients
            // 
            this.gridClients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridClients.Name = "gridClients";
            this.gridClients.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colClientId,
            this.colClientRemote,
            this.colClientCard,
            this.colClientTime});
            // 
            // colClientId
            // 
            this.colClientId.HeaderText = "#";
            this.colClientId.Name = "colClientId";
            this.colClientId.ReadOnly = true;
            this.colClientId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colClientId.FillWeight = 8F;
            // 
            // colClientRemote
            // 
            this.colClientRemote.HeaderText = "Địa chỉ Client";
            this.colClientRemote.Name = "colClientRemote";
            this.colClientRemote.ReadOnly = true;
            this.colClientRemote.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colClientRemote.FillWeight = 30F;
            // 
            // colClientCard
            // 
            this.colClientCard.HeaderText = "Số thẻ đang đăng nhập";
            this.colClientCard.Name = "colClientCard";
            this.colClientCard.ReadOnly = true;
            this.colClientCard.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colClientCard.FillWeight = 40F;
            // 
            // colClientTime
            // 
            this.colClientTime.HeaderText = "Kết nối lúc";
            this.colClientTime.Name = "colClientTime";
            this.colClientTime.ReadOnly = true;
            this.colClientTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colClientTime.FillWeight = 22F;
            // 
            // cardAccounts
            // 
            this.cardAccounts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardAccounts.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cardAccounts.Name = "cardAccounts";
            this.cardAccounts.Controls.Add(this.gridAccounts);
            // 
            // gridAccounts
            // 
            this.gridAccounts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridAccounts.Name = "gridAccounts";
            this.gridAccounts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colAccCard,
            this.colAccName,
            this.colAccBalance,
            this.colAccFailed,
            this.colAccStatus,
            this.colAccCreated});
            // 
            // colAccCard
            // 
            this.colAccCard.HeaderText = "Số thẻ";
            this.colAccCard.Name = "colAccCard";
            this.colAccCard.ReadOnly = true;
            this.colAccCard.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colAccCard.FillWeight = 16F;
            // 
            // colAccName
            // 
            this.colAccName.HeaderText = "Chủ thẻ";
            this.colAccName.Name = "colAccName";
            this.colAccName.ReadOnly = true;
            this.colAccName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colAccName.FillWeight = 24F;
            // 
            // colAccBalance
            // 
            this.colAccBalance.HeaderText = "Số dư";
            this.colAccBalance.Name = "colAccBalance";
            this.colAccBalance.ReadOnly = true;
            this.colAccBalance.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colAccBalance.FillWeight = 18F;
            // 
            // colAccFailed
            // 
            this.colAccFailed.HeaderText = "Lần sai PIN";
            this.colAccFailed.Name = "colAccFailed";
            this.colAccFailed.ReadOnly = true;
            this.colAccFailed.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colAccFailed.FillWeight = 12F;
            // 
            // colAccStatus
            // 
            this.colAccStatus.HeaderText = "Trạng thái";
            this.colAccStatus.Name = "colAccStatus";
            this.colAccStatus.ReadOnly = true;
            this.colAccStatus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colAccStatus.FillWeight = 14F;
            // 
            // colAccCreated
            // 
            this.colAccCreated.HeaderText = "Ngày tạo";
            this.colAccCreated.Name = "colAccCreated";
            this.colAccCreated.ReadOnly = true;
            this.colAccCreated.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colAccCreated.FillWeight = 20F;
            // 
            // timerRefresh
            // 
            this.timerRefresh.Enabled = true;
            this.timerRefresh.Interval = 1000;
            this.timerRefresh.Tick += new System.EventHandler(this.timerRefresh_Tick);
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(10, 16, 30);
            this.ClientSize = new System.Drawing.Size(1080, 740);
            this.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ForeColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.MinimumSize = new System.Drawing.Size(960, 680);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ATM Server";
            this.Name = "ServerForm";
            this.Controls.Add(this.tblRoot);
            this.ResumeLayout(false);
            this.cardAccounts.ResumeLayout(false);
            this.cardClients.ResumeLayout(false);
            this.cardLog.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.flowTabs.ResumeLayout(false);
            this.tblStats.ResumeLayout(false);
            this.tblConfig.ResumeLayout(false);
            this.pnlConfig.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.tblRoot.ResumeLayout(false);
            this.tblStats.PerformLayout();
            this.tblConfig.PerformLayout();
            this.tblRoot.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridClients)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridAccounts)).EndInit();
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblRoot;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSub;
        private ATM.UI.StatusPill pillStatus;
        private ATM.UI.RoundedPanel pnlConfig;
        private System.Windows.Forms.TableLayoutPanel tblConfig;
        private ATM.UI.ModernTextBox txtName;
        private ATM.UI.ModernTextBox txtPort;
        private ATM.UI.ModernTextBox txtDb;
        private ATM.UI.RoundedButton btnToggle;
        private System.Windows.Forms.TableLayoutPanel tblStats;
        private ATM.UI.StatCard stClients;
        private ATM.UI.StatCard stConn;
        private ATM.UI.StatCard stReq;
        private ATM.UI.StatCard stUptime;
        private System.Windows.Forms.FlowLayoutPanel flowTabs;
        private ATM.UI.RoundedButton btnTabLog;
        private ATM.UI.RoundedButton btnTabClients;
        private ATM.UI.RoundedButton btnTabAccounts;
        private System.Windows.Forms.Panel pnlContent;
        private ATM.UI.RoundedPanel cardLog;
        private System.Windows.Forms.RichTextBox rtbLog;
        private ATM.UI.RoundedPanel cardClients;
        private ATM.UI.ThemedGrid gridClients;
        private System.Windows.Forms.DataGridViewTextBoxColumn colClientId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colClientRemote;
        private System.Windows.Forms.DataGridViewTextBoxColumn colClientCard;
        private System.Windows.Forms.DataGridViewTextBoxColumn colClientTime;
        private ATM.UI.RoundedPanel cardAccounts;
        private ATM.UI.ThemedGrid gridAccounts;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAccCard;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAccName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAccBalance;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAccFailed;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAccStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAccCreated;
        private System.Windows.Forms.Timer timerRefresh;
    }
}
