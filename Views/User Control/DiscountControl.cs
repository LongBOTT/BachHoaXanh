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
    public partial class DiscountControl : UserControl, IDiscountView
    {
        public DiscountControl()
        {
            InitializeComponent();
            guna2ComboBox1.SelectedIndex = 0;
            AssociateAndRaiseViewEvents();
        }

        private void AssociateAndRaiseViewEvents()
        {
            guna2DateTimePicker1.TextChanged += delegate { SearchDiscountEvent?.Invoke(this, EventArgs.Empty); };
            guna2DateTimePicker2.TextChanged += delegate { SearchDiscountEvent?.Invoke(this, EventArgs.Empty); };

            guna2ComboBox1.SelectedValueChanged += delegate { SearchProductEvent?.Invoke(this, EventArgs.Empty); };
            guna2TextBox1.TextChanged += delegate { SearchProductEvent?.Invoke(this, EventArgs.Empty); };

            btnAddDiscount.Click += delegate { AddNewEvent?.Invoke(this, EventArgs.Empty); };
            btnCancel.Click += delegate { CancelEvent?.Invoke(this, EventArgs.Empty); };

            guna2DataGridView1.SelectionChanged += delegate { ShowDetail?.Invoke(this, EventArgs.Empty); };
            guna2DataGridView2.SelectionChanged += delegate { AddProduct?.Invoke(this, EventArgs.Empty); };
        }

        public Guna2DataGridView guna2DataGridDiscount { get { return guna2DataGridView1; } }

        public Guna2DataGridView guna2DataGridProduct { get { return guna2DataGridView2; } }

        public Guna2DateTimePicker DateTimePickerSearch1 { get { return DateTimePickerSearch1; } }

        public Guna2DateTimePicker DateTimePickerSearch2 { get { return DateTimePickerSearch2; } }

        public Guna2DateTimePicker DateTimePickerStart { get { return DateTimePickerStart; } }

        public Guna2DateTimePicker DateTimePickerEnd { get { return DateTimePickerEnd; } }

        public Guna2TextBox guna2TextBoxID { get { return guna2TextBox11; } }

        public Guna2TextBox guna2TextPerent { get { return guna2TextBox10; } }

        public Guna2GradientButton guna2ButtonAdd { get { return btnAddDiscount; } }

        public Guna2GradientButton guna2ButtonCancel { get { return btnCancel; } }

        public event EventHandler SearchDiscountEvent;
        public event EventHandler SearchProductEvent;
        public event EventHandler ShowDetail;
        public event EventHandler AddNewEvent;
        public event EventHandler CancelEvent;
        public event EventHandler AddProduct;
    }
}
