using BachHoaXanh._Repositories;
using BachHoaXanh.Models;
using BachHoaXanh.Views.InterfaceView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

            if (!checkInput())
            {
                return;
            }

            Staff account = new Staff(id, name, gender, birthdate, phone, address, email, entryDate, false);
            DialogResult result = MessageBox.Show("Xác nhận sửa thông tin nhân viên " + name + "?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
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
            else view.Message = "Sửa nhân viên không thành công!";

        }

        private bool checkInput()
        {
            string name = view.Guna2TextBoxName.Text;
            Boolean gender = view.Guna2RadioButtonMale.Checked;
            DateTime birthdate = Convert.ToDateTime(view.Guna2TextBoxBirthdate.Value);
            string phone = view.Guna2TextBoxPhone.Text;
            string address = view.Guna2TextBoxAddress.Text;
            string email = view.Guna2TextBoxEmail.Text;
            DateTime entryDate = Convert.ToDateTime(view.Guna2TextBoxEntryDate.Value);

            /* var textFields = new List<Guna2TextBox>()
            {
                view.Guna2TextBoxName,
                view.Guna2TextBoxPhone,
                view.Guna2TextBoxAddress,
                view.Guna2TextBoxEmail
            };

            foreach (var textField in textFields)
            {
                if (string.IsNullOrEmpty(textField.Text))
                {
                    view.Message = "Vui lòng điền đầy đủ thông tin!";
                    textField.Focus();
                    return false;
                }
            } */

            if (string.IsNullOrEmpty(name))
            {
                view.Message = "Tên không được để trống.";
                view.Guna2TextBoxName.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(phone))
            {
                view.Message = "Số điện thoại không được để trống.";
                view.Guna2TextBoxPhone.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(address))
            {
                view.Message = "Địa chỉ không được để trống.";
                view.Guna2TextBoxAddress.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(email))
            {
                view.Message = "Email không được để trống.";
                view.Guna2TextBoxEmail.Focus();
                return false;
            }

            if (!Regex.IsMatch(name, @"^[a-zA-ZÀ-ỹẠ-ỵĂăÂâĐđÊêÔôƠơƯư\s]*[a-zA-ZÀ-ỹẠ-ỵĂăÂâĐđÊêÔôƠơƯư][a-zA-ZÀ-ỹẠ-ỵĂăÂâĐđÊêÔôƠơƯư\s]*$"))
            {
                view.Message = "Tên nhân viên không hợp lệ!";
                view.Guna2TextBoxName.Text = "";
                view.Guna2TextBoxName.Focus();
                return false;
            }

            if (!Regex.IsMatch(phone, @"^(0)[35789]\d{8}$"))
            {
                view.Message = "Số điện thoại phải có 10 chữ số và bắt đầu từ \"0x\"\nvới \"x\" thuộc {3, 5, 7, 8, 9}";
                view.Guna2TextBoxPhone.Text = "";
                view.Guna2TextBoxPhone.Focus();
                return false;
            }

            if (!Regex.IsMatch(email, @"^\w+(\.\w+)*@\w+(\.\w+)+$"))
            {
                view.Message = "Email phải theo định dạng username@domain.name";
                view.Guna2TextBoxEmail.Text = "";
                view.Guna2TextBoxEmail.Focus();
                return false;
            }

            DateTime currentDate = DateTime.Now;
            DateTime minBirthdate = currentDate.AddYears(-18);

            if (birthdate >= currentDate)
            {
                view.Message = "Ngày sinh phải trước ngày hiện tại.";
                view.Guna2TextBoxBirthdate.Focus();
                return false;
            }

            if (birthdate > minBirthdate)
            {
                view.Message = "Nhân viên phải trên 18 tuổi.";
                view.Guna2TextBoxBirthdate.Focus();
                return false;
            }

            if (entryDate >= currentDate)
            {
                view.Message = "Ngày vào làm phải trước ngày hiện tại.";
                view.Guna2TextBoxEntryDate.Text = currentDate.ToString();
                view.Guna2TextBoxEntryDate.Focus();
                return false;
            }

            if (birthdate >= entryDate)
            {
                view.Message = "Ngày sinh phải trước ngày vào làm.";
                view.Guna2TextBoxEntryDate.Text = birthdate.ToString();
                view.Guna2TextBoxEntryDate.Focus();
                return false;
            }

            return true;
        }

        private void Refresh(object? sender, EventArgs e)
        {
            ShowDetail(sender, e);
        }
    }
}
