using BachHoaXanh.Models;
using Guna.UI2.WinForms;

namespace BachHoaXanh.Views.InterfaceView
{
    public interface IShowDetailSupplierView
    {
        Guna2TextBox Guna2TextBoxID { get; }
        Guna2TextBox Guna2TextBoxName { get; }
        Guna2TextBox Guna2TextBoxPhone { get; }
        Guna2TextBox Guna2TextBoxAddress { get; }
        Guna2TextBox Guna2TextBoxEmail { get; }
        Guna2DataGridView Guna2DataGridView { get; }
        Supplier GetSupplier { get; }
        event EventHandler ShowDetail;
        void show();
    }
}
