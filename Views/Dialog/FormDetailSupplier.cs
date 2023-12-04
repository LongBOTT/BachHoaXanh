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
    public partial class FormDetailSupplier : Form, IShowDetailSupplierView
    {
        private Supplier supplier;
        public FormDetailSupplier(Supplier supplier)
        {
            this.supplier = supplier;
            InitializeComponent();
            AssociateAndRaiseViewEvents();
        }

        private void AssociateAndRaiseViewEvents()
        {
            Load += delegate { ShowDetail?.Invoke(this, EventArgs.Empty); };
        }

        public Guna2TextBox Guna2TextBoxID { get { return guna2TextBox11; } }

        public Guna2TextBox Guna2TextBoxName { get { return guna2TextBox10; } }

        public Guna2TextBox Guna2TextBoxPhone { get { return guna2TextBox9; } }

        public Guna2TextBox Guna2TextBoxAddress { get { return guna2TextBox8; } }

        public Guna2TextBox Guna2TextBoxEmail { get { return guna2TextBox7; } }

        public Guna2DataGridView Guna2DataGridView { get { return guna2DataGridView1; } }

        public Supplier GetSupplier { get { return supplier; } }

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
