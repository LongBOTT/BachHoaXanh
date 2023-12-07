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
    public class ShowDetailImportPresenter
    {
        private IShowDetailImportView view;
        private IShipmentRepository repository;
        private Import import;
        private IProductRepository productRepository;
        private IStaffRepository staffRepository;
        private ISupplierRepository supplierRepository;
        private List<Staff> staffs;
        private List<Supplier> suppliers;
        private List<Shipment> shipments;

        public ShowDetailImportPresenter(IShowDetailImportView view, IShipmentRepository repository) 
        {
            this.view = view;
            this.repository = repository;
            staffRepository = new StaffRepository();
            productRepository = new ProductRepository();
            supplierRepository = new SupplierRepository();
            staffs = (List<Staff>)staffRepository.GetAll();
            suppliers = (List<Supplier>)supplierRepository.GetAll();
            import = this.view.GetImport;
            shipments = this.repository.FindShipmentsBy(new Dictionary<string, object>() { { "import_note_id", import.Id} });
            this.view.ShowDetail += ShowDetail;
            this.view.LoadListStaff += LoadListStaff;
            this.view.LoadListSupplier += LoadListSupplier;
            this.view.LoadListShipmentDetail += LoadListShipmenDetailt;
            LoadListShipmenDetailt(new object(), new EventArgs());
        }

        private void LoadListShipmenDetailt(object? sender, EventArgs e)
        {
            DataGridViewColumn column1 = new DataGridViewTextBoxColumn();
            DataGridViewColumn column2 = new DataGridViewTextBoxColumn();
            DataGridViewColumn column3 = new DataGridViewTextBoxColumn();
            column1.HeaderText = "Mã lô hàng";
            column2.HeaderText = "Tên sản phẩm";
            column3.HeaderText = "Số lượng nhập";
            view.Guna2DataGridView.Columns.Clear();
            view.Guna2DataGridView.Rows.Clear();
            view.Guna2DataGridView.Columns.AddRange(column1, column2, column3);
            
            foreach (Shipment shipment in shipments)
            {
                Console.WriteLine(shipment.Id);
                Product product = productRepository.FindProductsBy(new Dictionary<string, object>() { { "id", shipment.Product_id } })[0];
                view.Guna2DataGridView.Rows.Add(shipment.Id, product.Name, shipment.Quantity);
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
                if (staff.Id == import.Staff_id)
                    index = view.Guna2DataGridView.RowCount - 1;
            }
            view.Guna2DataGridView.Visible = true;
            view.Guna2DataGridView.Rows[index].Selected = true;
        }

        private void LoadListSupplier(object? sender, EventArgs e)
        {
            int index = -1;
            DataGridViewColumn column1 = new DataGridViewTextBoxColumn();
            DataGridViewColumn column2 = new DataGridViewTextBoxColumn();
            DataGridViewColumn column3 = new DataGridViewTextBoxColumn();
            column1.HeaderText = "Mã nhà cung cấp";
            column2.HeaderText = "Tên nhà cung cấp";
            column3.HeaderText = "SĐT";
            view.Guna2DataGridView.Columns.Clear();
            view.Guna2DataGridView.Rows.Clear();
            view.Guna2DataGridView.Columns.AddRange(column1, column2, column3);

            foreach (Supplier supplier in suppliers)
            {
                view.Guna2DataGridView.Rows.Add(supplier.Id, supplier.Name, supplier.Phone);
                if (supplier.Id == import.Staff_id)
                    index = view.Guna2DataGridView.RowCount - 1;
            }
            view.Guna2DataGridView.Visible = true;
            view.Guna2DataGridView.Rows[index].Selected = true;
        }

        private void ShowDetail(object? sender, EventArgs e)
        {
            view.Guna2TextBoxID.Text = import.Id.ToString();
            view.Guna2TextBoxStaffID.Text = import.Staff_id.ToString();
            view.Guna2TextBoxReceived.Text = import.Received_DateTime.ToString();
            view.Guna2TextBoxTotal.Text = import.Total.ToString();
            view.Guna2TextBoxSupplierID.Text = import.Supplier_id.ToString();
        }
    }
}
