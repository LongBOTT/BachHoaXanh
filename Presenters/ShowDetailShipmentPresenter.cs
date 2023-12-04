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
    public class ShowDetailShipmentPresenter
    {
        private IShowDetailShipmentView view;
        private IShipmentRepository repository;
        private Shipment shipment;
        private IProductRepository productRepository;
        private IImportRepository importRepository;
        private List<Product> products;
        private List<Import> imports;

        public ShowDetailShipmentPresenter(IShowDetailShipmentView view, IShipmentRepository repository) 
        {
            this.view = view;
            this.repository = repository;
            importRepository = new ImportRepository();
            productRepository = new ProductRepository();
            products = (List<Product>)productRepository.GetAll();
            imports = (List<Import>)importRepository.GetAll();
            shipment = this.view.GetShipment;
            this.view.ShowDetail += ShowDetail;
            this.view.LoadListImport += LoadListImport;
            this.view.LoadListProduct += LoadListProduct;
        }

        private void LoadListProduct(object? sender, EventArgs e)
        {
            int index = -1;
            DataGridViewColumn column1 = new DataGridViewTextBoxColumn();
            DataGridViewColumn column2 = new DataGridViewTextBoxColumn();
            column1.HeaderText = "Mã sản phẩm";
            column2.HeaderText = "Tên sản phẩm";
            view.Guna2DataGridView.Columns.Clear();
            view.Guna2DataGridView.Rows.Clear();
            view.Guna2DataGridView.Columns.AddRange(column1, column2);
            
            foreach (Product product in products)
            {
                view.Guna2DataGridView.Rows.Add(product.Id, product.Name);
                if (product.Id == shipment.Product_id)
                    index = view.Guna2DataGridView.RowCount - 1;
            }
             view.Guna2DataGridView.Visible = true;
             view.Guna2DataGridView.Rows[index].Selected = true;
        }

        private void LoadListImport(object? sender, EventArgs e)
        {
            int index = -1;
            DataGridViewColumn column1 = new DataGridViewTextBoxColumn();
            DataGridViewColumn column2 = new DataGridViewTextBoxColumn();
            DataGridViewColumn column3 = new DataGridViewTextBoxColumn();
            column1.HeaderText = "Mã phiếu nhập";
            column2.HeaderText = "Nhân viên nhập";
            column3.HeaderText = "Ngày nhập";
            view.Guna2DataGridView.Columns.Clear();
            view.Guna2DataGridView.Rows.Clear();
            view.Guna2DataGridView.Columns.AddRange(column1, column2, column3);

            foreach (Import import in imports)
            {
                IStaffRepository staffRepository = new StaffRepository();
                Staff staff = staffRepository.FindStaffsBy(new Dictionary<string, object>() { { "id", import.Staff_id } })[0];
                view.Guna2DataGridView.Rows.Add(import.Id, staff.Name, import.Received_DateTime.ToString("d"));
                if (import.Id == shipment.Import_id)
                    index = view.Guna2DataGridView.RowCount - 1;
            }
            view.Guna2DataGridView.Visible = true;
            view.Guna2DataGridView.Rows[index].Selected = true;
        }

        private void ShowDetail(object? sender, EventArgs e)
        {
            view.Guna2TextBoxID.Text = shipment.Id.ToString();
            view.Guna2TextBoxProductID.Text = shipment.Product_id.ToString();
            view.Guna2TextBoxUnitPRice.Text = shipment.Unit_price.ToString();
            view.Guna2TextBoxQuantity.Text = shipment.Quantity.ToString();
            view.Guna2TextBoxRemain.Text = shipment.Remain.ToString();
            view.Guna2TextBoxMfg.Text = shipment.Mfg.ToString();
            view.Guna2TextBoxExp.Text = shipment.Exp.ToString();
            view.Guna2TextBoxSku.Text = shipment.Sku.ToString();
            view.Guna2TextBoxImportID.Text = shipment.Import_id.ToString();
        }
    }
}
