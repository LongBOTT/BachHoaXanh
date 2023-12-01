using BachHoaXanh.Models;
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
        private IEnumerable<Shipment> shipmentList;

        public ShipmentPresenter(IShipmentView view, IShipmentRepository repository)
        {
            this.view = view;
            this.repository = repository;
            shipmentList = repository.GetAll();
            LoadShipmentList(shipmentList);

            this.view.SearchEvent += SearchShipment;
            //this.view.AddNewEvent += AddNewShipment;
            //this.view.EditEvent += LoadSelectedShipmentToEdit;
            //this.view.DeleteEvent += DeleteSelectedShipment;
            //this.view.SaveEvent += SaveShipment;
            //this.view.CancelEvent += CancelAction;
        }

        private void LoadShipmentList(IEnumerable<Shipment> shipments)
        {
            view.Guna2DataGridView.Rows.Clear();
            foreach (Shipment shipment in shipments)
            {
                view.Guna2DataGridView.Rows.Add(shipment.Id, shipment.Product_id, shipment.Unit_price, shipment.Quantity, shipment.Remain, shipment.Mfg, shipment.Exp, DateTime.Now);
            }
        }

        private void SearchShipment(object sender, EventArgs e)
        {
            bool emptyValue = string.IsNullOrWhiteSpace(this.view.SearchValue);
            if (!emptyValue)
            {
                string attribute = this.view.Attribute;
                //if (attribute == "Tên ")
                //    searchByUseranme(this.view.SearchValue);
            }
            else
            {
                LoadShipmentList(repository.GetAll());
            }
        }

        private void searchByUseranme(string searchValue)
        {
            List<Shipment> result = this.repository.FindShipments("name", searchValue);
            LoadShipmentList(result);
        }
    }
}
