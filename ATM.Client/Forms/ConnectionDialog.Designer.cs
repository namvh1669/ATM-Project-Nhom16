namespace ATM.Client.Forms
{
    partial class ConnectionDialog
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblDesc = new System.Windows.Forms.Label();
            this.txtHost = new ATM.UI.ModernTextBox();
            this.txtPort = new ATM.UI.ModernTextBox();
            this.pnlHint = new ATM.UI.RoundedPanel();
            this.lblHintKey1 = new System.Windows.Forms.Label();
            this.lblHintVal1 = new System.Windows.Forms.Label();
            this.lblHintKey2 = new System.Windows.Forms.Label();
            this.lblHintVal2 = new System.Windows.Forms.Label();
            this.lblHintKey3 = new System.Windows.Forms.Label();
            this.lblHintVal3 = new System.Windows.Forms.Label();
            this.lblResult = new System.Windows.Forms.Label();
            this.btnCancel = new ATM.UI.RoundedButton();
            this.btnTest = new ATM.UI.RoundedButton();
            this.btnSave = new ATM.UI.RoundedButton();
            this.pnlHint.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = false;
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.lblTitle.Location = new System.Drawing.Point(30, 26);
            this.lblTitle.Size = new System.Drawing.Size(500, 34);
            this.lblTitle.Text = "Thiết lập kết nối máy chủ";
            this.lblTitle.UseMnemonic = false;
            this.lblTitle.Name = "lblTitle";
            // 
            // lblDesc
            // 
            this.lblDesc.AutoSize = false;
            this.lblDesc.BackColor = System.Drawing.Color.Transparent;
            this.lblDesc.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblDesc.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.lblDesc.Location = new System.Drawing.Point(30, 62);
            this.lblDesc.Size = new System.Drawing.Size(500, 24);
            this.lblDesc.Text = "Client kết nối tới Load Balancer; Load Balancer sẽ chọn ATM Server phù hợp.";
            this.lblDesc.UseMnemonic = false;
            this.lblDesc.Name = "lblDesc";
            // 
            // txtHost
            // 
            this.txtHost.Caption = "Địa chỉ Load Balancer (IP hoặc tên miền)";
            this.txtHost.Location = new System.Drawing.Point(30, 98);
            this.txtHost.Size = new System.Drawing.Size(358, 76);
            this.txtHost.TabIndex = 0;
            this.txtHost.Name = "txtHost";
            // 
            // txtPort
            // 
            this.txtPort.Caption = "Cổng";
            this.txtPort.Location = new System.Drawing.Point(400, 98);
            this.txtPort.MaxLength = 5;
            this.txtPort.NumbersOnly = true;
            this.txtPort.Size = new System.Drawing.Size(130, 76);
            this.txtPort.TabIndex = 1;
            this.txtPort.Name = "txtPort";
            // 
            // pnlHint
            // 
            this.pnlHint.FillColor = System.Drawing.Color.FromArgb(30, 42, 68);
            this.pnlHint.Location = new System.Drawing.Point(30, 186);
            this.pnlHint.Radius = 12;
            this.pnlHint.Size = new System.Drawing.Size(500, 112);
            this.pnlHint.Name = "pnlHint";
            this.pnlHint.Controls.Add(this.lblHintKey1);
            this.pnlHint.Controls.Add(this.lblHintVal1);
            this.pnlHint.Controls.Add(this.lblHintKey2);
            this.pnlHint.Controls.Add(this.lblHintVal2);
            this.pnlHint.Controls.Add(this.lblHintKey3);
            this.pnlHint.Controls.Add(this.lblHintVal3);
            // 
            // lblHintKey1
            // 
            this.lblHintKey1.AutoSize = false;
            this.lblHintKey1.BackColor = System.Drawing.Color.Transparent;
            this.lblHintKey1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblHintKey1.ForeColor = System.Drawing.Color.FromArgb(56, 189, 248);
            this.lblHintKey1.Location = new System.Drawing.Point(14, 10);
            this.lblHintKey1.Size = new System.Drawing.Size(80, 30);
            this.lblHintKey1.Text = "Cùng máy";
            this.lblHintKey1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblHintKey1.UseMnemonic = false;
            this.lblHintKey1.Name = "lblHintKey1";
            // 
            // lblHintVal1
            // 
            this.lblHintVal1.AutoSize = false;
            this.lblHintVal1.AutoEllipsis = true;
            this.lblHintVal1.BackColor = System.Drawing.Color.Transparent;
            this.lblHintVal1.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblHintVal1.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.lblHintVal1.Location = new System.Drawing.Point(96, 10);
            this.lblHintVal1.Size = new System.Drawing.Size(390, 30);
            this.lblHintVal1.Text = "127.0.0.1";
            this.lblHintVal1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblHintVal1.UseMnemonic = false;
            this.lblHintVal1.Name = "lblHintVal1";
            // 
            // lblHintKey2
            // 
            this.lblHintKey2.AutoSize = false;
            this.lblHintKey2.BackColor = System.Drawing.Color.Transparent;
            this.lblHintKey2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblHintKey2.ForeColor = System.Drawing.Color.FromArgb(56, 189, 248);
            this.lblHintKey2.Location = new System.Drawing.Point(14, 40);
            this.lblHintKey2.Size = new System.Drawing.Size(80, 30);
            this.lblHintKey2.Text = "Mạng LAN";
            this.lblHintKey2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblHintKey2.UseMnemonic = false;
            this.lblHintKey2.Name = "lblHintKey2";
            // 
            // lblHintVal2
            // 
            this.lblHintVal2.AutoSize = false;
            this.lblHintVal2.AutoEllipsis = true;
            this.lblHintVal2.BackColor = System.Drawing.Color.Transparent;
            this.lblHintVal2.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblHintVal2.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.lblHintVal2.Location = new System.Drawing.Point(96, 40);
            this.lblHintVal2.Size = new System.Drawing.Size(390, 30);
            this.lblHintVal2.Text = "IP LAN của máy chạy Load Balancer, ví dụ 192.168.1.10";
            this.lblHintVal2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblHintVal2.UseMnemonic = false;
            this.lblHintVal2.Name = "lblHintVal2";
            // 
            // lblHintKey3
            // 
            this.lblHintKey3.AutoSize = false;
            this.lblHintKey3.BackColor = System.Drawing.Color.Transparent;
            this.lblHintKey3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblHintKey3.ForeColor = System.Drawing.Color.FromArgb(56, 189, 248);
            this.lblHintKey3.Location = new System.Drawing.Point(14, 70);
            this.lblHintKey3.Size = new System.Drawing.Size(80, 30);
            this.lblHintKey3.Text = "Internet";
            this.lblHintKey3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblHintKey3.UseMnemonic = false;
            this.lblHintKey3.Name = "lblHintKey3";
            // 
            // lblHintVal3
            // 
            this.lblHintVal3.AutoSize = false;
            this.lblHintVal3.AutoEllipsis = true;
            this.lblHintVal3.BackColor = System.Drawing.Color.Transparent;
            this.lblHintVal3.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblHintVal3.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.lblHintVal3.Location = new System.Drawing.Point(96, 70);
            this.lblHintVal3.Size = new System.Drawing.Size(390, 30);
            this.lblHintVal3.Text = "IP công cộng / tên miền (đã mở cổng) hoặc địa chỉ tunnel";
            this.lblHintVal3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblHintVal3.UseMnemonic = false;
            this.lblHintVal3.Name = "lblHintVal3";
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = false;
            this.lblResult.BackColor = System.Drawing.Color.Transparent;
            this.lblResult.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblResult.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.lblResult.Location = new System.Drawing.Point(30, 310);
            this.lblResult.Size = new System.Drawing.Size(500, 30);
            this.lblResult.Text = "";
            this.lblResult.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblResult.UseMnemonic = false;
            this.lblResult.Name = "lblResult";
            // 
            // btnCancel
            // 
            this.btnCancel.Kind = ATM.UI.ButtonKind.Ghost;
            this.btnCancel.Location = new System.Drawing.Point(30, 350);
            this.btnCancel.Size = new System.Drawing.Size(158, 48);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            // 
            // btnTest
            // 
            this.btnTest.Icon = ATM.UI.IconKind.Bolt;
            this.btnTest.Kind = ATM.UI.ButtonKind.Secondary;
            this.btnTest.Location = new System.Drawing.Point(200, 350);
            this.btnTest.Size = new System.Drawing.Size(158, 48);
            this.btnTest.TabIndex = 3;
            this.btnTest.Text = "Kiểm tra";
            this.btnTest.Name = "btnTest";
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnSave
            // 
            this.btnSave.Icon = ATM.UI.IconKind.Check;
            this.btnSave.Location = new System.Drawing.Point(370, 350);
            this.btnSave.Size = new System.Drawing.Size(158, 48);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Lưu";
            this.btnSave.Name = "btnSave";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // ConnectionDialog
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(20, 29, 49);
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(560, 426);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thiết lập kết nối máy chủ";
            this.Name = "ConnectionDialog";
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblDesc);
            this.Controls.Add(this.txtHost);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.pnlHint);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.btnSave);
            this.ResumeLayout(false);
            this.pnlHint.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblDesc;
        private ATM.UI.ModernTextBox txtHost;
        private ATM.UI.ModernTextBox txtPort;
        private ATM.UI.RoundedPanel pnlHint;
        private System.Windows.Forms.Label lblHintKey1;
        private System.Windows.Forms.Label lblHintVal1;
        private System.Windows.Forms.Label lblHintKey2;
        private System.Windows.Forms.Label lblHintVal2;
        private System.Windows.Forms.Label lblHintKey3;
        private System.Windows.Forms.Label lblHintVal3;
        private System.Windows.Forms.Label lblResult;
        private ATM.UI.RoundedButton btnCancel;
        private ATM.UI.RoundedButton btnTest;
        private ATM.UI.RoundedButton btnSave;
    }
}
