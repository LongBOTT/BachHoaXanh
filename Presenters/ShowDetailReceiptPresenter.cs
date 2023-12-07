using BachHoaXanh._Repositories;
using BachHoaXanh.Models;
using BachHoaXanh.Views.InterfaceView;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BachHoaXanh.Presenters
{
    public class ShowDetailReceiptPresenter
    {
        private IShowDetailReceiptView view;
        private IReceipt_detailRepository repository;
        private Receipt receipt;
        private IProductRepository productRepository;
        private IStaffRepository staffRepository;
        private List<Product> products;
        private List<Staff> staffs;
        private List<Receipt_detail> receipt_details;

        public ShowDetailReceiptPresenter(IShowDetailReceiptView view, IReceipt_detailRepository repository) 
        {
            this.view = view;
            this.repository = repository;
            staffRepository = new StaffRepository();
            productRepository = new ProductRepository();
            products = (List<Product>)productRepository.GetAll();
            staffs = (List<Staff>)staffRepository.GetAll();
            receipt = this.view.GetReceipt;
            receipt_details = this.repository.FindReceipt_detailsBy(new Dictionary<string, object>() { { "receipt_id", receipt.Id} });
            this.view.ShowDetail += ShowDetail;
            this.view.LoadListStaff += LoadListStaff;
            this.view.LoadListReceiptDetail += LoadListReceiptDetail;
            LoadListReceiptDetail(new object(), new EventArgs());
        }

        private void LoadListReceiptDetail(object? sender, EventArgs e)
        {
            DataGridViewColumn column1 = new DataGridViewTextBoxColumn();
            DataGridViewColumn column2 = new DataGridViewTextBoxColumn();
            DataGridViewColumn column3 = new DataGridViewTextBoxColumn();
            DataGridViewColumn column4 = new DataGridViewTextBoxColumn();
            column1.HeaderText = "Mã sản phẩm";
            column2.HeaderText = "Tên sản phẩm";
            column3.HeaderText = "Số lượng";
            column4.HeaderText = "Tổng tiền";
            view.Guna2DataGridView.Columns.Clear();
            view.Guna2DataGridView.Rows.Clear();
            view.Guna2DataGridView.Columns.AddRange(column1, column2, column3, column4);
            
            foreach (Receipt_detail receipt_Detail in receipt_details)
            {
                Product product = productRepository.FindProductsBy(new Dictionary<string, object>() { { "id", receipt_Detail.Product_id } })[0];
                view.Guna2DataGridView.Rows.Add(product.Id, product.Name, receipt_Detail.Quantity, receipt_Detail.Total);
            }
             view.Guna2DataGridView.Visible = true;
        }

        private void LoadListStaff(object? sender, EventArgs e)
        {
            int index = -1;
            DataGridViewColumn column1 = new DataGridViewTextBoxColumn();
            DataGridViewColumn column2 = new DataGridViewTextBoxColumn();
            DataGridViewColumn column3 = new DataGridViewTextBoxColumn();
            column1.HeaderText = "Mã nhân viên";
            column2.HeaderText = "Tên nhân viên";
            column3.HeaderText = "SĐT";
            view.Guna2DataGridView.Columns.Clear();
            view.Guna2DataGridView.Rows.Clear();
            view.Guna2DataGridView.Columns.AddRange(column1, column2, column3);

            foreach (Staff staff in staffs)
            {
                IStaffRepository staffRepository = new StaffRepository();
                view.Guna2DataGridView.Rows.Add(staff.Id, staff.Name, staff.Phone);
                if (staff.Id == receipt.Staff_id)
                    index = view.Guna2DataGridView.RowCount - 1;
            }
            view.Guna2DataGridView.Visible = true;
            view.Guna2DataGridView.Rows[index].Selected = true;
        }

        private void ShowDetail(object? sender, EventArgs e)
        {
            view.Guna2TextBoxID.Text = receipt.Id.ToString();
            view.Guna2TextBoxStaffID.Text = receipt.Staff_id.ToString();
            view.Guna2TextBoxInvoice.Text = receipt.Invoice_DateTime.ToString();
            view.Guna2TextBoxTotal.Text = receipt.Total.ToString();
            view.Guna2TextBoxReceived.Text = receipt.Received.ToString();
            view.Guna2TextBoxExcess.Text = receipt.Excess.ToString();
        }
    }
}
