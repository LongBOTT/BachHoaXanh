using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Models
{
    public class Promotion_item
    {
        int promotion_id;
        int product_id;
        double quantity;

        public Promotion_item()
        {
        }

        public Promotion_item(int promotion_id, int product_id, double quantity)
        {
            this.promotion_id = promotion_id;
            this.product_id = product_id;
            this.quantity = quantity;
        }

        public int Promotion_id
        {
            get { return promotion_id; }
            set { promotion_id = value; }
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

        public override string ToString()
        {
            return promotion_id + " | " +
                product_id + " | " +
                quantity;
        }
    }
}
