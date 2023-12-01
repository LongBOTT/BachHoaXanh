using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Models
{
    public class Customer
    {
        int id;
        string name;
        bool gender;
        DateTime birthday;
        string phone;
        bool membership;
        DateTime signed_up_DateTime;
        bool deleted;

        public Customer()
        {
        }

        public Customer(int id, string name, bool gender, DateTime birthday, string phone, bool membership, DateTime signed_up_DateTime, int point, bool deleted)
        {
            this.id = id;
            this.name = name;
            this.gender = gender;
            this.birthday = birthday;
            this.phone = phone;
            this.membership = membership;
            this.signed_up_DateTime = signed_up_DateTime;
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

        public bool Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        public DateTime Birthday
        {
            get { return birthday; }
            set { birthday = value; }
        }

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        public bool Membership
        {
            get { return membership; }
            set { membership = value; }
        }

        public DateTime Signed_up_DateTime
        {
            get { return signed_up_DateTime; }
            set { signed_up_DateTime = value;}
        }

        public bool Deleted
        {
            get { return deleted; }
            set { deleted = value; }
        }

        public override string ToString()
        {
            string gender1 = gender ? "Nam" : "Nữ";
            string membership1 = membership ? "Có" : "Không";
            return id + " | " +
                name + " | " +
                gender1 + " | " +
                birthday + " | " +
                phone + " | " +
                membership1 + " | " +
                signed_up_DateTime;
        }
    }
}
