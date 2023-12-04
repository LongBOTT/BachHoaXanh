using BachHoaXanh._Repositories;
using BachHoaXanh.Dialog;
using BachHoaXanh.Main;
using BachHoaXanh.Models;
using BachHoaXanh.Views.InterfaceView;
using Guna.UI2.WinForms;
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
        public static ISupplierRepository repository;
        public static IEnumerable<Supplier> supplierList;
        private static Guna2DataGridView Guna2DataGridView;

        public SupplierPresenter(ISupplierView view, ISupplierRepository repository)
        {
            this.view = view;
            SupplierPresenter.repository = repository;
            SupplierPresenter.Guna2DataGridView = view.Guna2DataGridView;
            SupplierPresenter.supplierList = SupplierPresenter.repository.GetAll();
            supplierList = repository.GetAll();
            LoadSupplierList(supplierList);

            this.view.SearchEvent += SearchSupplier;
            this.view.ShowDetail += ShowDetail;
            this.view.AddNewEvent += AddNewStaff;
            this.view.UpdateEvent += UpdateEvent;
            this.view.DeleteEvent += DeleteEvent;
        }

        public static void LoadSupplierList(IEnumerable<Supplier> suppliers)
        {
            Guna2DataGridView.Rows.Clear();
            foreach (Supplier supplier in suppliers)
            {
                Guna2DataGridView.Rows.Add(supplier.Id, supplier.Name, supplier.Phone, supplier.Address, supplier.Email);
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
            List<Supplier> result = repository.FindSuppliers("phone", searchValue);
            LoadSupplierList(result);
        }

        private void SearchByEmailSupplier(string searchValue)
        {
            List<Supplier> result = repository.FindSuppliers("email", searchValue);
            LoadSupplierList(result);
        }

        private void SearchByNameSupplier(string searchValue)
        {
            List<Supplier> result = repository.FindSuppliers("name", searchValue);
            LoadSupplierList(result);
        }

        private void ShowDetail(object? sender, EventArgs e)
        {
            DataGridViewRow selectedRow = this.view.Guna2DataGridView.SelectedRows[0];
            int id = Convert.ToInt16(selectedRow.Cells["Column1"].Value.ToString());
            Supplier supplier = repository.FindSuppliersBy(new Dictionary<string, object>() { { "id", id } })[0];
            IShowDetailSupplierView view = new FormDetailSupplier(supplier);
            ISupplierRepository supplierRepository = new SupplierRepository();
            new ShowDetailSupplierPresenter(view, supplierRepository);
            view.show();
        }

        private void AddNewStaff(object? sender, EventArgs e)
        {
            IAddSupplierView view = new FormAddSupplier();
            ISupplierRepository supplierRepository = new SupplierRepository();
            AddSupplierPresenter addSupplierPresenter = new AddSupplierPresenter(view, supplierRepository);
            view.show();
        }

        private void UpdateEvent(object? sender, EventArgs e)
        {
            DataGridViewRow selectedRow = this.view.Guna2DataGridView.SelectedRows[0];
            int id = Convert.ToInt16(selectedRow.Cells["Column1"].Value.ToString());
            Supplier supplier = repository.FindSuppliersBy(new Dictionary<string, object>() { { "id", id } })[0];
            IUpdateSupplierView view = new FormUpdateSupplier(supplier);
            ISupplierRepository supplierRepository = new SupplierRepository();
            new UpdateSupplierPresenter(view, supplierRepository);
            view.show();
        }



        private void DeleteEvent(object? sender, EventArgs e)
        {
            DataGridViewRow selectedRow = this.view.Guna2DataGridView.SelectedRows[0];
            int id = Convert.ToInt16(selectedRow.Cells["Column1"].Value.ToString());
            if (repository.Delete(new List<string> { " id = " + id }) == 1)
            {
                MessageDialog.Show(MiniSupermarketApp.menu, "Xoá nhà cung cấp thành công!", "Thông báo", MessageDialogButtons.OK, MessageDialogIcon.Information);
                repository = new SupplierRepository();
                supplierList = repository.GetAll();
                LoadSupplierList(supplierList);
            }
            else
            {
                MessageDialog.Show(MiniSupermarketApp.menu, "Xoá nhà cung cấp không thành công", "Thông báo", MessageDialogButtons.OK, MessageDialogIcon.Information);
            }
        }
    }
}
