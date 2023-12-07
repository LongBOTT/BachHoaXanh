using BachHoaXanh._Repositories;
using BachHoaXanh.Dialog;
using BachHoaXanh.Main;
using BachHoaXanh.Models;
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
    public class BillPresenter
    {
        private IBillView view;
        private IReceiptRepository repository;
        private IStaffRepository staffRepository;
        private IEnumerable<Receipt> receiptList;
        public BillPresenter(IBillView  view, IReceiptRepository repository) {
            this.view = view;
            this.repository = repository;
            staffRepository = new StaffRepository();
            receiptList = this.repository.GetAll();
            LoadReceiptList(receiptList);

            this.view.SortEvent += SortEvent;
            this.view.SearchEvent += SearchReceipt;
            this.view.ShowDetail += ShowDetail;
            SortEvent(new object(), new EventArgs());
        }

        private void SortEvent(object? sender, EventArgs e)
        {
            string attribute = this.view.Attribute;
            if (attribute == "Sắp xếp theo ngày lập tăng dần")
            {
                var immutableSortedSet = receiptList.ToImmutableSortedSet(
                                            Comparer<Receipt>.Create((r1, r2) => r1.Invoice_DateTime.CompareTo(r2.Invoice_DateTime)));
                List<Receipt> list = new List<Receipt> { };
                foreach (var receipt in immutableSortedSet) 
                {
                    Receipt receipt1 = new Receipt();
                    receipt1.Id = receipt.Id;
                    receipt1.Staff_id = receipt.Staff_id;
                    receipt1.Invoice_DateTime = receipt.Invoice_DateTime;
                    receipt1.Total = receipt.Total;
                    receipt1.Received = receipt.Received;
                    receipt1.Excess = receipt.Excess;
                    list.Add(receipt1);
                }
                receiptList = list;
                LoadReceiptList(receiptList);
            } 
            else
            {
                var immutableSortedSet = receiptList.ToImmutableSortedSet(
                                           Comparer<Receipt>.Create((r1, r2) => r2.Invoice_DateTime.CompareTo(r1.Invoice_DateTime)));
                List<Receipt> list = new List<Receipt> { };
                foreach (var receipt in immutableSortedSet) 
                {
                    Receipt receipt1 = new Receipt();
                    receipt1.Id = receipt.Id;
                    receipt1.Staff_id = receipt.Staff_id;
                    receipt1.Invoice_DateTime = receipt.Invoice_DateTime;
                    receipt1.Total = receipt.Total;
                    receipt1.Received = receipt.Received;
                    receipt1.Excess = receipt.Excess;
                    list.Add(receipt1);
                }
                receiptList = list;
                LoadReceiptList(receiptList);
            }
        }

        private void ShowDetail(object? sender, EventArgs e)
        {
            DataGridViewRow selectedRow = this.view.Guna2DataGridView.SelectedRows[0];
            int id = Convert.ToInt16(selectedRow.Cells["Column1"].Value.ToString());
            Receipt receipt = repository.FindReceiptsBy(new Dictionary<string, object>() { { "id", id } })[0];
            IShowDetailReceiptView view = new FormDetailBill(receipt);
            IReceipt_detailRepository receiptRepository = new Receipt_detailRepository();
            new ShowDetailReceiptPresenter(view, receiptRepository);
            view.show();
        }

        private void SearchReceipt(object? sender, EventArgs e)
        {
            receiptList = repository.GetAll();
            string staffName = this.view.SearchValue;
            DateTime startDate = view.Guna2DateTimePickerStart.Value;
            DateTime endDate = view.Guna2DateTimePickerEnd.Value;

            if (staffName != "")
            {
                List<Receipt> list = new List<Receipt> { };
                foreach (Receipt receipt in receiptList) 
                {
                    Staff staff = staffRepository.FindStaffsBy(new Dictionary<string, object>() { { "id", receipt.Staff_id} })[0];
                    if (staff.Name.ToLower().Contains(staffName.ToLower()))
                    {
                        list.Add(receipt);
                    }
                }
                receiptList = list;
            }


            if (startDate > endDate)
            {
                MessageDialog.Show(MiniSupermarketApp.menu, "Ngày kết thúc phải sau ngày bắt đầu", "Thông báo", MessageDialogButtons.OK, MessageDialogIcon.Information);
                return;
            }
            List<Receipt> receipts = new List<Receipt> { };
            foreach (Receipt receipt in receiptList)
            {
                if (startDate <= receipt.Invoice_DateTime && receipt.Invoice_DateTime <= endDate)
                    receipts.Add(receipt);
            }
            receiptList = receipts;
            LoadReceiptList(receiptList);
            SortEvent(new object(), new EventArgs());
        }

        private void LoadReceiptList (IEnumerable<Receipt> receipts)
        {
            view.Guna2DataGridView.Rows.Clear();
            foreach (Receipt receipt in receipts)
            {
                Staff staff = staffRepository.FindStaffsBy(new Dictionary<string, object>() { { "id", receipt.Staff_id } })[0];
                view.Guna2DataGridView.Rows.Add(receipt.Id, staff.Name, receipt.Invoice_DateTime, receipt.Total);
            }
        }
    }
}
