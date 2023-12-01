using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Models
{
    public class Import
    {
        int id;
        int staff_id;
        DateTime received_DateTime;
        double total;
        int supplier_id;

        public Import()
        {
        }

        public Import(int id, int staff_id, DateTime received_DateTime, double total, int supplier_id)
        {
            this.id = id;
            this.staff_id = staff_id;
            this.received_DateTime = received_DateTime;
            this.total = total;
            this.supplier_id = supplier_id;
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int Staff_id
        {
            get { return staff_id;}
            set { staff_id = value; }
        }

        public DateTime Received_DateTime
        {
            get { return received_DateTime;}
            set { received_DateTime = value; }
        }

        public double Total
        {
            get { return total;}
            set { total = value; }
        }

        public int Supplier_id
        {
            get { return supplier_id;}
            set { supplier_id = value; }
        }

        public override string ToString()
        {
            return id + " | " +
                staff_id + " | " +
                received_DateTime + " | " +
                total + " | " +
                supplier_id;
        }
    }
}
