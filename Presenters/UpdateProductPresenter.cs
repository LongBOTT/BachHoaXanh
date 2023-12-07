using BachHoaXanh._Repositories;
using BachHoaXanh.Main;
using BachHoaXanh.Models;
using BachHoaXanh.Properties;
using BachHoaXanh.User_Control;
using BachHoaXanh.Views;
using BachHoaXanh.Views.InterfaceView;
using Guna.UI2.WinForms;
using System;
using System.Resources;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Text.RegularExpressions;

namespace BachHoaXanh.Presenters
{
    public class UpdateProductPresenter
    {
        private IUpdateProductView view;
        private IProductRepository repository;
        private IBrandRepository brandRepository;
        private ICategoryRepository categoryRepository;
        private Product product;
        private List<Category> categorys;
        private List<Brand> brands;
        private Boolean flag;
        public UpdateProductPresenter(IUpdateProductView view, IProductRepository repository) 
        {
            this.view = view;
            this.repository = repository;
            brandRepository = new BrandRepository();
            categoryRepository = new CategoryRepository();
            categorys = (List<Category>)categoryRepository.GetAll();
            brands = (List<Brand>)brandRepository.GetAll();
            product = this.view.GetProduct;
            this.view.ShowDetail += ShowDetail;
            this.view.LoadListBrand += LoadListBrand;
            this.view.LoadListCategory += LoadListCategory;
            this.view.SelectedRow += SelectedRow;
            this.view.UpdateProduct += UpdateProduct;
            this.view.Refresh += Refresh;
            this.view.pictureBox.Click += LoadImage;
        }

