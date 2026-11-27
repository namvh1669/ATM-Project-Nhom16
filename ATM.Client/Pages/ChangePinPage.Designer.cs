namespace ATM.Client.Pages
{
    partial class ChangePinPage
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
            this.cardPin = new ATM.UI.RoundedPanel();
            this.tblPin = new System.Windows.Forms.TableLayoutPanel();
            this.txtOld = new ATM.UI.ModernTextBox();
            this.txtNew = new ATM.UI.ModernTextBox();
            this.txtNew2 = new ATM.UI.ModernTextBox();
            this.btnUpdate = new ATM.UI.RoundedButton();
            this.pageHeader = new ATM.UI.PageHeader();
            this.pagePanel.SuspendLayout();
            this.cardPin.SuspendLayout();
            this.tblPin.SuspendLayout();
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
            this.pagePanel.Controls.Add(this.cardPin);
            // 
            // cardPin
            // 
            this.cardPin.Dock = System.Windows.Forms.DockStyle.Top;
            this.cardPin.Padding = new System.Windows.Forms.Padding(30, 22, 30, 24);
            this.cardPin.Size = new System.Drawing.Size(600, 420);
            this.cardPin.Name = "cardPin";
            this.cardPin.Controls.Add(this.tblPin);
            // 
            // tblPin
            // 
            this.tblPin.BackColor = System.Drawing.Color.Transparent;
            this.tblPin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblPin.Name = "tblPin";
            this.tblPin.ColumnCount = 1;
            this.tblPin.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblPin.RowCount = 7;
            this.tblPin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 82F));
            this.tblPin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 6F));
            this.tblPin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 82F));
            this.tblPin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 6F));
            this.tblPin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 82F));
            this.tblPin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblPin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tblPin.Controls.Add(this.txtOld, 0, 0);
            this.tblPin.Controls.Add(this.txtNew, 0, 2);
            this.tblPin.Controls.Add(this.txtNew2, 0, 4);
            this.tblPin.Controls.Add(this.btnUpdate, 0, 6);
            // 
            // txtOld
            // 
            this.txtOld.Caption = "Mã PIN hiện tại";
            this.txtOld.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOld.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.txtOld.MaxLength = 6;
            this.txtOld.NumbersOnly = true;
            this.txtOld.Password = true;
            this.txtOld.TabIndex = 0;
            this.txtOld.Name = "txtOld";
            // 
            // txtNew
            // 
            this.txtNew.Caption = "Mã PIN mới (6 chữ số)";
            this.txtNew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNew.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.txtNew.MaxLength = 6;
            this.txtNew.NumbersOnly = true;
            this.txtNew.Password = true;
            this.txtNew.TabIndex = 1;
            this.txtNew.Name = "txtNew";
            // 
            // txtNew2
            // 
            this.txtNew2.Caption = "Nhập lại mã PIN mới";
            this.txtNew2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNew2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.txtNew2.MaxLength = 6;
            this.txtNew2.NumbersOnly = true;
            this.txtNew2.Password = true;
            this.txtNew2.TabIndex = 2;
            this.txtNew2.Name = "txtNew2";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnUpdate.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnUpdate.Icon = ATM.UI.IconKind.Lock;
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.btnUpdate.TabIndex = 3;
            this.btnUpdate.Text = "Cập nhật mã PIN";
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // pageHeader
            // 
            this.pageHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pageHeader.MaxContentWidth = 600;
            this.pageHeader.Size = new System.Drawing.Size(1100, 90);
            this.pageHeader.Subtitle = "Bảo vệ thẻ của bạn bằng mã PIN mới";
            this.pageHeader.Title = "Đổi mã PIN";
            this.pageHeader.Name = "pageHeader";
            // 
            // ChangePinPage
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(10, 16, 30);
            this.Size = new System.Drawing.Size(1100, 640);
            this.Name = "ChangePinPage";
            this.Controls.Add(this.pagePanel);
            this.Controls.Add(this.pageHeader);
            this.ResumeLayout(false);
            this.tblPin.ResumeLayout(false);
            this.cardPin.ResumeLayout(false);
            this.pagePanel.ResumeLayout(false);
            this.tblPin.PerformLayout();
            this.PerformLayout();

        }

        #endregion

        private ATM.UI.PagePanel pagePanel;
        private ATM.UI.RoundedPanel cardPin;
        private System.Windows.Forms.TableLayoutPanel tblPin;
        private ATM.UI.ModernTextBox txtOld;
        private ATM.UI.ModernTextBox txtNew;
        private ATM.UI.ModernTextBox txtNew2;
        private ATM.UI.RoundedButton btnUpdate;
        private ATM.UI.PageHeader pageHeader;
    }
}
