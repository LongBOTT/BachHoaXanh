using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BachHoaXanh.Views.InterfaceView
{
    public interface IDiscountView
    {
        int Id { get; set; }
        double Percent { get; set; }
        DateTime Start_Date { get; set; }
        DateTime End_Date { get; set; }
        bool Status { get; set; }

        string SearchValue { get; set; }
        bool IsEdit { get; set; }
        bool IsSuccessful { get; set; }
        string Message { get; set; }

        event EventHandler SearchEvent;
        event EventHandler AddNewEvent;
        event EventHandler EditEvent;
        event EventHandler SaveEvent;
        event EventHandler CancelEvent;

        void SetPetListBindingSource(BindingSource petList);
        void Show();
    }
}
