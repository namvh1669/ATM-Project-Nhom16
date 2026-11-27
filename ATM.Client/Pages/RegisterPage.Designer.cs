namespace ATM.Client.Pages
{
    partial class RegisterPage
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
            this.cardRegister = new ATM.UI.RoundedPanel();
            this.tblRegister = new System.Windows.Forms.TableLayoutPanel();
            this.txtName = new ATM.UI.ModernTextBox();
            this.txtPin = new ATM.UI.ModernTextBox();
            this.txtPin2 = new ATM.UI.ModernTextBox();
            this.lblNote = new System.Windows.Forms.Label();
            this.btnRegister = new ATM.UI.RoundedButton();
            this.pageHeader = new ATM.UI.PageHeader();
            this.pagePanel.SuspendLayout();
            this.cardRegister.SuspendLayout();
            this.tblRegister.SuspendLayout();
            this.SuspendLayout();
            // 
            // pagePanel
            // 
            this.pagePanel.AutoScroll = true;
            this.pagePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pagePanel.MaxContentWidth = 600;
            this.pagePanel.MinContentHeight = 500;
            this.pagePanel.Padding = new System.Windows.Forms.Padding(40, 4, 40, 24);
            this.pagePanel.Name = "pagePanel";
            this.pagePanel.Controls.Add(this.cardRegister);
            // 
            // cardRegister
            // 
            this.cardRegister.Dock = System.Windows.Forms.DockStyle.Top;
            this.cardRegister.Padding = new System.Windows.Forms.Padding(30, 22, 30, 24);
            this.cardRegister.Size = new System.Drawing.Size(600, 440);
            this.cardRegister.Name = "cardRegister";
            this.cardRegister.Controls.Add(this.tblRegister);
            // 
            // tblRegister
            // 
            this.tblRegister.BackColor = System.Drawing.Color.Transparent;
            this.tblRegister.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblRegister.Name = "tblRegister";
            this.tblRegister.ColumnCount = 1;
            this.tblRegister.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblRegister.RowCount = 10;
            this.tblRegister.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 82F));
            this.tblRegister.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 6F));
            this.tblRegister.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 82F));
            this.tblRegister.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 6F));
            this.tblRegister.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 82F));
            this.tblRegister.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 6F));
            this.tblRegister.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblRegister.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tblRegister.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tblRegister.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblRegister.Controls.Add(this.txtName, 0, 0);
            this.tblRegister.Controls.Add(this.txtPin, 0, 2);
            this.tblRegister.Controls.Add(this.txtPin2, 0, 4);
            this.tblRegister.Controls.Add(this.lblNote, 0, 6);
            this.tblRegister.Controls.Add(this.btnRegister, 0, 8);
            // 
            // txtName
            // 
            this.txtName.Caption = "Họ và tên chủ thẻ";
            this.txtName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtName.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.txtName.MaxLength = 50;
            this.txtName.Placeholder = "Ví dụ: Nguyễn Văn A";
            this.txtName.TabIndex = 0;
            this.txtName.Name = "txtName";
            // 
            // txtPin
            // 
            this.txtPin.Caption = "Mã PIN mới (6 chữ số)";
            this.txtPin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPin.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.txtPin.MaxLength = 6;
            this.txtPin.NumbersOnly = true;
            this.txtPin.Password = true;
            this.txtPin.TabIndex = 1;
            this.txtPin.Name = "txtPin";
            // 
            // txtPin2
            // 
            this.txtPin2.Caption = "Nhập lại mã PIN";
            this.txtPin2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPin2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.txtPin2.MaxLength = 6;
            this.txtPin2.NumbersOnly = true;
            this.txtPin2.Password = true;
            this.txtPin2.TabIndex = 2;
            this.txtPin2.Name = "txtPin2";
            // 
            // lblNote
            // 
            this.lblNote.BackColor = System.Drawing.Color.Transparent;
            this.lblNote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNote.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblNote.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.lblNote.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.lblNote.Text = "Mã PIN được mã hóa bằng PBKDF2-SHA256 (có salt) ngay trên máy chủ, không bao giờ lưu dạng rõ.";
            this.lblNote.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lblNote.UseMnemonic = false;
            this.lblNote.Name = "lblNote";
            // 
            // btnRegister
            // 
            this.btnRegister.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRegister.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnRegister.Icon = ATM.UI.IconKind.Card;
            this.btnRegister.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.btnRegister.TabIndex = 3;
            this.btnRegister.Text = "Tạo thẻ";
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // pageHeader
            // 
            this.pageHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pageHeader.MaxContentWidth = 600;
            this.pageHeader.Size = new System.Drawing.Size(1100, 90);
            this.pageHeader.Subtitle = "Tạo tài khoản ATM - số thẻ sẽ được hệ thống cấp tự động";
            this.pageHeader.Title = "Đăng ký thẻ mới";
            this.pageHeader.Name = "pageHeader";
            // 
            // RegisterPage
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(10, 16, 30);
            this.Size = new System.Drawing.Size(1100, 640);
            this.Name = "RegisterPage";
            this.Controls.Add(this.pagePanel);
            this.Controls.Add(this.pageHeader);
            this.ResumeLayout(false);
            this.tblRegister.ResumeLayout(false);
            this.cardRegister.ResumeLayout(false);
            this.pagePanel.ResumeLayout(false);
            this.tblRegister.PerformLayout();
            this.PerformLayout();

        }

        #endregion

        private ATM.UI.PagePanel pagePanel;
        private ATM.UI.RoundedPanel cardRegister;
        private System.Windows.Forms.TableLayoutPanel tblRegister;
        private ATM.UI.ModernTextBox txtName;
        private ATM.UI.ModernTextBox txtPin;
        private ATM.UI.ModernTextBox txtPin2;
        private System.Windows.Forms.Label lblNote;
        private ATM.UI.RoundedButton btnRegister;
        private ATM.UI.PageHeader pageHeader;
    }
}
