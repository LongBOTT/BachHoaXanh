using BachHoaXanh;
using BachHoaXanh._Repositories;
using BachHoaXanh.Main;
using BachHoaXanh.Models;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bachhoaxanh
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Login();
        }

        private void Login()
        {
            AccountRepository accountRepository = new AccountRepository();
            string username = tbAccount.Text;
            string password = tbPass.Text;
            if (username == null)
            {
                MessageDialog.Show(MiniSupermarketApp.menu, "Tên tài khoản không được bỏ trống!", "Thông báo", MessageDialogButtons.OK, MessageDialogIcon.Information);
                return;
            }
            if (password == null)
            {
                MessageDialog.Show(MiniSupermarketApp.menu, "Mật khẩu không được bỏ trống!", "Thông báo", MessageDialogButtons.OK, MessageDialogIcon.Information);
                return;

            }
            List<Account> accounts = accountRepository.FindAccountsBy(new Dictionary<string, object>() { { "username", username } });
            if (accounts.Count == 0)
            {
                MessageDialog.Show(MiniSupermarketApp.menu, "Tên tài khoản không tồn tại!", "Thông báo", MessageDialogButtons.OK, MessageDialogIcon.Information);
                return;

            }
            else
            {
                Account account = accounts[0];
                if (account.Password != password)
                {
                    MessageDialog.Show(MiniSupermarketApp.menu, "Mật khẩu không trùng khớp!", "Thông báo", MessageDialogButtons.OK, MessageDialogIcon.Information);
                    return;
                }
                else
                {
                    if (account.RoleID == 1)
                    {
                        MiniSupermarketApp.adminMenu.Show();
                        AdminMenu.Account = account;
                    }
                    else if (account.RoleID == 2)
                    {
                        MiniSupermarketApp.managerMenu.Show();
                        ManagerMenu.Account = account;
                    }
                    else if (account.RoleID == 3)
                    {
                        MiniSupermarketApp.nvBanHangMenu.Show();
                        NVBanHangMenu.Account = account;
                    }
                    else
                    {
                        MiniSupermarketApp.nvNhapHangMenu.Show();
                        NVNhapHangMenu.Account = account;
                    }
                    MiniSupermarketApp.login.Visible = false;
                }
            }
        }

        private void tbAccount_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
                Login();
        }

        private void tbPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
                Login();
        }

        private void tbPass_TextChanged(object sender, EventArgs e)
        {
            tbPass.PasswordChar = '*';
        }
    }
}
