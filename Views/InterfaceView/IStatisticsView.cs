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
        BachHoaXanh.Views.Chart.Charts GetChart1 { get; }
        BachHoaXanh.Views.Chart.Charts GetChart2 { get; }
        BachHoaXanh.Views.Chart.Charts GetChart3 { get; }
        Label GetTitle1 { get; }
        Label GetTitle2 { get; }
        Label GetTitle3 { get; }
        Guna2HtmlLabel guna2HtmlLabelNumofStaff { get; }
        Guna2HtmlLabel guna2HtmlLabelBestSeller { get; }
        Guna2HtmlLabel guna2HtmlLabelNameBestSeller { get; }
        Guna2HtmlLabel guna2HtmlLabelBadSeller { get; }
        Guna2HtmlLabel guna2HtmlLabelNameBadSeller { get; }
        Guna2HtmlLabel guna2HtmlLabelBillOfBestStaff { get; }
        Guna2HtmlLabel guna2HtmlLabelNameBestStaff { get; }
        Guna2HtmlLabel guna2HtmlLabelQuantityShipment { get; }
        Guna2HtmlLabel guna2HtmlLabelNameProduct { get; }
        Guna2HtmlLabel guna2HtmlLabelExpenses { get; }
        Guna2HtmlLabel guna2HtmlLabelAmount { get; }
        Guna2HtmlLabel guna2HtmlLabelProfit { get; }

        Guna2PictureBox guna2PictureBoxBestSeller {  get; }
        Guna2PictureBox guna2PictureBoxBadSeller {  get; }
        Guna2PictureBox guna2PictureBoxPoduct {  get; }

        Guna2TabControl guna2TabControl {  get; }

    }
}
