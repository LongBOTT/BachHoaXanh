using BachHoaXanh.Models;
using BachHoaXanh.Views.InterfaceView;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BachHoaXanh.User_Control
{
    public partial class AccountControl : UserControl, IAccountView
    {
        public AccountControl()
        {
            InitializeComponent();
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
                    MessageDialog.Show("Vui lòng chọn tài khoản cần xem chi tiết!", "Lỗi", MessageDialogButtons.OK, MessageDialogIcon.Error);
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
                    MessageDialog.Show("Vui lòng chọn tài khoản cần sửa!", "Lỗi", MessageDialogButtons.OK, MessageDialogIcon.Error);
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
                    MessageDialog.Show("Vui lòng chọn tài khoản cần xoá!", "Lỗi", MessageDialogButtons.OK, MessageDialogIcon.Error);
                }
            };
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

        public Guna2DataGridView Guna2DataGridView
        {
            get { return guna2DataGridView1; }
            set { guna2DataGridView1 = value; }
        }

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

        public event EventHandler SearchEvent;
        public event EventHandler ShowDetail;
        public event EventHandler AddNewEvent;
        public event EventHandler UpdateEvent;
        public event EventHandler DeleteEvent;
    }
}
