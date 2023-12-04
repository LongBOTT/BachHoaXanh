using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Views.InterfaceView
{
    public interface IShipmentView
    {
        Guna2DataGridView Guna2DataGridView { get; set; }

        string SearchValue { get; set; }
        string Attribute { get; set; }
        Guna2TextBox Guna2TextBox { get; set; }

        event EventHandler SearchEvent;
        event EventHandler ShowDetail;

    }
}
