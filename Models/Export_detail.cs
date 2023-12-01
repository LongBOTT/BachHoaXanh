using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Models
{
    public class Export_detail
    {
        int export_id;
        int shipment_id;
        double quantity;
        double total;

        public Export_detail()
        {
        }

        public Export_detail(int export_id, int shipment_id, double quantity, double total)
        {
            this.export_id = export_id;
            this.shipment_id = shipment_id;
            this.quantity = quantity;
            this.total = total;
        }

        public int Export_id
        {
            get { return export_id; } 
            set { export_id = value; }
        }

        public int Shipment_id
        {
            get { return shipment_id; }
            set { shipment_id = value; }
        }

        public double Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public double Total
        {
            get { return total; }
            set { total = value; }
        }
    
        public override string ToString()
        {
            return export_id + " | " +
                shipment_id + " | " +
                quantity + " | " +
                total;
        }
    }
}
