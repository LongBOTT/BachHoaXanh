﻿using BachHoaXanh.Models;
using Guna.UI2.WinForms;

namespace BachHoaXanh.Views.InterfaceView
{
    public interface IUpdateSupplierView
    {
        bool IsSuccessful { get; set; }
        string Message { get; set; }
        Guna2TextBox Guna2TextBoxID { get; }
        Guna2TextBox Guna2TextBoxName { get; }
        Guna2TextBox Guna2TextBoxPhone { get; }
        Guna2TextBox Guna2TextBoxAddress { get; }
        Guna2TextBox Guna2TextBoxEmail { get; }
        Supplier GetSupplier { get; }
        event EventHandler ShowDetail;
        event EventHandler UpdateSupplier;
        event EventHandler Refresh;
        void show();
        void close();
    }
}
