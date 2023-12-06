using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BachHoaXanh.Views.InterfaceView
{
    public interface IDiscountView
    {
        Guna2DataGridView guna2DataGridDiscount {  get; }
        Guna2DataGridView guna2DataGridProduct {  get; }

        Guna2DateTimePicker DateTimePickerSearch1 { get; }
        Guna2DateTimePicker DateTimePickerSearch2 { get; }
        Guna2DateTimePicker DateTimePickerStart { get; }
        Guna2DateTimePicker DateTimePickerEnd { get; }

        Guna2TextBox guna2TextBoxID { get; }
        Guna2TextBox guna2TextPerent { get; }

        Guna2GradientButton guna2ButtonAdd { get; }
        Guna2GradientButton guna2ButtonCancel { get; }

        event EventHandler SearchDiscountEvent;
        event EventHandler SearchProductEvent;
        event EventHandler ShowDetail;
        event EventHandler AddProduct;
        event EventHandler AddNewEvent;
        event EventHandler CancelEvent;
    }
}
