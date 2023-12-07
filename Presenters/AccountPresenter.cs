using BachHoaXanh._Repositories;
using BachHoaXanh.Main;
using BachHoaXanh.Models;
using BachHoaXanh.Views;
using BachHoaXanh.Views.Dialog;
using BachHoaXanh.Views.InterfaceView;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BachHoaXanh.Presenters
{
    public class AccountPresenter
    {
        private IAccountView view;
        public static IAccountRepository repository;
        public static IEnumerable<Account> accountList;
        private IStaffRepository staffRepository;
        private IRoleRepository roleRepository;
        private static Guna2DataGridView Guna2DataGridView;
        public AccountPresenter(IAccountView view, IAccountRepository repository)
        {
            this.view = view;
            AccountPresenter.repository = repository;
            AccountPresenter.Guna2DataGridView = view.Guna2DataGridView;
            AccountPresenter.accountList = AccountPresenter.repository.GetAll();
            staffRepository = new StaffRepository();
            roleRepository = new RoleRepository();
            LoadAccountList(accountList);

            this.view.SearchEvent += SearchAccount;
            this.view.ShowDetail += ShowDetail;
            this.view.AddNewEvent += AddNewEvent;
            this.view.UpdateEvent += UpdateEvent;
            this.view.DeleteEvent += DeleteEvent;
        }

        public static void LoadAccountList(IEnumerable<Account> accounts)
        {
            IStaffRepository staffRepository = new StaffRepository();
            IRoleRepository roleRepository = new RoleRepository();
            Guna2DataGridView.Rows.Clear();
            foreach (Account account in accounts)
            {
                Role role = roleRepository.FindRolesBy(new Dictionary<string, object>() { { "id", account.RoleID } })[0];
                Staff staff = staffRepository.FindStaffsBy(new Dictionary<string, object> () { { "id", account.StaffID} })[0];
                Guna2DataGridView.Rows.Add(account.Id, account.Username, role.Name, account.Last_signed_in, staff.Name);
            }
        }

        private void SearchAccount(object sender, EventArgs e)
        {
            bool emptyValue = string.IsNullOrWhiteSpace(this.view.SearchValue);
            if (!emptyValue)
            {
                string attribute = this.view.Attribute;
                if (attribute == "Tên tài khoản")
                    SearchByUseranme(this.view.SearchValue);
                if (attribute == "Tên chức vụ")
                    SearchByRoleName(this.view.SearchValue);
                if (attribute == "Tên nhân viên")
                    SearchByStaffName(this.view.SearchValue);
            }
            else
            {
                LoadAccountList(repository.GetAll());
            }
        }

        private void SearchByStaffName(string searchValue)
        {
            List<Staff> staffs = new List<Staff>();
            staffs = staffRepository.FindStaffs("name", searchValue);
            List<Account> result = new List<Account>();
            foreach (Staff staff in staffs)
            {
                List<Account> accounts = repository.FindAccountsBy(new Dictionary<string, object>() { { "staff_id", staff.Id } });
                if (accounts.Count > 0)
                    result.Add(accounts[0]);
            }
            LoadAccountList(result);
        }

        private void SearchByRoleName(string searchValue)
        {
            List<Role> roles = new List<Role>();
            roles = roleRepository.FindRoles("name", searchValue);
            List<Account> result = new List<Account>();
            foreach (Role role in roles)
            {
                List<Account> accounts = repository.FindAccountsBy(new Dictionary<string, object>() { { "role_id", role.Id } });
                if (accounts.Count > 0)
                    result.Add(accounts[0]);
            }
            LoadAccountList(result);
        }

        private void SearchByUseranme(string searchValue)
        {
            List<Account> result = repository.FindAccounts("username", searchValue);
            LoadAccountList(result);
        }

        private void ShowDetail(object? sender, EventArgs e)
        {
            DataGridViewRow selectedRow = this.view.Guna2DataGridView.SelectedRows[0];
            int id = Convert.ToInt16(selectedRow.Cells["Column1"].Value.ToString());
            Account account = repository.FindAccountsBy(new Dictionary<string, object> () { { "id", id} })[0];
            IShowDetailAccountView view = new FormDetailAccount(account);
            IAccountRepository accountRepository = new AccountRepository();
            new ShowDetailAccountPresenter(view, accountRepository);
            view.show();
        }

        private void AddNewEvent(object? sender, EventArgs e)
        {
            IAddAccountView view = new FormAddAccount();
            IAccountRepository accountRepository = new AccountRepository();
            AddAccountPresenter addAccountPresenter = new AddAccountPresenter(view, accountRepository);
            view.show();
        }

        private void UpdateEvent(object? sender, EventArgs e)
        {
            DataGridViewRow selectedRow = this.view.Guna2DataGridView.SelectedRows[0];
            int id = Convert.ToInt16(selectedRow.Cells["Column1"].Value.ToString());
            Account account = repository.FindAccountsBy(new Dictionary<string, object>() { { "id", id } })[0];
            IUpdateAccountView view = new FormUpdateAccount(account);
            IAccountRepository accountRepository = new AccountRepository();
            new UpdateAccountPresenter(view, accountRepository);
            view.show();
        }

        private void DeleteEvent(object? sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Xác nhận xóa tài khoản?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (result == DialogResult.OK)
            {
                DataGridViewRow selectedRow = this.view.Guna2DataGridView.SelectedRows[0];
                int id = Convert.ToInt16(selectedRow.Cells["Column1"].Value.ToString());
                if (repository.Delete(new List<string> { " id = " + id }) == 1)
                {
                    MessageDialog.Show(MiniSupermarketApp.menu, "Xoá tài khoản thành công!", "Thông báo", MessageDialogButtons.OK, MessageDialogIcon.Information);
                    repository = new AccountRepository();
                    accountList = repository.GetAll();
                    LoadAccountList(accountList);
                }
                else
                {
                    MessageDialog.Show(MiniSupermarketApp.menu, "Xoá tài khoản không thành công", "Thông báo", MessageDialogButtons.OK, MessageDialogIcon.Information);
                }
            }
            else
            {
                MessageDialog.Show(MiniSupermarketApp.menu, "Tài khoản chưa được xóa", "Thông báo", MessageDialogButtons.OK, MessageDialogIcon.Information);
            }
        }
    }
}
