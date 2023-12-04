using BachHoaXanh.Main;
using BachHoaXanh.Models;
using BachHoaXanh.Views.InterfaceView;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BachHoaXanh.Dialog
{
    public partial class FormUpdateStaff : Form, IUpdateStaffView
    {
        private string message;
        private bool isSuccessful;
        private Staff staff;
        public FormUpdateStaff(Staff staff)
        {
            this.staff = staff;
            InitializeComponent();
            AssociateAndRaiseViewEvents();
        }

        private void AssociateAndRaiseViewEvents()
        {
            Load += delegate { ShowDetail?.Invoke(this, EventArgs.Empty); };
            btnUpdate.Click += delegate
            {
                UpdateStaff?.Invoke(this, EventArgs.Empty);
                MessageDialog.Show(MiniSupermarketApp.menu, Message, "Thông báo", MessageDialogButtons.OK, MessageDialogIcon.Information);
            };
            btnCancel.Click += delegate { Refresh?.Invoke(this, EventArgs.Empty); };
        }

        public Staff GetStaff
        {
            get { return staff; }
        }

        public bool IsSuccessful
        {
            get { return isSuccessful; }
            set { isSuccessful = value; }
        }

        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        public Guna2TextBox Guna2TextBoxID { get { return guna2TextBox11; } }

        public Guna2TextBox Guna2TextBoxName { get { return guna2TextBox10; } }

        public Guna2CustomRadioButton Guna2RadioButtonMale { get { return guna2CustomRadioButton1; } }
        public Guna2CustomRadioButton Guna2RadioButtonFeMale { get { return guna2CustomRadioButton2; } }

        public Guna2DateTimePicker Guna2TextBoxBirthdate { get { return guna2DateTimePicker1; } }

        public Guna2TextBox Guna2TextBoxPhone { get { return guna2TextBox7; } }

        public Guna2TextBox Guna2TextBoxAddress { get { return guna2TextBox6; } }

        public Guna2TextBox Guna2TextBoxEmail { get { return guna2TextBox5; } }
        public Guna2DateTimePicker Guna2TextBoxEntryDate { get { return guna2DateTimePicker2; } }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void show()
        {
            ShowDialog();
        }

        public void close()
        {
            Close();
        }

        public event EventHandler ShowDetail;
        public event EventHandler LoadListRole;
        public event EventHandler LoadListStaff;
        public event EventHandler SelectedRow;
        public event EventHandler UpdateStaff;
        public event EventHandler Refresh;
    }
}
