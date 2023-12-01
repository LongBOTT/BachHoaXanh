using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Models
{
    public class Category
    {
        int id;
        string name;
        double quantity;
        Boolean deleted;

        public Category()
        {
        }

        public Category(int id, string name, double quantity, Boolean deleted)
        {
            this.id = id;
            this.name = name;
            this.quantity = quantity;
            this.deleted = deleted;
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public double Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public Boolean Deleted
        {
            get { return deleted; }
            set { deleted = value; }
        }

        public override string ToString()
        {
            return id + " | " +
                name + " | " +
                quantity;
        }
    }
}
