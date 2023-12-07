using BachHoaXanh._Repositories;
using BachHoaXanh.Main;
using BachHoaXanh.Models;
using BachHoaXanh.User_Control;
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
        private List<Product> listProduct;
        public DiscountPresenter(IDiscountView view, IDiscountRepository repository)
        {
            this.view = view;
            this.repository = repository;
            discount_DetailRepository = new Discount_detailRepository();
            productRepository = new ProductRepository();
            productInDiscount = new List<int>();
            listProduct = new List<Product>();
            this.view.SearchDiscountEvent += SearchDiscount;
            this.view.SearchProductEvent += SearchProduct;
            this.view.ShowDetail += ShowDetailDiscount;
            this.view.AddProduct += AddProduct;
            this.view.AddNewEvent += AddDiscount;
            this.view.CancelEvent += Refresh;
            this.view.guna2TextBoxID.Text = this.repository.GetAutoID().ToString();
            LoadDiscount(this.repository.GetAll());
        }

        private void SearchProduct(object? sender, EventArgs e)
        {
            string attribute = view.comboBoxSeacrchProduct.SelectedItem.ToString();
            string value = view.guna2TextSearchProduct.Text;
            List<Product> products = new List<Product>();
            if (attribute == "Tên sản phẩm")
            {
                foreach (Product product in listProduct)
                {
                    if (product.Name.ToLower().Contains(value.ToLower()))
                    {
                        products.Add(product);
                    }
                }
                LoadProduct(products);
            }
        }

        private void SearchDiscount(object? sender, EventArgs e)
        {
            DateTime startDate = view.DateTimePickerSearch1.Value;
            DateTime endDate = view.DateTimePickerSearch2.Value;
            if (startDate > endDate)
            {
                MessageDialog.Show(MiniSupermarketApp.menu, "Ngày kết thúc phải sau ngày bắt đầu", "Thông báo", MessageDialogButtons.OK, MessageDialogIcon.Information);
                return;
            }
            List<Discount> discounts = new List<Discount> { };
            foreach (Discount discount in repository.GetAll())
            {
                if (startDate <= discount.Start_DateTime && discount.End_DateTime <= endDate)
                    discounts.Add(discount);
            }
            LoadDiscount(discounts);
        }

        private void Refresh(object? sender, EventArgs e)
        {
            productInDiscount.Clear();
            repository = new DiscountRepository();
            discount_DetailRepository = new Discount_detailRepository();
            LoadDiscount(repository.GetAll());
            newDiscount = null;
            listProduct = new List<Product> { };
            LoadProduct(listProduct);
            view.guna2TextBoxID.Text = repository.GetAutoID().ToString();
            view.guna2TextPerent.Text = "";
            view.guna2DataGridProduct.ClearSelection();
            view.guna2DataGridDiscount.ClearSelection();
        }

        private void AddDiscount(object? sender, EventArgs e)
        {
            if (productInDiscount.Count == 0)
            {
                int id = repository.GetAutoID();
                double percent = 0;
                try
                {
                    percent = double.Parse(view.guna2TextPerent.Text);
                } catch (Exception ex)
                {
                    MessageDialog.Show(MiniSupermarketApp.menu, "Phàn trăm giảm giá không hợp lệ!", "Thông báo", MessageDialogButtons.OK, MessageDialogIcon.Information);
                    return;
                }
                if (percent <= 0)
                {
                    MessageDialog.Show(MiniSupermarketApp.menu, "Phần trăm giảm giá phải lớn hơn 0!", "Thông báo", MessageDialogButtons.OK, MessageDialogIcon.Information);
                    return;
                }
                DateTime startDate = view.DateTimePickerStart.Value;
                DateTime endDate = view.DateTimePickerEnd.Value;
                if (startDate > endDate)
                {
                    MessageDialog.Show(MiniSupermarketApp.menu, "Ngày kết thúc phải sau ngày bắt đầu", "Thông báo", MessageDialogButtons.OK, MessageDialogIcon.Information);
                    return;
                }

                if (endDate < DateTime.Now)
                {
                    MessageDialog.Show(MiniSupermarketApp.menu, "Ngày kết thúc phải sau ngày hiện tại", "Thông báo", MessageDialogButtons.OK, MessageDialogIcon.Information);
                    return;
                }

                newDiscount = new Discount(id, percent, startDate, endDate, false);
                view.guna2DataGridDiscount.ClearSelection();
                MessageDialog.Show(MiniSupermarketApp.menu, "Vui lòng chọn sản phẩm của đợt giảm giá", "Thông báo", MessageDialogButtons.OK, MessageDialogIcon.Information);
                listProduct = (List<Product>)productRepository.GetAll();
                LoadProduct(listProduct);
                view.guna2DataGridProduct.ClearSelection();
            }
            else
            {
                DialogResult result = MessageBox.Show("Xác nhận thêm đợt giảm giá?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (result == DialogResult.Cancel)
                {
                    MessageDialog.Show(MiniSupermarketApp.menu, "Thêm đợt giảm giá không thành công", "Thông báo", MessageDialogButtons.OK, MessageDialogIcon.Information);
                    return;
                }

                if (repository.Add(newDiscount) == 1)
                {
                    foreach (Discount discount in repository.GetAll())
                    {
                        if (discount.Id != newDiscount.Id)
                        {
                            discount.Status = true ;
                            repository.Update(discount);
                            foreach (Discount_detail discount_Detail in discount_DetailRepository.GetAll())
                            {
                                discount_Detail.Status = true ;
                                discount_DetailRepository.Update(discount_Detail);
                            }
                        }
                    }

                    foreach (int i in productInDiscount)
                    {
                        Discount_detail discount_Detail = new Discount_detail();
                        discount_Detail.Discount_id = newDiscount.Id;
                        discount_Detail.Product_id = i;
                        discount_Detail.Status = false;
                        discount_DetailRepository.Add(discount_Detail);
                    }
                    MessageDialog.Show(MiniSupermarketApp.menu, "Thêm đợt giảm giá thành công!", "Thông báo", MessageDialogButtons.OK, MessageDialogIcon.Information);
                    Refresh(sender, e);
                }
                else
                {
                    MessageDialog.Show(MiniSupermarketApp.menu, "Thêm đợt giảm giá không thành công", "Thông báo", MessageDialogButtons.OK, MessageDialogIcon.Information);
                }
            }
        }

        private void AddProduct(object? sender, EventArgs e)
        {
            if (newDiscount != null && view.guna2DataGridDiscount.SelectedRows.Count == 0)
            {
                if (view.guna2DataGridProduct.SelectedRows.Count <= 0)
                    return;
                int index = view.guna2DataGridProduct.SelectedRows[0].Index;
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
            if (view.guna2DataGridDiscount.SelectedRows.Count > 0 && newDiscount == null)
            {
                int id = int.Parse(view.guna2DataGridDiscount.SelectedRows[0].Cells[0].Value.ToString());
                List<Product> products = new List<Product>();

                foreach (Discount_detail discount in discount_DetailRepository.FindDiscount_detailsBy(new Dictionary<string, object>() { { "discount_id", id } }))
                {
                    products.Add(productRepository.FindProductsBy(new Dictionary<string, object>() { { "id", discount.Product_id } })[0]);

                }
                listProduct = products;
                LoadProduct(listProduct);
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
            if (view.guna2DataGridDiscount.SelectedRows.Count > 0 && newDiscount == null)
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
