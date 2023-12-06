using BachHoaXanh._Repositories;
using BachHoaXanh.Models;
using BachHoaXanh.Views.InterfaceView;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Presenters
{
    public class DiscountPresenter
    {
        private IDiscountView view;
        private IDiscountRepository repository;
        private IDiscount_detailRepository discount_DetailRepository;
        private IProductRepository productRepository;
        private Discount newDiscount = null;
        private List<int> productInDiscount;
        public DiscountPresenter(IDiscountView view, IDiscountRepository repository)
        {
            this.view = view;
            this.repository = repository;
            discount_DetailRepository = new Discount_detailRepository();
            productRepository = new ProductRepository();
            productInDiscount = new List<int>();

            this.view.ShowDetail += ShowDetailDiscount;
            this.view.AddProduct += AddProduct;
            this.view.AddNewEvent += AddDiscount;
            LoadDiscount(this.repository.GetAll());
        }

        private void AddDiscount(object? sender, EventArgs e)
        {
            newDiscount = new Discount(8, 50, DateTime.Now, DateTime.Now, false);
            LoadProduct(productRepository.GetAll());
        }

        private void AddProduct(object? sender, EventArgs e)
        {
            if (newDiscount != null && view.guna2DataGridDiscount.SelectedRows[0].Index == -1)
            {
                int index = view.guna2DataGridProduct.SelectedRows[0].Index;
                Console.WriteLine(index);
                int id = int.Parse(view.guna2DataGridProduct.SelectedRows[0].Cells[0].Value.ToString());
                Product product = productRepository.FindProductsBy(new Dictionary<string, object>() { { "id", id } })[0];
                if (!(bool)view.guna2DataGridProduct.SelectedRows[0].Cells[4].Value)
                {
                    productInDiscount.Add(id);
                    view.guna2DataGridProduct.Rows[index].Cells[3].Value = product.Cost - product.Cost * newDiscount.Percent / 100;
                    view.guna2DataGridProduct.Rows[index].Cells[4].Value = true;
                }
                else
                {
                    productInDiscount.Remove(id);
                    view.guna2DataGridProduct.Rows[index].Cells[3].Value = "";
                    view.guna2DataGridProduct.Rows[index].Cells[4].Value = false;
                }
            }
            
        }

        private void ShowDetailDiscount(object? sender, EventArgs e)
        {
            if (view.guna2DataGridDiscount.SelectedRows[0].Index != -1 && newDiscount == null)
            {
                int id = int.Parse(view.guna2DataGridDiscount.SelectedRows[0].Cells[0].Value.ToString());
                List<Product> products = new List<Product>();

                foreach (Discount_detail discount in discount_DetailRepository.FindDiscount_detailsBy(new Dictionary<string, object>() { { "discount_id", id } }))
                {
                    products.Add(productRepository.FindProductsBy(new Dictionary<string, object>() { { "id", discount.Product_id } })[0]);

                }
                LoadProduct(products);
            }
            
        }

        public void LoadDiscount (IEnumerable<Discount> discounts)
        {
            view.guna2DataGridDiscount.RowCount = 0;
            foreach (Discount discount in discounts)
            {
                view.guna2DataGridDiscount.Rows.Add(discount.Id, discount.Percent, discount.Start_DateTime, discount.End_DateTime, discount.Status? "Ngừng áp dụng" : "Đang áp dụng");
            }
        }

        public void LoadProduct(IEnumerable<Product> products)
        {
            view.guna2DataGridProduct.RowCount = 0;
            if (view.guna2DataGridDiscount.SelectedRows[0].Index != -1 && newDiscount == null)
            {
                int id = int.Parse(view.guna2DataGridDiscount.SelectedRows[0].Cells[0].Value.ToString());
                Discount discount = repository.FindDiscountsBy(new Dictionary<string, object>() { { "id", id } })[0];
                foreach (Product product in products)
                {
                    view.guna2DataGridProduct.Rows.Add(product.Id, product.Name, product.Cost, product.Cost - product.Cost * discount.Percent /100, true);
                }
            }
            else
            {
                foreach(Product product in products)
                {
                    if (productInDiscount.Contains(product.Id))
                    {
                        view.guna2DataGridProduct.Rows.Add(product.Id, product.Name, product.Cost, product.Cost - product.Cost * newDiscount.Percent / 100, true);
                    }
                    else
                    {
                        view.guna2DataGridProduct.Rows.Add(product.Id, product.Name, product.Cost, "", false);
                    }
                }
            }
        }

        
    }
}
