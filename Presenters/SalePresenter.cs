using BachHoaXanh._Repositories;
using BachHoaXanh.Dialog;
using BachHoaXanh.Main;
using BachHoaXanh.Models;
using BachHoaXanh.Properties;
using BachHoaXanh.Views.InterfaceView;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Resources;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BachHoaXanh.Presenters
{
    public class SalePresenter
    {
        private ISaleView view;
        private IProductRepository repository;
        private IBrandRepository brandRepository;
        private ICategoryRepository categoryRepository;
        private List<FlowLayoutPanel> listProduct;
        private List<Guna2HtmlLabel> listProductLabel;
        private List<Guna2PictureBox> listProductImage;
        private List<int> productID;
        private List<int> productInCart;
        private List<int> productInBill;
        private List<double> quantityInBill;
        private List<double> totalInBill;
        private List<List<Object>> Cart;
        private int index = -1;
        private double total = 0;
        public SalePresenter(ISaleView view, IProductRepository repository)
        {
            this.view = view;
            this.repository = repository;
            brandRepository = new BrandRepository();
            categoryRepository = new CategoryRepository();
            listProduct = new List<FlowLayoutPanel>();
            listProductLabel = new List<Guna2HtmlLabel>();
            listProductImage = new List<Guna2PictureBox>();
            productID = new List<int>();
            productInCart = new List<int>();
            productInBill = new List<int>();
            quantityInBill = new List<double>();
            totalInBill = new List<double>();
            Cart = new List<List<object>>();

            this.view.LoadProduct += LoadProduct;
            this.view.Guna2TextBoxQuantity.KeyPress += checkInput;
            this.view.Guna2GradientButtonAddProduct.Click += AddProductToCart;
            this.view.Guna2GradientButtonDeleteProduct.Click += DeleteProductFromCart;
            this.view.Guna2DataGridView.CellMouseClick += FillDetail;
            this.view.Guna2GradientButtonSearch.Click += SearchProduct;
            this.view.Guna2GradientButtonConfirm.Click += ConfirmCart;
            this.view.Guna2GradientButtonCancel.Click += Refresh;
            this.view.Guna2GradientButtonPay.Click += PayBill;
            this.view.Guna2TextBoxReceived.KeyPress += Calculate;

            IStaffRepository staffRepository = new StaffRepository();
            this.view.Guna2TextBoxStaffName.Text = staffRepository.FindStaffsBy(new Dictionary<string, object>() { { "id", Menu.Account.StaffID} })[0].Name;
            this.view.Guna2TextBoxDate.Text = DateTime.Now.ToString("d");
        }

        private void SearchProduct(object? sender, EventArgs e)
        {
            string value = view.Guna2TextBoxSearch.Text;
            if (value != "")
            {
                List<Product> products = repository.FindProducts("name", value);
                LoadProduct(products);
            }
            else
            {
                LoadProduct(repository.GetAll());
            }
        }

        private void checkInput(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

        }

        private void LoadProduct(object? sender, EventArgs e)
        {
            LoadProduct(repository.GetAll());
        }

        private void LoadProduct(IEnumerable<Product> products)
        {
            view.ContainerProduct.Controls.Clear();
            index = -1;
            listProduct.Clear();
            productID.Clear();
            listProductLabel.Clear();
            listProductImage.Clear();
            foreach (Product product in products)
            {
                Guna2GradientPanel guna2GradientPanel = new Guna2GradientPanel();
                guna2GradientPanel.Size = new Size(412, 230);
                guna2GradientPanel.BorderRadius = 20;
                guna2GradientPanel.FillColor = Color.FromArgb(189, 210, 219);
                guna2GradientPanel.FillColor2 = Color.FromArgb(189, 210, 219);

                FlowLayoutPanel panel = new FlowLayoutPanel();
                panel.Size = new Size(401, 208);
                panel.Location = new Point(7, 10);
                panel.BackColor = Color.FromArgb(189, 210, 219);
                guna2GradientPanel.Controls.Add(panel);

                Guna2HtmlLabel label = new Guna2HtmlLabel();
                label.Text = product.Name + "<br>" + "SL: " + product.Quantity;
                label.Font = new Font(FontFamily.GenericSansSerif, 11F, FontStyle.Regular, GraphicsUnit.Point);
                label.MaximumSize = new Size(401, 70);
                label.Dock = DockStyle.Top;
                label.AutoSize = false;
                panel.Controls.Add(label);

                Guna2PictureBox pictureBox = new Guna2PictureBox();
                ResourceManager resourceManager = new ResourceManager(typeof(Resources));
                pictureBox.Image = (Image)resourceManager.GetObject(product.Image);
                pictureBox.WaitOnLoad = true;
                panel.Controls.Add(pictureBox);

                view.ContainerProduct.Controls.Add(guna2GradientPanel);
                listProduct.Add(panel);
                listProductLabel.Add(label);
                listProductImage.Add(pictureBox);
                productID.Add(product.Id);

            }

            foreach (FlowLayoutPanel flowLayoutPanel in listProduct)
            {
                flowLayoutPanel.Click += LoadDetaillProduct;
            }

            foreach (Guna2HtmlLabel label in listProductLabel)
            {
                label.Click += LoadDetaillProduct;
            }

            foreach (Guna2PictureBox image in listProductImage)
            {
                image.Click += LoadDetaillProduct;
            }

            view.Guna2TextBoxId.Text = "";
            view.Guna2TextBoxName.Text = "";
            view.Guna2TextBoxBrand.Text = "";
            view.Guna2TextBoxCategory.Text = "";
            view.Guna2TextBoxCost.Text = "";
            view.Guna2TextBoxBarcode.Text = "";
            view.Guna2TextBoxQuantity.Text = "";
            view.Guna2GradientButtonDeleteProduct.Visible = false;
        }

        private void LoadDetaillProduct(object? sender, EventArgs e)
        {
            if (sender.GetType() == typeof(FlowLayoutPanel))
            {
                for (int i = 0; i < listProduct.Count; i++)
                {
                    if (listProduct[i] == sender)
                        index = productID[i];
                }
            }
            if (sender.GetType() == typeof(Guna2HtmlLabel))
            {
                for (int i = 0; i < listProductLabel.Count; i++)
                {
                    if (listProductLabel[i] == sender)
                        index = productID[i];
                }
            }
            if (sender.GetType() == typeof(Guna2PictureBox))
            {
                for (int i = 0; i < listProductImage.Count; i++)
                {
                    if (listProductImage[i] == sender)
                        index = productID[i];
                }
            }

            Product product = repository.FindProductsBy(new Dictionary<string, object>() { { "id", index } })[0];

            if (product != null)
            {
                LoadDetail(product);
            }

            
        }

        private void LoadDetail(Product product)
        {
            // check discount

            view.Guna2TextBoxId.Text = product.Id.ToString();
            view.Guna2TextBoxName.Text = product.Name.ToString();
            Brand brand = brandRepository.FindBrandsBy(new Dictionary<string, object>() { { "id", product.Brand_id } })[0];
            view.Guna2TextBoxBrand.Text = brand.Name;
            Category category = categoryRepository.FindCategorysBy(new Dictionary<string, object>() { { "id", product.Category_id } })[0];
            view.Guna2TextBoxCategory.Text = category.Name;
            view.Guna2TextBoxCost.Text = product.Cost.ToString();
            view.Guna2TextBoxBarcode.Text = product.Barcode.ToString();
            view.Guna2TextBoxQuantity.Text = "0";
            if (productInCart.Contains(product.Id))
                view.Guna2GradientButtonDeleteProduct.Visible = true;
            else
                view.Guna2GradientButtonDeleteProduct.Visible = false;
        }

        private void AddProductToCart(object? sender, EventArgs e)
        {
            if (index == -1)
            {
                MessageDialog.Show(MiniSupermarketApp.menu, "Vui lòng chọn sản phẩm!", "Thông báo", MessageDialogButtons.OK, MessageDialogIcon.Information);
                return;
            }

            if (repository.FindProductsBy(new Dictionary<string, object>() { { "id", index } })[0].Quantity < Convert.ToDouble(view.Guna2TextBoxQuantity.Text))
            {
                MessageDialog.Show(MiniSupermarketApp.menu, "Số lượng sản phẩm trên kệ không đủ.\nVui lòng nhập thêm sản phẩm!", "Thông báo", MessageDialogButtons.OK, MessageDialogIcon.Information);
                return;
            }

            if (Convert.ToDouble(view.Guna2TextBoxQuantity.Text) <= 0)
            {
                MessageDialog.Show(MiniSupermarketApp.menu, "Số lượng sản phẩm mua không hợp lệ!", "Thông báo", MessageDialogButtons.OK, MessageDialogIcon.Information);
                return;
            }

            if (productInCart.Contains(index))
            {
                int indexRow = productInCart.IndexOf(index);
                Cart[indexRow][1] = Convert.ToDouble(view.Guna2TextBoxQuantity.Text);
                Cart[indexRow][2] = Convert.ToDouble(view.Guna2TextBoxQuantity.Text) * Convert.ToDouble(view.Guna2TextBoxCost.Text);
            }
            else
            {
                List<Object> list = new List<Object> { };
                list.Add(view.Guna2TextBoxName.Text);
                list.Add(Convert.ToDouble(view.Guna2TextBoxQuantity.Text));
                list.Add(Convert.ToDouble(view.Guna2TextBoxQuantity.Text) * Convert.ToDouble(view.Guna2TextBoxCost.Text));

                productInCart.Add(index);
                Cart.Add(list);
            }
            // check promotion

            LoadCartShopping(Cart);
            view.Guna2TextBoxId.Text = "";
            view.Guna2TextBoxName.Text = "";
            view.Guna2TextBoxBrand.Text = "";
            view.Guna2TextBoxCategory.Text = "";
            view.Guna2TextBoxCost.Text = "";
            view.Guna2TextBoxBarcode.Text = "";
            view.Guna2TextBoxQuantity.Text = "";
            view.Guna2GradientButtonDeleteProduct.Visible = false;
            index = -1;
        }

        private void DeleteProductFromCart(object? sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn muốn xoá sản phẩm khỏi giỏ hàng?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                int indexRow = view.Guna2DataGridView.SelectedRows[0].Index;
                Console.WriteLine(indexRow);
                productInCart.RemoveAt(indexRow);
                Cart.RemoveAt(indexRow);
                LoadCartShopping(Cart);
                view.Guna2TextBoxId.Text = "";
                view.Guna2TextBoxName.Text = "";
                view.Guna2TextBoxBrand.Text = "";
                view.Guna2TextBoxCategory.Text = "";
                view.Guna2TextBoxCost.Text = "";
                view.Guna2TextBoxBarcode.Text = "";
                view.Guna2TextBoxQuantity.Text = "";
                view.Guna2GradientButtonDeleteProduct.Visible = false;
                index = -1;
            }
            
        }


        private void ConfirmCart(object? sender, EventArgs e)
        {
            if (productInBill.Count > 0)
            {
                MessageDialog.Show(MiniSupermarketApp.menu, "Không thể thay đổi giỏ hàng!\nVui lòng thanh toán hoặc huỷ hoá đơn!", "Thông báo", MessageDialogButtons.OK, MessageDialogIcon.Information);
                return;
            }

            if (productInCart.Count == 0)
            {
                MessageDialog.Show(MiniSupermarketApp.menu, "Vui lòng chọn sản phẩm!", "Thông báo", MessageDialogButtons.OK, MessageDialogIcon.Information);
                return;
            }

            DialogResult result = MessageBox.Show("Xác nhận giỏ hàng?\nBạn sẽ không thể thay đổi giỏ hàng!", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                total = 0;
                for (int i = 0; i < productInCart.Count; i++)
                {
                    Guna2GradientPanel guna2GradientPanel = new Guna2GradientPanel();
                    guna2GradientPanel.Size = new Size(401, 140);
                    guna2GradientPanel.BorderRadius = 20;
                    guna2GradientPanel.FillColor = Color.FromArgb(189, 210, 219);
                    guna2GradientPanel.FillColor2 = Color.FromArgb(189, 210, 219);

                    FlowLayoutPanel panel = new FlowLayoutPanel();
                    panel.Size = new Size(392, 120);
                    panel.Location = new Point(5, 8);
                    panel.BackColor = Color.FromArgb(189, 210, 219);
                    guna2GradientPanel.Controls.Add(panel);

                    Guna2HtmlLabel label = new Guna2HtmlLabel();
                    int id = i + 1;
                    label.Text = id + ". " + repository.FindProductsBy(new Dictionary<string, object> () { { "id", productInCart[i] } })[0].Name;
                    label.Font = new Font("Arial", 11F, FontStyle.Bold, GraphicsUnit.Point);
                    label.MaximumSize = new Size(392, 50);
                    label.Dock = DockStyle.Top;
                    label.AutoSize = false;
                    panel.Controls.Add(label);

                    Guna2HtmlLabel label1 = new Guna2HtmlLabel();
                    label1.Text = "Số lượng: " + Cart[i][1] ;
                    quantityInBill.Add(Convert.ToDouble(Cart[i][1].ToString()));
                    label1.Font = new Font("Arial", 11F, FontStyle.Regular, GraphicsUnit.Point);
                    label1.MinimumSize = new Size(392, 20);
                    label1.AutoSize = false;
                    panel.Controls.Add(label1);

                    Guna2HtmlLabel label2 = new Guna2HtmlLabel();
                    label2.Text = "Giá tiền: " + Cart[i][2] + " vnđ";
                    total += Convert.ToDouble(Cart[i][2].ToString());
                    totalInBill.Add(Convert.ToDouble(Cart[i][2].ToString()));
                    label2.Font = new Font("Arial", 11F, FontStyle.Regular, GraphicsUnit.Point);
                    label2.MinimumSize = new Size(392, 20);
                    label2.AutoSize = false;
                    panel.Controls.Add(label2);

                    view.ContainerProductInBill.Controls.Add(guna2GradientPanel);
                    listProduct.Add(panel);
                    productInBill.Add(productInCart[i]);
                }

                view.Guna2TextBoxTotal.Text = total.ToString();

                productInCart.Clear();
                Cart.Clear();
                LoadCartShopping(Cart);
                index = -1;
                view.Guna2TextBoxId.Text = "";
                view.Guna2TextBoxName.Text = "";
                view.Guna2TextBoxBrand.Text = "";
                view.Guna2TextBoxCategory.Text = "";
                view.Guna2TextBoxCost.Text = "";
                view.Guna2TextBoxBarcode.Text = "";
                view.Guna2TextBoxQuantity.Text = "";
                view.Guna2GradientButtonDeleteProduct.Visible = false;
            }
            
        }

        private void LoadCartShopping(List<List<object>> cart)
        {
            view.Guna2DataGridView.RowCount = 0;
            foreach (List<object> item in cart)
            {
                view.Guna2DataGridView.Rows.Add(item[0], item[1], item[2]);
            }
        }

        private void FillDetail(object? sender, EventArgs e)
        {
            if (view.Guna2DataGridView.SelectedRows.Count > 0)
            {
                int indexRow = view.Guna2DataGridView.SelectedRows[0].Index;
                if (indexRow < Cart.Count)
                {
                    int id = productInCart[indexRow];

                    Product selectedProduct = repository.FindProductsBy(new Dictionary<string, object>() { { "id", id } })[0];
                    LoadDetail(selectedProduct);

                    view.Guna2TextBoxQuantity.Text = Cart[indexRow][1].ToString();
                    index = productInCart[indexRow];
                }
            }
            
        }

        private void Refresh(object? sender, EventArgs e)
        {
            productInBill.Clear();
            view.ContainerProductInBill.Controls.Clear();
            productInCart.Clear();
            Cart.Clear();
            repository = new ProductRepository();
            LoadProduct(repository.GetAll());
            LoadCartShopping(Cart);
            index = -1;
            view.Guna2TextBoxId.Text = "";
            view.Guna2TextBoxName.Text = "";
            view.Guna2TextBoxBrand.Text = "";
            view.Guna2TextBoxCategory.Text = "";
            view.Guna2TextBoxCost.Text = "";
            view.Guna2TextBoxBarcode.Text = "";
            view.Guna2TextBoxQuantity.Text = "";
            view.Guna2TextBoxTotal.Text = "";
            view.Guna2TextBoxReceived.Text = "";
            view.Guna2TextBoxExcess.Text = "";

            view.Guna2GradientButtonDeleteProduct.Visible = false;
        }

        private void PayBill(object? sender, EventArgs e)
        {
            if (productInBill.Count <= 0)
            {
                MessageDialog.Show(MiniSupermarketApp.menu, "Vui lòng chọn sản phẩm!", "Thông báo", MessageDialogButtons.OK, MessageDialogIcon.Information);
                return;
            }

            if (view.Guna2TextBoxExcess.Text == "")
            {
                MessageDialog.Show(MiniSupermarketApp.menu, "Vui lòng nhập số tiền nhận và nhấn Enter!", "Thông báo", MessageDialogButtons.OK, MessageDialogIcon.Information);
                return;
            }

            if (view.Guna2TextBoxExcess.Text.Contains('-'))
            {
                MessageDialog.Show(MiniSupermarketApp.menu, "Không đủ tiền thanh toán!", "Thông báo", MessageDialogButtons.OK, MessageDialogIcon.Information);
                return;
            }

            DialogResult result = MessageBox.Show("Xác nhận lập hoá đơn?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                Receipt receipt = new Receipt();
                IReceiptRepository receiptRepository = new ReceiptRepository();
                receipt.Id = receiptRepository.GetAutoID();
                receipt.Staff_id = Menu.Account.StaffID;
                receipt.Total = Convert.ToDouble(view.Guna2TextBoxTotal.Text);
                receipt.Invoice_DateTime = Convert.ToDateTime(view.Guna2TextBoxDate.Text);
                receipt.Received = Convert.ToDouble(view.Guna2TextBoxReceived.Text);
                receipt.Excess = Convert.ToDouble(view.Guna2TextBoxExcess.Text);
                if (receiptRepository.Add(receipt) == 0)
                {
                    MessageDialog.Show(MiniSupermarketApp.menu, "Thanh toán không thành công!", "Thông báo", MessageDialogButtons.OK, MessageDialogIcon.Information);
                    return;
                }

                IReceipt_detailRepository receipt_detailRepository = new Receipt_detailRepository();
                for (int i = 0; i < productInBill.Count; i++)
                {
                    Receipt_detail receiptDetail = new Receipt_detail();
                    receiptDetail.Receipt_id = receipt.Id;
                    receiptDetail.Product_id = productInBill[i];
                    receiptDetail.Quantity = quantityInBill[i];
                    receiptDetail.Total = totalInBill[i];
                    receipt_detailRepository.Add(receiptDetail);

                    Product product = repository.FindProductsBy(new Dictionary<string, object>() { { "id", productInBill[i] } })[0];
                    product.Quantity = product.Quantity - quantityInBill[i];
                    repository.Update(product);
                }
                MessageDialog.Show(MiniSupermarketApp.menu, "Thanh toán thành công!", "Thông báo", MessageDialogButtons.OK, MessageDialogIcon.Information);
                Refresh(sender, e);
            }
            
        }

        private void Calculate(object? sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            if (e.KeyChar == (char)Keys.Enter)
            {
                double received = Convert.ToDouble(view.Guna2TextBoxReceived.Text);
                view.Guna2TextBoxExcess.Text = (received - total).ToString();
            }
        }

    }
}
