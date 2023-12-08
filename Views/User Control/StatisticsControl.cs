using BachHoaXanh.Views.Chart;
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

        public Charts GetChart1 { get { return chart1; } }

        public Charts GetChart2 { get { return chart2; } }

        public Charts GetChart3 { get { return chart3; } }

        public TabPage TabPageGenneral { get { return tabPage1; } }

        public TabPage TabPageByYear { get { return tabPage2; } }

        public TabPage TabPageByQuater { get { return tabPage3; } }

        public TabPage TabPageByMonth { get { return tabPage4; } }

        public Guna2HtmlLabel guna2HtmlLabelNumofStaff { get { return labelNumofStaff; } }

        public Guna2HtmlLabel guna2HtmlLabelBestSeller { get { return labelBestSeller; } }

        public Guna2HtmlLabel guna2HtmlLabelNameBestSeller { get { return labelBestSellerName; } }

        public Guna2HtmlLabel guna2HtmlLabelBadSeller { get { return labelBadSeller; } }

        public Guna2HtmlLabel guna2HtmlLabelNameBadSeller { get { return labelBadSellerName; } }

        public Guna2HtmlLabel guna2HtmlLabelBillOfBestStaff { get { return lablelBillOfStaff; } }

        public Guna2HtmlLabel guna2HtmlLabelNameBestStaff { get { return labelBestStaffName; } }

        public Guna2HtmlLabel guna2HtmlLabelQuantityShipment { get { return labelQuantityShipment; } }

        public Guna2HtmlLabel guna2HtmlLabelNameProduct { get { return labelNamePoduct; } }

        public Guna2HtmlLabel guna2HtmlLabelExpenses { get { return labelExpenses; } }

        public Guna2HtmlLabel guna2HtmlLabelAmount { get { return labelAmount; } }

        public Guna2HtmlLabel guna2HtmlLabelProfit { get { return labelBenefit; } }

        public Guna2PictureBox guna2PictureBoxBestSeller { get { return imageBestSeller; } }

        public Guna2PictureBox guna2PictureBoxBadSeller { get { return imageBadSeller; } }

        public Guna2PictureBox guna2PictureBoxPoduct { get { return imageShipment; } }

        public Guna2TabControl guna2TabControl { get { return guna2TabControl1; } }

        private void charts_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
