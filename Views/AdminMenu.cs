using BachHoaXanh._Repositories;
using BachHoaXanh.Main;
using BachHoaXanh.Models;
using BachHoaXanh.Presenters;
using BachHoaXanh.User_Control;
using BachHoaXanh.Views;
using BachHoaXanh.Views.InterfaceView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BachHoaXanh
{
    public partial class AdminMenu : Form
    {
        public static Account Account;
        public AdminMenu()
        {
            IAccountRepository accountRepository = new AccountRepository();
            Account = accountRepository.GetAll().ElementAt(0);
            InitializeComponent();
            Thread thread = new Thread(() => RenderTime());
            thread.Start();
            Homepage homepage = new Homepage();
            addUserControl(homepage);
        }

        private void RenderTime()
        {
            while (true)
            {
                Thread.Sleep(1000);
                lbDateTime.Text = DateTime.Now.ToString();
            }
        }

        private void addUserControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            pnlContent.Controls.Clear();
            pnlContent.Controls.Add(userControl);
            userControl.BringToFront();
        }

        private void btnHomePage_Click(object sender, EventArgs e)
        {
            Homepage homepage = new Homepage();
            addUserControl(homepage);
        }

        private void btnSell_Click(object sender, EventArgs e)
        {
            ISaleView view = new SaleControl();
            IProductRepository repository = new ProductRepository();
            new SalePresenter(view, repository);
            addUserControl((UserControl)view);
        }

        private void btnWarehouse_Click(object sender, EventArgs e)
        {
            IShipmentView view = new WareHouseControl();
            IShipmentRepository repository = new ShipmentRepository();
            new ShipmentPresenter(view, repository);
            addUserControl((UserControl)view);
        }

        private void btnStatistics_Click(object sender, EventArgs e)
        {
            IStatisticsView view = new StatisticsControl();
            IStatisticsRepository repository = new StatisticRepository();
            new StatisticsPresenter(view, repository);
            addUserControl((UserControl)view);
        }

        private void btnDiscount_Click(object sender, EventArgs e)
        {
            IDiscountView view = new DiscountControl();
            IDiscountRepository repository = new DiscountRepository();
            new DiscountPresenter(view, repository);
            addUserControl((UserControl)view);
        }

        private void btnBill_Click(object sender, EventArgs e)
        {
            IBillView view = new BillControl();
            IReceiptRepository repository = new ReceiptRepository();
            new BillPresenter(view, repository);
            addUserControl((UserControl)view);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            IExportView view = new ExportControl();
            IExportRepository repository = new ExportRepository();
            new ExportPresenter(view, repository);
            addUserControl((UserControl)view);
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            IImportView view = new ImportControl();
            IImportRepository repository = new ImportRepository();
            new ImportPresenter(view, repository);
            addUserControl((UserControl)view);
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            IProductView view = new ProductControl();
            IProductRepository repository = new ProductRepository();
            new ProductPresenter(view, repository);
            addUserControl((UserControl)view);
        }

        private void btnSupplier_Click(object sender, EventArgs e)
        {
            ISupplierView view = new SupplierControl();
            ISupplierRepository repository = new SupplierRepository();
            new SupplierPresenter(view, repository);
            addUserControl((UserControl)view);
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            IStaffView view = new StaffControl();
            IStaffRepository repository = new StaffRepository();
            new StaffPresenter(view, repository);
            addUserControl((UserControl)view);
        }

        private void btnAccount_Click(object sender, EventArgs e)
        {
            IAccountView view = new AccountControl();
            IAccountRepository repository = new AccountRepository();
            new AccountPresenter(view, repository);
            addUserControl((UserControl)view);
        }

        private void btnDecentralization_Click(object sender, EventArgs e)
        {
            DecentralizationControl decentralizationControl = new DecentralizationControl();
            addUserControl(decentralizationControl);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Dispose();
            MiniSupermarketApp.login.Visible = true;
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pnlTittleBar_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnMinimize_MouseHover(object sender, EventArgs e)
        {
            btnMinimize.BackColor = Color.FromArgb(142, 188, 218);
        }
    }
}
