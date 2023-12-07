using BachHoaXanh._Repositories;
using BachHoaXanh.Dialog;
using BachHoaXanh.Main;
using BachHoaXanh.Models;
using BachHoaXanh.Views;
using BachHoaXanh.Views.InterfaceView;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Presenters
{
    public class ImportPresenter
    {
        private static IImportView view;
        private static IImportRepository repository;
        private IStaffRepository staffRepository;
        private ISupplierRepository supplierRepository;
        private IEnumerable<Import> importList;
        public ImportPresenter(IImportView  view, IImportRepository repository) {
            ImportPresenter.view = view;
            ImportPresenter.repository = repository;
            staffRepository = new StaffRepository();
            supplierRepository = new SupplierRepository();
            importList = ImportPresenter.repository.GetAll();
            LoadImportList(importList);

            ImportPresenter.view.SortEvent += SortEvent;
            ImportPresenter.view.SearchEvent += SearchImport;
            ImportPresenter.view.ShowDetail += ShowDetail;
            ImportPresenter.view.AddNewEvent += AddNewEvent;
            SortEvent(new object(), new EventArgs());
        }

        private void AddNewEvent(object? sender, EventArgs e)
        {
            IAddImportView view = new FormAddImport();
            IImportRepository importRepository = new ImportRepository();
            AddImportPresenter addImportPresenter = new AddImportPresenter(view, importRepository);
            view.show();
        }

        private void SortEvent(object? sender, EventArgs e)
        {
            string attribute = ImportPresenter.view.Attribute;
            if (attribute == "Sắp xếp theo ngày lập tăng dần")
            {
                var immutableSortedSet = importList.ToImmutableSortedSet(
                                            Comparer<Import>.Create((r1, r2) => r1.Received_DateTime.CompareTo(r2.Received_DateTime)));
                List<Import> list = new List<Import> { };
                foreach (var import in immutableSortedSet) 
                {
                    Import import1 = new Import();
                    import1.Id = import.Id;
                    import1.Staff_id = import.Staff_id;
                    import1.Received_DateTime = import.Received_DateTime;
                    import1.Total = import.Total;
                    import1.Supplier_id = import.Supplier_id;
                    list.Add(import1);
                }
                importList = list;
                LoadImportList(importList);
            } 
            else
            {
                var immutableSortedSet = importList.ToImmutableSortedSet(
                                           Comparer<Import>.Create((r1, r2) => r2.Received_DateTime.CompareTo(r1.Received_DateTime)));
                List<Import> list = new List<Import> { };
                foreach (var import in immutableSortedSet) 
                {
                    Import import1 = new Import();
                    import1.Id = import.Id;
                    import1.Staff_id = import.Staff_id;
                    import1.Received_DateTime = import.Received_DateTime;
                    import1.Total = import.Total;
                    import1.Supplier_id = import.Supplier_id;
                    list.Add(import1);
                }
                importList = list;
                LoadImportList(importList);
            }
        }

        private void ShowDetail(object? sender, EventArgs e)
        {
            DataGridViewRow selectedRow = ImportPresenter.view.Guna2DataGridView.SelectedRows[0];
            int id = Convert.ToInt16(selectedRow.Cells["Column1"].Value.ToString());
            Import import = repository.FindImportsBy(new Dictionary<string, object>() { { "id", id } })[0];
            IShowDetailImportView view = new FormDetailImport(import);
            IShipmentRepository importRepository = new ShipmentRepository();
            new ShowDetailImportPresenter(view, importRepository);
            view.show();
        }

        private void SearchImport(object? sender, EventArgs e)
        {
            importList = repository.GetAll();
            string staffName = ImportPresenter.view.SearchValue;
            DateTime startDate = view.Guna2DateTimePickerStart.Value;
            DateTime endDate = view.Guna2DateTimePickerEnd.Value;

            if (staffName != "")
            {
                List<Import> list = new List<Import> { };
                foreach (Import import in importList) 
                {
                    Staff staff = staffRepository.FindStaffsBy(new Dictionary<string, object>() { { "id", import.Staff_id} })[0];
                    if (staff.Name.ToLower().Contains(staffName.ToLower()))
                    {
                        list.Add(import);
                    }
                }
                importList = list;
            }

            if (startDate > endDate)
            {
                MessageDialog.Show(MiniSupermarketApp.menu, "Ngày kết thúc phải sau ngày bắt đầu", "Thông báo", MessageDialogButtons.OK, MessageDialogIcon.Information);
                return;
            }
            List<Import> imports = new List<Import> { };
            foreach (Import import in importList)
            {
                if (startDate <= import.Received_DateTime && import.Received_DateTime <= endDate)
                    imports.Add(import);
            }
            importList = imports;
            LoadImportList(importList);
            SortEvent(new object(), new EventArgs());
        }

        public static void LoadImportList (IEnumerable<Import> imports)
        {
            view.Guna2DataGridView.Rows.Clear();
            foreach (Import import in imports)
            {
                IStaffRepository staffRepository = new StaffRepository();
                ISupplierRepository supplierRepository = new SupplierRepository();
                Staff staff = staffRepository.FindStaffsBy(new Dictionary<string, object>() { { "id", import.Staff_id } })[0];
                Supplier supplier = supplierRepository.FindSuppliersBy(new Dictionary<string, object>() { { "id", import.Supplier_id } })[0];
                view.Guna2DataGridView.Rows.Add(import.Id, staff.Name, import.Received_DateTime, import.Total, supplier.Name);
            }
        }
    }
}
