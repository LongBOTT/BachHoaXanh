using BachHoaXanh._Repositories;
using BachHoaXanh.Models;
using BachHoaXanh.Views.InterfaceView;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BachHoaXanh.Presenters
{
    public class ShowDetailAccountPresenter
    {
        private IShowDetailAccountView view;
        private IAccountRepository repository;
        private Account account;
        private IRoleRepository roleRepository;
        private IStaffRepository staffRepository;
        private List<Staff> staffs;
        private List<Role> roles;
        public ShowDetailAccountPresenter(IShowDetailAccountView view, IAccountRepository repository) 
        {
            this.view = view;
            this.repository = repository;
            roleRepository = new RoleRepository();
            staffRepository = new StaffRepository();
            staffs = (List<Staff>)staffRepository.GetAll();
            roles = (List<Role>)roleRepository.GetAll();
            account = this.view.GetAccount;
            this.view.ShowDetail += ShowDetail;
            this.view.LoadListRole += LoadListRole;
            this.view.LoadListStaff += LoadListStaff;
        }

        private void LoadListStaff(object? sender, EventArgs e)
        {
            int index = -1;
            DataGridViewColumn column1 = new DataGridViewTextBoxColumn();
            DataGridViewColumn column2 = new DataGridViewTextBoxColumn();
            DataGridViewColumn column3 = new DataGridViewTextBoxColumn();
            column1.HeaderText = "Mã nhân viên";
            column2.HeaderText = "Tên nhân viên";
            column3.HeaderText = "SĐT";
            view.Guna2DataGridView.Columns.Clear();
            view.Guna2DataGridView.Rows.Clear();
            view.Guna2DataGridView.Columns.AddRange(column1, column2, column3);
            
            foreach (Staff staff in staffs)
            {
                view.Guna2DataGridView.Rows.Add(staff.Id, staff.Name, staff.Phone);
                if (staff.Id == account.StaffID)
                    index = view.Guna2DataGridView.RowCount - 1;
            }
             view.Guna2DataGridView.Visible = true;
             view.Guna2DataGridView.Rows[index].Selected = true;
        }

        private void LoadListRole(object? sender, EventArgs e)
        {
            int index = -1;
            DataGridViewColumn column1 = new DataGridViewTextBoxColumn();
            DataGridViewColumn column2 = new DataGridViewTextBoxColumn();
            column1.HeaderText = "Mã chức vụ";
            column2.HeaderText = "Tên chức vụ";
            view.Guna2DataGridView.Columns.Clear();
            view.Guna2DataGridView.Rows.Clear();
            view.Guna2DataGridView.Columns.AddRange(column1, column2);

            foreach (Role role in roles)
            {
                view.Guna2DataGridView.Rows.Add(role.Id, role.Name);
                if (role.Id == account.RoleID)
                    index = view.Guna2DataGridView.RowCount - 1;
            }
            view.Guna2DataGridView.Visible = true;
            view.Guna2DataGridView.Rows[index].Selected = true;
        }

        private void ShowDetail(object? sender, EventArgs e)
        {
            view.Guna2TextBoxID.Text = account.Id.ToString();
            view.Guna2TextBoxUsername.Text = account.Username.ToString();
            view.Guna2TextBoxPassword.Text = account.Password.ToString();
            view.Guna2TextBoxRoleID.Text = account.RoleID.ToString();
            view.Guna2TextBoxLastSignedIn.Text = account.Last_signed_in.ToString();
            view.Guna2TextBoxStaffID.Text = account.StaffID.ToString();
        }
    }
}
