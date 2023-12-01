using BachHoaXanh._Repositories;
using BachHoaXanh.Models;
using BachHoaXanh.Presenters;
using BachHoaXanh.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BachHoaXanh.Main
{
    internal static class BachHoaXanh
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AllocConsole();
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            AccountRepository accountRepository = new AccountRepository();
            //foreach (Account account in accountRepository.GetAll())
            //{
            //    Console.WriteLine(account.ToString());
            //}
            ApplicationConfiguration.Initialize();
            Application.Run(new Menu());

        }
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
    }
}
