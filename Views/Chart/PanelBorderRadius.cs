using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace BachHoaXanh.Views.Chart
{
    public class PanelBorderRadius : Panel
    {
        private int shadowSize = 3;
        private Color HoverBackgroundColor = Color.FromArgb(187, 222, 251);

        public PanelBorderRadius()
        {
            SetStyle(ControlStyles.Opaque, false);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            CreateShadow(e.Graphics);
            base.OnPaint(e);
        }

        private void CreateShadow(Graphics grphcs)
        {
            int size = shadowSize * 2;
            int x = 0;
            int y = 0;
            int width = Width - size;
            int height = Height - size;
            Bitmap img = new Bitmap(width, height);

            using (Graphics g = Graphics.FromImage(img))
            {
                g.Clear(HoverBackgroundColor);
                using (Brush brush = new SolidBrush(HoverBackgroundColor))
                {
                    FillRoundRectangle(g, brush, 0, 0, width, height, 15, 15);
                }
            }

            grphcs.DrawImage(img, x, y);
        }

        private void FillRoundRectangle(Graphics g, Brush brush, float x, float y, float width, float height, float radiusX, float radiusY)
        {
            g.FillEllipse(brush, x, y, radiusX * 2, radiusY * 2);
            g.FillEllipse(brush, x + width - 2 * radiusX, y, radiusX * 2, radiusY * 2);
            g.FillEllipse(brush, x, y + height - 2 * radiusY, radiusX * 2, radiusY * 2);
            g.FillEllipse(brush, x + width - 2 * radiusX, y + height - 2 * radiusY, radiusX * 2, radiusY * 2);

            g.FillRectangle(brush, x + radiusX, y, width - 2 * radiusX, height);
            g.FillRectangle(brush, x, y + radiusY, width, height - 2 * radiusY);
        }

    }

}
