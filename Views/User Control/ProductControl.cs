using BachHoaXanh.Main;
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

namespace BachHoaXanh.User_Control
{
    public partial class ProductControl : UserControl, IProductView
    {
        public ProductControl()
        {
            InitializeComponent();
            cbbSearch.SelectedIndex = 0;
            AssociateAndRaiseViewEvents();
        }

        private void AssociateAndRaiseViewEvents()
        {
            txtSearch.TextChanged += delegate { SearchEvent?.Invoke(this, EventArgs.Empty); };
            cbbSearch.SelectedValueChanged += delegate { SearchEvent?.Invoke(this, EventArgs.Empty); };
            btnDetail.Click += delegate
            {
                if (Guna2DataGridView.SelectedRows[0].Index != -1)
                {
                    ShowDetail?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    MessageDialog.Show(MiniSupermarketApp.menu, "Vui lòng chọn sản phẩm cần xem chi tiết!", "Lỗi", MessageDialogButtons.OK, MessageDialogIcon.Error);
                }
            };
            btnAdd.Click += delegate { AddNewEvent?.Invoke(this, EventArgs.Empty); };
            btnEdit.Click += delegate
            {
                if (Guna2DataGridView.SelectedRows[0].Index != -1)
                {
                    UpdateEvent?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    MessageDialog.Show(MiniSupermarketApp.menu, "Vui lòng chọn sản phẩm cần sửa!", "Lỗi", MessageDialogButtons.OK, MessageDialogIcon.Error);
                }
            };
            btnDelete.Click += delegate
            {
                if (Guna2DataGridView.SelectedRows[0].Index != -1)
                {
                    DeleteEvent?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    MessageDialog.Show(MiniSupermarketApp.menu, "Vui lòng chọn sản phẩm cần xoá!", "Lỗi", MessageDialogButtons.OK, MessageDialogIcon.Error);
                }
            };
            guna2GradientButton1.Click += delegate { DeleteCategoryEvent?.Invoke(this, EventArgs.Empty); };
            guna2GradientButton2.Click += delegate { AddCategoryEvent?.Invoke(this, EventArgs.Empty); };
            guna2GradientButton3.Click += delegate { AddBrandEvent?.Invoke(this, EventArgs.Empty); };
            guna2GradientButton4.Click += delegate { DeleteBrandEvent?.Invoke(this, EventArgs.Empty); };
            guna2DataGridView1.SelectionChanged += delegate { ShowBrand_Category?.Invoke(this, EventArgs.Empty); };
        }
        public Guna2DataGridView Guna2DataGridView { get {return guna2DataGridView1; } set { guna2DataGridView1 = value;} }
        public string SearchValue
        {
            get { return txtSearch.Text; }
            set { txtSearch.Text = value; }
        }

        public string Attribute
        {
            get { return cbbSearch.SelectedItem.ToString(); }
            set { cbbSearch.SelectedItem = value; }
        }

        public Guna2TextBox txtNameBrand {get { return guna2TextBox4; }}

        public Guna2ComboBox cbbSupplier {get{ return guna2ComboBox1; }}

        public Guna2TextBox txtNameCategory { get { return guna2TextBox1; }}

        public Guna2TextBox txtQuantityCategory {get { return guna2TextBox2; }}

        Guna2TextBox IProductView.txtSearch { get {return txtSearch; } }

        public event EventHandler SearchEvent;
        public event EventHandler ShowDetail;
        public event EventHandler AddNewEvent;
        public event EventHandler UpdateEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler AddCategoryEvent;
        public event EventHandler AddBrandEvent;
        public event EventHandler DeleteCategoryEvent;
        public event EventHandler DeleteBrandEvent;
        public event EventHandler ShowBrand_Category;
    }
}
