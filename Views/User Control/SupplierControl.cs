﻿using BachHoaXanh.Views.InterfaceView;
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
            AssociateAndRaiseViewEvents();
        }

        private void AssociateAndRaiseViewEvents()
        {
            guna2TextBox1.TextChanged += delegate { SearchEvent?.Invoke(this, EventArgs.Empty); };
            guna2ComboBox1.SelectedValueChanged += delegate { SearchEvent?.Invoke(this, EventArgs.Empty); };
        }

        //public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        //public string Phone { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        //public string Address { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        //public string Email { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        //public bool Deleted { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
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
        //public bool IsEdit { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        //public bool IsSuccessful { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        //public string Message { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event EventHandler SearchEvent;
        //public event EventHandler AddNewEvent;
        //public event EventHandler EditEvent;
        //public event EventHandler DeleteEvent;
        //public event EventHandler SaveEvent;
        //public event EventHandler CancelEvent;
    }
}
