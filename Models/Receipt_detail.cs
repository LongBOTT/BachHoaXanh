using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Models
{
    public class Receipt_detail
    {
        int receipt_id;
        int product_id;
        double quantity;
        double total;
        double percent;

        public Receipt_detail()
        {
        }

        public Receipt_detail(int receipt_id, int product_id, double quantity, double total, double percent)
        {
            this.receipt_id = receipt_id;
            this.product_id = product_id;
            this.quantity = quantity;
            this.total = total;
            this.percent = percent;
        }

        public int Receipt_id
        {
            get { return receipt_id; }
            set { receipt_id = value; }
        }

        public int Product_id
        {
            get { return product_id; }
            set { product_id = value; }
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

        public double Percent
        {
            get { return percent; }
            set { percent = value; }
        }

        public override string ToString()
        {
            return receipt_id + " | " +
                product_id + " | " +
                quantity + " | " +
                total + " | " +
                percent;
        }
    }
}
