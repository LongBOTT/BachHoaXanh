﻿using BachHoaXanh.Models;
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
    public partial class FormDetailBill : Form, IShowDetailReceiptView
    {
        private Receipt receipt;

        public event EventHandler ShowDetail;
        public event EventHandler LoadListReceiptDetail;
        public event EventHandler LoadListStaff;

        public Guna2DataGridView Guna2DataGridView { get { return guna2DataGridView1; } }

        public Guna2TextBox Guna2TextBoxID { get { return guna2TextBox11;} }

        public Guna2TextBox Guna2TextBoxStaffID { get { return guna2TextBox10;} }

        public Guna2TextBox Guna2TextBoxInvoice { get { return guna2TextBox8;} }

        public Guna2TextBox Guna2TextBoxTotal { get { return guna2TextBox7;} }

        public Guna2TextBox Guna2TextBoxReceived { get { return guna2TextBox6;} }

        public Guna2TextBox Guna2TextBoxExcess { get { return guna2TextBox5;} }

        public Receipt GetReceipt { get { return receipt;} }

        public FormDetailBill(Receipt receipt)
        {
            this.receipt = receipt;
            InitializeComponent();
            AssociateAndRaiseViewEvents();
        }

        private void AssociateAndRaiseViewEvents()
        {
            Load += delegate { ShowDetail?.Invoke(this, EventArgs.Empty); };
            guna2TextBox10.Click += delegate { LoadListStaff?.Invoke(this, EventArgs.Empty); };
            guna2TextBox10.Leave += delegate { LoadListReceiptDetail?.Invoke(this, EventArgs.Empty); };
        }

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
    }
}
