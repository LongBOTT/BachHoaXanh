namespace BachHoaXanh.Views.Chart.BlankChart
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;
    using System.Globalization;
    public class BlankPlotChart : UserControl
    {
        private NiceScale niceScale;
        private double maxValues;
        private double minValues;
        private int labelCount;
        private string valuesFormat = "#,##0.##";
        private BlankPlotChatRender blankPlotChatRender;
        private NumberFormatInfo numberFormatInfo = new NumberFormatInfo { NumberDecimalDigits = 2 };

        public BlankPlotChatRender BlankPlotChatRender
        {
            get { return blankPlotChatRender; }
            set { blankPlotChatRender = value; }
        }

        public double MaxValues
        {
            get { return maxValues; }
            set
            {
                maxValues = value;
                niceScale.SetMax(maxValues);
                Invalidate();
            }
        }

        public double MinValues
        {
            get { return minValues; }
        }

        public int LabelCount
        {
            get { return labelCount; }
            set { labelCount = value; }
        }

        public string ValuesFormat
        {
            get { return valuesFormat; }
            set
            {
                valuesFormat = value;
                numberFormatInfo.NumberDecimalDigits = valuesFormat.Length - valuesFormat.IndexOf('.') - 1;
            }
        }

        public BlankPlotChart()
        {
            BackColor = Color.White;
            ForeColor = Color.FromArgb(100, 100, 100);
            Padding = new Padding(20, 10, 10, 30);
            Init();
        }

        private void Init()
        {
            InitValues(0, 10);
        }

        public void InitValues(double minValues, double maxValues)
        {
            this.minValues = minValues;
            this.maxValues = maxValues;
            niceScale = new NiceScale(minValues, maxValues);
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (niceScale != null)
            {
                using (Graphics g = e.Graphics)
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    CreateLine(g);
                    CreateValues(g);
                    CreateLabelText(g);
                    RenderSeries(g);
                }
            }
        }

        private void CreateLine(Graphics g)
        {
            using (Pen pen = new Pen(Color.FromArgb(220, 220, 220)))
            {
                Padding insets = Padding;
                double textHeight = GetLabelTextHeight(g);
                double height = Height - (insets.Top + insets.Bottom) - textHeight;
                double space = height / niceScale.GetMaxTicks();
                double locationY = insets.Bottom + textHeight;
                double textWidth = GetMaxValuesTextWidth(g);
                double spaceText = 5;

                for (int i = 0; i <= niceScale.GetMaxTicks(); i++)
                {
                    int y = (int)(Height - locationY);
                    g.DrawLine(pen, (int)(insets.Left + textWidth + spaceText), y, (int)(Width - insets.Right), y);
                    locationY += space;
                }
            }
        }

        private void CreateValues(Graphics g)
        {
            using (Brush brush = new SolidBrush(ForeColor))
            {
                Padding insets = Padding;
                double textHeight = GetLabelTextHeight(g);
                double height = Height - (insets.Top + insets.Bottom) - textHeight;
                double space = height / niceScale.GetMaxTicks();
                double valuesCount = niceScale.GetNiceMin();
                double locationY = insets.Bottom + textHeight;
                Font ft = Font;

                for (int i = 0; i <= niceScale.GetMaxTicks(); i++)
                {
                    string text = valuesCount.ToString(valuesFormat, numberFormatInfo);
                    SizeF size = g.MeasureString(text, ft);
                    double stringY = size.Height / -2;
                    double y = Height - locationY + stringY;
                    g.DrawString(text, ft, brush, insets.Left, (float)y);
                    locationY += space;
                    valuesCount += niceScale.GetTickSpacing();
                }
            }
        }

        private void CreateLabelText(Graphics g)
        {
            if (labelCount > 0)
            {
                using (Brush brush = new SolidBrush(ForeColor))
                {
                    Padding insets = Padding;
                    double textWidth = GetMaxValuesTextWidth(g);
                    double spaceText = 5;
                    double width = Width - insets.Left - insets.Right - textWidth - spaceText;
                    double space = width / labelCount;
                    double locationX = insets.Left + textWidth + spaceText;
                    double locationText = Height - insets.Bottom;

                    for (int i = 0; i < labelCount; i++)
                    {
                        double centerX = ((locationX + space / 2));
                        g.DrawString(GetChartText(i), Font, brush, (float)(centerX - g.MeasureString(GetChartText(i), Font).Width / 2), (float)locationText);
                        locationX += space;
                    }
                }
            }
        }

        private void RenderSeries(Graphics g)
        {
            if (blankPlotChatRender != null)
            {
                Padding insets = Padding;
                double textWidth = GetMaxValuesTextWidth(g);
                double textHeight = GetLabelTextHeight(g);
                double spaceText = 5;
                double width = Width - insets.Left - insets.Right - textWidth - spaceText;
                double height = Height - insets.Top - insets.Bottom - textHeight;
                double space = width / labelCount;
                double locationX = insets.Left + textWidth + spaceText;

                for (int i = 0; i < labelCount; i++)
                {
                    blankPlotChatRender.RenderSeries(this, g, GetRectangle(i, height, space, locationX, insets.Top), i);
                }
            }
        }

        private double GetMaxValuesTextWidth(Graphics g)
        {
            double width = 0;
            double valuesCount = niceScale.GetNiceMin();

            foreach (int i in Enumerable.Range(0, niceScale.GetMaxTicks() + 1))
            {
                string text = valuesCount.ToString(valuesFormat, numberFormatInfo);
                SizeF size = g.MeasureString(text, Font);
                double w = size.Width;

                if (w > width)
                {
                    width = w;
                }

                valuesCount += niceScale.GetTickSpacing();
            }

            return width;
        }

        private int GetLabelTextHeight(Graphics g)
        {
            return (int)g.MeasureString("Sample", Font).Height;
        }

        private string GetChartText(int index)
        {
            return blankPlotChatRender != null ? blankPlotChatRender.GetLabelText(index) : "Label";
        }

        public SeriesSize GetRectangle(int index, double height, double space, double startX, double startY)
        {
            double x = startX + space * index;
            SeriesSize size = new SeriesSize(x, startY + 1, space, height);
            return size;
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            BackColor = Color.White;
            ForeColor = Color.FromArgb(100, 100, 100);
            Name = "BlankPlotChart";
            Padding = new Padding(20, 10, 10, 10);
            Init();
            ResumeLayout(false);
        }

        public double GetSeriesValuesOf(double values, double height)
        {
            double max = niceScale.GetTickSpacing() * niceScale.GetMaxTicks();
            double percentValues = values * 100d / max;
            return height * percentValues / 100d;
        }
    }
}
