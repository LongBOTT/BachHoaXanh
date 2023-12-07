using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Views.Chart
{
    public class LabelColor : Label
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            int width = Width;
            int height = Height;
            int size = Math.Min(width, height) - 4;
            int x = (width - size) / 2;
            int y = (height - size) / 2;
            using (SolidBrush brush = new SolidBrush(BackColor))
            {
                g.FillEllipse(brush, x, y, size, size);
            }
        }
    }
}
