using BachHoaXanh.Models;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Views.InterfaceView
{
    public interface IUpdateAccountView
    {
        bool IsSuccessful { get; set; }
        string Message { get; set; }
        Guna2TextBox Guna2TextBoxID { get; }
        Guna2TextBox Guna2TextBoxUsername { get; }
        Guna2TextBox Guna2TextBoxPassword { get; }
        Guna2TextBox Guna2TextBoxRoleID { get; }
        Guna2TextBox Guna2TextBoxLastSignedIn { get; }
        Guna2TextBox Guna2TextBoxStaffID { get; }
        Guna2DataGridView Guna2DataGridView { get; set; }
        Account GetAccount { get; }
        event EventHandler ShowDetail;
        event EventHandler LoadListRole;
        event EventHandler LoadListStaff;
        event EventHandler SelectedRow;
        event EventHandler UpdateAccount;
        event EventHandler Refresh;
        void show();
        void close();
    }
}
