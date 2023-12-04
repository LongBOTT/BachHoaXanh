using BachHoaXanh._Repositories;
using BachHoaXanh.Models;
using BachHoaXanh.Views.InterfaceView;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BachHoaXanh.Presenters
{
    public class ShowDetailStaffPresenter
    {
        private IShowDetailStaffView view;
        private IStaffRepository repository;
        private Staff staff;
        public ShowDetailStaffPresenter(IShowDetailStaffView view, IStaffRepository repository) 
        {
            this.view = view;
            this.repository = repository;
            staff = this.view.GetStaff;
            this.view.ShowDetail += ShowDetail;
        }

        private void ShowDetail(object? sender, EventArgs e)
        {
            view.Guna2TextBoxID.Text = staff.Id.ToString();
            view.Guna2TextBoxName.Text = staff.Name.ToString();
            if (staff.Gender)
                view.Guna2RadioButtonMale.Checked = true;
            else 
                view.Guna2RadioButtonFeMale.Checked = true;
            view.Guna2TextBoxBirthdate.Text = staff.Birthday.ToString("yyyy-MM-dd");
            view.Guna2TextBoxPhone.Text = staff.Phone.ToString();
            view.Guna2TextBoxAddress.Text = staff.Address.ToString();
            view.Guna2TextBoxEmail.Text = staff.Email.ToString();
            view.Guna2TextBoxEntryDate.Text = staff.Entry_DateTime.ToString("yyyy-MM-dd");
        }
    }
}
