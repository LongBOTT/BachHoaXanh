using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Models
{
    public class Receipt
    {
        int id;
        int staff_id;
        DateTime invoice_DateTime;
        double total;
        double received;
        double excess;

        public Receipt()
        {
        }

        public Receipt(int id, int staff_id, DateTime invoice_DateTime, double total, double received, double excess)
        {
            this.id = id;
            this.staff_id = staff_id;
            this.invoice_DateTime = invoice_DateTime;
            this.total = total;
            this.received = received;
            this.excess = excess;
        }

        public int Id
        {
            get { return id; } 
            set {  id = value; }
        }

        public int Staff_id
        {
            get { return staff_id;}
            set { staff_id = value; }
        }

        public DateTime Invoice_DateTime
        {
            get { return invoice_DateTime;}
            set { invoice_DateTime = value; }
        }

        public double Total
        {
            get { return total;}
            set { total = value; }
        }

        public double Received
        {
            get { return received;}
            set { received = value; }
        }

        public double Excess
        {
            get { return excess;}
            set { excess = value; }
        }

        public override string ToString()
        {
            return id + " | " +
                staff_id + " | " +
                invoice_DateTime + " | " +
                total + " | " +
                received + " | " +
                excess;
        }
    }
}
