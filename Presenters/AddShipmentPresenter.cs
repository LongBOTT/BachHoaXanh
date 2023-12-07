using BachHoaXanh._Repositories;
using BachHoaXanh.Main;
using BachHoaXanh.Models;
using BachHoaXanh.Views.InterfaceView;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BachHoaXanh.Presenters
{
    public class AddShipmentPresenter
    {
        private IAddShipmentView view;
        private IShipmentRepository repository;
        private IProductRepository productRepository;
        private Import newImport;
        private List<int> productIDInImport;
        private List<Shipment> shipments;

        public AddShipmentPresenter(IAddShipmentView view, IShipmentRepository repository, Import newImport, List<int> productIDInImport) 
        {
            this.view = view;
            this.repository = repository;
            this.newImport = newImport;
            this.productIDInImport = productIDInImport;
            shipments = new List<Shipment>() { };
            productRepository = new ProductRepository();
            foreach (int i in productIDInImport)
            {
                Shipment shipment = new Shipment();
                shipment.Product_id = i;
                shipment.Import_id = newImport.Id;
                shipment.Sku = "00" + newImport.Id + "00" + i;
                shipments.Add(shipment);
            }

            this.view.ShowDetail += ShowDetail;
            this.view.ConfirmShipment += Confirm;
            this.view.AddShipment += Add;

            LoadListProduct();
        }

        private void Add(object? sender, EventArgs e)
        {
            foreach (Shipment shipment in shipments)
            {
                if (shipment.Quantity <= 0)
                {
                    MessageDialog.Show(MiniSupermarketApp.menu, "Nhập thông tin lô hàng đầy đủ!", "Thông báo", MessageDialogButtons.OK, MessageDialogIcon.Information);
                    return;
                }
                if (shipment.Unit_price <= 0)
                {
                    MessageDialog.Show(MiniSupermarketApp.menu, "Nhập thông tin lô hàng đầy đủ!", "Thông báo", MessageDialogButtons.OK, MessageDialogIcon.Information);
                    return;
                }
                if (shipment.Mfg == null)
                {
                    MessageDialog.Show(MiniSupermarketApp.menu, "Nhập thông tin lô hàng đầy đủ!", "Thông báo", MessageDialogButtons.OK, MessageDialogIcon.Information);
                    return;
                }
                if (shipment.Exp == null)
                {
                    MessageDialog.Show(MiniSupermarketApp.menu, "Nhập thông tin lô hàng đầy đủ!", "Thông báo", MessageDialogButtons.OK, MessageDialogIcon.Information);
                    return;
                }
            }

            DialogResult result = MessageBox.Show("Xác nhận nhập các lô hàng đã chọn?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                double total = 0;
                foreach (Shipment shipment in shipments)
                {
                    shipment.Remain = shipment.Quantity;
                    total += shipment.Quantity * shipment.Unit_price;
                }
                newImport.Total = total;
                IImportRepository importRepository = new ImportRepository();
                if (importRepository.Add(newImport) == 1)
                {
                    foreach (Shipment shipment in shipments)
                    {
                        repository = new ShipmentRepository();
                        shipment.Id = repository.GetAutoID();
                        Console.WriteLine(shipment);
                        repository.Add(shipment);
                    }
                    MessageDialog.Show(MiniSupermarketApp.menu, "Nhập lô hàng thành công!", "Thông báo", MessageDialogButtons.OK, MessageDialogIcon.Information);
                    this.view.close();
                    return;
                }
                MessageDialog.Show(MiniSupermarketApp.menu, "Nhập lô hàng không thành công!", "Thông báo", MessageDialogButtons.OK, MessageDialogIcon.Information);

            }
        }

        private void Confirm(object? sender, EventArgs e)
        {
            if (view.Guna2DataGridView.SelectedRows.Count <= 0)
                return;
            int index = view.Guna2DataGridView.SelectedRows[0].Index;
            shipments[index].Unit_price = double.Parse(view.Guna2TextBoxUnitPRice.Text);
            shipments[index].Quantity = double.Parse(view.Guna2TextBoxQuantity.Text);
            shipments[index].Mfg = view.Guna2DateTimePickerMfg.Value;
            shipments[index].Exp = view.Guna2DateTimePickerExp.Value;
            MessageDialog.Show(MiniSupermarketApp.menu, "Xác nhận lô hàng thành công!", "Thông báo", MessageDialogButtons.OK, MessageDialogIcon.Information);

        }

        private void LoadListProduct()
        {
            DataGridViewColumn column1 = new DataGridViewTextBoxColumn();
            DataGridViewColumn column2 = new DataGridViewTextBoxColumn();
            column1.HeaderText = "Mã sản phẩm";
            column2.HeaderText = "Tên sản phẩm";
            view.Guna2DataGridView.Columns.Clear();
            view.Guna2DataGridView.Rows.Clear();
            view.Guna2DataGridView.Columns.AddRange(column1, column2);
            
            foreach (int id in productIDInImport)
            {
                Product product = productRepository.FindProductsBy(new Dictionary<string, object>() { { "id", id } })[0];
                view.Guna2DataGridView.Rows.Add(product.Id, product.Name);
            }
             view.Guna2DataGridView.Visible = true;
        }

        private void ShowDetail(object? sender, EventArgs e)
        {
            if (view.Guna2DataGridView.SelectedRows.Count <= 0)
                return;
            int index = view.Guna2DataGridView.SelectedRows[0].Index;
            view.Guna2TextBoxProductID.Text = shipments[index].Product_id.ToString();
            view.Guna2TextBoxUnitPRice.Text = shipments[index].Unit_price.ToString();
            view.Guna2TextBoxQuantity.Text = shipments[index].Quantity.ToString();
            if (shipments[index].Mfg != DateTime.Parse("1/1/0001 12:00:00 AM"))
                view.Guna2DateTimePickerMfg.Value = shipments[index].Mfg;
            if (shipments[index].Exp != DateTime.Parse("1/1/0001 12:00:00 AM"))
                view.Guna2DateTimePickerExp.Value = shipments[index].Exp;
            view.Guna2TextBoxSku.Text = shipments[index].Sku.ToString();
        }
    }
}
