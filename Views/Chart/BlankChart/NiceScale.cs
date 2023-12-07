using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Views.Chart.BlankChart
{
    public class NiceScale
    {
        private double min;
        private double max;
        private int maxTicks = 10;
        private double tickSpacing;
        private double range;
        private double niceMin;
        private double niceMax;

        public NiceScale(double min, double max)
        {
            this.min = min;
            this.max = max;
            Calculate();
        }

        private void Calculate()
        {
            range = NiceNum(max - min, false);
            tickSpacing = NiceNum(range / (maxTicks - 1), true);
            niceMin = Math.Floor(min / tickSpacing) * tickSpacing;
            niceMax = Math.Ceiling(max / tickSpacing) * tickSpacing;
        }

        private double NiceNum(double range, bool round)
        {
            double exponent;
            double fraction;
            double niceFraction;

            exponent = Math.Floor(Math.Log10(range));
            fraction = range / Math.Pow(10, exponent);

            if (round)
            {
                if (fraction < 1.5)
                {
                    niceFraction = 1;
                }
                else if (fraction < 3)
                {
                    niceFraction = 2;
                }
                else if (fraction < 7)
                {
                    niceFraction = 5;
                }
                else
                {
                    niceFraction = 10;
                }
            }
            else
            {
                if (fraction <= 1)
                {
                    niceFraction = 1;
                }
                else if (fraction <= 2)
                {
                    niceFraction = 2;
                }
                else if (fraction <= 5)
                {
                    niceFraction = 5;
                }
                else
                {
                    niceFraction = 10;
                }
            }

            return niceFraction * Math.Pow(10, exponent);
        }

        public void SetMinMax(double min, double max)
        {
            this.min = min;
            this.max = max;
            Calculate();
        }

        public void SetMaxTicks(int maxTicks)
        {
            this.maxTicks = maxTicks;
            Calculate();
        }

        public double GetTickSpacing()
        {
            return tickSpacing;
        }

        public double GetNiceMin()
        {
            return niceMin;
        }

        public double GetNiceMax()
        {
            return niceMax;
        }

        public int GetMaxTicks()
        {
            return maxTicks;
        }

        public double GetMin()
        {
            return min;
        }

        public void SetMin(double min)
        {
            this.min = min;
            Calculate();
        }

        public double GetMax()
        {
            return max;
        }

        public void SetMax(double max)
        {
            this.max = max;
            Calculate();
        }
    }
}
