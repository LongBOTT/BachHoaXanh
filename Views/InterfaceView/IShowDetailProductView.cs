using BachHoaXanh.Models;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Views.InterfaceView
{
    public interface IShowDetailProductView
    {
        Guna2TextBox Guna2TextBoxID { get; }
        Guna2TextBox Guna2TextBoxname { get; }
        Guna2TextBox Guna2TextBoxBrand { get; }
        Guna2TextBox Guna2TextBoxCategory { get; }
        Guna2TextBox Guna2TextBoxUnit { get; }
        Guna2TextBox Guna2TextBoxCost { get; }
        Guna2TextBox Guna2TextBoxQuantity { get; }
        Guna2TextBox Guna2TextBoxBarcode { get; }
        Guna2DataGridView Guna2DataGridView { get; set; }
        Guna2PictureBox pictureBox { get; }
        Product GetProduct { get; }
        event EventHandler ShowDetail;
        event EventHandler LoadListBrand;
        event EventHandler LoadListCategory;
        void show();
    }
}
