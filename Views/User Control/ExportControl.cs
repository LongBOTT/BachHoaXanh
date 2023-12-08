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
    public partial class ExportControl : UserControl, IExportView
    {
        public ExportControl()
        {
            InitializeComponent();
            cbbSearch.SelectedIndex = 0;
            AssociateAndRaiseViewEvents();
        }

        private void AssociateAndRaiseViewEvents()
        {
            guna2TextBox1.TextChanged += delegate { SearchEvent?.Invoke(this, EventArgs.Empty); };
            cbbSearch.SelectedValueChanged += delegate { SortEvent?.Invoke(this, EventArgs.Empty); };
            guna2DateTimePicker1.ValueChanged += delegate { SearchEvent?.Invoke(this, EventArgs.Empty); };
            guna2DateTimePicker2.ValueChanged += delegate { SearchEvent?.Invoke(this, EventArgs.Empty); };
            btnDetail.Click += delegate
            {
                if (Guna2DataGridView.SelectedRows[0].Index != -1)
                {
                    ShowDetail?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    MessageDialog.Show(MiniSupermarketApp.menu, "Vui lòng chọn phiếu xuất cần xem chi tiết!", "Lỗi", MessageDialogButtons.OK, MessageDialogIcon.Error);
                }
            };
            btnAdd.Click += delegate { AddNewEvent?.Invoke(this, EventArgs.Empty); };
        }
        public Guna2DataGridView Guna2DataGridView { get { return guna2DataGridView1; } }

        public Guna2ComboBox Guna2ComboBoxSearch { get { return cbbSearch; } }

        public Guna2DateTimePicker Guna2DateTimePickerStart { get { return guna2DateTimePicker1; } }

        public Guna2DateTimePicker Guna2DateTimePickerEnd { get { return guna2DateTimePicker2; } }

        public Guna2TextBox Guna2TextBoxStaffName { get { return guna2TextBox1; } }

        public string SearchValue { get { return guna2TextBox1.Text; } }

        public string Attribute { get { return cbbSearch.SelectedItem.ToString(); } }

        public event EventHandler SearchEvent;
        public event EventHandler ShowDetail;
        public event EventHandler SortEvent;
        public event EventHandler AddNewEvent;
    }
}
