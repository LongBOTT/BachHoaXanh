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
    public partial class StaffControl : UserControl, IStaffView
    {
        public StaffControl()
        {
            InitializeComponent();
            AssociateAndRaiseViewEvents();
        }

        private void AssociateAndRaiseViewEvents()
        {
            txtSearch.TextChanged += delegate { SearchEvent?.Invoke(this, EventArgs.Empty); };
            cbbSearch.SelectedValueChanged += delegate { SearchEvent?.Invoke(this, EventArgs.Empty); };
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
    }
}
