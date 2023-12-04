using BachHoaXanh.Views.InterfaceView;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BachHoaXanh
{
    public partial class StatisticsControl : UserControl, IStatisticsView
    {
        public StatisticsControl()
        {
            InitializeComponent();
        }

        public TabPage TabPageGenneral { get { return tabPage1; } }

        public TabPage TabPageByYear { get { return tabPage2; } }

        public TabPage TabPageByQuater { get { return tabPage3; } }

        public TabPage TabPageByMonth { get { return tabPage4; } }
    }
}
