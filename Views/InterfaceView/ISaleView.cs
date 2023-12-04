using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Views.InterfaceView
{
    public interface ISaleView
    {
        FlowLayoutPanel ContainerProduct { get; }
        FlowLayoutPanel ContainerProductInBill { get; }
        Guna2TextBox Guna2TextBoxId {  get; }
        Guna2TextBox Guna2TextBoxName { get; }
        Guna2TextBox Guna2TextBoxBrand { get; }
        Guna2TextBox Guna2TextBoxCategory { get; }
        Guna2TextBox Guna2TextBoxCost { get; }
        Guna2TextBox Guna2TextBoxBarcode { get; }
        Guna2TextBox Guna2TextBoxQuantity { get; }
        Guna2TextBox Guna2TextBoxSearch { get; }
        Guna2TextBox Guna2TextBoxStaffName { get; }
        Guna2TextBox Guna2TextBoxDate { get; }
        Guna2TextBox Guna2TextBoxTotal { get; }
        Guna2TextBox Guna2TextBoxReceived { get; }
        Guna2TextBox Guna2TextBoxExcess { get; }

        Guna2GradientButton Guna2GradientButtonAddProduct { get; }
        Guna2GradientButton Guna2GradientButtonDeleteProduct { get; }
        Guna2GradientButton Guna2GradientButtonConfirm { get; }
        Guna2GradientButton Guna2GradientButtonSearch { get; }
        Guna2GradientButton Guna2GradientButtonCancel { get; }
        Guna2GradientButton Guna2GradientButtonPay { get; }

        Guna2DataGridView Guna2DataGridView {  get; }
        event EventHandler LoadProduct;
    }
}
