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
    public partial class FormUpdateProduct : Form, IUpdateProductView
    {
        private string message;
        private bool isSuccessful;
        private Product product;
        public FormUpdateProduct(Product product)
        {
            this.product = product;
            InitializeComponent();
            AssociateAndRaiseViewEvents();
        }

        private void AssociateAndRaiseViewEvents()
        {
            Load += delegate { ShowDetail?.Invoke(this, EventArgs.Empty); };
            guna2TextBox9.Click += delegate { LoadListBrand?.Invoke(this, EventArgs.Empty); };
            guna2TextBox8.Click += delegate { LoadListCategory?.Invoke(this, EventArgs.Empty); };
            guna2DataGridView1.SelectionChanged += delegate { SelectedRow?.Invoke(this, EventArgs.Empty); };
            btnUpdate.Click += delegate
            {
                UpdateProduct?.Invoke(this, EventArgs.Empty);
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

        public Product GetProduct
        {
            get { return product; }
        }

        public Guna2DataGridView Guna2DataGridView
        {
            get { return guna2DataGridView1; }
            set { guna2DataGridView1 = value; }
        }

        public Guna2TextBox Guna2TextBoxID { get { return guna2TextBox11; } }

        public Guna2TextBox Guna2TextBoxname { get { return guna2TextBox10; } }

        public Guna2TextBox Guna2TextBoxBrand { get { return guna2TextBox9; } }

        public Guna2TextBox Guna2TextBoxCategory { get { return guna2TextBox8; } }

        public Guna2ComboBox Guna2ComboBoxUnit { get { return guna2ComboBox1; } }

        public Guna2TextBox Guna2TextBoxCost { get { return guna2TextBox6; } }

        public Guna2TextBox Guna2TextBoxQuantity { get { return guna2TextBox5; } }

        public Guna2TextBox Guna2TextBoxBarcode { get { return guna2TextBox4; } }

        public Guna2PictureBox pictureBox { get { return guna2PictureBox1; } }
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
        public event EventHandler SelectedRow;
        public event EventHandler UpdateProduct;
        public event EventHandler Refresh;
        public event EventHandler LoadListBrand;
        public event EventHandler LoadListCategory;
    }
}
