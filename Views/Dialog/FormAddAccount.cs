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

namespace BachHoaXanh.Views
{
    public partial class FormAddAccount : Form, IAddAccountView
    {
        private string message;
        private bool isSuccessful;
        public FormAddAccount()
        {
            InitializeComponent();
            AssociateAndRaiseViewEvents();
        }

        private void AssociateAndRaiseViewEvents()
        {
            Load += delegate { ShowDetail?.Invoke(this, EventArgs.Empty); };
            guna2TextBox8.Click += delegate { LoadListRole?.Invoke(this, EventArgs.Empty); };
            guna2TextBox6.Click += delegate { LoadListStaff?.Invoke(this, EventArgs.Empty); };
            guna2DataGridView1.SelectionChanged += delegate { SelectedRow?.Invoke(this, EventArgs.Empty); };
            btnAdd.Click += delegate
            {
                AddAccount?.Invoke(this, EventArgs.Empty);
                MessageDialog.Show(MiniSupermarketApp.menu, Message, "Thông báo", MessageDialogButtons.OK, MessageDialogIcon.Information);
            };
            btnCancel.Click += delegate { Refresh?.Invoke(this, EventArgs.Empty); };
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
        public Guna2TextBox Guna2TextBoxUsername { get { return guna2TextBox10; } }
        public Guna2TextBox Guna2TextBoxPassword { get { return guna2TextBox9; } }
        public Guna2TextBox Guna2TextBoxRoleID { get { return guna2TextBox8; } }
        public Guna2TextBox Guna2TextBoxLastSignedIn { get { return guna2TextBox7; } }
        public Guna2TextBox Guna2TextBoxStaffID { get { return guna2TextBox6; } }
        public Guna2DataGridView Guna2DataGridView
        {
            get { return guna2DataGridView1; }
            set { guna2DataGridView1 = value; }
        }

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
        public event EventHandler AddAccount;
        public event EventHandler Refresh;
    }
}
