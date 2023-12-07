using BachHoaXanh._Repositories;
using BachHoaXanh.Models;
using BachHoaXanh.User_Control;
using BachHoaXanh.Views;
using BachHoaXanh.Views.InterfaceView;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BachHoaXanh.Presenters
{
    public class AddStaffPresenter
    {
        private IAddStaffView view;
        private IStaffRepository repository;
        public AddStaffPresenter(IAddStaffView view, IStaffRepository repository)
        {
            this.view = view;
            this.repository = repository;
            this.view.ShowDetail += ShowDetail;
            this.view.AddStaff += AddStaff;
            this.view.Refresh += Refresh;
        }

        private void ShowDetail(object? sender, EventArgs e)
        {
            view.Guna2TextBoxID.Text = repository.GetAutoID().ToString();
            view.Guna2TextBoxBirthdate.Value = DateTime.Now;
            view.Guna2TextBoxEntryDate.Value = DateTime.Now;
        }

        private void AddStaff(object? sender, EventArgs e)
        {
            if (!checkInput())
            {
                return;
            }
            DialogResult result = MessageBox.Show("Xác nhận thêm nhân viên?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                int id = Convert.ToInt16(view.Guna2TextBoxID.Text);
                string name = view.Guna2TextBoxName.Text;
                Boolean gender = view.Guna2RadioButtonMale.Checked;
                DateTime birthdate = Convert.ToDateTime(view.Guna2TextBoxBirthdate.Value);
                string phone = view.Guna2TextBoxPhone.Text;
                string address = view.Guna2TextBoxAddress.Text;
                string email = view.Guna2TextBoxEmail.Text;
                DateTime entryDate = Convert.ToDateTime(view.Guna2TextBoxEntryDate.Value);

                Staff account = new Staff(id, name, gender, birthdate, phone, address, email, entryDate, false);
                if (repository.Add(account) == 1)
                {
                    view.Message = "Thêm nhân viên thành công!";
                    view.close();
                    StaffPresenter.repository = new StaffRepository();
                    StaffPresenter.staffList = StaffPresenter.repository.GetAll();
                    StaffPresenter.LoadStaffList(StaffPresenter.staffList);
                }
                else
                {
                    view.Message = "Thêm nhân viên không thành công!";
                }
            }
            else view.Message = "Thêm nhân viên không thành công";

        }

        private void Refresh(object? sender, EventArgs e)
        {
            ShowDetail(sender, e);
            view.Guna2TextBoxName.Text = "";
            view.Guna2RadioButtonMale.Checked = false;
            view.Guna2RadioButtonFeMale.Checked = false;
            view.Guna2TextBoxPhone.Text = "";
            view.Guna2TextBoxAddress.Text = "";
            view.Guna2TextBoxEmail.Text = "";
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
    }
}
