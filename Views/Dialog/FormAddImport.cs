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
    public partial class FormAddImport : Form, IAddImportView
    {
        private string message;
        private bool isSuccessful;

        public event EventHandler LoadListSupplier;
        public event EventHandler ShowDetail;
        public event EventHandler SelectedRow;
        public event EventHandler AddImport;

        public Guna2DataGridView Guna2DataGridView { get { return guna2DataGridView1; } }

        public Guna2TextBox Guna2TextBoxID { get { return guna2TextBox11; } }

        public Guna2TextBox Guna2TextBoxStaffID { get { return guna2TextBox10; } }

        public Guna2TextBox Guna2TextBoxReceived { get { return guna2TextBox9; } }

        public Guna2TextBox Guna2TextBoxTotal { get { return guna2TextBox8; } }

        public Guna2TextBox Guna2TextBoxSupplierID { get { return guna2TextBox7; } }

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

        public Guna2Button Guna2GradientButtonAddProduct { get { return btnAddProduct; } }

        public FormAddImport()
        {
            InitializeComponent();
            AssociateAndRaiseViewEvents();
        }

        public event EventHandler Refresh;

        private void AssociateAndRaiseViewEvents()
        {
            Load += delegate { ShowDetail?.Invoke(this, EventArgs.Empty); };
            guna2TextBox7.Click += delegate { LoadListSupplier?.Invoke(this, EventArgs.Empty); };
            guna2DataGridView1.SelectionChanged += delegate { SelectedRow?.Invoke(this, EventArgs.Empty); };
            btnAdd.Click += delegate
            {
                AddImport?.Invoke(this, EventArgs.Empty);
                MessageDialog.Show(MiniSupermarketApp.menu, Message, "Thông báo", MessageDialogButtons.OK, MessageDialogIcon.Information);
            };
            btnCancel.Click += delegate { Refresh?.Invoke(this, EventArgs.Empty); };
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
    }
}
