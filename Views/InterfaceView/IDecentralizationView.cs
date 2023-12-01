using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BachHoaXanh.Views.InterfaceView
{
    public interface IDecentralizationView
    {
        int RoleID { get; set; }
        int ModuleID { get; set; }
        int FunctionID { get; set; }

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
