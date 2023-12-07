using BachHoaXanh.Models;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Views.InterfaceView
{
    public interface IShowDetailReceiptView
    {
        Guna2DataGridView Guna2DataGridView { get; }
        Guna2TextBox Guna2TextBoxID { get; }
        Guna2TextBox Guna2TextBoxStaffID { get; }
        Guna2TextBox Guna2TextBoxInvoice { get; }
        Guna2TextBox Guna2TextBoxTotal { get; }
        Guna2TextBox Guna2TextBoxReceived { get; }
        Guna2TextBox Guna2TextBoxExcess { get; }

        Receipt GetReceipt { get; }
        event EventHandler ShowDetail;
        void show();
        event EventHandler LoadListReceiptDetail;
        event EventHandler LoadListStaff;
    }
}
