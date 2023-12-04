using BachHoaXanh._Repositories;
using BachHoaXanh.Models;
using BachHoaXanh.Views.InterfaceView;

namespace BachHoaXanh.Presenters
{
    internal class ShowDetailSupplierPresenter
    {
        private IShowDetailSupplierView view;
        private ISupplierRepository repository;
        private IBrandRepository BrandRepository;
        private IProductRepository ProductRepository;
        private Supplier supplier;
        public ShowDetailSupplierPresenter(IShowDetailSupplierView view, ISupplierRepository repository)
        {
            this.view = view;
            this.repository = repository;
            BrandRepository = new BrandRepository();
            ProductRepository = new ProductRepository();
            supplier = this.view.GetSupplier;
            this.view.ShowDetail += ShowDetail;
        }

        private void LoadProduct()
        {
            List<Brand> brands = BrandRepository.FindBrandsBy(new Dictionary<string, object>() { { "supplier_id", supplier.Id } });
            List<Product> products = new List<Product>();
            List<int> idBrand = new List<int>();

            foreach (Brand brand in brands)
            {
                idBrand.Add(brand.Id);
            }

            foreach (Product product in ProductRepository.GetAll())
            {
                if (idBrand.Contains(product.Brand_id))
                    products.Add(product);
            }

            foreach (Product product1 in products)
            {
                view.Guna2DataGridView.Rows.Add(product1.Id, product1.Name);
            }
        }

        private void ShowDetail(object? sender, EventArgs e)
        {
            LoadProduct();
            view.Guna2TextBoxID.Text = supplier.Id.ToString();
            view.Guna2TextBoxName.Text = supplier.Name.ToString();
            view.Guna2TextBoxPhone.Text = supplier.Phone.ToString();
            view.Guna2TextBoxAddress.Text = supplier.Address.ToString();
            view.Guna2TextBoxEmail.Text = supplier.Email.ToString();
        }
    }
}
