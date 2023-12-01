using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Models
{
    public class Staff
    {
        int id;
        string name;
        Boolean gender;
        DateTime birthday;
        string phone;
        string address;
        string email;
        DateTime entry_DateTime;
        Boolean deleted;

        public Staff()
        {
        }

        public Staff(int id, string name, Boolean gender, DateTime birthday, string phone, string address, string email, DateTime entry_DateTime, Boolean deleted)
        {
            this.id = id;
            this.name = name;
            this.gender = gender;
            this.birthday = birthday;
            this.phone = phone;
            this.address = address;
            this.email = email;
            this.entry_DateTime = entry_DateTime;
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

        public Boolean Gender
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

        public DateTime Entry_DateTime
        {
            get { return entry_DateTime; }
            set { entry_DateTime = value; }
        }

        public Boolean Deleted
        {
            get { return deleted; }
            set { deleted = value; }
        }
        public override string ToString()
        {
            string gender1 = gender ? "Nam" : "Nữ";
            return id + " | " +
                name + " | " +
                gender1 + " | " +
                birthday + " | " +
                phone + " | " +
                address + " | " +
                email + " | " +
                entry_DateTime;
        }
    }
}
