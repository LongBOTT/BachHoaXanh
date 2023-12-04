using BachHoaXanh._Repositories;
using BachHoaXanh.Models;
using BachHoaXanh.Views.InterfaceView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Presenters
{
    public class UpdateStaffPresenter
    {
        private IUpdateStaffView view;
        private IStaffRepository repository;
        private Staff staff;
        public UpdateStaffPresenter(IUpdateStaffView view, IStaffRepository repository)
        {
            this.view = view;
            this.repository = repository;
            staff = this.view.GetStaff;
            this.view.ShowDetail += ShowDetail;
            this.view.UpdateStaff += UpdateStaff;
            this.view.Refresh += Refresh;
        }

        private void ShowDetail(object? sender, EventArgs e)
        {
            view.Guna2TextBoxID.Text = staff.Id.ToString();
            view.Guna2TextBoxName.Text = staff.Name.ToString();
            if (staff.Gender)
                view.Guna2RadioButtonMale.Checked = true;
            else
                view.Guna2RadioButtonFeMale.Checked = true;
            view.Guna2TextBoxBirthdate.Value = staff.Birthday;
            view.Guna2TextBoxPhone.Text = staff.Phone.ToString();
            view.Guna2TextBoxAddress.Text = staff.Address.ToString();
            view.Guna2TextBoxEmail.Text = staff.Email.ToString();
            view.Guna2TextBoxEntryDate.Value = staff.Entry_DateTime;
        }

        private void UpdateStaff(object? sender, EventArgs e)
        {
            int id = Convert.ToInt16(view.Guna2TextBoxID.Text);
            string name = view.Guna2TextBoxName.Text;
            Boolean gender = view.Guna2RadioButtonMale.Checked;
            DateTime birthdate = view.Guna2TextBoxBirthdate.Value;
            string phone = view.Guna2TextBoxPhone.Text;
            string address = view.Guna2TextBoxAddress.Text;
            string email = view.Guna2TextBoxEmail.Text;
            DateTime entryDate = view.Guna2TextBoxEntryDate.Value;

            Staff account = new Staff(id, name, gender, birthdate, phone, address, email, entryDate, false);
            if (repository.Update(account) == 1)
            {
                view.Message = "Sửa nhân viên thành công!";
                view.close();
                StaffPresenter.repository = new StaffRepository();
                StaffPresenter.staffList = StaffPresenter.repository.GetAll();
                StaffPresenter.LoadStaffList(StaffPresenter.staffList);
            }
            else
            {
                view.Message = "Sửa nhân viên không thành công!";
            }

        }

        private void Refresh(object? sender, EventArgs e)
        {
            ShowDetail(sender, e);
        }
    }
}
