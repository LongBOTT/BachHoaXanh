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
    public partial class FormDetailStaff : Form, IShowDetailStaffView
    {
        private Staff staff;
        public FormDetailStaff(Staff staff)
        {
            this.staff = staff;
            InitializeComponent();
            AssociateAndRaiseViewEvents();
        }

        private void AssociateAndRaiseViewEvents()
        {
            Load += delegate { ShowDetail?.Invoke(this, EventArgs.Empty); };
        }

        public Guna2TextBox Guna2TextBoxID { get { return guna2TextBox11; } }

        public Guna2TextBox Guna2TextBoxName { get { return guna2TextBox10; } }

        public Guna2CustomRadioButton Guna2RadioButtonMale { get { return guna2CustomRadioButton1; } }
        public Guna2CustomRadioButton Guna2RadioButtonFeMale { get { return guna2CustomRadioButton2; } }

        public Guna2TextBox Guna2TextBoxBirthdate { get { return guna2TextBox8; } }

        public Guna2TextBox Guna2TextBoxPhone { get { return guna2TextBox7; } }

        public Guna2TextBox Guna2TextBoxAddress { get { return guna2TextBox6; } }

        public Guna2TextBox Guna2TextBoxEmail { get { return guna2TextBox5; } }
        public Guna2TextBox Guna2TextBoxEntryDate { get { return guna2TextBox4; } }

        public Staff GetStaff { get { return staff; } }


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

        public event EventHandler ShowDetail;
    }
}
