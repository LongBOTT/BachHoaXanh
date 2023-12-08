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
            AccountRepository  accountRepository = new AccountRepository();
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
                    MiniSupermarketApp.menu.Show();
                    Menu.Account = account;
                    MiniSupermarketApp.login.Enabled = false;
                }
            }
        }
    }
}
