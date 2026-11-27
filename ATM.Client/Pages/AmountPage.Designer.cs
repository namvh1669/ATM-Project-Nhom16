namespace ATM.Client.Pages
{
    partial class AmountPage
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
            this.tblAmount = new System.Windows.Forms.TableLayoutPanel();
            this.pnlLeft = new ATM.UI.RoundedPanel();
            this.tblLeft = new System.Windows.Forms.TableLayoutPanel();
            this.txtAmount = new ATM.UI.ModernTextBox();
            this.lblPreview = new System.Windows.Forms.Label();
            this.lblHint = new System.Windows.Forms.Label();
            this.tblChips = new System.Windows.Forms.TableLayoutPanel();
            this.chip1 = new ATM.UI.RoundedButton();
            this.chip2 = new ATM.UI.RoundedButton();
            this.chip3 = new ATM.UI.RoundedButton();
            this.chip4 = new ATM.UI.RoundedButton();
            this.lblAfter = new System.Windows.Forms.Label();
            this.btnSubmit = new ATM.UI.RoundedButton();
            this.pnlRight = new ATM.UI.RoundedPanel();
            this.numPad = new ATM.UI.NumPad();
            this.pageHeader = new ATM.UI.PageHeader();
            this.pagePanel.SuspendLayout();
            this.tblAmount.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.tblLeft.SuspendLayout();
            this.tblChips.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // pagePanel
            // 
            this.pagePanel.AutoScroll = true;
            this.pagePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pagePanel.MaxContentWidth = 980;
            this.pagePanel.MinContentHeight = 470;
            this.pagePanel.Padding = new System.Windows.Forms.Padding(40, 4, 40, 24);
            this.pagePanel.Name = "pagePanel";
            this.pagePanel.Controls.Add(this.tblAmount);
            // 
            // tblAmount
            // 
            this.tblAmount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblAmount.Name = "tblAmount";
            this.tblAmount.ColumnCount = 2;
            this.tblAmount.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tblAmount.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tblAmount.RowCount = 1;
            this.tblAmount.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblAmount.Controls.Add(this.pnlLeft, 0, 0);
            this.tblAmount.Controls.Add(this.pnlRight, 1, 0);
            // 
            // pnlLeft
            // 
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeft.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.pnlLeft.Padding = new System.Windows.Forms.Padding(26, 20, 26, 20);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Controls.Add(this.tblLeft);
            // 
            // tblLeft
            // 
            this.tblLeft.BackColor = System.Drawing.Color.Transparent;
            this.tblLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLeft.Name = "tblLeft";
            this.tblLeft.ColumnCount = 1;
            this.tblLeft.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLeft.RowCount = 10;
            this.tblLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 84F));
            this.tblLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tblLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tblLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tblLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 6F));
            this.tblLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tblLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tblLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tblLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tblLeft.Controls.Add(this.txtAmount, 0, 0);
            this.tblLeft.Controls.Add(this.lblPreview, 0, 2);
            this.tblLeft.Controls.Add(this.lblHint, 0, 3);
            this.tblLeft.Controls.Add(this.tblChips, 0, 5);
            this.tblLeft.Controls.Add(this.lblAfter, 0, 7);
            this.tblLeft.Controls.Add(this.btnSubmit, 0, 9);
            // 
            // txtAmount
            // 
            this.txtAmount.Caption = "Số tiền (VND)";
            this.txtAmount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAmount.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.txtAmount.MaxLength = 10;
            this.txtAmount.NumbersOnly = true;
            this.txtAmount.Placeholder = "0";
            this.txtAmount.TabIndex = 0;
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.TextChanged += new System.EventHandler(this.txtAmount_TextChanged);
            // 
            // lblPreview
            // 
            this.lblPreview.BackColor = System.Drawing.Color.Transparent;
            this.lblPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPreview.Font = new System.Drawing.Font("Segoe UI Semibold", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblPreview.ForeColor = System.Drawing.Color.FromArgb(56, 189, 248);
            this.lblPreview.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.lblPreview.Text = "= 1.500.000 ₫";
            this.lblPreview.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblPreview.UseMnemonic = false;
            this.lblPreview.Name = "lblPreview";
            // 
            // lblHint
            // 
            this.lblHint.BackColor = System.Drawing.Color.Transparent;
            this.lblHint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHint.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblHint.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.lblHint.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.lblHint.Text = "Bội số của 10.000 ₫, tối đa 10.000.000 ₫ mỗi giao dịch.";
            this.lblHint.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lblHint.UseMnemonic = false;
            this.lblHint.Name = "lblHint";
            // 
            // tblChips
            // 
            this.tblChips.BackColor = System.Drawing.Color.Transparent;
            this.tblChips.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblChips.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.tblChips.Name = "tblChips";
            this.tblChips.ColumnCount = 4;
            this.tblChips.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblChips.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblChips.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblChips.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblChips.RowCount = 1;
            this.tblChips.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblChips.Controls.Add(this.chip1, 0, 0);
            this.tblChips.Controls.Add(this.chip2, 1, 0);
            this.tblChips.Controls.Add(this.chip3, 2, 0);
            this.tblChips.Controls.Add(this.chip4, 3, 0);
            // 
            // chip1
            // 
            this.chip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chip1.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.chip1.Kind = ATM.UI.ButtonKind.Secondary;
            this.chip1.Margin = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.chip1.Focusable = false;
            this.chip1.Text = "+100k";
            this.chip1.Name = "chip1";
            this.chip1.Click += new System.EventHandler(this.chip_Click);
            // 
            // chip2
            // 
            this.chip2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chip2.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.chip2.Kind = ATM.UI.ButtonKind.Secondary;
            this.chip2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.chip2.Focusable = false;
            this.chip2.Text = "+200k";
            this.chip2.Name = "chip2";
            this.chip2.Click += new System.EventHandler(this.chip_Click);
            // 
            // chip3
            // 
            this.chip3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chip3.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.chip3.Kind = ATM.UI.ButtonKind.Secondary;
            this.chip3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.chip3.Focusable = false;
            this.chip3.Text = "+500k";
            this.chip3.Name = "chip3";
            this.chip3.Click += new System.EventHandler(this.chip_Click);
            // 
            // chip4
            // 
            this.chip4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chip4.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.chip4.Kind = ATM.UI.ButtonKind.Secondary;
            this.chip4.Margin = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.chip4.Focusable = false;
            this.chip4.Text = "+1tr";
            this.chip4.Name = "chip4";
            this.chip4.Click += new System.EventHandler(this.chip_Click);
            // 
            // lblAfter
            // 
            this.lblAfter.BackColor = System.Drawing.Color.Transparent;
            this.lblAfter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAfter.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblAfter.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.lblAfter.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.lblAfter.Text = "Số dư sau giao dịch:  13.500.000 ₫";
            this.lblAfter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblAfter.UseMnemonic = false;
            this.lblAfter.Name = "lblAfter";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSubmit.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnSubmit.Icon = ATM.UI.IconKind.Withdraw;
            this.btnSubmit.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.btnSubmit.TabIndex = 1;
            this.btnSubmit.Text = "Rút tiền";
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // pnlRight
            // 
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Margin = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.pnlRight.Padding = new System.Windows.Forms.Padding(14, 14, 14, 14);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Controls.Add(this.numPad);
            // 
            // numPad
            // 
            this.numPad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numPad.Name = "numPad";
            this.numPad.KeyPressed += new ATM.UI.NumPadKeyEventHandler(this.numPad_KeyPressed);
            // 
            // pageHeader
            // 
            this.pageHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pageHeader.MaxContentWidth = 980;
            this.pageHeader.Size = new System.Drawing.Size(1100, 90);
            this.pageHeader.Subtitle = "Nhập số tiền cần rút";
            this.pageHeader.Title = "Rút tiền";
            this.pageHeader.Name = "pageHeader";
            // 
            // AmountPage
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(10, 16, 30);
            this.Size = new System.Drawing.Size(1100, 640);
            this.Name = "AmountPage";
            this.Controls.Add(this.pagePanel);
            this.Controls.Add(this.pageHeader);
            this.ResumeLayout(false);
            this.pnlRight.ResumeLayout(false);
            this.tblChips.ResumeLayout(false);
            this.tblLeft.ResumeLayout(false);
            this.pnlLeft.ResumeLayout(false);
            this.tblAmount.ResumeLayout(false);
            this.pagePanel.ResumeLayout(false);
            this.tblChips.PerformLayout();
            this.tblLeft.PerformLayout();
            this.tblAmount.PerformLayout();
            this.PerformLayout();

        }

        #endregion

        private ATM.UI.PagePanel pagePanel;
        private System.Windows.Forms.TableLayoutPanel tblAmount;
        private ATM.UI.RoundedPanel pnlLeft;
        private System.Windows.Forms.TableLayoutPanel tblLeft;
        private ATM.UI.ModernTextBox txtAmount;
        private System.Windows.Forms.Label lblPreview;
        private System.Windows.Forms.Label lblHint;
        private System.Windows.Forms.TableLayoutPanel tblChips;
        private ATM.UI.RoundedButton chip1;
        private ATM.UI.RoundedButton chip2;
        private ATM.UI.RoundedButton chip3;
        private ATM.UI.RoundedButton chip4;
        private System.Windows.Forms.Label lblAfter;
        private ATM.UI.RoundedButton btnSubmit;
        private ATM.UI.RoundedPanel pnlRight;
        private ATM.UI.NumPad numPad;
        private ATM.UI.PageHeader pageHeader;
    }
}
