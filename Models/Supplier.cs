using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Models
{
    public class Supplier
    {
        int id;
        string name;
        string phone;
        string address;
        string email;
        Boolean deleted;

        public Supplier()
        {
        }

        public Supplier(int id, string name, string phone, string address, string email, Boolean deleted)
        {
            this.id = id;
            this.name = name;
            this.phone = phone;
            this.address = address;
            this.email = email;
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
            set {  name = value; }
        }

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public Boolean Deleted
        {
            get { return deleted;}
            set { deleted = value;}
        }

        public override string ToString()
        {
            return id + " | " +
                name + " | " +
                phone + " | " +
                address + " | " +
                email;
        }
    }
}
