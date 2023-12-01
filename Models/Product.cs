using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Models
{
    public class Product
    {
        int id;
        string name;
        int brand_id;
        int category_id;
        string unit;
        double cost;
        double quantity;
        string image;
        string barcode;
        Boolean deleted;

        public Product()
        {
        }

        public Product(int id, string name, int brand_id, int category_id, string unit, double cost, double quantity, string image, string barcode, Boolean deleted)
        {
            this.id = id;
            this.name = name;
            this.brand_id = brand_id;
            this.category_id = category_id;
            this.unit = unit;
            this.cost = cost;
            this.quantity = quantity;
            this.image = image;
            this.barcode = barcode;
            this.deleted = deleted;
        }

        public int Id
        {
            get { return id; } 
            set {  id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Brand_id
        {
            get { return brand_id;}
            set { brand_id = value; }
        }

        public int Category_id
        {
            get { return category_id;}
            set { category_id = value; }
        }

        public string Unit
        {
            get { return unit; }
            set { unit = value; }
        }

        public double Cost
        {
            get { return cost;}
            set { cost = value; }
        }

        public double Quantity
        {
            get { return quantity;}
            set { quantity = value; }
        }

        public string Image
        {
            get { return image;}
            set { image = value; }
        }

        public string Barcode
        {
            get { return barcode;}
            set { barcode = value; }
        }

        public Boolean Deleted
        {
            get { return deleted;}
            set { deleted = value; }
        }

        public override string ToString()
        {
            return id + " | " +
                name + " | " +
                brand_id + " | " +
                category_id + " | " +
                unit + " | " +
                cost + " | " +
                quantity + " | " +
                barcode;
        }
    }
}
