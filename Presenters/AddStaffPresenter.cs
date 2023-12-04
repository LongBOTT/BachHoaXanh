using BachHoaXanh._Repositories;
using BachHoaXanh.Models;
using BachHoaXanh.User_Control;
using BachHoaXanh.Views;
using BachHoaXanh.Views.InterfaceView;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
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
    }
}
