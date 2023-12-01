using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Models
{
    public class Shipment
    {
        int id;
        int product_id;
        double unit_price;
        double quantity;
        double remain;
        DateTime mfg;
        DateTime exp;
        string sku;
        int import_id;

        public Shipment()
        {
        }

        public Shipment(int id, int product_id, double unit_price, double quantity, double remain, DateTime mfg, DateTime exp, string sku, int import_id)
        {
            this.id = id;
            this.product_id = product_id;
            this.unit_price = unit_price;
            this.quantity = quantity;
            this.remain = remain;
            this.mfg = mfg;
            this.exp = exp;
            this.sku = sku;
            this.import_id = import_id;
        }

        public int Id
        {
            get { return id; } 
            set {  id = value; }
        }

        public int Product_id
        {
            get { return product_id;}
            set { product_id = value; }
        }

        public double Unit_price
        {
            get { return unit_price;}
            set { unit_price = value; }
        }

        public double Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public double Remain
        {
            get { return remain; }
            set { remain = value; }
        }

        public DateTime Mfg
        {
            get { return mfg;}
            set { mfg = value; }
        }

        public DateTime Exp
        {
            get { return exp; }
            set { exp = value; }
        }

        public string Sku
        {
            get { return sku; }
            set { sku = value; }
        }

        public int Import_id
        {
            get { return import_id;}
            set { import_id = value; }
        }

        public override string ToString()
        {
            return id + " | " +
                product_id + " | " +
                unit_price + " | " +
                quantity + " | " +
                remain + " | " +
                mfg + " | " +
                exp + " | " +
                sku + " | " +
                import_id;
        }
    }
}
