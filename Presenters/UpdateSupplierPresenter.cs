using BachHoaXanh._Repositories;
using BachHoaXanh.Models;
using BachHoaXanh.Views.InterfaceView;
using System.Text.RegularExpressions;

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
            DialogResult result = MessageBox.Show("Xác nhận sửa nhà cung cấp?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                int id = Convert.ToInt16(view.Guna2TextBoxID.Text);
                string name = view.Guna2TextBoxName.Text;
                string phone = view.Guna2TextBoxPhone.Text;
                string address = view.Guna2TextBoxAddress.Text;
                string email = view.Guna2TextBoxEmail.Text;

                if (checkInput())
                {
                    Supplier account = new Supplier(id, name, phone, address, email, false);
                    if (repository.Update(account) == 1)
                    {
                        view.Message = "Cập nhât nhà cung cấp thành công!";
                        view.close();
                        SupplierPresenter.repository = new SupplierRepository();
                        SupplierPresenter.supplierList = SupplierPresenter.repository.GetAll();
                        SupplierPresenter.LoadSupplierList(SupplierPresenter.supplierList);
                    }
                    view.Message = "Cập nhật nhà cung cấp không thành công!";
                }
            }
        }

        private void Refresh(object? sender, EventArgs e)
        {
            ShowDetail(sender, e);
        }

        public Boolean checkInput()
        {
            string name = view.Guna2TextBoxName.Text;
            string phone = view.Guna2TextBoxPhone.Text;
            string address = view.Guna2TextBoxAddress.Text;
            string email = view.Guna2TextBoxEmail.Text;

            if (string.IsNullOrEmpty(name) || !Regex.IsMatch(name, @"^[a-zA-ZÀ-ỹẠ-ỵĂăÂâĐđÊêÔôƠơƯư/0-9\s]*[a-zA-ZÀ-ỹẠ-ỵĂăÂâĐđÊêÔôƠơƯư][a-zA-ZÀ-ỹẠ-ỵĂăÂâĐđÊêÔôƠơƯư/0-9\s]*$"))
            {
                view.Message = "Tên nhà cung cấp không hợp lệ!";
                view.Guna2TextBoxName.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(phone) || !Regex.IsMatch(phone, @"^(\+?84|0)[35789]\d{8}$"))
            {
                view.Message = "Số điện thoại phải bắt đầu từ \"0x\" hoặc \"+84x\" hoặc \"84x\"\nvới \"x\" thuộc {3, 5, 7, 8, 9}";
                view.Guna2TextBoxPhone.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(address) || !Regex.IsMatch(address, @"^[a-zA-ZÀ-ỹẠ-ỵĂăÂâĐđÊêÔôƠơƯư/0-9\\s]*[/a-zA-ZÀ-ỹẠ-ỵĂăÂâĐđÊêÔôƠơƯư][a-zA-ZÀ-ỹẠ-ỵĂăÂâĐđÊêÔôƠơƯư/0-9\\s]*$"))
            {
                view.Message = "Địa chỉ không hợp lê";
                view.Guna2TextBoxAddress.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(email) || !Regex.IsMatch(email, @"^\\w+(\\.\\w+)*@\\w+(\\.\\w+)+"))
            {
                view.Message = "Email phải theo định dạng \\\"username@domain.name\\";
                view.Guna2TextBoxEmail.Focus();
                return false;
            }
            return true;
        }
    }
}
