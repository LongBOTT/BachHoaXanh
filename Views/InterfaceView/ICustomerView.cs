using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Views.InterfaceView
{
    public interface ICustomerView
    {
        int Id { get; set; }
        string Name { get; set; }
        bool Gender { get; set; }
        DateTime Birthday { get; set; }
        string phone { get; set; }
        bool membership { get; set; }
        DateTime signed_up_DateTime { get; set; }
        bool deleted { get; set; }

        Guna2DataGridView Guna2DataGridView { get; set; }


        bool IsEdit { get; set; }
        bool IsSuccessful { get; set; }
        string Message { get; set; }

        event EventHandler SearchEvent;
        event EventHandler AddNewEvent;
        event EventHandler EditEvent;
        event EventHandler DeleteEvent;
        event EventHandler SaveEvent;
        event EventHandler CancelEvent;

        void SetCustomerListBindingSource(BindingSource customerList);
        void Show();
    }
}
