using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Views.InterfaceView
{
    public interface IBillView
    {
        string SearchValue { get;}
        string Attribute { get;}
        Guna2DataGridView Guna2DataGridView { get; }
        Guna2ComboBox Guna2ComboBoxSearch { get; }
        Guna2DateTimePicker Guna2DateTimePickerStart { get; }
        Guna2DateTimePicker Guna2DateTimePickerEnd { get; }
        Guna2TextBox Guna2TextBoxStaffName { get; }


        event EventHandler SearchEvent;
        event EventHandler SortEvent;
        event EventHandler ShowDetail;
    }
}
