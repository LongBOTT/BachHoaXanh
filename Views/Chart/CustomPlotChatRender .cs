using BachHoaXanh.Views.Chart.BlankChart;

namespace BachHoaXanh.Views.Chart
{
    public class CustomPlotChatRender : BlankPlotChatRender
    {
        private List<ModelLegend> legends;
        private List<ModelChart> model;
        private int seriesSize;
        private int seriesSpace;
        private float animate;

        // Constructor that takes the necessary parameters
        public CustomPlotChatRender(List<ModelLegend> legends, List<ModelChart> model, int seriesSize, int seriesSpace, float animate)
        {
            this.legends = legends;
            this.model = model;
            this.seriesSize = seriesSize;
            this.seriesSpace = seriesSpace;
            this.animate = animate;
        }
        public override string GetLabelText(int index)
        {
            return model[index].Label;
        }

        public override void RenderSeries(BlankPlotChart chart, Graphics g2, SeriesSize size, int index)
        {
            double totalSeriesWidth = (seriesSize * legends.Count) + (seriesSpace * (legends.Count - 1));
            double x = (size.Width - totalSeriesWidth) / 2;
            for (int i = 0; i < legends.Count; i++)
            {
                ModelLegend legend = legends[i];
                using (SolidBrush brush = new SolidBrush(legend.Color))
                {
                    double seriesValues = chart.GetSeriesValuesOf(model[index].Values[i], size.Height) * animate;
                    float rectX = (float)(size.X + x);
                    float rectY = (float)(size.Y + size.Height - seriesValues);
                    float rectWidth = seriesSize;
                    float rectHeight = (float)seriesValues;
                    g2.FillRectangle(brush, rectX, rectY, rectWidth, rectHeight);
                }
                x += seriesSpace + seriesSize;
            }
        }
    }
}
