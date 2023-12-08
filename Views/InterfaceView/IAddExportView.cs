using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Views.InterfaceView
{
    public interface IAddExportView
    {
        bool IsSuccessful { get; set; }
        string Message { get; set; }
        Guna2DataGridView Guna2DataGridView { get; }
        Guna2TextBox Guna2TextBoxID { get; }
        Guna2TextBox Guna2TextBoxStaffID { get; }
        Guna2TextBox Guna2TextBoxInvoice { get; }
        Guna2TextBox Guna2TextBoxTotal { get; }
        Guna2ComboBox Guna2ComboBoxReason { get; }
        Guna2Button Guna2GradientButtonAddProduct { get; }

        event EventHandler ShowDetail;
        event EventHandler SelectedRow;
        event EventHandler AddExport;
        event EventHandler Refresh;
        void show();
        void close();
    }
}
