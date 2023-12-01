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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BachHoaXanh.Presenters
{
    public class AddAccountPresenter
    {
        private IAddAccountView view;
        private IAccountRepository repository;
        private IRoleRepository roleRepository;
        private IStaffRepository staffRepository;
        private List<Staff> staffs;
        private List<Role> roles;
        private Boolean flag;
        public AddAccountPresenter(IAddAccountView view, IAccountRepository repository) 
        {
            this.view = view;
            this.repository = repository;
            roleRepository = new RoleRepository();
            staffRepository = new StaffRepository();
            staffs = (List<Staff>)staffRepository.GetAll();
            roles = (List<Role>)roleRepository.GetAll();
            this.view.ShowDetail += ShowDetail;
            this.view.LoadListRole += LoadListRole;
            this.view.LoadListStaff += LoadListStaff;
            this.view.SelectedRow += SelectedRow;
            this.view.AddAccount += AddAccount;
            this.view.Refresh += Refresh;
        }

        private void LoadListStaff(object? sender, EventArgs e)
        {
            flag = false;
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
            }
             view.Guna2DataGridView.Visible = true;
            if (view.Guna2TextBoxStaffID.PlaceholderText != "Chọn nhân viên")
            {
                Staff staff = staffRepository.FindStaffsBy(new Dictionary<string, object>() { { "id", Convert.ToInt16(view.Guna2TextBoxStaffID.Text) } })[0];
                int index = Repository.GetIndex(staff, "id", staffs);
                view.Guna2DataGridView.Rows[index].Selected = true;
            }
        }

        private void LoadListRole(object? sender, EventArgs e)
        {
            flag = true;
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
            }
            view.Guna2DataGridView.Visible = true;
            if (view.Guna2TextBoxRoleID.PlaceholderText != "Chọn chức vụ")
            {
                Role role = roleRepository.FindRolesBy(new Dictionary<string, object>() { { "id", Convert.ToInt16(view.Guna2TextBoxRoleID.Text) } })[0];
                int index = Repository.GetIndex(role, "id", roles);
                view.Guna2DataGridView.Rows[index].Selected = true;
            }
        }

        private void ShowDetail(object? sender, EventArgs e)
        {
            view.Guna2TextBoxID.Text = repository.GetAutoID().ToString();
            view.Guna2TextBoxLastSignedIn.Text = DateTime.Now.ToString();
        }

        private void SelectedRow(object? sender, EventArgs e)
        {
            string id = "";
            if (view.Guna2DataGridView.SelectedRows.Count > 0)
            {
                id = view.Guna2DataGridView.SelectedRows[0].Cells[0].Value.ToString();
                if (flag)
                    view.Guna2TextBoxRoleID.Text = id;
                else
                    view.Guna2TextBoxStaffID.Text = id;
            }
        }

        private void AddAccount(object? sender, EventArgs e)
        {
            int id = Convert.ToInt16(view.Guna2TextBoxID.Text);
            string username = view.Guna2TextBoxUsername.Text;
            string password = view.Guna2TextBoxPassword.Text;
            int roleID = Convert.ToInt16(view.Guna2TextBoxRoleID.Text);
            DateTime lastSigneIn = Convert.ToDateTime(view.Guna2TextBoxLastSignedIn.Text);
            int staffID = Convert.ToInt16(view.Guna2TextBoxStaffID.Text);
            Account account = new Account(id, username, password, roleID, staffID, lastSigneIn);
            if (repository.Add(account) == 1)
            {
                view.Message = "Thêm tài khoản thành công!";
                view.close();
                AccountPresenter.repository = new AccountRepository();
                AccountPresenter.accountList = AccountPresenter.repository.GetAll();
                AccountPresenter.LoadAccountList(AccountPresenter.accountList);
            }
            else
            {
                view.Message = "Thêm tài khoản không thành công!";
            }
            
        }

        private void Refresh(object? sender, EventArgs e)
        {
            ShowDetail(sender, e);
            view.Guna2TextBoxUsername.Text = null;
            view.Guna2TextBoxPassword.Text = null;
            view.Guna2TextBoxRoleID.Text = null;
            view.Guna2TextBoxStaffID.Text = null;
        }
    }
}
