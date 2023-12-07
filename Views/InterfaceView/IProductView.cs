using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Views.InterfaceView
{
    public interface IProductView
    {
        Guna2DataGridView Guna2DataGridView { get; set; }
        string SearchValue { get; set; }
        string Attribute { get; set; }

        Guna2TextBox txtNameBrand { get; }
        Guna2ComboBox cbbSupplier { get; }
        Guna2TextBox txtNameCategory { get; }
        Guna2TextBox txtQuantityCategory { get; }
        Guna2TextBox txtSearch { get; }
        event EventHandler ShowBrand_Category;

        event EventHandler SearchEvent;
        event EventHandler ShowDetail;
        event EventHandler AddNewEvent;
        event EventHandler UpdateEvent;
        event EventHandler DeleteEvent;
        event EventHandler AddCategoryEvent;
        event EventHandler AddBrandEvent;
        event EventHandler DeleteCategoryEvent;
        event EventHandler DeleteBrandEvent;
    }
}
