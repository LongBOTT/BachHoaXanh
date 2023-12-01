using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Models
{
    public class Discount_detail
    {
        int discount_id;
        int product_id;
        Boolean status;

        public Discount_detail()
        {
        }

        public Discount_detail(int discount_id, int product_id, Boolean status)
        {
            this.discount_id = discount_id;
            this.product_id = product_id;
            this.status = status;
        }

        public int Discount_id
        {
            get { return discount_id; }
            set { discount_id = value; }
        }

        public int Product_id
        {
            get { return product_id; }
            set { product_id = value; }
        }

        public Boolean Status
        {
            get { return status; }
            set { status = value; }
        }

        public override string ToString()
        {
            string status1 = status ? "Ngưng áp dụng" : "Đang áp dụng";
            return discount_id + " | " +
                product_id + " | " +
                status1;
        }
    }
}
