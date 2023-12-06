using BachHoaXanh._Repositories;
using BachHoaXanh.Models;
using BachHoaXanh.Properties;
using BachHoaXanh.Views.InterfaceView;
using System;
using System.Resources;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.Math.Field;

namespace BachHoaXanh.Presenters
{
    public class StatisticsPresenter
    {
        private IStatisticsView view;
        private IStatisticsRepository repository;
        private IProductRepository productRepository;
        public StatisticsPresenter(IStatisticsView view, IStatisticsRepository repository)
        {
            this.view = view;
            this.repository = repository;
            productRepository = new ProductRepository();
            view.guna2TabControl.SelectedIndexChanged += SelectTab;
            view.guna2TabControl.SelectedIndex = -1;
            Genneral();
        }

        private void SelectTab(object? sender, EventArgs e)
        {
            int index = view.guna2TabControl.SelectedIndex;
            if (index == 0)
                Genneral();
            if (index == 1)
                ByYear();
            if (index == 2)
                ByQuater();
            if (index == 3)
                ByMonth();
        }

        private void ByMonth()
        {
            throw new NotImplementedException();
        }

        private void ByQuater()
        {
            throw new NotImplementedException();
        }

        private void ByYear()
        {
            throw new NotImplementedException();
        }

        private void Genneral()
        {
            ResourceManager resourceManager = new ResourceManager(typeof(Resources));
            List<List<string>> result = new List<List<string>>();

            result = repository.ExcuteQuerry("SELECT COUNT(staff.id) FROM staff WHERE staff.deleted = 0");

            view.guna2HtmlLabelNumofStaff.Text = result[0][0];

            result = repository.ExcuteQuerry("SELECT product.id, product.image, SUM(receipt_detail.quantity) " +
                "FROM receipt_detail JOIN product on receipt_detail.product_id = product.id " +
                                        "JOIN receipt on receipt.id = receipt_detail.receipt_id " +
                "WHERE (MONTH(CURDATE()) = MONTH(`receipt`.invoice_date)) AND (YEAR(CURDATE()) = YEAR(`receipt`.invoice_date)) " +
                "GROUP BY product.id, product.image " +
                "ORDER BY SUM(receipt_detail.quantity) DESC " +
                "LIMIT 1");
            if (result.Count > 0)
            {
                int id = int.Parse(result[0][0]);
                Product product = productRepository.FindProductsBy(new Dictionary<string, object>() { { "id", id } })[0];
                view.guna2HtmlLabelNameBestSeller.Text = product.Name;
                view.guna2PictureBoxBestSeller.Image = (Image)resourceManager.GetObject(product.Image);
                view.guna2HtmlLabelBestSeller.Text = result[0][2];
            }

            result = repository.ExcuteQuerry("SELECT product.id, product.image, SUM(receipt_detail.quantity) " +
                "FROM receipt_detail JOIN product on receipt_detail.product_id = product.id " +
                                        "JOIN receipt on receipt.id = receipt_detail.receipt_id " +
                "WHERE (MONTH(CURDATE()) = MONTH(`receipt`.invoice_date)) AND (YEAR(CURDATE()) = YEAR(`receipt`.invoice_date)) " +
                "GROUP BY product.id, product.image " +
                "ORDER BY SUM(receipt_detail.quantity) ASC " +
                "LIMIT 1");
            if (result.Count > 0)
            {
                int id = int.Parse(result[0][0]);
                Product product = productRepository.FindProductsBy(new Dictionary<string, object>() { { "id", id } })[0];
                view.guna2HtmlLabelNameBadSeller.Text = product.Name;
                view.guna2PictureBoxBadSeller.Image = (Image)resourceManager.GetObject(product.Image);
                view.guna2HtmlLabelBadSeller.Text = result[0][2];
            }

            result = repository.ExcuteQuerry("SELECT staff.id, staff.name, COUNT(receipt.id) " +
                "FROM receipt JOIN staff on receipt.staff_id = staff.id " +
                "WHERE (MONTH(CURDATE()) = MONTH(`receipt`.invoice_date)) AND (YEAR(CURDATE()) = YEAR(`receipt`.invoice_date)) " +
                "GROUP BY staff.id, staff.name " +
                "ORDER BY COUNT(receipt.id) DESC " +
                "LIMIT 1");
            if (result.Count > 0)
            {
                int id = int.Parse(result[0][0]);
                view.guna2HtmlLabelNameBestStaff.Text = result[0][1];
                view.guna2HtmlLabelBillOfBestStaff.Text = result[0][2];
            }

            result = repository.ExcuteQuerry("SELECT product.id, product.image, shipment.remain " +
                "FROM shipment JOIN product on shipment.product_id = product.id " +
                "ORDER BY shipment.remain ASC" +
                " LIMIT 1");
            if (result.Count > 0)
            {
                int id = int.Parse(result[0][0]);
                Product product = productRepository.FindProductsBy(new Dictionary<string, object>() { { "id", id } })[0];
                view.guna2HtmlLabelNameProduct.Text = product.Name;
                view.guna2PictureBoxPoduct.Image = (Image)resourceManager.GetObject(product.Image);
                view.guna2HtmlLabelQuantityShipment.Text = result[0][2];
            }

            result = repository.ExcuteQuerry("SELECT SUM(`import_note`.total) " +
                "FROM `import_note` " +
                "WHERE (MONTH(CURDATE()) - MONTH(`import_note`.received_date)) = 0 AND (YEAR(CURDATE()) - YEAR(`import_note`.received_date)) = 0");
            if (result.Count > 0)
            {
                if (result[0].Count > 0) { 
                    view.guna2HtmlLabelExpenses.Text = result[0][0] + " VNĐ";
                }
            }

            result = repository.ExcuteQuerry("SELECT SUM(`receipt`.total) " +
                "FROM `receipt` " +
                "WHERE (MONTH(CURDATE()) - MONTH(`receipt`.invoice_date)) = 0 AND (YEAR(CURDATE()) - YEAR(`receipt`.invoice_date)) = 0");
            if (result.Count > 0)
            {
                if (result[0].Count > 0)
                {
                    view.guna2HtmlLabelAmount.Text = result[0][0] + " VNĐ";
                }
            }

            double expenses = Convert.ToDouble(view.guna2HtmlLabelExpenses.Text.Split(" ")[0]);
            double amount = Convert.ToDouble(view.guna2HtmlLabelAmount.Text.Split(" ")[0]);
            view.guna2HtmlLabelProfit.Text = (amount - expenses).ToString() + " VNĐ";
        }
    }
}
