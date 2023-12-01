using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Models
{
    public class Role
    {
        int id;
        string name;
        Boolean deleted;
        public Role()
        {
        }

        public Role(int id, string name, bool deleted)
        {
            this.id = id;
            this.name = name;
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
        
        public Boolean Deleted 
        { 
            get { return deleted; } 
            set { deleted = value; } 
        }

        public override string ToString()
        {
            return id + " | " +
                name;
        }
    }
}
