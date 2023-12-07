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
    public partial class FormAddShipment : Form, IAddShipmentView
    {

        public FormAddShipment()
        {
            InitializeComponent();
            AssociateAndRaiseViewEvents();
        }

        private void AssociateAndRaiseViewEvents()
        {
            guna2DataGridView1.SelectionChanged += delegate { ShowDetail?.Invoke(this, EventArgs.Empty); };
            btnAdd.Click += delegate { AddShipment?.Invoke(this, EventArgs.Empty); };
            btnConfirm.Click += delegate { ConfirmShipment?.Invoke(this, EventArgs.Empty); };
        }


        public Guna2TextBox Guna2TextBoxProductID { get { return guna2TextBox10; } }

        public Guna2TextBox Guna2TextBoxUnitPRice { get { return guna2TextBox9; } }

        public Guna2TextBox Guna2TextBoxQuantity { get { return guna2TextBox8; } }


        public Guna2DateTimePicker Guna2DateTimePickerMfg { get { return guna2DateTimePicker1; } }

        public Guna2DateTimePicker Guna2DateTimePickerExp { get { return guna2DateTimePicker2; } }
        public Guna2TextBox Guna2TextBoxSku { get { return guna2TextBox4; } }


        public Guna2DataGridView Guna2DataGridView { get { return guna2DataGridView1; } }


        public Guna2Button Guna2ButtonAdd { get { return btnAdd; } }

        public Guna2Button Guna2ButtonConfirm { get { return btnConfirm; } }
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
            this.Close();
        }

        public event EventHandler ShowDetail;
        public event EventHandler AddShipment;
        public event EventHandler ConfirmShipment;
    }
}
