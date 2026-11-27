namespace ATM.Client.Pages
{
    partial class TransferPage
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
            this.cardTransfer = new ATM.UI.RoundedPanel();
            this.tblTransfer = new System.Windows.Forms.TableLayoutPanel();
            this.tblTo = new System.Windows.Forms.TableLayoutPanel();
            this.txtTo = new ATM.UI.ModernTextBox();
            this.btnLookup = new ATM.UI.RoundedButton();
            this.lblRecipient = new System.Windows.Forms.Label();
            this.txtAmount = new ATM.UI.ModernTextBox();
            this.txtNote = new ATM.UI.ModernTextBox();
            this.btnSend = new ATM.UI.RoundedButton();
            this.pageHeader = new ATM.UI.PageHeader();
            this.pagePanel.SuspendLayout();
            this.cardTransfer.SuspendLayout();
            this.tblTransfer.SuspendLayout();
            this.tblTo.SuspendLayout();
            this.SuspendLayout();
            // 
            // pagePanel
            // 
            this.pagePanel.AutoScroll = true;
            this.pagePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pagePanel.MaxContentWidth = 640;
            this.pagePanel.MinContentHeight = 560;
            this.pagePanel.Padding = new System.Windows.Forms.Padding(40, 4, 40, 24);
            this.pagePanel.Name = "pagePanel";
            this.pagePanel.Controls.Add(this.cardTransfer);
            // 
            // cardTransfer
            // 
            this.cardTransfer.Dock = System.Windows.Forms.DockStyle.Top;
            this.cardTransfer.Padding = new System.Windows.Forms.Padding(28, 20, 28, 22);
            this.cardTransfer.Size = new System.Drawing.Size(640, 500);
            this.cardTransfer.Name = "cardTransfer";
            this.cardTransfer.Controls.Add(this.tblTransfer);
            // 
            // tblTransfer
            // 
            this.tblTransfer.BackColor = System.Drawing.Color.Transparent;
            this.tblTransfer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblTransfer.Name = "tblTransfer";
            this.tblTransfer.ColumnCount = 1;
            this.tblTransfer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblTransfer.RowCount = 7;
            this.tblTransfer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 84F));
            this.tblTransfer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tblTransfer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 84F));
            this.tblTransfer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 6F));
            this.tblTransfer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 84F));
            this.tblTransfer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblTransfer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tblTransfer.Controls.Add(this.tblTo, 0, 0);
            this.tblTransfer.Controls.Add(this.lblRecipient, 0, 1);
            this.tblTransfer.Controls.Add(this.txtAmount, 0, 2);
            this.tblTransfer.Controls.Add(this.txtNote, 0, 4);
            this.tblTransfer.Controls.Add(this.btnSend, 0, 6);
            // 
            // tblTo
            // 
            this.tblTo.BackColor = System.Drawing.Color.Transparent;
            this.tblTo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblTo.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.tblTo.Name = "tblTo";
            this.tblTo.ColumnCount = 2;
            this.tblTo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblTo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tblTo.RowCount = 1;
            this.tblTo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblTo.Controls.Add(this.txtTo, 0, 0);
            this.tblTo.Controls.Add(this.btnLookup, 1, 0);
            // 
            // txtTo
            // 
            this.txtTo.Caption = "Số thẻ người nhận";
            this.txtTo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTo.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.txtTo.MaxLength = 16;
            this.txtTo.NumbersOnly = true;
            this.txtTo.TabIndex = 0;
            this.txtTo.Name = "txtTo";
            this.txtTo.TextChanged += new System.EventHandler(this.txtTo_TextChanged);
            // 
            // btnLookup
            // 
            this.btnLookup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnLookup.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnLookup.Kind = ATM.UI.ButtonKind.Secondary;
            this.btnLookup.Margin = new System.Windows.Forms.Padding(10, 0, 0, 4);
            this.btnLookup.TabIndex = 1;
            this.btnLookup.Text = "Kiểm tra";
            this.btnLookup.Name = "btnLookup";
            this.btnLookup.Click += new System.EventHandler(this.btnLookup_Click);
            // 
            // lblRecipient
            // 
            this.lblRecipient.BackColor = System.Drawing.Color.Transparent;
            this.lblRecipient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRecipient.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblRecipient.ForeColor = System.Drawing.Color.FromArgb(34, 197, 94);
            this.lblRecipient.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.lblRecipient.Text = "✓  Trần Thị Bình";
            this.lblRecipient.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblRecipient.UseMnemonic = false;
            this.lblRecipient.Name = "lblRecipient";
            // 
            // txtAmount
            // 
            this.txtAmount.Caption = "Số tiền chuyển (VND)";
            this.txtAmount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAmount.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.txtAmount.MaxLength = 10;
            this.txtAmount.NumbersOnly = true;
            this.txtAmount.TabIndex = 2;
            this.txtAmount.Name = "txtAmount";
            // 
            // txtNote
            // 
            this.txtNote.Caption = "Nội dung chuyển khoản (tùy chọn)";
            this.txtNote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNote.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.txtNote.MaxLength = 100;
            this.txtNote.Placeholder = "Ví dụ: Tiền ăn trưa";
            this.txtNote.TabIndex = 3;
            this.txtNote.Name = "txtNote";
            // 
            // btnSend
            // 
            this.btnSend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSend.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnSend.Icon = ATM.UI.IconKind.Transfer;
            this.btnSend.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.btnSend.TabIndex = 4;
            this.btnSend.Text = "Chuyển tiền";
            this.btnSend.Name = "btnSend";
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // pageHeader
            // 
            this.pageHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pageHeader.MaxContentWidth = 640;
            this.pageHeader.Size = new System.Drawing.Size(1100, 90);
            this.pageHeader.Subtitle = "Số dư hiện tại";
            this.pageHeader.Title = "Chuyển khoản";
            this.pageHeader.Name = "pageHeader";
            // 
            // TransferPage
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(10, 16, 30);
            this.Size = new System.Drawing.Size(1100, 640);
            this.Name = "TransferPage";
            this.Controls.Add(this.pagePanel);
            this.Controls.Add(this.pageHeader);
            this.ResumeLayout(false);
            this.tblTo.ResumeLayout(false);
            this.tblTransfer.ResumeLayout(false);
            this.cardTransfer.ResumeLayout(false);
            this.pagePanel.ResumeLayout(false);
            this.tblTo.PerformLayout();
            this.tblTransfer.PerformLayout();
            this.PerformLayout();

        }

        #endregion

        private ATM.UI.PagePanel pagePanel;
        private ATM.UI.RoundedPanel cardTransfer;
        private System.Windows.Forms.TableLayoutPanel tblTransfer;
        private System.Windows.Forms.TableLayoutPanel tblTo;
        private ATM.UI.ModernTextBox txtTo;
        private ATM.UI.RoundedButton btnLookup;
        private System.Windows.Forms.Label lblRecipient;
        private ATM.UI.ModernTextBox txtAmount;
        private ATM.UI.ModernTextBox txtNote;
        private ATM.UI.RoundedButton btnSend;
        private ATM.UI.PageHeader pageHeader;
    }
}
