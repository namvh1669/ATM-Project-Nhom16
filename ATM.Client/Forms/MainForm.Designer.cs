namespace ATM.Client.Forms
{
    partial class MainForm
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
            this.pnlHost = new System.Windows.Forms.Panel();
            this.busyStrip = new ATM.UI.BusyStrip();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.logo = new ATM.Client.Forms.LogoMark();
            this.lblBrand = new System.Windows.Forms.Label();
            this.lblBrandSub = new System.Windows.Forms.Label();
            this.lblClock = new System.Windows.Forms.Label();
            this.pillConn = new ATM.UI.StatusPill();
            this.btnConn = new ATM.UI.RoundedButton();
            this.lblUser = new System.Windows.Forms.Label();
            this.btnLogout = new ATM.UI.RoundedButton();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.timerClock = new System.Windows.Forms.Timer(this.components);
            this.pnlTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHost
            // 
            this.pnlHost.BackColor = System.Drawing.Color.FromArgb(10, 16, 30);
            this.pnlHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHost.Name = "pnlHost";
            // 
            // busyStrip
            // 
            this.busyStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.busyStrip.Size = new System.Drawing.Size(1180, 3);
            this.busyStrip.Name = "busyStrip";
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(13, 21, 38);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Size = new System.Drawing.Size(1180, 66);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Controls.Add(this.logo);
            this.pnlTop.Controls.Add(this.lblBrand);
            this.pnlTop.Controls.Add(this.lblBrandSub);
            this.pnlTop.Controls.Add(this.lblClock);
            this.pnlTop.Controls.Add(this.pillConn);
            this.pnlTop.Controls.Add(this.btnConn);
            this.pnlTop.Controls.Add(this.lblUser);
            this.pnlTop.Controls.Add(this.btnLogout);
            this.pnlTop.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlTop_Paint);
            this.pnlTop.Resize += new System.EventHandler(this.pnlTop_Resize);
            // 
            // logo
            // 
            this.logo.Location = new System.Drawing.Point(24, 12);
            this.logo.Size = new System.Drawing.Size(42, 42);
            this.logo.Name = "logo";
            // 
            // lblBrand
            // 
            this.lblBrand.AutoSize = true;
            this.lblBrand.BackColor = System.Drawing.Color.Transparent;
            this.lblBrand.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblBrand.ForeColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.lblBrand.Location = new System.Drawing.Point(76, 8);
            this.lblBrand.Text = "ATM Banking";
            this.lblBrand.UseMnemonic = false;
            this.lblBrand.Name = "lblBrand";
            // 
            // lblBrandSub
            // 
            this.lblBrandSub.AutoSize = true;
            this.lblBrandSub.BackColor = System.Drawing.Color.Transparent;
            this.lblBrandSub.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblBrandSub.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.lblBrandSub.Location = new System.Drawing.Point(78, 38);
            this.lblBrandSub.Text = "Giao dịch an toàn qua mạng";
            this.lblBrandSub.UseMnemonic = false;
            this.lblBrandSub.Name = "lblBrandSub";
            // 
            // lblClock
            // 
            this.lblClock.AutoSize = false;
            this.lblClock.BackColor = System.Drawing.Color.Transparent;
            this.lblClock.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblClock.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.lblClock.Location = new System.Drawing.Point(990, 21);
            this.lblClock.Size = new System.Drawing.Size(170, 24);
            this.lblClock.Text = "07/11/2026  09:00:00";
            this.lblClock.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblClock.UseMnemonic = false;
            this.lblClock.Name = "lblClock";
            // 
            // pillConn
            // 
            this.pillConn.Location = new System.Drawing.Point(790, 18);
            this.pillConn.PillColor = System.Drawing.Color.FromArgb(239, 68, 68);
            this.pillConn.PillText = "Chưa kết nối";
            this.pillConn.Name = "pillConn";
            // 
            // btnConn
            // 
            this.btnConn.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnConn.Icon = ATM.UI.IconKind.Server;
            this.btnConn.Kind = ATM.UI.ButtonKind.Secondary;
            this.btnConn.Location = new System.Drawing.Point(650, 14);
            this.btnConn.Size = new System.Drawing.Size(120, 38);
            this.btnConn.Text = "Kết nối";
            this.btnConn.Name = "btnConn";
            this.btnConn.Click += new System.EventHandler(this.btnConn_Click);
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = false;
            this.lblUser.AutoEllipsis = true;
            this.lblUser.BackColor = System.Drawing.Color.Transparent;
            this.lblUser.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblUser.ForeColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.lblUser.Location = new System.Drawing.Point(360, 21);
            this.lblUser.Size = new System.Drawing.Size(260, 24);
            this.lblUser.Text = "Nguyễn Văn An  ·  1000000001";
            this.lblUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblUser.UseMnemonic = false;
            this.lblUser.Name = "lblUser";
            // 
            // btnLogout
            // 
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnLogout.Icon = ATM.UI.IconKind.Power;
            this.btnLogout.Kind = ATM.UI.ButtonKind.Secondary;
            this.btnLogout.Location = new System.Drawing.Point(640, 14);
            this.btnLogout.Size = new System.Drawing.Size(130, 38);
            this.btnLogout.Text = "Đăng xuất";
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // toolTip
            // 
            // 
            // timerClock
            // 
            this.timerClock.Enabled = true;
            this.timerClock.Interval = 1000;
            this.timerClock.Tick += new System.EventHandler(this.timerClock_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(10, 16, 30);
            this.ClientSize = new System.Drawing.Size(1180, 780);
            this.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ForeColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.KeyPreview = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ATM Banking";
            this.Name = "MainForm";
            this.Controls.Add(this.pnlHost);
            this.Controls.Add(this.busyStrip);
            this.Controls.Add(this.pnlTop);
            this.ResumeLayout(false);
            this.pnlTop.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlHost;
        private ATM.UI.BusyStrip busyStrip;
        private System.Windows.Forms.Panel pnlTop;
        private ATM.Client.Forms.LogoMark logo;
        private System.Windows.Forms.Label lblBrand;
        private System.Windows.Forms.Label lblBrandSub;
        private System.Windows.Forms.Label lblClock;
        private ATM.UI.StatusPill pillConn;
        private ATM.UI.RoundedButton btnConn;
        private System.Windows.Forms.Label lblUser;
        private ATM.UI.RoundedButton btnLogout;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Timer timerClock;
    }
}
