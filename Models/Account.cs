using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Models
{
    public class Account
    {
        int id;
        string username;
        string password;
        int roleID;
        int staffID;
        DateTime last_signed_in;
        public Account()
        {
        }

        public Account(int id, string username, string password, int roleID, int staffID, DateTime last_signed_in)
        {
            this.id = id;
            this.username = username;
            this.password = password;
            this.roleID = roleID;
            this.staffID = staffID;
            this.last_signed_in = last_signed_in;
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public int RoleID
        {
            get { return roleID; }
            set { roleID = value; }
        }

        public int StaffID
        {
            get { return staffID; }
            set { staffID = value; }
        }

        public DateTime Last_signed_in
        {
            get { return last_signed_in; }
            set { last_signed_in = value; }
        }

        public override string ToString()
        {
            return id + " | " +
                username + " | " +
                password + " | " +
                roleID + " | " +
                last_signed_in + " | " +
                staffID;
        }
    }
}
