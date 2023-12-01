using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Utils
{
    public class Database
    {
        public static MySqlConnection GetConnection()
        {
            string myConnectionString = "server=localhost;database=mini-supermarket;uid=root;pwd=;";
            MySqlConnection cnn = new MySqlConnection(myConnectionString);
            try
            {
                cnn.Open();
                return cnn;
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot open connection!");
            }
            return null;
        }

        public static void CloseConnection(MySqlConnection cnn)
        {
            if (cnn != null)
            {
                cnn.Close();
            }
        }
    }
}
