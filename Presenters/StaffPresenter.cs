using BachHoaXanh._Repositories;
using BachHoaXanh.Dialog;
using BachHoaXanh.Main;
using BachHoaXanh.Models;
using BachHoaXanh.Views;
using BachHoaXanh.Views.Dialog;
using BachHoaXanh.Views.InterfaceView;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Presenters
{
    public class StaffPresenter
    {
        private IStaffView view;
        public static IStaffRepository repository;
        public static IEnumerable<Staff> staffList;
        private static Guna2DataGridView Guna2DataGridView;
        public StaffPresenter(IStaffView view, IStaffRepository repository)
        {
            this.view = view;
            StaffPresenter.repository = repository;
            StaffPresenter.Guna2DataGridView = view.Guna2DataGridView;
            StaffPresenter.staffList = StaffPresenter.repository.GetAll();
            staffList = repository.GetAll();
            LoadStaffList(staffList);

            this.view.SearchEvent += SearchStaff;
            this.view.ShowDetail += ShowDetail;
            this.view.AddNewEvent += AddNewStaff;
            this.view.UpdateEvent += UpdateEvent;
            this.view.DeleteEvent += DeleteEvent;
        }

        public static void LoadStaffList(IEnumerable<Staff> staffs)
        {
            Guna2DataGridView.Rows.Clear();
            foreach (Staff staff in staffs)
            {
                Guna2DataGridView.Rows.Add(staff.Id, staff.Name, staff.Gender, staff.Birthday, staff.Phone, staff.Address, staff.Email);
            }
        }

        private void SearchStaff(object sender, EventArgs e)
        {
            bool emptyValue = string.IsNullOrWhiteSpace(this.view.SearchValue);
            if (!emptyValue)
            {
                string attribute = this.view.Attribute;
                if (attribute == "Tên nhân viên")
                    SearchByNameStaff(this.view.SearchValue);
                if (attribute == "SĐT")
                    SearchByPhoneStaff(this.view.SearchValue);
                if (attribute == "Email")
                    SearchByEmailStaff(this.view.SearchValue);
            }
            else
            {
                LoadStaffList(repository.GetAll());
            }
        }

        private void SearchByPhoneStaff(string searchValue)
        {
            List<Staff> result = repository.FindStaffs("phone", searchValue);
            LoadStaffList(result);
        }

        private void SearchByEmailStaff(string searchValue)
        {
            List<Staff> result = repository.FindStaffs("email", searchValue);
            LoadStaffList(result);
        }

        private void SearchByNameStaff(string searchValue)
        {
            List<Staff> result = repository.FindStaffs("name", searchValue);
            LoadStaffList(result);
        }

        private void ShowDetail(object? sender, EventArgs e)
        {
            DataGridViewRow selectedRow = this.view.Guna2DataGridView.SelectedRows[0];
            int id = Convert.ToInt16(selectedRow.Cells["Column1"].Value.ToString());
            Staff staff = repository.FindStaffsBy(new Dictionary<string, object>() { { "id", id } })[0];
            IShowDetailStaffView view = new FormDetailStaff(staff);
            IStaffRepository staffRepository = new StaffRepository();
            new ShowDetailStaffPresenter(view, staffRepository);
            view.show();
        }

        private void AddNewStaff(object? sender, EventArgs e)
        {
            IAddStaffView view = new FormAddStaff();
            IStaffRepository staffRepository = new StaffRepository();
            AddStaffPresenter addStaffPresenter = new AddStaffPresenter(view, staffRepository);
            view.show();
        }

        private void UpdateEvent(object? sender, EventArgs e)
        {
            DataGridViewRow selectedRow = this.view.Guna2DataGridView.SelectedRows[0];
            int id = Convert.ToInt16(selectedRow.Cells["Column1"].Value.ToString());
            Staff staff = repository.FindStaffsBy(new Dictionary<string, object>() { { "id", id } })[0];
            IUpdateStaffView view = new FormUpdateStaff(staff);
            IStaffRepository staffRepository = new StaffRepository();
            new UpdateStaffPresenter(view, staffRepository);
            view.show();
        }

        private void DeleteEvent(object? sender, EventArgs e)
        {
            DataGridViewRow selectedRow = this.view.Guna2DataGridView.SelectedRows[0];
            int id = Convert.ToInt16(selectedRow.Cells["Column1"].Value.ToString());
            string name = selectedRow.Cells["Column2"].Value.ToString();

            DialogResult result = MessageBox.Show("Xác nhận xóa nhân viên " + name + "?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                if (repository.Delete(new List<string> { " id = " + id }) == 1)
                {
                    MessageDialog.Show(MiniSupermarketApp.menu, "Xoá nhân viên thành công!", "Thông báo", MessageDialogButtons.OK, MessageDialogIcon.Information);
                    repository = new StaffRepository();
                    staffList = repository.GetAll();
                    LoadStaffList(staffList);
                }
                else
                {
                    MessageDialog.Show(MiniSupermarketApp.menu, "Xoá nhân viên không thành công", "Thông báo", MessageDialogButtons.OK, MessageDialogIcon.Information);
                }
            }
            //MessageDialog.Show(MiniSupermarketApp.menu, "Xoá nhân viên không thành công", "Thông báo", MessageDialogButtons.OK, MessageDialogIcon.Information);
        }
    }
}
