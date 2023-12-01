using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Models
{
    public class Brand
    {
        int id;
        string name;
        int supplier_id;
        Boolean deleted;

        public Brand()
        {
        }

        public Brand(int id, string name, int supplier_id, Boolean deleted)
        {
            this.id = id;
            this.name = name;
            this.supplier_id = supplier_id;
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

        public int Supplier_id
        {
            get { return supplier_id;}
            set { supplier_id = value; }
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
                supplier_id;
        }
    }
}
