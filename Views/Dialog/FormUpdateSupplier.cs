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
    public partial class FormUpdateSupplier : Form, IUpdateSupplierView
    {
        private string message;
        private bool isSuccessful;
        private Supplier supplier;
        public FormUpdateSupplier(Supplier supplier)
        {
            this.supplier = supplier;
            InitializeComponent();
            AssociateAndRaiseViewEvents();
        }

        private void AssociateAndRaiseViewEvents()
        {
            Load += delegate { ShowDetail?.Invoke(this, EventArgs.Empty); };
            btnUpdate.Click += delegate
            {
                UpdateSupplier?.Invoke(this, EventArgs.Empty);
                MessageDialog.Show(MiniSupermarketApp.menu, Message, "Thông báo", MessageDialogButtons.OK, MessageDialogIcon.Information);
            };
            btnCancel.Click += delegate { Refresh?.Invoke(this, EventArgs.Empty); };
        }

        public Supplier GetSupplier
        {
            get { return supplier; }
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

        public Guna2TextBox Guna2TextBoxPhone { get { return guna2TextBox9; } }

        public Guna2TextBox Guna2TextBoxAddress { get { return guna2TextBox8; } }

        public Guna2TextBox Guna2TextBoxEmail { get { return guna2TextBox7; } }

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
        public event EventHandler LoadListSupplier;
        public event EventHandler SelectedRow;
        public event EventHandler UpdateSupplier;
        public event EventHandler Refresh;
    }
}
