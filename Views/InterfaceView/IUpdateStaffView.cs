using BachHoaXanh.Models;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Views.InterfaceView
{
    public interface IUpdateStaffView
    {
        bool IsSuccessful { get; set; }
        string Message { get; set; }
        Guna2TextBox Guna2TextBoxID { get; }
        Guna2TextBox Guna2TextBoxName { get; }
        Guna2CustomRadioButton Guna2RadioButtonMale { get; }
        Guna2CustomRadioButton Guna2RadioButtonFeMale { get; }
        Guna2DateTimePicker Guna2TextBoxBirthdate { get; }
        Guna2TextBox Guna2TextBoxPhone { get; }
        Guna2TextBox Guna2TextBoxAddress { get; }
        Guna2TextBox Guna2TextBoxEmail { get; }
        Guna2DateTimePicker Guna2TextBoxEntryDate { get; }
        Staff GetStaff { get; }
        event EventHandler ShowDetail;
        event EventHandler UpdateStaff;
        event EventHandler Refresh;
        void show();
        void close();
    }
}
