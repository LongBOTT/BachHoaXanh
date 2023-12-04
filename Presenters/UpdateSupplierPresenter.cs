using BachHoaXanh._Repositories;
using BachHoaXanh.Models;
using BachHoaXanh.Views.InterfaceView;

namespace BachHoaXanh.Presenters
{
    internal class UpdateSupplierPresenter
    {
        private IUpdateSupplierView view;
        private ISupplierRepository repository;
        private Supplier supplier;
        public UpdateSupplierPresenter(IUpdateSupplierView view, ISupplierRepository repository)
        {
            this.view = view;
            this.repository = repository;
            supplier = this.view.GetSupplier;
            this.view.ShowDetail += ShowDetail;
            this.view.UpdateSupplier += UpdateSupplier;
            this.view.Refresh += Refresh;
        }

        private void ShowDetail(object? sender, EventArgs e)
        {
            view.Guna2TextBoxID.Text = supplier.Id.ToString();
            view.Guna2TextBoxName.Text = supplier.Name.ToString();
            view.Guna2TextBoxPhone.Text = supplier.Phone.ToString();
            view.Guna2TextBoxAddress.Text = supplier.Address.ToString();
            view.Guna2TextBoxEmail.Text = supplier.Email.ToString();
        }

        private void UpdateSupplier(object? sender, EventArgs e)
        {
            int id = Convert.ToInt16(view.Guna2TextBoxID.Text);
            string name = view.Guna2TextBoxName.Text;
            string phone = view.Guna2TextBoxPhone.Text;
            string address = view.Guna2TextBoxAddress.Text;
            string email = view.Guna2TextBoxEmail.Text;

            Supplier account = new Supplier(id, name, phone, address, email, false);
            if (repository.Update(account) == 1)
            {
                view.Message = "Sửa nhà cung cấp thành công!";
                view.close();
                SupplierPresenter.repository = new SupplierRepository();
                SupplierPresenter.supplierList = SupplierPresenter.repository.GetAll();
                SupplierPresenter.LoadSupplierList(SupplierPresenter.supplierList);
            }
            else
            {
                view.Message = "Sửa nhà cung cấp không thành công!";
            }

        }

        private void Refresh(object? sender, EventArgs e)
        {
            ShowDetail(sender, e);
        }
    }
}
