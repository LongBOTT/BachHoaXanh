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
    public partial class FormDetailImport : Form, IShowDetailImportView
    {
        private Import import;

        public event EventHandler ShowDetail;
        public event EventHandler LoadListStaff;
        public event EventHandler LoadListSupplier;
        public event EventHandler LoadListShipmentDetail;

        public Guna2DataGridView Guna2DataGridView { get { return guna2DataGridView1; } }

        public Guna2TextBox Guna2TextBoxID { get { return guna2TextBox11; } }

        public Guna2TextBox Guna2TextBoxStaffID { get { return guna2TextBox10; } }

        public Guna2TextBox Guna2TextBoxReceived { get { return guna2TextBox9; } }

        public Guna2TextBox Guna2TextBoxTotal { get { return guna2TextBox8; } }

        public Guna2TextBox Guna2TextBoxSupplierID { get { return guna2TextBox7; } }


        public Import GetImport { get { return import; } }


        public FormDetailImport(Import import)
        {
            this.import = import;
            InitializeComponent();
            AssociateAndRaiseViewEvents();
        }

        private void AssociateAndRaiseViewEvents()
        {
            Load += delegate { ShowDetail?.Invoke(this, EventArgs.Empty); };
            guna2TextBox10.Click += delegate { LoadListStaff?.Invoke(this, EventArgs.Empty); };
            guna2TextBox10.Leave += delegate { LoadListShipmentDetail?.Invoke(this, EventArgs.Empty); };
            guna2TextBox7.Click += delegate { LoadListSupplier?.Invoke(this, EventArgs.Empty); };
            guna2TextBox7.Leave += delegate { LoadListShipmentDetail?.Invoke(this, EventArgs.Empty); };
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
    }
}
