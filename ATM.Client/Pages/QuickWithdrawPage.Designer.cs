namespace ATM.Client.Pages
{
    partial class QuickWithdrawPage
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
            this.tblTiles = new System.Windows.Forms.TableLayoutPanel();
            this.tile1 = new ATM.Client.Pages.TileButton();
            this.tile2 = new ATM.Client.Pages.TileButton();
            this.tile3 = new ATM.Client.Pages.TileButton();
            this.tile4 = new ATM.Client.Pages.TileButton();
            this.tile5 = new ATM.Client.Pages.TileButton();
            this.tile6 = new ATM.Client.Pages.TileButton();
            this.pageHeader = new ATM.UI.PageHeader();
            this.pagePanel.SuspendLayout();
            this.tblTiles.SuspendLayout();
            this.SuspendLayout();
            // 
            // pagePanel
            // 
            this.pagePanel.AutoScroll = true;
            this.pagePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pagePanel.MaxContentWidth = 900;
            this.pagePanel.MinContentHeight = 380;
            this.pagePanel.Padding = new System.Windows.Forms.Padding(40, 4, 40, 24);
            this.pagePanel.Name = "pagePanel";
            this.pagePanel.Controls.Add(this.tblTiles);
            // 
            // tblTiles
            // 
            this.tblTiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblTiles.Name = "tblTiles";
            this.tblTiles.ColumnCount = 3;
            this.tblTiles.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tblTiles.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tblTiles.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tblTiles.RowCount = 2;
            this.tblTiles.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblTiles.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblTiles.Controls.Add(this.tile1, 0, 0);
            this.tblTiles.Controls.Add(this.tile2, 1, 0);
            this.tblTiles.Controls.Add(this.tile3, 2, 0);
            this.tblTiles.Controls.Add(this.tile4, 0, 1);
            this.tblTiles.Controls.Add(this.tile5, 1, 1);
            this.tblTiles.Controls.Add(this.tile6, 2, 1);
            // 
            // tile1
            // 
            this.tile1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tile1.Icon = ATM.UI.IconKind.Withdraw;
            this.tile1.Margin = new System.Windows.Forms.Padding(0, 6, 12, 6);
            this.tile1.Subtitle = "Nhấn để rút ngay";
            this.tile1.Title = "100.000 ₫";
            this.tile1.TitleSize = 18F;
            this.tile1.Name = "tile1";
            this.tile1.Click += new System.EventHandler(this.tile_Click);
            // 
            // tile2
            // 
            this.tile2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tile2.Icon = ATM.UI.IconKind.Withdraw;
            this.tile2.Margin = new System.Windows.Forms.Padding(0, 6, 12, 6);
            this.tile2.Subtitle = "Nhấn để rút ngay";
            this.tile2.Title = "200.000 ₫";
            this.tile2.TitleSize = 18F;
            this.tile2.Name = "tile2";
            this.tile2.Click += new System.EventHandler(this.tile_Click);
            // 
            // tile3
            // 
            this.tile3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tile3.Icon = ATM.UI.IconKind.Withdraw;
            this.tile3.Margin = new System.Windows.Forms.Padding(0, 6, 0, 6);
            this.tile3.Subtitle = "Nhấn để rút ngay";
            this.tile3.Title = "500.000 ₫";
            this.tile3.TitleSize = 18F;
            this.tile3.Name = "tile3";
            this.tile3.Click += new System.EventHandler(this.tile_Click);
            // 
            // tile4
            // 
            this.tile4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tile4.Icon = ATM.UI.IconKind.Withdraw;
            this.tile4.Margin = new System.Windows.Forms.Padding(0, 6, 12, 6);
            this.tile4.Subtitle = "Nhấn để rút ngay";
            this.tile4.Title = "1.000.000 ₫";
            this.tile4.TitleSize = 18F;
            this.tile4.Name = "tile4";
            this.tile4.Click += new System.EventHandler(this.tile_Click);
            // 
            // tile5
            // 
            this.tile5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tile5.Icon = ATM.UI.IconKind.Withdraw;
            this.tile5.Margin = new System.Windows.Forms.Padding(0, 6, 12, 6);
            this.tile5.Subtitle = "Nhấn để rút ngay";
            this.tile5.Title = "2.000.000 ₫";
            this.tile5.TitleSize = 18F;
            this.tile5.Name = "tile5";
            this.tile5.Click += new System.EventHandler(this.tile_Click);
            // 
            // tile6
            // 
            this.tile6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tile6.Icon = ATM.UI.IconKind.Withdraw;
            this.tile6.Margin = new System.Windows.Forms.Padding(0, 6, 0, 6);
            this.tile6.Subtitle = "Nhấn để rút ngay";
            this.tile6.Title = "5.000.000 ₫";
            this.tile6.TitleSize = 18F;
            this.tile6.Name = "tile6";
            this.tile6.Click += new System.EventHandler(this.tile_Click);
            // 
            // pageHeader
            // 
            this.pageHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pageHeader.MaxContentWidth = 900;
            this.pageHeader.Size = new System.Drawing.Size(1100, 90);
            this.pageHeader.Subtitle = "Chọn mệnh giá";
            this.pageHeader.Title = "Rút tiền nhanh";
            this.pageHeader.Name = "pageHeader";
            // 
            // QuickWithdrawPage
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(10, 16, 30);
            this.Size = new System.Drawing.Size(1100, 640);
            this.Name = "QuickWithdrawPage";
            this.Controls.Add(this.pagePanel);
            this.Controls.Add(this.pageHeader);
            this.ResumeLayout(false);
            this.tblTiles.ResumeLayout(false);
            this.pagePanel.ResumeLayout(false);
            this.tblTiles.PerformLayout();
            this.PerformLayout();

        }

        #endregion

        private ATM.UI.PagePanel pagePanel;
        private System.Windows.Forms.TableLayoutPanel tblTiles;
        private ATM.Client.Pages.TileButton tile1;
        private ATM.Client.Pages.TileButton tile2;
        private ATM.Client.Pages.TileButton tile3;
        private ATM.Client.Pages.TileButton tile4;
        private ATM.Client.Pages.TileButton tile5;
        private ATM.Client.Pages.TileButton tile6;
        private ATM.UI.PageHeader pageHeader;
    }
}
