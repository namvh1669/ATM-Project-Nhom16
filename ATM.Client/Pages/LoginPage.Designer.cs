namespace ATM.Client.Pages
{
    partial class LoginPage
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
            this.tblRoot = new System.Windows.Forms.TableLayoutPanel();
            this.brandPanel = new ATM.Client.Pages.BrandPanel();
            this.pnlRightHost = new ATM.UI.PagePanel();
            this.cardLogin = new ATM.UI.RoundedPanel();
            this.tblLogin = new System.Windows.Forms.TableLayoutPanel();
            this.pnlHead = new System.Windows.Forms.Panel();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.lblWelcomeSub = new System.Windows.Forms.Label();
            this.txtCard = new ATM.UI.ModernTextBox();
            this.lblPinCaption = new System.Windows.Forms.Label();
            this.pinDots = new ATM.UI.PinDots();
            this.numPad = new ATM.UI.NumPad();
            this.btnLogin = new ATM.UI.RoundedButton();
            this.btnRegister = new ATM.UI.RoundedButton();
            this.pagePanel.SuspendLayout();
            this.tblRoot.SuspendLayout();
            this.pnlRightHost.SuspendLayout();
            this.cardLogin.SuspendLayout();
            this.tblLogin.SuspendLayout();
            this.pnlHead.SuspendLayout();
            this.SuspendLayout();
            // 
            // pagePanel
            // 
            this.pagePanel.AutoScroll = true;
            this.pagePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pagePanel.MaxContentWidth = 1100;
            this.pagePanel.MinContentHeight = 610;
            this.pagePanel.Padding = new System.Windows.Forms.Padding(40, 16, 40, 16);
            this.pagePanel.Name = "pagePanel";
            this.pagePanel.Controls.Add(this.tblRoot);
            // 
            // tblRoot
            // 
            this.tblRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblRoot.Name = "tblRoot";
            this.tblRoot.ColumnCount = 2;
            this.tblRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48F));
            this.tblRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52F));
            this.tblRoot.RowCount = 1;
            this.tblRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblRoot.Controls.Add(this.brandPanel, 0, 0);
            this.tblRoot.Controls.Add(this.pnlRightHost, 1, 0);
            // 
            // brandPanel
            // 
            this.brandPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.brandPanel.Margin = new System.Windows.Forms.Padding(0, 0, 14, 0);
            this.brandPanel.Name = "brandPanel";
            this.brandPanel.DemoClick += new System.EventHandler(this.brandPanel_DemoClick);
            // 
            // pnlRightHost
            // 
            this.pnlRightHost.AutoScroll = false;
            this.pnlRightHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRightHost.Margin = new System.Windows.Forms.Padding(14, 0, 0, 0);
            this.pnlRightHost.MaxContentWidth = 480;
            this.pnlRightHost.MinContentHeight = 0;
            this.pnlRightHost.Padding = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.pnlRightHost.Name = "pnlRightHost";
            this.pnlRightHost.Controls.Add(this.cardLogin);
            // 
            // cardLogin
            // 
            this.cardLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardLogin.Padding = new System.Windows.Forms.Padding(28, 22, 28, 18);
            this.cardLogin.Name = "cardLogin";
            this.cardLogin.Controls.Add(this.tblLogin);
            // 
            // tblLogin
            // 
            this.tblLogin.BackColor = System.Drawing.Color.Transparent;
            this.tblLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLogin.Name = "tblLogin";
            this.tblLogin.ColumnCount = 1;
            this.tblLogin.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLogin.RowCount = 11;
            this.tblLogin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 76F));
            this.tblLogin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tblLogin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tblLogin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tblLogin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tblLogin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tblLogin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLogin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tblLogin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tblLogin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 4F));
            this.tblLogin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblLogin.Controls.Add(this.pnlHead, 0, 0);
            this.tblLogin.Controls.Add(this.txtCard, 0, 1);
            this.tblLogin.Controls.Add(this.lblPinCaption, 0, 3);
            this.tblLogin.Controls.Add(this.pinDots, 0, 4);
            this.tblLogin.Controls.Add(this.numPad, 0, 6);
            this.tblLogin.Controls.Add(this.btnLogin, 0, 8);
            this.tblLogin.Controls.Add(this.btnRegister, 0, 10);
            // 
            // pnlHead
            // 
            this.pnlHead.BackColor = System.Drawing.Color.Transparent;
            this.pnlHead.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHead.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.pnlHead.Name = "pnlHead";
            this.pnlHead.Controls.Add(this.lblWelcome);
            this.pnlHead.Controls.Add(this.lblWelcomeSub);
            // 
            // lblWelcome
            // 
            this.lblWelcome.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWelcome.BackColor = System.Drawing.Color.Transparent;
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI Semibold", 19F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblWelcome.ForeColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.lblWelcome.Location = new System.Drawing.Point(0, 0);
            this.lblWelcome.Size = new System.Drawing.Size(400, 36);
            this.lblWelcome.Text = "Chào mừng trở lại";
            this.lblWelcome.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblWelcome.UseMnemonic = false;
            this.lblWelcome.Name = "lblWelcome";
            // 
            // lblWelcomeSub
            // 
            this.lblWelcomeSub.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWelcomeSub.BackColor = System.Drawing.Color.Transparent;
            this.lblWelcomeSub.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblWelcomeSub.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.lblWelcomeSub.Location = new System.Drawing.Point(0, 36);
            this.lblWelcomeSub.Size = new System.Drawing.Size(400, 26);
            this.lblWelcomeSub.Text = "Nhập số thẻ và mã PIN để tiếp tục";
            this.lblWelcomeSub.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblWelcomeSub.UseMnemonic = false;
            this.lblWelcomeSub.Name = "lblWelcomeSub";
            // 
            // txtCard
            // 
            this.txtCard.Caption = "Số thẻ";
            this.txtCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCard.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.txtCard.MaxLength = 16;
            this.txtCard.NumbersOnly = true;
            this.txtCard.Placeholder = "Ví dụ: 1000000001";
            this.txtCard.TabIndex = 1;
            this.txtCard.Name = "txtCard";
            // 
            // lblPinCaption
            // 
            this.lblPinCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblPinCaption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPinCaption.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblPinCaption.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.lblPinCaption.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.lblPinCaption.Text = "Mã PIN (6 chữ số)";
            this.lblPinCaption.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.lblPinCaption.UseMnemonic = false;
            this.lblPinCaption.Name = "lblPinCaption";
            // 
            // pinDots
            // 
            this.pinDots.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pinDots.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.pinDots.TabIndex = 2;
            this.pinDots.Name = "pinDots";
            // 
            // numPad
            // 
            this.numPad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numPad.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.numPad.Name = "numPad";
            this.numPad.KeyPressed += new ATM.UI.NumPadKeyEventHandler(this.numPad_KeyPressed);
            // 
            // btnLogin
            // 
            this.btnLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLogin.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnLogin.Icon = ATM.UI.IconKind.Lock;
            this.btnLogin.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.btnLogin.TabIndex = 3;
            this.btnLogin.Text = "Đăng nhập";
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnRegister
            // 
            this.btnRegister.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRegister.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnRegister.ForeColor = System.Drawing.Color.FromArgb(56, 189, 248);
            this.btnRegister.Kind = ATM.UI.ButtonKind.Ghost;
            this.btnRegister.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.btnRegister.TabIndex = 4;
            this.btnRegister.Text = "Chưa có thẻ?  Đăng ký ngay";
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // LoginPage
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(10, 16, 30);
            this.Size = new System.Drawing.Size(1100, 640);
            this.Name = "LoginPage";
            this.Controls.Add(this.pagePanel);
            this.ResumeLayout(false);
            this.pnlHead.ResumeLayout(false);
            this.tblLogin.ResumeLayout(false);
            this.cardLogin.ResumeLayout(false);
            this.pnlRightHost.ResumeLayout(false);
            this.tblRoot.ResumeLayout(false);
            this.pagePanel.ResumeLayout(false);
            this.tblLogin.PerformLayout();
            this.tblRoot.PerformLayout();
            this.PerformLayout();

        }

        #endregion

        private ATM.UI.PagePanel pagePanel;
        private System.Windows.Forms.TableLayoutPanel tblRoot;
        private ATM.Client.Pages.BrandPanel brandPanel;
        private ATM.UI.PagePanel pnlRightHost;
        private ATM.UI.RoundedPanel cardLogin;
        private System.Windows.Forms.TableLayoutPanel tblLogin;
        private System.Windows.Forms.Panel pnlHead;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Label lblWelcomeSub;
        private ATM.UI.ModernTextBox txtCard;
        private System.Windows.Forms.Label lblPinCaption;
        private ATM.UI.PinDots pinDots;
        private ATM.UI.NumPad numPad;
        private ATM.UI.RoundedButton btnLogin;
        private ATM.UI.RoundedButton btnRegister;
    }
}
