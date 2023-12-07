using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Views.Chart
{
    public class ModelChart
    {
        public string Label { get; set; }
        public double[] Values { get; set; }

        public ModelChart(string label, double[] values)
        {
            Label = label;
            Values = values;
        }

        public ModelChart()
        {
        }

        public double GetMaxValues()
        {
            double max = 0;
            foreach (double v in Values)
            {
                if (v > max)
                {
                    max = v;
                }
            }
            return max;
        }
    }
}
