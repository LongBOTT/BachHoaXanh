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
    public class ShowDetailExportPresenter
    {
        private IShowDetailExportView view;
        private IExport_detailRepository repository;
        private IShipmentRepository shipmentRepository;
        private Export export;
        private IProductRepository productRepository;
        private IStaffRepository staffRepository;
        private List<Staff> staffs;
        private List<Export_detail> exportDetails;

        public ShowDetailExportPresenter(IShowDetailExportView view, IExport_detailRepository repository) 
        {
            this.view = view;
            this.repository = repository;
            staffRepository = new StaffRepository();
            shipmentRepository = new ShipmentRepository();
            productRepository = new ProductRepository();
            staffs = (List<Staff>)staffRepository.GetAll();
            export = this.view.GetExport;
            exportDetails = this.repository.FindExport_detailsBy(new Dictionary<string, object>() { { "export_note_id", export.Id} });
            this.view.ShowDetail += ShowDetail;
            this.view.LoadListStaff += LoadListStaff;
            this.view.LoadListExportDetail += LoadListExportDetailt;
            LoadListExportDetailt(new object(), new EventArgs());
        }

        private void LoadListExportDetailt(object? sender, EventArgs e)
        {
            DataGridViewColumn column1 = new DataGridViewTextBoxColumn();
            DataGridViewColumn column2 = new DataGridViewTextBoxColumn();
            DataGridViewColumn column3 = new DataGridViewTextBoxColumn();
            column1.HeaderText = "Mã lô hàng";
            column2.HeaderText = "Tên sản phẩm";
            column3.HeaderText = "Số lượng xuất";
            view.Guna2DataGridView.Columns.Clear();
            view.Guna2DataGridView.Rows.Clear();
            view.Guna2DataGridView.Columns.AddRange(column1, column2, column3);
            
            foreach (Export_detail export_Detail in exportDetails)
            {
                Shipment shipment = shipmentRepository.FindShipmentsBy(new Dictionary<string, object>() { { "id", export_Detail.Shipment_id } })[0];
                Product product = productRepository.FindProductsBy(new Dictionary<string, object>() { { "id", shipment.Product_id } })[0];
                view.Guna2DataGridView.Rows.Add(shipment.Id, product.Name, export_Detail.Quantity);
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
                if (staff.Id == export.Staff_id)
                    index = view.Guna2DataGridView.RowCount - 1;
            }
            view.Guna2DataGridView.Visible = true;
            view.Guna2DataGridView.Rows[index].Selected = true;
        }

        private void ShowDetail(object? sender, EventArgs e)
        {
            view.Guna2TextBoxID.Text = export.Id.ToString();
            view.Guna2TextBoxStaffID.Text = export.Staff_id.ToString();
            view.Guna2TextBoxInvoice.Text = export.Invoice_DateTime.ToString();
            view.Guna2TextBoxTotal.Text = export.Total.ToString();
            view.Guna2TextBoxReason.Text = export.Reason.ToString();
        }
    }
}
