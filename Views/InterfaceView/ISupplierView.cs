using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Views.InterfaceView
{
    public interface ISupplierView
    {
        //int Id { get; set; }
        //string Name { get; set; }
        //string Phone { get; set; }
        //string Address { get; set; }
        //string Email { get; set; }
        //Boolean Deleted { get; set; }

        Guna2DataGridView Guna2DataGridView { get; set; }

        string SearchValue { get; set; }
        string Attribute { get; set; }


        event EventHandler SearchEvent;
        //event EventHandler AddNewEvent;
        //event EventHandler EditEvent;
        //event EventHandler DeleteEvent;
        //event EventHandler SaveEvent;
        //event EventHandler CancelEvent;

    }
}
