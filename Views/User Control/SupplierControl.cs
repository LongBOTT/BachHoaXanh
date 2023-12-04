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
    public partial class SupplierControl : UserControl, ISupplierView
    {

        public SupplierControl()
        {
            InitializeComponent();
            guna2ComboBox1.SelectedIndex = 0;
            AssociateAndRaiseViewEvents();
        }

        private void AssociateAndRaiseViewEvents()
        {
            guna2TextBox1.TextChanged += delegate { SearchEvent?.Invoke(this, EventArgs.Empty); };
            guna2ComboBox1.SelectedValueChanged += delegate { SearchEvent?.Invoke(this, EventArgs.Empty); };
            btnDetail.Click += delegate
            {
                if (Guna2DataGridView.SelectedRows[0].Index != -1)
                {
                    ShowDetail?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    MessageDialog.Show(MiniSupermarketApp.menu, "Vui lòng chọn nhà cung cấp cần xem chi tiết!", "Lỗi", MessageDialogButtons.OK, MessageDialogIcon.Error);
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
                    MessageDialog.Show(MiniSupermarketApp.menu, "Vui lòng chọn nhà cung cấp cần sửa!", "Lỗi", MessageDialogButtons.OK, MessageDialogIcon.Error);
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
                    MessageDialog.Show(MiniSupermarketApp.menu, "Vui lòng chọn nhà cung cấp cần xoá!", "Lỗi", MessageDialogButtons.OK, MessageDialogIcon.Error);
                }
            };
        }

        private void SupplierControl_Load(object sender, EventArgs e)
        {
            guna2ComboBox1.SelectedItem = 0;
        }

        public Guna2DataGridView Guna2DataGridView
        {
            get { return guna2DataGridView1; }
            set { guna2DataGridView1 = value; }
        }

        public string SearchValue
        {
            get { return guna2TextBox1.Text; }
            set { guna2TextBox1.Text = value; }
        }

        public string Attribute
        {
            get { return guna2ComboBox1.SelectedItem.ToString(); }
            set { guna2ComboBox1.SelectedItem = value; }
        }

        public event EventHandler SearchEvent;
        public event EventHandler AddNewEvent;
        public event EventHandler EditEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler ShowDetail;
        public event EventHandler UpdateEvent;
    }
}
