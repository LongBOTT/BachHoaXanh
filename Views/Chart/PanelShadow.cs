using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Views.Chart
{
    public class PanelShadow : Panel
    {
        private Bitmap renderImage;
        private ShadowType shadowType = ShadowType.CENTER;
        private int shadowSize = 6;
        private float shadowOpacity = 0.5f;
        private Color shadowColor = Color.Black;
        private GradientType gradientType = GradientType.HORIZONTAL;
        private Color colorGradient = Color.FromArgb(255, 255, 255);
        private int radius;

        public PanelShadow()
        {
            SetStyle(ControlStyles.Opaque, false);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.White;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (renderImage == null)
            {
                CreateRenderImage();
            }
            e.Graphics.DrawImage(renderImage, 0, 0);
            base.OnPaint(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            CreateRenderImage();
        }

        private void CreateRenderImage()
        {
            renderImage = new Bitmap(Width, Height);
            using (Graphics g2 = Graphics.FromImage(renderImage))
            {
                int size = shadowSize * 2;
                int x, y;
                int width = Width - size;
                int height = Height - size;
                if (shadowType == ShadowType.TOP)
                {
                    x = shadowSize;
                    y = size;
                }
                else if (shadowType == ShadowType.BOT)
                {
                    x = shadowSize;
                    y = 0;
                }
                else if (shadowType == ShadowType.TOP_LEFT)
                {
                    x = size;
                    y = size;
                }
                else if (shadowType == ShadowType.TOP_RIGHT)
                {
                    x = 0;
                    y = size;
                }
                else if (shadowType == ShadowType.BOT_LEFT)
                {
                    x = size;
                    y = 0;
                }
                else if (shadowType == ShadowType.BOT_RIGHT)
                {
                    x = 0;
                    y = 0;
                }
                else
                {
                    x = shadowSize;
                    y = shadowSize;
                }
                Bitmap img = new Bitmap(width, height);
                using (Graphics g = Graphics.FromImage(img))
                {
                    CreateBackground(g, width, height);
                    ShadowRenderer render = new ShadowRenderer(shadowSize, shadowOpacity, shadowColor);
                    g2.DrawImage(render.CreateShadow(img), 0, 0);
                    g2.DrawImage(img, x, y);
                }
            }
        }

        private void CreateBackground(Graphics g2, int width, int height)
        {
            g2.Clear(BackColor);
            int x1, x2, y1, y2;
            if (gradientType == GradientType.HORIZONTAL || gradientType == null)
            {
                x1 = 0;
                y1 = 0;
                x2 = width;
                y2 = 0;
            }
            else if (gradientType == GradientType.VERTICAL)
            {
                x1 = 0;
                y1 = 0;
                x2 = 0;
                y2 = height;
            }
            else if (gradientType == GradientType.DIAGONAL_1)
            {
                x1 = 0;
                y1 = height;
                x2 = width;
                y2 = 0;
            }
            else
            {
                x1 = 0;
                y1 = 0;
                x2 = width;
                y2 = height;
            }
            Point p1 = new Point(x1, y1);
            Point p2 = new Point(x2, y2);
            using (LinearGradientBrush brush = new LinearGradientBrush(p1, p2, BackColor, colorGradient))
            {
                using (GraphicsPath path = CreateRoundedRectanglePath(new Rectangle(0, 0, width, height), radius))
                {
                    g2.FillPath(brush, path);
                }
            }
        }

        private GraphicsPath CreateRoundedRectanglePath(Rectangle bounds, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;
            Rectangle arc = new Rectangle(bounds.Location, new Size(diameter, diameter));
            path.AddArc(arc, 180, 90);
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);
            path.CloseFigure();
            return path;
        }

        public Color ColorGradient
        {
            get { return colorGradient; }
            set
            {
                colorGradient = value;
                Invalidate();
            }
        }

        public int Radius
        {
            get { return radius; }
            set
            {
                radius = value;
                Invalidate();
            }
        }

        public enum GradientType
        {
            VERTICAL, HORIZONTAL, DIAGONAL_1, DIAGONAL_2
        }

        public enum ShadowType
        {
            CENTER, TOP_RIGHT, TOP_LEFT, BOT_RIGHT, BOT_LEFT, BOT, TOP
        }
    }

}
