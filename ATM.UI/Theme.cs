using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ATM.UI
{
    /// <summary>Bảng màu, phông chữ và các hàm vẽ dùng chung (giao diện tối kiểu ngân hàng số).</summary>
    public static class Theme
    {
        public static readonly Color Bg = Color.FromArgb(10, 16, 30);
        public static readonly Color Surface = Color.FromArgb(20, 29, 49);
        public static readonly Color Surface2 = Color.FromArgb(30, 42, 68);
        public static readonly Color Border = Color.FromArgb(44, 58, 90);
        public static readonly Color Text = Color.FromArgb(241, 245, 249);
        public static readonly Color Muted = Color.FromArgb(148, 163, 184);
        public static readonly Color Accent = Color.FromArgb(56, 189, 248);
        public static readonly Color Accent2 = Color.FromArgb(99, 102, 241);
        public static readonly Color Success = Color.FromArgb(34, 197, 94);
        public static readonly Color Danger = Color.FromArgb(239, 68, 68);
        public static readonly Color Warning = Color.FromArgb(245, 158, 11);

        private static readonly Dictionary<string, Font> Fonts = new Dictionary<string, Font>();

        /// <summary>Phông chữ (đơn vị point nên tự co giãn theo DPI), có cache.</summary>
        public static Font F(float size, bool bold)
        {
            string key = size.ToString("0.0") + (bold ? "b" : "r");
            lock (Fonts)
            {
                Font f;
                if (!Fonts.TryGetValue(key, out f))
                {
                    f = new Font(bold ? "Segoe UI Semibold" : "Segoe UI", size, FontStyle.Regular, GraphicsUnit.Point);
                    Fonts[key] = f;
                }
                return f;
            }
        }

        public static Font F(float size)
        {
            return F(size, false);
        }

        public static float Dpi(Graphics g)
        {
            return g.DpiX / 96f;
        }

        public static GraphicsPath RoundRect(RectangleF r, float radius)
        {
            float d = Math.Min(radius * 2f, Math.Min(r.Width, r.Height));
            var p = new GraphicsPath();
            if (d <= 0.5f)
            {
                p.AddRectangle(r);
                return p;
            }
            p.AddArc(r.X, r.Y, d, d, 180, 90);
            p.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            p.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            p.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            p.CloseFigure();
            return p;
        }

        /// <summary>Màu nền thực sự phía sau một control (để vẽ góc bo cho đúng màu).</summary>
        public static Color SurfaceOf(Control c)
        {
            for (Control p = c.Parent; p != null; p = p.Parent)
            {
                var rp = p as RoundedPanel;
                if (rp != null) return rp.FillColor;
                if (p.BackColor != Color.Transparent && p.BackColor.A == 255) return p.BackColor;
            }
            return Bg;
        }

        public static Color WithAlpha(Color c, int alpha)
        {
            return Color.FromArgb(alpha, c.R, c.G, c.B);
        }

        public static Color Lighten(Color c, float amount)
        {
            return Color.FromArgb(c.A,
                (int)Math.Min(255, c.R + (255 - c.R) * amount),
                (int)Math.Min(255, c.G + (255 - c.G) * amount),
                (int)Math.Min(255, c.B + (255 - c.B) * amount));
        }

        /// <summary>Thêm cột văn bản cho DataGridView (tạo tường minh DataGridViewTextBoxColumn).</summary>
        public static DataGridViewColumn AddColumn(DataGridView g, string name, string header)
        {
            var c = new DataGridViewTextBoxColumn();
            c.Name = name;
            c.HeaderText = header;
            c.SortMode = DataGridViewColumnSortMode.NotSortable;
            g.Columns.Add(c);
            return c;
        }

        public static void StyleGrid(DataGridView g)
        {
            g.BorderStyle = BorderStyle.None;
            g.BackgroundColor = Surface;
            g.GridColor = Border;
            g.EnableHeadersVisualStyles = false;
            g.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            g.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            g.ColumnHeadersHeight = 42;
            g.ColumnHeadersDefaultCellStyle.BackColor = Surface2;
            g.ColumnHeadersDefaultCellStyle.ForeColor = Muted;
            g.ColumnHeadersDefaultCellStyle.SelectionBackColor = Surface2;
            g.ColumnHeadersDefaultCellStyle.SelectionForeColor = Muted;
            g.ColumnHeadersDefaultCellStyle.Font = F(9.5f, true);
            g.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 0, 10, 0);
            g.DefaultCellStyle.BackColor = Surface;
            g.DefaultCellStyle.ForeColor = Text;
            g.DefaultCellStyle.SelectionBackColor = Color.FromArgb(38, 54, 88);
            g.DefaultCellStyle.SelectionForeColor = Text;
            g.DefaultCellStyle.Font = F(10f);
            g.DefaultCellStyle.Padding = new Padding(10, 0, 10, 0);
            g.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(24, 34, 57);
            g.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(38, 54, 88);
            g.RowHeadersVisible = false;
            g.AllowUserToAddRows = false;
            g.AllowUserToDeleteRows = false;
            g.AllowUserToResizeRows = false;
            g.ReadOnly = true;
            g.MultiSelect = false;
            g.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            g.RowTemplate.Height = 38;
            g.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            g.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
    }
}
