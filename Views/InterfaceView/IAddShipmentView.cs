using BachHoaXanh.Models;
using Guna.UI2.WinForms;

namespace BachHoaXanh.Views.InterfaceView
{
    public interface IAddShipmentView
    {
        Guna2TextBox Guna2TextBoxProductID { get; }
        Guna2TextBox Guna2TextBoxUnitPRice { get; }
        Guna2TextBox Guna2TextBoxQuantity { get; }
        Guna2DateTimePicker Guna2DateTimePickerMfg { get; }
        Guna2DateTimePicker Guna2DateTimePickerExp { get; }
        Guna2TextBox Guna2TextBoxSku { get; }
        Guna2DataGridView Guna2DataGridView { get; }
        Guna2Button Guna2ButtonAdd {  get; }
        Guna2Button Guna2ButtonConfirm {  get; }
        event EventHandler ShowDetail;
        void show(); void close();
        event EventHandler AddShipment;
        event EventHandler ConfirmShipment;
    }
}
