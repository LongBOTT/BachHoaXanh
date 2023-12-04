using BachHoaXanh.Models;
using Guna.UI2.WinForms;

namespace BachHoaXanh.Views.InterfaceView
{
    public interface IShowDetailShipmentView
    {
        Guna2TextBox Guna2TextBoxID { get; }
        Guna2TextBox Guna2TextBoxProductID { get; }
        Guna2TextBox Guna2TextBoxUnitPRice { get; }
        Guna2TextBox Guna2TextBoxQuantity { get; }
        Guna2TextBox Guna2TextBoxRemain { get; }
        Guna2TextBox Guna2TextBoxMfg { get; }
        Guna2TextBox Guna2TextBoxExp { get; }
        Guna2TextBox Guna2TextBoxSku { get; }
        Guna2TextBox Guna2TextBoxImportID { get; }
        Guna2DataGridView Guna2DataGridView { get; }
        Shipment GetShipment { get; }
        event EventHandler ShowDetail;
        void show();
        event EventHandler LoadListProduct;
        event EventHandler LoadListImport;
    }
}
