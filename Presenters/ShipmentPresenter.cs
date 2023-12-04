using BachHoaXanh._Repositories;
using BachHoaXanh.Dialog;
using BachHoaXanh.Models;
using BachHoaXanh.Views.Dialog;
using BachHoaXanh.Views.InterfaceView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Presenters
{
    public class ShipmentPresenter
    {
        private IShipmentView view;
        private IShipmentRepository repository;
        private IProductRepository productRepository;
        private IImportRepository importRepository;
        private IEnumerable<Shipment> shipmentList;

        public ShipmentPresenter(IShipmentView view, IShipmentRepository repository)
        {
            this.view = view;
            this.repository = repository;
            productRepository = new ProductRepository();
            importRepository = new ImportRepository();
            shipmentList = repository.GetAll();
            LoadShipmentList(shipmentList);

            this.view.SearchEvent += SearchShipment;
            this.view.ShowDetail += ShowDetail;
        }

        private void LoadShipmentList(IEnumerable<Shipment> shipments)
        {
            IProductRepository productRepository = new ProductRepository();
            IImportRepository importRepository = new ImportRepository();
            view.Guna2DataGridView.Rows.Clear();
            foreach (Shipment shipment in shipments)
            {
                Product product = productRepository.FindProductsBy(new Dictionary<string, object>() { { "id", shipment.Product_id } })[0];
                Import import = importRepository.FindImportsBy(new Dictionary<string, object>() { { "id", shipment.Import_id } })[0];
                view.Guna2DataGridView.Rows.Add(shipment.Id, product.Name, shipment.Unit_price, shipment.Quantity, shipment.Remain, shipment.Mfg, shipment.Exp, import.Received_DateTime);
            }
        }

        private void SearchShipment(object sender, EventArgs e)
        {

            string attribute = this.view.Attribute;
            if (attribute == "Tên sản phẩm")
            {
                bool emptyValue = string.IsNullOrWhiteSpace(this.view.SearchValue);
                if (!emptyValue)
                {
                    view.Guna2TextBox.Visible = true;
                    searchByProductName(this.view.SearchValue);
                }
                else
                {
                    view.Guna2TextBox.Visible = true;
                    LoadShipmentList(repository.GetAll());
                }
            }
            if (attribute == "Sắp hết hạn")
            {
                view.Guna2TextBox.Visible = false;
                searchByMfg();
            }
            if (attribute == "Sắp hết hàng")
            {
                view.Guna2TextBox.Visible = false;
                searchByRemain();
            }

            
        }

        private void searchByRemain()
        {
            List<Shipment> result = repository.SearchShipment( new List<string> { "`shipment`.remain > 0",  "`shipment`.remain <= 20" });
            LoadShipmentList(result);
        }

        private void searchByMfg()
        {
            List<Shipment> result = repository.SearchShipment( new List<string> { "MONTH(`shipment`.exp) - MONTH(CURRENT_DATE()) = 1", "YEAR(`shipment`.exp) = YEAR(CURRENT_DATE())" });
            LoadShipmentList(result);
        }

        private void searchByProductName(string searchValue)
        {
            List<Product> products = new List<Product>();
            products = productRepository.FindProducts("name", searchValue);
            List<Shipment> result = new List<Shipment>();
            foreach (Product product in products)
            {
                List<Shipment> shipments = repository.FindShipmentsBy(new Dictionary<string, object>() { { "product_id", product.Id } });
                if (shipments.Count > 0)
                    result.Add(shipments[0]);
            }
            LoadShipmentList(result);
        }

        private void ShowDetail(object? sender, EventArgs e)
        {
            DataGridViewRow selectedRow = this.view.Guna2DataGridView.SelectedRows[0];
            int id = Convert.ToInt16(selectedRow.Cells["Column1"].Value.ToString());
            Shipment shipment = repository.FindShipmentsBy(new Dictionary<string, object>() { { "id", id } })[0];
            IShowDetailShipmentView view = new FormDetailWareHouse(shipment);
            IShipmentRepository shipmentRepository = new ShipmentRepository();
            new ShowDetailShipmentPresenter(view, shipmentRepository);
            view.show();
        }
    }
}
