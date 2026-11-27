using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ATM.UI
{
    public enum IconKind
    {
        None, Withdraw, Deposit, Transfer, Wallet, History, Lock, Power, Bolt, Check, Cross, Warning, Info, Server, Shield, Card
    }

    /// <summary>Biểu tượng vẽ bằng GDI+ (không phụ thuộc phông chữ icon nên chạy trên mọi máy Windows).</summary>
    public static class Icons
    {
        public static void Draw(Graphics g, IconKind kind, RectangleF r, Color color, float stroke)
        {
            if (kind == IconKind.None) return;
            SmoothingMode old = g.SmoothingMode;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            float s = Theme.Dpi(g);
            using (var pen = new Pen(color, stroke * s))
            using (var brush = new SolidBrush(color))
            {
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;
                pen.LineJoin = LineJoin.Round;
                Func<float, float, PointF> P = delegate (float x, float y) { return new PointF(r.X + x * r.Width, r.Y + y * r.Height); };
                Action<float, float, float, float> L = delegate (float x1, float y1, float x2, float y2) { g.DrawLine(pen, P(x1, y1), P(x2, y2)); };

                switch (kind)
                {
                    case IconKind.Withdraw:
                        L(0.5f, 0.10f, 0.5f, 0.66f);
                        g.DrawLines(pen, new[] { P(0.28f, 0.46f), P(0.5f, 0.68f), P(0.72f, 0.46f) });
                        g.DrawLines(pen, new[] { P(0.2f, 0.78f), P(0.2f, 0.9f), P(0.8f, 0.9f), P(0.8f, 0.78f) });
                        break;
                    case IconKind.Deposit:
                        L(0.5f, 0.68f, 0.5f, 0.12f);
                        g.DrawLines(pen, new[] { P(0.28f, 0.34f), P(0.5f, 0.12f), P(0.72f, 0.34f) });
                        g.DrawLines(pen, new[] { P(0.2f, 0.78f), P(0.2f, 0.9f), P(0.8f, 0.9f), P(0.8f, 0.78f) });
                        break;
                    case IconKind.Transfer:
                        L(0.12f, 0.33f, 0.88f, 0.33f);
                        g.DrawLines(pen, new[] { P(0.68f, 0.15f), P(0.88f, 0.33f), P(0.68f, 0.51f) });
                        L(0.88f, 0.70f, 0.12f, 0.70f);
                        g.DrawLines(pen, new[] { P(0.32f, 0.52f), P(0.12f, 0.70f), P(0.32f, 0.88f) });
                        break;
                    case IconKind.Wallet:
                        using (GraphicsPath p = Theme.RoundRect(new RectangleF(r.X + r.Width * 0.08f, r.Y + r.Height * 0.22f, r.Width * 0.84f, r.Height * 0.58f), r.Width * 0.12f))
                            g.DrawPath(pen, p);
                        L(0.08f, 0.40f, 0.92f, 0.40f);
                        g.FillEllipse(brush, r.X + r.Width * 0.68f, r.Y + r.Height * 0.52f, r.Width * 0.1f, r.Width * 0.1f);
                        break;
                    case IconKind.Card:
                        using (GraphicsPath p = Theme.RoundRect(new RectangleF(r.X + r.Width * 0.06f, r.Y + r.Height * 0.2f, r.Width * 0.88f, r.Height * 0.6f), r.Width * 0.1f))
                            g.DrawPath(pen, p);
                        L(0.06f, 0.42f, 0.94f, 0.42f);
                        L(0.18f, 0.64f, 0.42f, 0.64f);
                        break;
                    case IconKind.History:
                        g.DrawEllipse(pen, r.X + r.Width * 0.1f, r.Y + r.Height * 0.1f, r.Width * 0.8f, r.Height * 0.8f);
                        g.DrawLines(pen, new[] { P(0.5f, 0.28f), P(0.5f, 0.5f), P(0.67f, 0.62f) });
                        break;
                    case IconKind.Lock:
                        using (GraphicsPath p = Theme.RoundRect(new RectangleF(r.X + r.Width * 0.2f, r.Y + r.Height * 0.46f, r.Width * 0.6f, r.Height * 0.42f), r.Width * 0.08f))
                            g.DrawPath(pen, p);
                        g.DrawArc(pen, r.X + r.Width * 0.3f, r.Y + r.Height * 0.1f, r.Width * 0.4f, r.Height * 0.7f, 180, 180);
                        L(0.5f, 0.62f, 0.5f, 0.72f);
                        break;
                    case IconKind.Power:
                        g.DrawArc(pen, r.X + r.Width * 0.14f, r.Y + r.Height * 0.16f, r.Width * 0.72f, r.Height * 0.72f, -60, 300);
                        L(0.5f, 0.08f, 0.5f, 0.50f);
                        break;
                    case IconKind.Bolt:
                        g.FillPolygon(brush, new[] { P(0.58f, 0.06f), P(0.22f, 0.56f), P(0.46f, 0.56f), P(0.40f, 0.94f), P(0.78f, 0.42f), P(0.54f, 0.42f) });
                        break;
                    case IconKind.Check:
                        g.DrawLines(pen, new[] { P(0.2f, 0.52f), P(0.42f, 0.74f), P(0.8f, 0.3f) });
                        break;
                    case IconKind.Cross:
                        L(0.25f, 0.25f, 0.75f, 0.75f);
                        L(0.75f, 0.25f, 0.25f, 0.75f);
                        break;
                    case IconKind.Warning:
                        g.DrawPolygon(pen, new[] { P(0.5f, 0.12f), P(0.92f, 0.85f), P(0.08f, 0.85f) });
                        L(0.5f, 0.42f, 0.5f, 0.62f);
                        g.FillEllipse(brush, r.X + r.Width * 0.5f - 1.6f * s, r.Y + r.Height * 0.71f, 3.2f * s, 3.2f * s);
                        break;
                    case IconKind.Info:
                        g.DrawEllipse(pen, r.X + r.Width * 0.1f, r.Y + r.Height * 0.1f, r.Width * 0.8f, r.Height * 0.8f);
                        L(0.5f, 0.46f, 0.5f, 0.72f);
                        g.FillEllipse(brush, r.X + r.Width * 0.5f - 1.6f * s, r.Y + r.Height * 0.27f, 3.2f * s, 3.2f * s);
                        break;
                    case IconKind.Server:
                        using (GraphicsPath p1 = Theme.RoundRect(new RectangleF(r.X + r.Width * 0.12f, r.Y + r.Height * 0.14f, r.Width * 0.76f, r.Height * 0.3f), r.Width * 0.06f))
                            g.DrawPath(pen, p1);
                        using (GraphicsPath p2 = Theme.RoundRect(new RectangleF(r.X + r.Width * 0.12f, r.Y + r.Height * 0.56f, r.Width * 0.76f, r.Height * 0.3f), r.Width * 0.06f))
                            g.DrawPath(pen, p2);
                        g.FillEllipse(brush, r.X + r.Width * 0.24f, r.Y + r.Height * 0.25f, 3.6f * s, 3.6f * s);
                        g.FillEllipse(brush, r.X + r.Width * 0.24f, r.Y + r.Height * 0.67f, 3.6f * s, 3.6f * s);
                        break;
                    case IconKind.Shield:
                        g.DrawPolygon(pen, new[] { P(0.5f, 0.08f), P(0.86f, 0.22f), P(0.86f, 0.52f), P(0.5f, 0.92f), P(0.14f, 0.52f), P(0.14f, 0.22f) });
                        g.DrawLines(pen, new[] { P(0.34f, 0.5f), P(0.46f, 0.62f), P(0.68f, 0.38f) });
                        break;
                }
            }
            g.SmoothingMode = old;
        }
    }
}
