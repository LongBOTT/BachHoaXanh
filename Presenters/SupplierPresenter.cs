using BachHoaXanh.Models;
using BachHoaXanh.Views.InterfaceView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Presenters
{
    public class SupplierPresenter
    {
        private ISupplierView view;
        private ISupplierRepository repository;
        private IEnumerable<Supplier> supplierList;

        public SupplierPresenter(ISupplierView view, ISupplierRepository repository)
        {
            this.view = view;
            this.repository = repository;
            supplierList = repository.GetAll();
            LoadSupplierList(supplierList);

            this.view.SearchEvent += SearchSupplier;
            //this.view.AddNewEvent += AddNewSupplier;
            //this.view.EditEvent += LoadSelectedSupplierToEdit;
            //this.view.DeleteEvent += DeleteSelectedSupplier;
            //this.view.SaveEvent += SaveSupplier;
            //this.view.CancelEvent += CancelAction;
        }

        private void LoadSupplierList(IEnumerable<Supplier> suppliers)
        {
            view.Guna2DataGridView.Rows.Clear();
            foreach (Supplier supplier in suppliers)
            {
                view.Guna2DataGridView.Rows.Add(supplier.Id, supplier.Name, supplier.Phone, supplier.Address, supplier.Email);
            }
        }

        private void SearchSupplier(object sender, EventArgs e)
        {
            bool emptyValue = string.IsNullOrWhiteSpace(this.view.SearchValue);
            if (!emptyValue)
            {
                string attribute = this.view.Attribute;
                if (attribute == "Tên nhà cung cấp")
                    SearchByNameSupplier(this.view.SearchValue);
                if (attribute == "SĐT")
                    SearchByPhoneSupplier(this.view.SearchValue);
                if (attribute == "Email")
                    SearchByEmailSupplier(this.view.SearchValue);
            }
            else
            {
                LoadSupplierList(repository.GetAll());
            }
        }

        private void SearchByPhoneSupplier(string searchValue)
        {
            List<Supplier> result = this.repository.FindSuppliers("phone", searchValue);
            LoadSupplierList(result);
        }

        private void SearchByEmailSupplier(string searchValue)
        {
            List<Supplier> result = this.repository.FindSuppliers("email", searchValue);
            LoadSupplierList(result);
        }

        private void SearchByNameSupplier(string searchValue)
        {
            List<Supplier> result = this.repository.FindSuppliers("name", searchValue);
            LoadSupplierList(result);
        }

    }
}
