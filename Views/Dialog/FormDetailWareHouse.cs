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
    public partial class FormDetailWareHouse : Form, IShowDetailShipmentView 
    {

        private Shipment shipment;
        public FormDetailWareHouse(Shipment shipment)
        {
            this.shipment = shipment;
            InitializeComponent();
            AssociateAndRaiseViewEvents();
        }

        private void AssociateAndRaiseViewEvents()
        {
            Load += delegate { ShowDetail?.Invoke(this, EventArgs.Empty); };
            guna2TextBox10.Click += delegate { LoadListProduct?.Invoke(this, EventArgs.Empty); };
            guna2TextBox3.Click += delegate { LoadListImport?.Invoke(this, EventArgs.Empty); };
        }

        public Guna2TextBox Guna2TextBoxID { get { return guna2TextBox11; } }

        public Guna2TextBox Guna2TextBoxProductID { get { return guna2TextBox10; } }

        public Guna2TextBox Guna2TextBoxUnitPRice { get { return guna2TextBox9; } }

        public Guna2TextBox Guna2TextBoxQuantity { get { return guna2TextBox8; } }

        public Guna2TextBox Guna2TextBoxRemain { get { return guna2TextBox7; } }

        public Guna2TextBox Guna2TextBoxMfg { get { return guna2TextBox6; } }

        public Guna2TextBox Guna2TextBoxExp { get { return guna2TextBox5; } }
        public Guna2TextBox Guna2TextBoxSku { get { return guna2TextBox4; } }

        public Guna2TextBox Guna2TextBoxImportID { get { return guna2TextBox3; } }

        public Guna2DataGridView Guna2DataGridView { get { return guna2DataGridView1; } }

        public Shipment GetShipment { get { return shipment; }}

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
        public event EventHandler LoadListProduct;
        public event EventHandler LoadListImport;
    }
}
