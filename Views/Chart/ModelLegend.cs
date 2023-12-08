using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Views.Chart
{
    public class ModelLegend
    {
        public string Name { get; set; }
        public Color Color { get; set; }

        public ModelLegend(string name, Color color)
        {
            Name = name;
            Color = color;
        }

        public ModelLegend()
        {
        }
    }
}
