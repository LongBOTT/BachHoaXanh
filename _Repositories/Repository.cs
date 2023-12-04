using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh._Repositories
{
    public class Repository : MySQL
    {
        readonly string tableName;
        readonly List<string> columnNames;

        public Repository(string tableName, List<string> columnNames) : base()
        {
            this.tableName = tableName;
            this.columnNames = columnNames;
        }

        public string TableName
        {
            get { return tableName; }
        }

        public List<string> ColumnNames
        {
            get { return columnNames; }
        }

        public int create(List<object> values)
        {
            if (values == null || values.Count != columnNames.Count)
                return 0;
            string query = "INSERT INTO `" + tableName + "` VALUES(?" + string.Concat(Enumerable.Repeat(", ?", values.Count - 1)) + ");";
            return executeUpdate(query, values);
        }

        public List<List<string>> read(List<string> conditions)
        {
            string query = "SELECT * FROM `" + tableName + "`";
            if (conditions != null && conditions.Count > 0)
            {
                query += " WHERE " + string.Join(" AND ", conditions);
            }
            query += ";";
            return executeQuery(query);
        }

        public int update(List<object> updateValues, List<string> conditions)
        {
            if (updateValues == null || updateValues.Count == 0)
            {
                Console.WriteLine("Update values cannot be null or empty.");
                return 0;
            }

            int conditionsLength = 0;
            if (conditions != null && conditions.Count > 0)
            {
                conditionsLength = conditions.Count;
            }

            string setClause;
            if (updateValues.Count() == 1)
            {
                setClause = "Deleted = ?";
            }
            else
            {
                setClause = string.Join(" = ?, ", columnNames) + " = ?";
            }

            string query = "UPDATE `" + tableName + "` SET " + setClause;

            if (conditionsLength > 0)
            {
                query += " WHERE " + string.Join(" AND ", conditions);
            }
            query += ";";
            return executeUpdate(query, updateValues);
        }

        public int delete(List<string> conditions)
        {
            string query = "DELETE FROM `" + tableName + "`";
            if (conditions != null && conditions.Count > 0)
            {
                query += " WHERE " + string.Join(" AND ", conditions);
            }
            query += ";";
            return executeUpdate(query, new List<object> { });
        }

        public List<T> convert<T>(List<List<string>> data, Func<List<string>, T> converter)
        {
            List<T> list = new List<T>();
            foreach (List<string> row in data)
            {
                T obj = converter(row);
                list.Add(obj);
            }
            return list;
        }

        public int GetAutoID<T>(List<T> objectList)
        {
            if (objectList.Count == 0)
            {
                return 1;
            }

            T lastObject = objectList[objectList.Count - 1];
            string[] parts = lastObject.ToString().Split(new string[] { " | " }, StringSplitOptions.None);

            if (parts.Length > 0)
            {
                string lastID = parts[0];
                if (int.TryParse(lastID, out int id))
                {
                    return id + 1;
                }
            }

            return 1;
        }

        public static int GetIndex<T>(T obj, string key, List<T> objectList)
        {
            return Enumerable.Range(0, objectList.Count)
                .FirstOrDefault(i =>
                {
                    var propertyInfo = objectList[i].GetType().GetProperty(key);
                    var objValue = propertyInfo?.GetValue(objectList[i]);
                    var targetValue = obj.GetType().GetProperty(key)?.GetValue(obj);

                    return object.Equals(objValue, targetValue);
                }, -1);
        }

    }
}
