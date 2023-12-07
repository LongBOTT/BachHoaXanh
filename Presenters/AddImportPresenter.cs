using BachHoaXanh._Repositories;
using BachHoaXanh.Dialog;
using BachHoaXanh.Main;
using BachHoaXanh.Models;
using BachHoaXanh.User_Control;
using BachHoaXanh.Views.InterfaceView;
using Guna.UI2.AnimatorNS;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BachHoaXanh.Presenters
{
    public class AddImportPresenter
    {
        private IAddImportView view;
        private IImportRepository repository;
        private IProductRepository productRepository;
        private ISupplierRepository supplierRepository;
        private List<Supplier> suppliers;
        private List<int> productsInImport;
        private Import newImport;

        public AddImportPresenter(IAddImportView view, IImportRepository repository) 
        {
            this.view = view;
            this.repository = repository;
            supplierRepository = new SupplierRepository();
            productRepository = new ProductRepository();
            suppliers = (List<Supplier>)supplierRepository.GetAll();
            productsInImport = new List<int> { };
            this.view.ShowDetail += ShowDetail;
            this.view.LoadListSupplier += LoadListSupplier;
            this.view.SelectedRow += SelectedRow;
            this.view.AddImport += AddImport;
            this.view.Refresh += Refresh;
            this.view.Guna2GradientButtonAddProduct.Click += AddProduct;
            view.Guna2DataGridView.Click += AddDetail;
        }

        private void AddProduct(object? sender, EventArgs e)
        {
            IAddProductView view = new FormAddProduct();
            IProductView Proview = new ProductControl();
            IProductRepository productRepository1 = new ProductRepository();
            ProductPresenter productPresenter = new ProductPresenter(Proview, productRepository1);
            AddProductPresenter addProductPresenter = new AddProductPresenter(view, productRepository, newImport.Supplier_id);
            view.show();

            productRepository = new ProductRepository();
            IBrandRepository brandRepository = new BrandRepository();

            List<Brand> brands = brandRepository.FindBrandsBy(new Dictionary<string, object>() { { "supplier_id", newImport.Supplier_id } });
            List<Product> products = new List<Product>();
            List<int> idBrand = new List<int>();

            foreach (Brand brand in brands)
            {
                idBrand.Add(brand.Id);
            }

            foreach (Product product in productRepository.GetAll())
            {
                if (idBrand.Contains(product.Brand_id))
                    products.Add(product);
            }

            LoadListProduct(products);
        }

        private void Refresh(object? sender, EventArgs e)
        {
            ShowDetail(sender, e);
            view.Guna2TextBoxSupplierID.Text = "";
        }

        private void AddImport(object? sender, EventArgs e)
        {
            if (productsInImport.Count == 0)
            {
                int id = int.Parse(view.Guna2TextBoxID.Text);
                int staffID = int.Parse(view.Guna2TextBoxStaffID.Text);
                DateTime receivedDate = DateTime.Parse(view.Guna2TextBoxReceived.Text);
                int supplierID = int.Parse(view.Guna2TextBoxSupplierID.Text);
                newImport = new Import(id, staffID, receivedDate, 0, supplierID);
                IBrandRepository brandRepository = new BrandRepository();
                List<Brand> brands = brandRepository.FindBrandsBy(new Dictionary<string, object>() { { "supplier_id", newImport.Supplier_id } });
                List<Product> products = new List<Product>();
                List<int> idBrand = new List<int>();

                foreach (Brand brand in brands)
                {
                    idBrand.Add(brand.Id);
                }

                foreach (Product product in productRepository.GetAll())
                {
                    if (idBrand.Contains(product.Brand_id))
                        products.Add(product);
                }

                LoadListProduct(products);
                view.Guna2GradientButtonAddProduct.Visible = true;
                view.Message = "Vui lòng chọn sản phẩm nhập!";
            }
            else
            {
                DialogResult result = MessageBox.Show("Xác nhận nhập các sản phẩm đã chọn?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (result == DialogResult.OK)
                {
                    view.close();
                    IAddShipmentView addShipmentView = new FormAddShipment();
                    IShipmentRepository shipmentRepository = new ShipmentRepository();
                    AddShipmentPresenter addShipmentPresenter = new AddShipmentPresenter(addShipmentView, shipmentRepository, newImport, productsInImport);
                    addShipmentView.show();
                }
            }
        }

        private void LoadListProduct(List<Product> products)
        {
            DataGridViewColumn column1 = new DataGridViewTextBoxColumn();
            DataGridViewColumn column2 = new DataGridViewTextBoxColumn();
            DataGridViewColumn column3 = new DataGridViewCheckBoxColumn();
            column1.HeaderText = "Mã sản phẩm";
            column2.HeaderText = "Tên sản phẩm";
            column3.HeaderText = "";
            view.Guna2DataGridView.Columns.Clear();
            view.Guna2DataGridView.Rows.Clear();
            view.Guna2DataGridView.Columns.AddRange(column1, column2, column3);
            
            foreach (Product product1 in products)
            {
                Console.WriteLine(product1);
                if (productsInImport.Contains(product1.Id) && productsInImport.Count > 0)
                    view.Guna2DataGridView.Rows.Add(product1.Id, product1.Name, true);
                else
                    view.Guna2DataGridView.Rows.Add(product1.Id, product1.Name, false);
            }
            
            view.Guna2DataGridView.Visible = true;
        }

        private void AddDetail(object? sender, EventArgs e)
        {
            if (view.Guna2DataGridView.Columns[0].HeaderCell.Value.ToString() == "Mã nhà cung cấp")
                return;
            if (view.Guna2DataGridView.SelectedRows.Count <= 0)
                return;
            int index = view.Guna2DataGridView.SelectedRows[0].Index;
            int id = int.Parse(view.Guna2DataGridView.SelectedRows[0].Cells[0].Value.ToString());
            Product product = productRepository.FindProductsBy(new Dictionary<string, object>() { { "id", id } })[0];
            if (!(bool)view.Guna2DataGridView.SelectedRows[0].Cells[2].Value)
            {
                productsInImport.Add(id);
                view.Guna2DataGridView.Rows[index].Cells[2].Value = true;
            }
            else
            {
                productsInImport.Remove(id);
                view.Guna2DataGridView.Rows[index].Cells[2].Value = false;
            }
        }

        private void LoadListSupplier(object? sender, EventArgs e)
        {
            productsInImport.Clear();
            DataGridViewColumn column1 = new DataGridViewTextBoxColumn();
            DataGridViewColumn column2 = new DataGridViewTextBoxColumn();
            DataGridViewColumn column3 = new DataGridViewTextBoxColumn();
            column1.HeaderText = "Mã nhà cung cấp";
            column2.HeaderText = "Tên nhà cung cấp";
            column3.HeaderText = "SĐT";
            view.Guna2DataGridView.Columns.Clear();
            view.Guna2DataGridView.Rows.Clear();
            view.Guna2DataGridView.Columns.AddRange(column1, column2, column3);

            foreach (Supplier supplier in suppliers)
            {
                view.Guna2DataGridView.Rows.Add(supplier.Id, supplier.Name, supplier.Phone);

            }
            view.Guna2DataGridView.Visible = true;
            if (view.Guna2TextBoxSupplierID.PlaceholderText != "Chọn nhà cung cấp")
            {
                Supplier supplier = supplierRepository.FindSuppliersBy(new Dictionary<string, object>() { { "id", Convert.ToInt16(view.Guna2TextBoxSupplierID.Text) } })[0];
                int index = Repository.GetIndex(supplier, "id", suppliers);
                view.Guna2DataGridView.Rows[index].Selected = true;
            }
            view.Guna2GradientButtonAddProduct.Visible = false;
        }

        private void SelectedRow(object? sender, EventArgs e)
        {
            if (view.Guna2DataGridView.Columns[0].HeaderCell.Value.ToString() != "Mã nhà cung cấp")
                return;
            string id = "";
            if (view.Guna2DataGridView.SelectedRows.Count > 0)
            {
                id = view.Guna2DataGridView.SelectedRows[0].Cells[0].Value.ToString();
                view.Guna2TextBoxSupplierID.Text = id;

            }
        }
        private void ShowDetail(object? sender, EventArgs e)
        {
            view.Guna2TextBoxID.Text = repository.GetAutoID().ToString();
            view.Guna2TextBoxStaffID.Text = Menu.Account.StaffID.ToString();
            view.Guna2TextBoxReceived.Text = DateTime.Now.ToString("d");
            view.Guna2TextBoxTotal.Text = "0";
        }
    }
}
