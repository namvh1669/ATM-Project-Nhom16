namespace ATM.Client.Forms
{
    partial class SplashForm
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
            this.logo = new ATM.Client.Forms.LogoMark();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSub = new System.Windows.Forms.Label();
            this.progress = new ATM.UI.GradientProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // logo
            // 
            this.logo.Location = new System.Drawing.Point(296, 62);
            this.logo.Size = new System.Drawing.Size(88, 88);
            this.logo.Name = "logo";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = false;
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 166);
            this.lblTitle.Size = new System.Drawing.Size(680, 54);
            this.lblTitle.Text = "ATM BANKING";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTitle.UseMnemonic = false;
            this.lblTitle.Name = "lblTitle";
            // 
            // lblSub
            // 
            this.lblSub.AutoSize = false;
            this.lblSub.BackColor = System.Drawing.Color.Transparent;
            this.lblSub.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSub.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.lblSub.Location = new System.Drawing.Point(0, 220);
            this.lblSub.Size = new System.Drawing.Size(680, 28);
            this.lblSub.Text = "Hệ thống quản lý & giao dịch ATM  •  Client – Server – Load Balancer";
            this.lblSub.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSub.UseMnemonic = false;
            this.lblSub.Name = "lblSub";
            // 
            // progress
            // 
            this.progress.Location = new System.Drawing.Point(110, 310);
            this.progress.Size = new System.Drawing.Size(460, 8);
            this.progress.Name = "progress";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = false;
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.lblStatus.Location = new System.Drawing.Point(0, 328);
            this.lblStatus.Size = new System.Drawing.Size(680, 26);
            this.lblStatus.Text = "Đang khởi tạo ứng dụng...";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStatus.UseMnemonic = false;
            this.lblStatus.Name = "lblStatus";
            // 
            // SplashForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(10, 16, 30);
            this.ClientSize = new System.Drawing.Size(680, 400);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ATM Banking";
            this.Name = "SplashForm";
            this.Controls.Add(this.logo);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblSub);
            this.Controls.Add(this.progress);
            this.Controls.Add(this.lblStatus);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ATM.Client.Forms.LogoMark logo;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSub;
        private ATM.UI.GradientProgressBar progress;
        private System.Windows.Forms.Label lblStatus;
    }
}
