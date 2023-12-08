using BachHoaXanh.Views.Chart.BlankChart;
using System;
using System.Drawing;

namespace BachHoaXanh.Views.Chart.BlankChart
{ 
    public abstract class BlankPlotChatRender
    {
        public abstract string GetLabelText(int index);

        public abstract void RenderSeries(BlankPlotChart chart, Graphics g2, SeriesSize size, int index);
    }
}
