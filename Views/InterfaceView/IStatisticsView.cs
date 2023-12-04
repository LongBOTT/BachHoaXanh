using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Views.InterfaceView
{
    public interface IStatisticsView
    {
        TabPage TabPageGenneral { get; }
        TabPage TabPageByYear { get; }
        TabPage TabPageByQuater { get; }
        TabPage TabPageByMonth { get; }
    }
}
