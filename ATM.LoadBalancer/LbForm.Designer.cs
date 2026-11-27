namespace ATM.LoadBalancer
{
    partial class LbForm
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
            this.txtPort = new ATM.UI.ModernTextBox();
            this.tblAlgo = new System.Windows.Forms.TableLayoutPanel();
            this.btnRr = new ATM.UI.RoundedButton();
            this.btnLc = new ATM.UI.RoundedButton();
            this.btnToggle = new ATM.UI.RoundedButton();
            this.lblIp = new System.Windows.Forms.Label();
            this.tblStats = new System.Windows.Forms.TableLayoutPanel();
            this.stActive = new ATM.UI.StatCard();
            this.stTotal = new ATM.UI.StatCard();
            this.stAlive = new ATM.UI.StatCard();
            this.stUptime = new ATM.UI.StatCard();
            this.tblMain = new System.Windows.Forms.TableLayoutPanel();
            this.pnlLeft = new ATM.UI.RoundedPanel();
            this.gridBackends = new ATM.UI.ThemedGrid();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colActive = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colChecked = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tblAdd = new System.Windows.Forms.TableLayoutPanel();
            this.txtName = new ATM.UI.ModernTextBox();
            this.txtHost = new ATM.UI.ModernTextBox();
            this.txtBport = new ATM.UI.ModernTextBox();
            this.btnAdd = new ATM.UI.RoundedButton();
            this.btnDel = new ATM.UI.RoundedButton();
            this.pnlRight = new ATM.UI.RoundedPanel();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.timerRefresh = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gridBackends)).BeginInit();
            this.tblRoot.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.pnlConfig.SuspendLayout();
            this.tblConfig.SuspendLayout();
            this.tblAlgo.SuspendLayout();
            this.tblStats.SuspendLayout();
            this.tblMain.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.tblAdd.SuspendLayout();
            this.pnlRight.SuspendLayout();
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
            this.tblRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tblRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblRoot.Controls.Add(this.pnlHeader, 0, 0);
            this.tblRoot.Controls.Add(this.pnlConfig, 0, 1);
            this.tblRoot.Controls.Add(this.lblIp, 0, 2);
            this.tblRoot.Controls.Add(this.tblStats, 0, 3);
            this.tblRoot.Controls.Add(this.tblMain, 0, 4);
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
            this.lblTitle.Text = "Load Balancer";
            this.lblTitle.UseMnemonic = false;
            this.lblTitle.Name = "lblTitle";
            // 
            // lblSub
            // 
            this.lblSub.AutoSize = true;
            this.lblSub.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSub.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.lblSub.Location = new System.Drawing.Point(2, 40);
            this.lblSub.Text = "Phân chia kết nối của Client cho các ATM Server • Round-Robin / Least-Connections • Health check • Failover";
            this.lblSub.UseMnemonic = false;
            this.lblSub.Name = "lblSub";
            // 
            // pillStatus
            // 
            this.pillStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pillStatus.Location = new System.Drawing.Point(960, 8);
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
            this.tblConfig.ColumnCount = 3;
            this.tblConfig.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24F));
            this.tblConfig.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 44F));
            this.tblConfig.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32F));
            this.tblConfig.RowCount = 1;
            this.tblConfig.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblConfig.Controls.Add(this.txtPort, 0, 0);
            this.tblConfig.Controls.Add(this.tblAlgo, 1, 0);
            this.tblConfig.Controls.Add(this.btnToggle, 2, 0);
            // 
            // txtPort
            // 
            this.txtPort.Caption = "Cổng lắng nghe cho Client";
            this.txtPort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPort.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.txtPort.MaxLength = 5;
            this.txtPort.NumbersOnly = true;
            this.txtPort.Name = "txtPort";
            // 
            // tblAlgo
            // 
            this.tblAlgo.BackColor = System.Drawing.Color.Transparent;
            this.tblAlgo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblAlgo.Name = "tblAlgo";
            this.tblAlgo.ColumnCount = 2;
            this.tblAlgo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblAlgo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblAlgo.RowCount = 1;
            this.tblAlgo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblAlgo.Controls.Add(this.btnRr, 0, 0);
            this.tblAlgo.Controls.Add(this.btnLc, 1, 0);
            // 
            // btnRr
            // 
            this.btnRr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnRr.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnRr.Margin = new System.Windows.Forms.Padding(6, 0, 6, 4);
            this.btnRr.Text = "Round-Robin";
            this.btnRr.Name = "btnRr";
            this.btnRr.Click += new System.EventHandler(this.btnRr_Click);
            // 
            // btnLc
            // 
            this.btnLc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnLc.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnLc.Kind = ATM.UI.ButtonKind.Secondary;
            this.btnLc.Margin = new System.Windows.Forms.Padding(6, 0, 6, 4);
            this.btnLc.Text = "Least-Connections";
            this.btnLc.Name = "btnLc";
            this.btnLc.Click += new System.EventHandler(this.btnLc_Click);
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
            // lblIp
            // 
            this.lblIp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIp.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblIp.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.lblIp.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.lblIp.Text = "IP của máy này (nhập vào Client khi demo LAN): ...";
            this.lblIp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblIp.UseMnemonic = false;
            this.lblIp.Name = "lblIp";
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
            this.tblStats.Controls.Add(this.stActive, 0, 0);
            this.tblStats.Controls.Add(this.stTotal, 1, 0);
            this.tblStats.Controls.Add(this.stAlive, 2, 0);
            this.tblStats.Controls.Add(this.stUptime, 3, 0);
            // 
            // stActive
            // 
            this.stActive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stActive.Icon = ATM.UI.IconKind.Transfer;
            this.stActive.Margin = new System.Windows.Forms.Padding(6, 14, 6, 6);
            this.stActive.Title = "Kết nối đang chuyển tiếp";
            this.stActive.Value = "0";
            this.stActive.Name = "stActive";
            // 
            // stTotal
            // 
            this.stTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stTotal.Icon = ATM.UI.IconKind.Bolt;
            this.stTotal.Margin = new System.Windows.Forms.Padding(6, 14, 6, 6);
            this.stTotal.Title = "Tổng kết nối đã phân phối";
            this.stTotal.Value = "0";
            this.stTotal.Accent = System.Drawing.Color.FromArgb(99, 102, 241);
            this.stTotal.Name = "stTotal";
            // 
            // stAlive
            // 
            this.stAlive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stAlive.Icon = ATM.UI.IconKind.Server;
            this.stAlive.Margin = new System.Windows.Forms.Padding(6, 14, 6, 6);
            this.stAlive.Title = "Server khả dụng";
            this.stAlive.Value = "0";
            this.stAlive.Accent = System.Drawing.Color.FromArgb(34, 197, 94);
            this.stAlive.Name = "stAlive";
            // 
            // stUptime
            // 
            this.stUptime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stUptime.Icon = ATM.UI.IconKind.History;
            this.stUptime.Margin = new System.Windows.Forms.Padding(6, 14, 6, 6);
            this.stUptime.Title = "Thời gian hoạt động";
            this.stUptime.Value = "0";
            this.stUptime.Accent = System.Drawing.Color.FromArgb(245, 158, 11);
            this.stUptime.Name = "stUptime";
            // 
            // tblMain
            // 
            this.tblMain.BackColor = System.Drawing.Color.Transparent;
            this.tblMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMain.Name = "tblMain";
            this.tblMain.ColumnCount = 2;
            this.tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58F));
            this.tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42F));
            this.tblMain.RowCount = 1;
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMain.Controls.Add(this.pnlLeft, 0, 0);
            this.tblMain.Controls.Add(this.pnlRight, 1, 0);
            // 
            // pnlLeft
            // 
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeft.Margin = new System.Windows.Forms.Padding(0, 6, 8, 0);
            this.pnlLeft.Padding = new System.Windows.Forms.Padding(14, 14, 14, 14);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Controls.Add(this.gridBackends);
            this.pnlLeft.Controls.Add(this.tblAdd);
            // 
            // gridBackends
            // 
            this.gridBackends.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridBackends.Name = "gridBackends";
            this.gridBackends.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colName,
            this.colAddress,
            this.colStatus,
            this.colActive,
            this.colTotal,
            this.colChecked});
            // 
            // colName
            // 
            this.colName.HeaderText = "Tên";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            this.colName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colName.FillWeight = 18F;
            // 
            // colAddress
            // 
            this.colAddress.HeaderText = "Địa chỉ";
            this.colAddress.Name = "colAddress";
            this.colAddress.ReadOnly = true;
            this.colAddress.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colAddress.FillWeight = 26F;
            // 
            // colStatus
            // 
            this.colStatus.HeaderText = "Trạng thái";
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            this.colStatus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colStatus.FillWeight = 16F;
            // 
            // colActive
            // 
            this.colActive.HeaderText = "Đang nối";
            this.colActive.Name = "colActive";
            this.colActive.ReadOnly = true;
            this.colActive.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colActive.FillWeight = 14F;
            // 
            // colTotal
            // 
            this.colTotal.HeaderText = "Đã phục vụ";
            this.colTotal.Name = "colTotal";
            this.colTotal.ReadOnly = true;
            this.colTotal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colTotal.FillWeight = 14F;
            // 
            // colChecked
            // 
            this.colChecked.HeaderText = "Kiểm tra";
            this.colChecked.Name = "colChecked";
            this.colChecked.ReadOnly = true;
            this.colChecked.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colChecked.FillWeight = 14F;
            // 
            // tblAdd
            // 
            this.tblAdd.BackColor = System.Drawing.Color.Transparent;
            this.tblAdd.Dock = System.Windows.Forms.DockStyle.Top;
            this.tblAdd.Size = new System.Drawing.Size(400, 94);
            this.tblAdd.Name = "tblAdd";
            this.tblAdd.ColumnCount = 5;
            this.tblAdd.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24F));
            this.tblAdd.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.tblAdd.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.tblAdd.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tblAdd.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tblAdd.RowCount = 1;
            this.tblAdd.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblAdd.Controls.Add(this.txtName, 0, 0);
            this.tblAdd.Controls.Add(this.txtHost, 1, 0);
            this.tblAdd.Controls.Add(this.txtBport, 2, 0);
            this.tblAdd.Controls.Add(this.btnAdd, 3, 0);
            this.tblAdd.Controls.Add(this.btnDel, 4, 0);
            // 
            // txtName
            // 
            this.txtName.Caption = "Tên";
            this.txtName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtName.Margin = new System.Windows.Forms.Padding(3, 0, 3, 8);
            this.txtName.Name = "txtName";
            // 
            // txtHost
            // 
            this.txtHost.Caption = "Địa chỉ IP / host";
            this.txtHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtHost.Margin = new System.Windows.Forms.Padding(3, 0, 3, 8);
            this.txtHost.Name = "txtHost";
            // 
            // txtBport
            // 
            this.txtBport.Caption = "Cổng";
            this.txtBport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBport.Margin = new System.Windows.Forms.Padding(3, 0, 3, 8);
            this.txtBport.MaxLength = 5;
            this.txtBport.NumbersOnly = true;
            this.txtBport.Name = "txtBport";
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnAdd.Margin = new System.Windows.Forms.Padding(3, 0, 3, 12);
            this.btnAdd.Text = "Thêm";
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDel
            // 
            this.btnDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnDel.Kind = ATM.UI.ButtonKind.Secondary;
            this.btnDel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 12);
            this.btnDel.Text = "Xóa";
            this.btnDel.Name = "btnDel";
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // pnlRight
            // 
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Margin = new System.Windows.Forms.Padding(8, 6, 0, 0);
            this.pnlRight.Padding = new System.Windows.Forms.Padding(14, 14, 14, 14);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Controls.Add(this.rtbLog);
            // 
            // rtbLog
            // 
            this.rtbLog.BackColor = System.Drawing.Color.FromArgb(20, 29, 49);
            this.rtbLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbLog.Font = new System.Drawing.Font("Consolas", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rtbLog.ForeColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.rtbLog.ReadOnly = true;
            this.rtbLog.Name = "rtbLog";
            // 
            // timerRefresh
            // 
            this.timerRefresh.Enabled = true;
            this.timerRefresh.Interval = 500;
            this.timerRefresh.Tick += new System.EventHandler(this.timerRefresh_Tick);
            // 
            // LbForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(10, 16, 30);
            this.ClientSize = new System.Drawing.Size(1180, 780);
            this.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ForeColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.MinimumSize = new System.Drawing.Size(1040, 700);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ATM Load Balancer";
            this.Name = "LbForm";
            this.Controls.Add(this.tblRoot);
            this.ResumeLayout(false);
            this.pnlRight.ResumeLayout(false);
            this.tblAdd.ResumeLayout(false);
            this.pnlLeft.ResumeLayout(false);
            this.tblMain.ResumeLayout(false);
            this.tblStats.ResumeLayout(false);
            this.tblAlgo.ResumeLayout(false);
            this.tblConfig.ResumeLayout(false);
            this.pnlConfig.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.tblRoot.ResumeLayout(false);
            this.tblAdd.PerformLayout();
            this.tblMain.PerformLayout();
            this.tblStats.PerformLayout();
            this.tblAlgo.PerformLayout();
            this.tblConfig.PerformLayout();
            this.tblRoot.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridBackends)).EndInit();
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
        private ATM.UI.ModernTextBox txtPort;
        private System.Windows.Forms.TableLayoutPanel tblAlgo;
        private ATM.UI.RoundedButton btnRr;
        private ATM.UI.RoundedButton btnLc;
        private ATM.UI.RoundedButton btnToggle;
        private System.Windows.Forms.Label lblIp;
        private System.Windows.Forms.TableLayoutPanel tblStats;
        private ATM.UI.StatCard stActive;
        private ATM.UI.StatCard stTotal;
        private ATM.UI.StatCard stAlive;
        private ATM.UI.StatCard stUptime;
        private System.Windows.Forms.TableLayoutPanel tblMain;
        private ATM.UI.RoundedPanel pnlLeft;
        private ATM.UI.ThemedGrid gridBackends;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colActive;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn colChecked;
        private System.Windows.Forms.TableLayoutPanel tblAdd;
        private ATM.UI.ModernTextBox txtName;
        private ATM.UI.ModernTextBox txtHost;
        private ATM.UI.ModernTextBox txtBport;
        private ATM.UI.RoundedButton btnAdd;
        private ATM.UI.RoundedButton btnDel;
        private ATM.UI.RoundedPanel pnlRight;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.Timer timerRefresh;
    }
}
