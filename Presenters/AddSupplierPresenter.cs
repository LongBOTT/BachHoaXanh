using BachHoaXanh._Repositories;
using BachHoaXanh.Models;
using BachHoaXanh.Views.InterfaceView;

namespace BachHoaXanh.Presenters
{
    internal class AddSupplierPresenter
    {
        private IAddSupplierView view;
        private ISupplierRepository repository;
        public AddSupplierPresenter(IAddSupplierView view, ISupplierRepository repository)
        {
            this.view = view;
            this.repository = repository;
            this.view.ShowDetail += ShowDetail;
            this.view.AddSupplier += AddSupplier;
            this.view.Refresh += Refresh;
        }

        private void ShowDetail(object? sender, EventArgs e)
        {
            view.Guna2TextBoxID.Text = repository.GetAutoID().ToString();
        }

        private void AddSupplier(object? sender, EventArgs e)
        {
            int id = Convert.ToInt16(view.Guna2TextBoxID.Text);
            string name = view.Guna2TextBoxName.Text;
            string phone = view.Guna2TextBoxPhone.Text;
            string address = view.Guna2TextBoxAddress.Text;
            string email = view.Guna2TextBoxEmail.Text;

            Supplier account = new Supplier(id, name, phone, address, email, false);
            if (repository.Add(account) == 1)
            {
                view.Message = "Thêm nhà cung cấp thành công!";
                view.close();
                SupplierPresenter.repository = new SupplierRepository();
                SupplierPresenter.supplierList = SupplierPresenter.repository.GetAll();
                SupplierPresenter.LoadSupplierList(SupplierPresenter.supplierList);
            }
            else
            {
                view.Message = "Thêm nhà cung cấp không thành công!";
            }

        }

        private void Refresh(object? sender, EventArgs e)
        {
            ShowDetail(sender, e);
            view.Guna2TextBoxName.Text = "";
            view.Guna2TextBoxPhone.Text = "";
            view.Guna2TextBoxAddress.Text = "";
            view.Guna2TextBoxEmail.Text = "";
        }
    }
}
