using BachHoaXanh._Repositories;
using BachHoaXanh.Models;
using BachHoaXanh.Properties;
using BachHoaXanh.Views.InterfaceView;
using BachHoaXanh.Views;
using System;
using System.Resources;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.Math.Field;
using BachHoaXanh.Views.Chart;

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
            view.GetChart1.AddLegend("Vốn", ColorTranslator.FromHtml("#7b4397"));
            view.GetChart1.AddLegend("Doanh Thu", ColorTranslator.FromHtml("#e65c00"));
            view.GetChart1.AddLegend("Lợi Nhuận", ColorTranslator.FromHtml("#0099F7"));
            view.GetChart2.AddLegend("Vốn", ColorTranslator.FromHtml("#7b4397"));
            view.GetChart2.AddLegend("Doanh Thu", ColorTranslator.FromHtml("#e65c00"));
            view.GetChart2.AddLegend("Lợi Nhuận", ColorTranslator.FromHtml("#0099F7"));
            view.GetChart3.AddLegend("Vốn", ColorTranslator.FromHtml("#7b4397"));
            view.GetChart3.AddLegend("Doanh Thu", ColorTranslator.FromHtml("#e65c00"));
            view.GetChart3.AddLegend("Lợi Nhuận", ColorTranslator.FromHtml("#0099F7"));
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
            currentMonth = DateTime.Now.Month;
            currentYear = DateTime.Now.Year;
            expenses = new List<double>();
            amount = new List<double>();
            int x = 200, y = 150;
            for (int i = 0; i < 12; i++)
            {
                x += 150;
                expenses.Add(x);
            }
            for (int i = 0; i < 12; i++)
            {
                y += 200;
                amount.Add(y);
            }
            view.GetTitle1.Text = "THỐNG KÊ THEO THÁNG TRONG NĂM " + currentYear;
            //for (int i = 1; i <= currentMonth; i++)
            //{
            //    List<List<string>> result = repository.ExcuteQuerry("SELECT SUM(`import`.total) " +
            //                "FROM `import` " +
            //                "WHERE YEAR(`import`.received_date) = " + currentYear + " AND MONTH(`import`.received_date) = " + i);
            //    if (result.Count > 0)
            //    {
            //        expenses.Add(Double.Parse(result[0][0].Split("\\.")[0]));
            //    }

            //    result = repository.ExcuteQuerry("SELECT SUM(`receipt`.total) " +
            //                "FROM `receipt` " +
            //                "WHERE YEAR(`receipt`.invoice_date) = " + currentYear + " AND MONTH(`receipt`.invoice_date) = " + i);
            //    if (result.Count > 0)
            //    {
            //        amount.Add(Double.Parse(result[0][0].Split("\\.")[0])); ;
            //    }
            //}

            view.GetChart3.Clear();
            view.GetChart3.AddData(new ModelChart("January", new double[] { expenses[0], amount[0], amount[0] - expenses[0] }));
            view.GetChart3.AddData(new ModelChart("February", new double[] { expenses[1], amount[1], amount[1] - expenses[1] }));
            view.GetChart3.AddData(new ModelChart("March", new double[] { expenses[2], amount[2], amount[2] - expenses[2] }));
            view.GetChart3.AddData(new ModelChart("April", new double[] { expenses[3], amount[3], amount[3] - expenses[3] }));
            view.GetChart3.AddData(new ModelChart("May", new double[] { expenses[4], amount[4], amount[4] - expenses[4] }));
            view.GetChart3.AddData(new ModelChart("June", new double[] { expenses[5], amount[5], amount[5] - expenses[5] }));
            view.GetChart3.AddData(new ModelChart("July", new double[] { expenses[6], amount[6], amount[6] - expenses[6] }));
            view.GetChart3.AddData(new ModelChart("August", new double[] { expenses[7], amount[7], amount[7] - expenses[7] }));
            view.GetChart3.AddData(new ModelChart("September", new double[] { expenses[8], amount[8], amount[8] - expenses[8] }));
            view.GetChart3.AddData(new ModelChart("October", new double[] { expenses[9], amount[9], amount[9] - expenses[9] }));
            view.GetChart3.AddData(new ModelChart("November", new double[] { expenses[10], amount[10], amount[10] - expenses[10] }));
            view.GetChart3.AddData(new ModelChart("December", new double[] { expenses[11], amount[11], amount[11] - expenses[11] }));
            view.GetChart3.Start();
        }

        private void ByQuater()
        {
            currentMonth = DateTime.Now.Month;
            currentYear = DateTime.Now.Year;
            expenses = new List<double>();
            amount = new List<double>();
            currentQuarter = (currentMonth - 1) / 3 + 1;
            int x = 200, y = 150;
            for (int i = 0; i < 12; i++)
            {
                x += 150;
                expenses.Add(x);
            }
            for (int i = 0; i < 12; i++)
            {
                y += 200;
                amount.Add(y);
            }
            view.GetTitle2.Text = "THỐNG KÊ THEO QUÝ TRONG NĂM " + currentYear;
            //for (int i = 1; i <= currentQuarter; i++)
            //{
            //    //    XYChart.Series<String, Number> series1 = new XYChart.Series<>();
            //    //    series1.setName("Quý " + i);

            //    List<List<string>> result = repository.ExcuteQuerry("SELECT SUM(`import`.total) " +
            //                "FROM `import` " +
            //                "WHERE YEAR(`import`.received_date) = " + currentYear + " AND QUARTER(`import`.received_date) = " + i);
            //    if (result.Count > 0)
            //    {
            //        expenses.Add(Double.Parse(result[0][0].Split("\\.")[0]));
            //    }

            //    result = repository.ExcuteQuerry("SELECT SUM(`receipt`.total) " +
            //            "FROM `receipt` " +
            //            "WHERE YEAR(`receipt`.invoice_date) = " + currentYear + " AND QUARTER(`receipt`.invoice_date) = " + i);
            //    if (result.Count > 0)
            //    {
            //        amount.Add(Double.Parse(result[0][0].Split("\\.")[0])); ;
            //    }
            //}
            view.GetChart2.Clear();
            view.GetChart2.AddData(new ModelChart("The First Quarter", new double[] { expenses[0], amount[0], amount[0] - expenses[0] }));
            view.GetChart2.AddData(new ModelChart("Second Quarter", new double[] { expenses[1], amount[1], amount[1] - expenses[1] }));
            view.GetChart2.AddData(new ModelChart("Third Quarter", new double[] { expenses[2], amount[2], amount[2] - expenses[2] }));
            view.GetChart2.AddData(new ModelChart("Fourth Quarter", new double[] { expenses[3], amount[3], amount[3] - expenses[3] }));
            view.GetChart2.Start();
        }

        private int currentMonth, currentYear, currentQuarter;
        private List<Double> expenses, amount;

        private void ByYear()
        {
            expenses = new List<double>();
            amount = new List<double>();
            view.GetChart3.Text = "THỐNG KÊ THEO 3 NĂM GẦN NHẤT ";
            view.GetChart1.Clear();
            currentYear = DateTime.Now.Year;
            //for (int i = currentYear - 2; i <= currentYear; i++)
            //{
            //    List<List<string>> result = repository.ExcuteQuerry("SELECT SUM(`import`.total) " +
            //        "FROM `import` " +
            //        "WHERE YEAR(`import`.received_date) = " + i);
            //    if (result.Count > 0)
            //    {
            //        expenses.Add(Double.Parse(result[0][0].Split("\\.")[0]));
            //    }

            //    result = repository.ExcuteQuerry("SELECT SUM(`receipt`.total) " +
            //        "FROM `receipt` " +
            //        "WHERE YEAR(`receipt`.invoice_date) = " + i);
            //    if (result.Count > 0)
            //    {
            //        amount.Add(Double.Parse(result[0][0].Split("\\.")[0]));
            //    }
            //    view.GetChart1.AddData(new ModelChart( i + "", new double[] { expenses[expenses.Count - 1], amount[expenses.Count - 1], amount[expenses.Count - 1] - expenses[expenses.Count - 1]}));
            //}
            //view.GetChart1.Start();
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

            //double expenses = Convert.ToDouble(view.guna2HtmlLabelExpenses.Text.Split(" ")[0]);
            //double amount = Convert.ToDouble(view.guna2HtmlLabelAmount.Text.Split(" ")[0]);
            //view.guna2HtmlLabelProfit.Text = (amount - expenses).ToString() + " VNĐ";
        }
    }
}
