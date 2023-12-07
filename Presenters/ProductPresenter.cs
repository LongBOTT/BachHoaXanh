using BachHoaXanh._Repositories;
using BachHoaXanh.Dialog;
using BachHoaXanh.Main;
using BachHoaXanh.Models;
using BachHoaXanh.Views;
using BachHoaXanh.Views.Dialog;
using BachHoaXanh.Views.InterfaceView;
using Guna.UI2.WinForms;
using Org.BouncyCastle.Asn1.Cms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Presenters
{
    public class ProductPresenter
    {
        private IProductView view;
        public static IProductRepository repository;
        public static IEnumerable<Product> productList;
        private IBrandRepository brandRepository;
        private ICategoryRepository categoryRepository;
        private ISupplierRepository supplierRepository;
        private static Guna2DataGridView Guna2DataGridView;
        public ProductPresenter(IProductView view, IProductRepository repository)
        {
            this.view = view;
            ProductPresenter.repository = repository;
            ProductPresenter.Guna2DataGridView = view.Guna2DataGridView;
            ProductPresenter.productList = ProductPresenter.repository.GetAll();
            brandRepository = new BrandRepository();
            categoryRepository = new CategoryRepository();
            supplierRepository = new SupplierRepository();
            LoadProductList(productList);

            this.view.SearchEvent += SearchProduct;
            this.view.ShowDetail += ShowDetail;
            this.view.AddNewEvent += AddNewEvent;
            this.view.UpdateEvent += UpdateEvent;
            this.view.DeleteEvent += DeleteEvent;
            this.view.ShowBrand_Category += ShowBrand_Category;
            this.view.AddBrandEvent += AddBrandEvent;
            this.view.AddCategoryEvent += AddCategoryEvent;
            this.view.DeleteBrandEvent += DeleteBrandEvent;
            this.view.DeleteCategoryEvent += DeleteCategoryEvent;

            foreach (Supplier supplier in supplierRepository.GetAll()) 
                view.cbbSupplier.Items.Add(supplier.Name);
        }

        private void DeleteCategoryEvent(object? sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Xác nhận xoá thể loại?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                string name = view.txtNameCategory.Text;
                if (categoryRepository.FindCategorysBy(new Dictionary<string, object>() { { "name", name } }).Count == 0)
                {
                    MessageDialog.Show(MiniSupermarketApp.menu, "Tên thể loại không tồn tại", "Thông báo", MessageDialogButtons.OK, MessageDialogIcon.Information);
                    return;
                }
                Category category = categoryRepository.FindCategorysBy(new Dictionary<string, object>() { { "name", name } })[0];
                if (repository.FindProductsBy(new Dictionary<string, object>() { { "category_id", category.Id } }).Count != 0)
                {
                    MessageDialog.Show(MiniSupermarketApp.menu, "Tồn tại sản phẩm thuộc thể loại không thể xoá!", "Thông báo", MessageDialogButtons.OK, MessageDialogIcon.Information);
                    return;
                }
                if (categoryRepository.Delete(new List<string>() { " id = " + category.Id }) == 1)
                {
                    MessageDialog.Show(MiniSupermarketApp.menu, "Xoá thể loại thành công!", "Thông báo", MessageDialogButtons.OK, MessageDialogIcon.Information);
                    return;
                }
            }
            MessageDialog.Show(MiniSupermarketApp.menu, "Xoá thể loại không thành công!", "Thông báo", MessageDialogButtons.OK, MessageDialogIcon.Information);
            return;
        }

        private void DeleteBrandEvent(object? sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Xác nhận xoá thương hiệu?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                string name = view.txtNameBrand.Text;
                if (brandRepository.FindBrandsBy(new Dictionary<string, object>() { { "name", name } }).Count == 0)
                {
                    MessageDialog.Show(MiniSupermarketApp.menu, "Tên thương hiệu không tồn tại", "Thông báo", MessageDialogButtons.OK, MessageDialogIcon.Information);
                    return;
                }
                Brand brand = brandRepository.FindBrandsBy(new Dictionary<string, object>() { { "name", name } })[0];
                if (repository.FindProductsBy(new Dictionary<string, object>() { { "brand_id", brand.Id } }).Count != 0)
                {
                    MessageDialog.Show(MiniSupermarketApp.menu, "Tồn tại sản phẩm thuộc thương hiệu không thể xoá!", "Thông báo", MessageDialogButtons.OK, MessageDialogIcon.Information);
                    return;
                }
                if (brandRepository.Delete(new List<string>() { " id = " + brand.Id }) == 1)
                {
                    MessageDialog.Show(MiniSupermarketApp.menu, "Xoá thương hiệu thành công!", "Thông báo", MessageDialogButtons.OK, MessageDialogIcon.Information);
                    return;
                }
            }
            MessageDialog.Show(MiniSupermarketApp.menu, "Xoá thương hiệu không thành công!", "Thông báo", MessageDialogButtons.OK, MessageDialogIcon.Information);
            return;
        }

        private void AddCategoryEvent(object? sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Xác nhận thêm thể loại?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                string name = view.txtNameCategory.Text;
                if (categoryRepository.FindCategorysBy(new Dictionary<string, object>() { { "name", name } }).Count > 0)
                {
                    MessageDialog.Show(MiniSupermarketApp.menu, "Tên thể loại đã tồn tại", "Thông báo", MessageDialogButtons.OK, MessageDialogIcon.Information);
                    return;
                }
                Category category = new Category();
                category.Id = categoryRepository.GetAutoID();
                category.Name = name;
                category.Quantity = 0;
                if (categoryRepository.Add(category) == 1)
                {
                    MessageDialog.Show(MiniSupermarketApp.menu, "Thêm thể loại thành công!", "Thông báo", MessageDialogButtons.OK, MessageDialogIcon.Information);
                    return;
                }
            }
            MessageDialog.Show(MiniSupermarketApp.menu, "Thêm thể loại không thành công!", "Thông báo", MessageDialogButtons.OK, MessageDialogIcon.Information);
            return;
        }

        private void AddBrandEvent(object? sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Xác nhận thêm thương hiệu?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                string name = view.txtNameBrand.Text;
                if (brandRepository.FindBrandsBy(new Dictionary<string, object>() { { "name", name} }).Count > 0)
                {
                    MessageDialog.Show(MiniSupermarketApp.menu, "Tên thương hiệu đã tồn tại", "Thông báo", MessageDialogButtons.OK, MessageDialogIcon.Information);
                    return;
                }
                Brand brand = new Brand();
                brand.Id = brandRepository.GetAutoID();
                brand.Name = name;
                int index = view.cbbSupplier.SelectedIndex;
                Supplier supplier = supplierRepository.GetAll().ElementAt(index);
                brand.Supplier_id = supplier.Id;
                if (brandRepository.Add(brand) == 1)
                {
                    MessageDialog.Show(MiniSupermarketApp.menu, "Thêm thương hiệu thành công!", "Thông báo", MessageDialogButtons.OK, MessageDialogIcon.Information);
                    return;
                }
            }
            MessageDialog.Show(MiniSupermarketApp.menu, "Thêm thương hiệu không thành công!", "Thông báo", MessageDialogButtons.OK, MessageDialogIcon.Information);
            return;
        }

        private void ShowBrand_Category(object? sender, EventArgs e)
        {
            if (Guna2DataGridView.SelectedRows.Count <= 0)
                return;
            int id = int.Parse(Guna2DataGridView.SelectedRows[0].Cells[0].Value.ToString());
            Product product = repository.FindProductsBy(new Dictionary<string, object>() { { "id", id } })[0];
            Brand brand = brandRepository.FindBrandsBy(new Dictionary<string, object>() { { "id", product.Brand_id } })[0];
            view.txtNameBrand.Text = brand.Name;
            view.cbbSupplier.SelectedItem = supplierRepository.FindSuppliersBy(new Dictionary<string, object>() { { "id", brand.Supplier_id } })[0].Name;

            Category category = categoryRepository.FindCategorysBy(new Dictionary<string, object>() { { "id", product.Category_id } })[0];
            view.txtNameCategory.Text = category.Name;
            view.txtQuantityCategory.Text = category.Quantity.ToString(); 
        }

        private void DeleteEvent(object? sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Xác nhận xoá sản phẩm?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                DataGridViewRow selectedRow = this.view.Guna2DataGridView.SelectedRows[0];
                int id = Convert.ToInt16(selectedRow.Cells["Column1"].Value.ToString());
                if (repository.Delete(new List<string> { " id = " + id }) == 1)
                {
                    MessageDialog.Show(MiniSupermarketApp.menu, "Xoá tài khoản thành công!", "Thông báo", MessageDialogButtons.OK, MessageDialogIcon.Information);
                    repository = new ProductRepository();
                    productList = repository.GetAll();
                    LoadProductList(productList);
                    return;
                }
            }
            MessageDialog.Show(MiniSupermarketApp.menu, "Xoá sản phẩm không thành công", "Thông báo", MessageDialogButtons.OK, MessageDialogIcon.Information);
            return;
        }

        private void UpdateEvent(object? sender, EventArgs e)
        {
            DataGridViewRow selectedRow = this.view.Guna2DataGridView.SelectedRows[0];
            int id = Convert.ToInt16(selectedRow.Cells["Column1"].Value.ToString());
            Product product = repository.FindProductsBy(new Dictionary<string, object>() { { "id", id } })[0];
            IUpdateProductView view = new FormUpdateProduct(product);
            IProductRepository productRepository = new ProductRepository();
            new UpdateProductPresenter(view, productRepository);
            view.show();
        }

        private void AddNewEvent(object? sender, EventArgs e)
        {
            IAddProductView view = new FormAddProduct();
            IProductRepository productRepository = new ProductRepository();
            AddProductPresenter addProductPresenter = new AddProductPresenter(view, productRepository);
            view.show();
        }

        private void ShowDetail(object? sender, EventArgs e)
        {
            DataGridViewRow selectedRow = this.view.Guna2DataGridView.SelectedRows[0];
            int id = Convert.ToInt16(selectedRow.Cells["Column1"].Value.ToString());
            Product product = repository.FindProductsBy(new Dictionary<string, object>() { { "id", id } })[0];
            IShowDetailProductView view = new FormDetailProduct(product);
            IProductRepository productRepository = new ProductRepository();
            new ShowDetailProductPresenter(view, productRepository);
            view.show();
        }

        private void SearchProduct(object sender, EventArgs e)
        {
            string attribute = this.view.Attribute;
            bool emptyValue = string.IsNullOrWhiteSpace(this.view.SearchValue);
            if (!emptyValue || attribute == "Sắp hết hàng")
            {
                if (attribute == "Tên sản phẩm")
                    SearchByProductName(this.view.SearchValue);
                if (attribute == "Thể loại")
                    SearchByCategoryName(this.view.SearchValue);
                if (attribute == "Thương hiệu")
                    SearchByBrandName(this.view.SearchValue);
                if (attribute == "Sắp hết hàng")
                {
                    SearchByQuantity();
                    view.txtSearch.Visible = false;
                    return;
                }
            }
            else
            {
                LoadProductList(repository.GetAll());
            }
            view.txtSearch.Visible = true;
            
            
        }

        private void SearchByQuantity()
        {
            List<Product> result = repository.SearchProduct(new List<string> { "deleted = 0", "quantity < 20" });
            LoadProductList(result);
        }

        private void SearchByBrandName(string searchValue)
        {
            List<Brand> brands = new List<Brand>();
            brands = brandRepository.FindBrands("name", searchValue);
            List<Product> result = new List<Product>();
            foreach (Brand brand in brands)
            {
                List<Product> accounts = repository.FindProductsBy(new Dictionary<string, object>() { { "brand_id", brand.Id } });
                if (accounts.Count > 0)
                    result.AddRange(accounts);
            }
            LoadProductList(result);
        }

        private void SearchByCategoryName(string searchValue)
        {
            List<Category> categorys = new List<Category>();
            categorys = categoryRepository.FindCategorys("name", searchValue);
            List<Product> result = new List<Product>();
            foreach (Category category in categorys)
            {
                List<Product> accounts = repository.FindProductsBy(new Dictionary<string, object>() { { "category_id", category.Id } });
                if (accounts.Count > 0)
                    result.AddRange(accounts);
            }
            LoadProductList(result);
        }

        private void SearchByProductName(string searchValue)
        {
            List<Product> result = repository.FindProducts("name", searchValue);
            LoadProductList(result);
        }

        public static void LoadProductList(IEnumerable<Product> products)
        {
            IBrandRepository brandRepository = new BrandRepository();
            ICategoryRepository categoryRepository = new CategoryRepository();
            Guna2DataGridView.Rows.Clear();
            foreach (Product product in products)
            {
                Category category = categoryRepository.FindCategorysBy(new Dictionary<string, object>() { { "id", product.Category_id } })[0];
                Brand brand = brandRepository.FindBrandsBy(new Dictionary<string, object>() { { "id", product.Brand_id } })[0];
                Guna2DataGridView.Rows.Add(product.Id, product.Name, product.Quantity, category.Name, brand.Name);
            }
        }
    }
}
