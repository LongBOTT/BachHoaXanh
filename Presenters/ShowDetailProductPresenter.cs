using BachHoaXanh._Repositories;
using BachHoaXanh.Models;
using BachHoaXanh.Properties;
using BachHoaXanh.Views.InterfaceView;
using System;
using System.Resources;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BachHoaXanh.Presenters
{
    public class ShowDetailProductPresenter
    {
        private IShowDetailProductView view;
        private IProductRepository repository;
        private Product product;
        private IBrandRepository brandRepository;
        private ICategoryRepository categoryRepository;
        private List<Category> categorys;
        private List<Brand> brands;
        public ShowDetailProductPresenter(IShowDetailProductView view, IProductRepository repository) 
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
        }

        private void LoadListCategory(object? sender, EventArgs e)
        {
            int index = -1;
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
                if (category.Id == product.Category_id)
                    index = view.Guna2DataGridView.RowCount - 1;
            }
             view.Guna2DataGridView.Visible = true;
             view.Guna2DataGridView.Rows[index].Selected = true;
        }

        private void LoadListBrand(object? sender, EventArgs e)
        {
            int index = -1;
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
                if (brand.Id == product.Brand_id)
                    index = view.Guna2DataGridView.RowCount - 1;
            }
            view.Guna2DataGridView.Visible = true;
            view.Guna2DataGridView.Rows[index].Selected = true;
        }

        private void ShowDetail(object? sender, EventArgs e)
        {
            ResourceManager resourceManager = new ResourceManager(typeof(Resources));
            view.pictureBox.Image = (Image)resourceManager.GetObject(product.Image);
            view.Guna2TextBoxID.Text = product.Id.ToString();
            view.Guna2TextBoxname.Text = product.Name.ToString();
            view.Guna2TextBoxBrand.Text = product.Brand_id.ToString();
            view.Guna2TextBoxCategory.Text = product.Category_id.ToString();
            view.Guna2TextBoxUnit.Text = product.Unit.ToString();
            view.Guna2TextBoxCost.Text = product.Cost.ToString();
            view.Guna2TextBoxQuantity.Text = product.Quantity.ToString();
            view.Guna2TextBoxBarcode.Text = product.Barcode.ToString();
        }
    }
}
