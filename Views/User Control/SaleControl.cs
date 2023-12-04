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

namespace BachHoaXanh
{
    public partial class SaleControl : UserControl, ISaleView
    {
        public SaleControl()
        {
            InitializeComponent();
            AssociateAndRaiseViewEvents();
        }

        private void AssociateAndRaiseViewEvents()
        {
            Load += delegate { LoadProduct?.Invoke(this, EventArgs.Empty); };
        }

        public FlowLayoutPanel ContainerProduct
        {
            get { return flowLayoutPanel1; }
        }

        public FlowLayoutPanel ContainerProductInBill
        {
            get { return flowLayoutPanel2; }
        }

        public Guna2TextBox Guna2TextBoxId { get { return txtProduct_ID; } }

        public Guna2TextBox Guna2TextBoxName { get { return txtProduct_Name; } }

        public Guna2TextBox Guna2TextBoxBrand { get { return txtBrand_ID; } }

        public Guna2TextBox Guna2TextBoxCategory { get { return txtCategory_ID; } }

        public Guna2TextBox Guna2TextBoxCost { get { return txtCost; } }

        public Guna2TextBox Guna2TextBoxBarcode { get { return txtBarcode; } }

        public Guna2TextBox Guna2TextBoxQuantity { get { return txtQuantity; } }

        public Guna2GradientButton Guna2GradientButtonAddProduct { get { return btnAddProduct; } }

        public Guna2GradientButton Guna2GradientButtonDeleteProduct { get { return btnDeleteProduct; } }

        public Guna2DataGridView Guna2DataGridView { get { return guna2DataGridView1; } }

        public Guna2TextBox Guna2TextBoxSearch { get { return txtFindProduct; } }

        public Guna2TextBox Guna2TextBoxStaffName { get { return txtStaffName; } }

        public Guna2TextBox Guna2TextBoxDate { get { return txtInvoicedDate; } }

        public Guna2TextBox Guna2TextBoxTotal { get { return txtTotal; } }

        public Guna2TextBox Guna2TextBoxReceived { get { return txtReceived; } }

        public Guna2TextBox Guna2TextBoxExcess { get { return txtExcess; } }

        public Guna2GradientButton Guna2GradientButtonConfirm { get { return btnConfirm; } }

        public Guna2GradientButton Guna2GradientButtonSearch { get { return btnFindProdcut; } }

        public Guna2GradientButton Guna2GradientButtonCancel { get { return btnCancel; } }

        public Guna2GradientButton Guna2GradientButtonPay { get { return btnPurchase; } }

        public event EventHandler LoadProduct;
    }
}
