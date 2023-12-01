using BachHoaXanh.Models;
using BachHoaXanh.Views.InterfaceView;
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
        private IStaffRepository repository;
        private IEnumerable<Staff> staffList;

        public StaffPresenter(IStaffView view, IStaffRepository repository)
        {
            this.view = view;
            this.repository = repository;
            staffList = repository.GetAll();
            LoadStaffList(staffList);

            this.view.SearchEvent += SearchStaff;
            //this.view.AddNewEvent += AddNewStaff;
            //this.view.EditEvent += LoadSelectedStaffToEdit;
            //this.view.DeleteEvent += DeleteSelectedStaff;
            //this.view.SaveEvent += SaveStaff;
            //this.view.CancelEvent += CancelAction;
        }

        private void LoadStaffList(IEnumerable<Staff> staffs)
        {
            view.Guna2DataGridView.Rows.Clear();
            foreach (Staff staff in staffs)
            {
                view.Guna2DataGridView.Rows.Add(staff.Id, staff.Name, staff.Gender, staff.Birthday, staff.Phone, staff.Address, staff.Email);
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
            List<Staff> result = this.repository.FindStaffs("phone", searchValue);
            LoadStaffList(result);
        }

        private void SearchByEmailStaff(string searchValue)
        {
            List<Staff> result = this.repository.FindStaffs("email", searchValue);
            LoadStaffList(result);
        }

        private void SearchByNameStaff(string searchValue)
        {
            List<Staff> result = this.repository.FindStaffs("name", searchValue);
            LoadStaffList(result);
        }
    }
}