        private void LoadImage(object? sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = Application.StartupPath;
            openFileDialog1.Filter = "PNG Files|*.png";

            openFileDialog1.Title = "Select PNG File";
            openFileDialog1.Multiselect = false;

            if (openFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            string selectedFileName = openFileDialog1.FileName;
            view.pictureBox.Image = Image.FromFile(selectedFileName);
        }

        private void LoadListCategory(object? sender, EventArgs e)
        {
            flag = false;
            DataGridViewColumn column1 = new DataGridViewTextBoxColumn();
            DataGridViewColumn column2 = new DataGridViewTextBoxColumn();
            DataGridViewColumn column3 = new DataGridViewTextBoxColumn();
            column1.HeaderText = "Mã thể loại";
            column2.HeaderText = "Tên thể loại";
            column3.HeaderText = "Số lượng";
            view.Guna2DataGridView.Columns.Clear();
            view.Guna2DataGridView.Rows.Clear();
            view.Guna2DataGridView.Columns.AddRange(column1, column2, column3);

            foreach (Category category in categorys)
            {
                view.Guna2DataGridView.Rows.Add(category.Id, category.Name, category.Quantity);
            }
             view.Guna2DataGridView.Visible = true;
            if (view.Guna2TextBoxCategory.PlaceholderText != "Chọn thể loại")
            {
                Category category = categoryRepository.FindCategorysBy(new Dictionary<string, object>() { { "id", Convert.ToInt16(view.Guna2TextBoxCategory.Text) } })[0];
                int index = Repository.GetIndex(category, "id", categorys);
                view.Guna2DataGridView.Rows[index].Selected = true;
            }
        }

        private void LoadListBrand(object? sender, EventArgs e)
        {
            flag = true;
            DataGridViewColumn column1 = new DataGridViewTextBoxColumn();
            DataGridViewColumn column2 = new DataGridViewTextBoxColumn();
            column1.HeaderText = "Mã thương hiệu";
            column2.HeaderText = "Tên thương hiệu";
            view.Guna2DataGridView.Columns.Clear();
            view.Guna2DataGridView.Rows.Clear();
            view.Guna2DataGridView.Columns.AddRange(column1, column2);

            foreach (Brand brand in brands)
            {
                view.Guna2DataGridView.Rows.Add(brand.Id, brand.Name);
            }
            view.Guna2DataGridView.Visible = true;
            if (view.Guna2TextBoxBrand.PlaceholderText != "Chọn thương hiệu")
            {
                Brand brand = brandRepository.FindBrandsBy(new Dictionary<string, object>() { { "id", Convert.ToInt16(view.Guna2TextBoxBrand.Text) } })[0];
                int index = Repository.GetIndex(brand, "id", brands);
                view.Guna2DataGridView.Rows[index].Selected = true;
            }
        }

        private void ShowDetail(object? sender, EventArgs e)
        {
            ResourceManager resourceManager = new ResourceManager(typeof(Resources));
            view.pictureBox.Image = (Image)resourceManager.GetObject(product.Image);
            view.Guna2TextBoxID.Text = product.Id.ToString();
            view.Guna2TextBoxname.Text = product.Name.ToString();
            view.Guna2TextBoxBrand.Text = product.Brand_id.ToString();
            view.Guna2TextBoxCategory.Text = product.Category_id.ToString();
            view.Guna2ComboBoxUnit.SelectedItem = product.Unit.ToString();
            view.Guna2TextBoxCost.Text = product.Cost.ToString();
            view.Guna2TextBoxQuantity.Text = product.Quantity.ToString();
            view.Guna2TextBoxBarcode.Text = product.Barcode.ToString();
        }

        private void SelectedRow(object? sender, EventArgs e)
        {
            string id = "";
            if (view.Guna2DataGridView.SelectedRows.Count > 0)
            {
                id = view.Guna2DataGridView.SelectedRows[0].Cells[0].Value.ToString();
                if (flag)
                    view.Guna2TextBoxBrand.Text = id;
                else
                    view.Guna2TextBoxCategory.Text = id;
            }
        }

        private void UpdateProduct(object? sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Xác nhận sửa sản phẩm?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (result == DialogResult.OK)
                {
                    int id = Convert.ToInt16(view.Guna2TextBoxID.Text);
                    string name = view.Guna2TextBoxname.Text;
                    int brandID = Convert.ToInt16(view.Guna2TextBoxBrand.Text);
                    int categoryID = Convert.ToInt16(view.Guna2TextBoxCategory.Text);
                    string unit = view.Guna2ComboBoxUnit.SelectedItem.ToString();
                    double cost = Convert.ToDouble(view.Guna2TextBoxCost.Text);
                    double quantity = Convert.ToDouble(view.Guna2TextBoxQuantity.Text);
                    string barcode = view.Guna2TextBoxBarcode.Text;
                    string image = "Pro1";

                    // check input o day khong hop le thi thong bao va return
                    if (checkInput())
                    {
                        Product product = new Product(id, name, brandID, categoryID, unit, cost, quantity, image, barcode, false);
                        if (repository.Update(product) == 1)
                        {
                            view.Message = "Cập nhật sản phẩm thành công!";
                            view.close();
                            ProductPresenter.repository = new ProductRepository();
                            ProductPresenter.productList = ProductPresenter.repository.GetAll();
                            ProductPresenter.LoadProductList(ProductPresenter.productList);
                            return;
                        }
                        view.Message = "Cập nhật sản phẩm không thành công";
                    }
                }
            } catch (Exception ex)
            {
                view.Message = "Vui lòng nhập đầy đủ thông tin";
            }
        }

        private void Refresh(object? sender, EventArgs e)
        {
            ShowDetail(sender, e);
            view.Guna2TextBoxname.Text = null;
            view.Guna2TextBoxBrand.Text = null;
            view.Guna2TextBoxCategory.Text = null;
            view.Guna2ComboBoxUnit.SelectedIndex = 0;
            view.Guna2TextBoxCost.Text = null;
            view.Guna2TextBoxQuantity.Text = null;
            view.Guna2TextBoxBarcode.Text = null;
        }

        public Boolean checkInput()
        {
            string name = view.Guna2TextBoxname.Text;
            double cost = Convert.ToDouble(view.Guna2TextBoxCost.Text);
            double quantity = Convert.ToDouble(view.Guna2TextBoxQuantity.Text);
            string barcode = view.Guna2TextBoxBarcode.Text;

            if (string.IsNullOrEmpty(name) || !Regex.IsMatch(name, @"^[a-zA-ZÀ-ỹẠ-ỵĂăÂâĐđÊêÔôƠơƯư/0-9\s]*[a-zA-ZÀ-ỹẠ-ỵĂăÂâĐđÊêÔôƠơƯư][a-zA-ZÀ-ỹẠ-ỵĂăÂâĐđÊêÔôƠơƯư/0-9\s]*$"))
            {
                view.Message = "Tên sản phẩm không hợp lệ!";
                view.Guna2TextBoxname.Focus();
                return false;
            }
            return true;
        }
    }
}
