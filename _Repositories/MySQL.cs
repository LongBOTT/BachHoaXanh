using BachHoaXanh.Utils;
using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh._Repositories
{
    public class MySQL
    {
        public MySQL() { }

        public List<List<string>> executeQuery(string query)
        {
            MySqlConnection conn = Database.GetConnection();
            if (conn == null)
            {
                return new List<List<string>>();
            }
            List<List<string>> result = new List<List<string>>();
            MySqlCommand cmd = new MySqlCommand(query, conn);
            Console.WriteLine(query);
            MySqlDataReader reader = cmd.ExecuteReader();
            DataTable schemaTable = reader.GetSchemaTable();
            int columnCount = schemaTable.Rows.Count;
            while (reader.Read())
            {

                List<string> row = new List<string>(columnCount);
                for (int i = 0; i < columnCount; i++)
                {
                    if (!reader.IsDBNull(i))
                        row.Add(reader.GetString(i));
                }
                result.Add(row);
            }
            conn.Close();
            conn.Dispose();
            return result;
        }

        public int executeUpdate(string query, List<object> values)
        {
            MySqlConnection conn = Database.GetConnection();
            if (conn == null)
            {
                return 0;
            }
            int numOfRows;
            String formattedQuery = formatQuery(query, values);
            Console.WriteLine(formattedQuery);
            MySqlCommand cmd = new MySqlCommand(formattedQuery, conn);
            numOfRows = cmd.ExecuteNonQuery();
            conn.Close();
            conn.Dispose();
            return numOfRows;
        }

        public string formatQuery(string query, List<object> values)
        {
            string stringValue;
            foreach (object value in values)
            {
                if (value.GetType() == typeof(string))
                {
                    stringValue = "'" + value + "'";
                }
                else if (value.GetType() == typeof(bool))
                {
                    stringValue = (bool)value ? "1" : "0";
                }
                else if (value.GetType() == typeof(int) || value.GetType() == typeof(double))
                {
                    stringValue = value.ToString();
                }
                else if (value.GetType() == typeof(DateTime))
                {
                    DateTime date = Convert.ToDateTime(value.ToString());
                    stringValue = "'" + date.ToString("yyyy-MM-dd H:mm:ss") + "'";
                }
                else
                {
                    stringValue = "'" + value + "'";
                }
                string newString = "";

                int index = query.IndexOf("?");
                if (index > -1)
                {
                    newString = query.Substring(0, index) + stringValue + query.Substring(index + 1);
                }
                query = newString;
            }
            return query;
        }
    }
}
