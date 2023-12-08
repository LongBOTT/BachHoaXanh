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
    public class ExportPresenter
    {
        private static IExportView view;
        private static IExportRepository repository;
        private IStaffRepository staffRepository;
        private ISupplierRepository supplierRepository;
        private IEnumerable<Export> exportList;
        public ExportPresenter(IExportView  view, IExportRepository repository) {
            ExportPresenter.view = view;
            ExportPresenter.repository = repository;
            staffRepository = new StaffRepository();
            supplierRepository = new SupplierRepository();
            exportList = ExportPresenter.repository.GetAll();
            LoadExportList(exportList);

            ExportPresenter.view.SortEvent += SortEvent;
            ExportPresenter.view.SearchEvent += SearchExport;
            ExportPresenter.view.ShowDetail += ShowDetail;
            ExportPresenter.view.AddNewEvent += AddNewEvent;
            SortEvent(new object(), new EventArgs());
        }

        private void AddNewEvent(object? sender, EventArgs e)
        {
            MessageDialog.Show(MiniSupermarketApp.menu, "Chức năng đang phát triển", "Thông báo", MessageDialogButtons.OK, MessageDialogIcon.Information);

            //IAddExportView view = new FormAddExport();
            //IExportRepository exportRepository = new ExportRepository();
            //AddExportPresenter addExportPresenter = new AddExportPresenter(view, exportRepository);
            //view.show();
        }

        private void SortEvent(object? sender, EventArgs e)
        {
            string attribute = ExportPresenter.view.Attribute;
            if (attribute == "Sắp xếp theo ngày lập tăng dần")
            {
                var immutableSortedSet = exportList.ToImmutableSortedSet(
                                            Comparer<Export>.Create((r1, r2) => r1.Invoice_DateTime.CompareTo(r2.Invoice_DateTime)));
                List<Export> list = new List<Export> { };
                foreach (var export in immutableSortedSet) 
                {
                    Export export1 = new Export();
                    export1.Id = export.Id;
                    export1.Staff_id = export.Staff_id;
                    export1.Invoice_DateTime = export.Invoice_DateTime;
                    export1.Total = export.Total;
                    export1.Reason = export.Reason;
                    list.Add(export1);
                }
                exportList = list;
                LoadExportList(exportList);
            } 
            else
            {
                var immutableSortedSet = exportList.ToImmutableSortedSet(
                                           Comparer<Export>.Create((r1, r2) => r2.Invoice_DateTime.CompareTo(r1.Invoice_DateTime)));
                List<Export> list = new List<Export> { };
                foreach (var export in immutableSortedSet) 
                {
                    Export export1 = new Export();
                    export1.Id = export.Id;
                    export1.Staff_id = export.Staff_id;
                    export1.Invoice_DateTime = export.Invoice_DateTime;
                    export1.Total = export.Total;
                    export1.Reason = export.Reason;
                    list.Add(export1);
                }
                exportList = list;
                LoadExportList(exportList);
            }
        }

        private void ShowDetail(object? sender, EventArgs e)
        {
            DataGridViewRow selectedRow = ExportPresenter.view.Guna2DataGridView.SelectedRows[0];
            int id = Convert.ToInt16(selectedRow.Cells["Column1"].Value.ToString());
            Export export = repository.FindExportsBy(new Dictionary<string, object>() { { "id", id } })[0];
            IShowDetailExportView view = new FormDetailExport(export);
            IExport_detailRepository exportdetailRepository = new Export_detailRepository();
            new ShowDetailExportPresenter(view, exportdetailRepository);
            view.show();
        }

        private void SearchExport(object? sender, EventArgs e)
        {
            exportList = repository.GetAll();
            string staffName = ExportPresenter.view.SearchValue;
            DateTime startDate = view.Guna2DateTimePickerStart.Value;
            DateTime endDate = view.Guna2DateTimePickerEnd.Value;

            if (staffName != "")
            {
                List<Export> list = new List<Export> { };
                foreach (Export export in exportList) 
                {
                    Staff staff = staffRepository.FindStaffsBy(new Dictionary<string, object>() { { "id", export.Staff_id} })[0];
                    if (staff.Name.ToLower().Contains(staffName.ToLower()))
                    {
                        list.Add(export);
                    }
                }
                exportList = list;
            }

            if (startDate > endDate)
            {
                MessageDialog.Show(MiniSupermarketApp.menu, "Ngày kết thúc phải sau ngày bắt đầu", "Thông báo", MessageDialogButtons.OK, MessageDialogIcon.Information);
                return;
            }
            List<Export> exports = new List<Export> { };
            foreach (Export export in exportList)
            {
                if (startDate <= export.Invoice_DateTime && export.Invoice_DateTime <= endDate)
                    exports.Add(export);
            }
            exportList = exports;
            LoadExportList(exportList);
            SortEvent(new object(), new EventArgs());
        }

        public static void LoadExportList (IEnumerable<Export> exports)
        {
            view.Guna2DataGridView.Rows.Clear();
            foreach (Export export in exports)
            {
                IStaffRepository staffRepository = new StaffRepository();
                Staff staff = staffRepository.FindStaffsBy(new Dictionary<string, object>() { { "id", export.Staff_id } })[0];
                view.Guna2DataGridView.Rows.Add(export.Id, staff.Name, export.Invoice_DateTime, export.Total, export.Reason);
            }
        }
    }
}
