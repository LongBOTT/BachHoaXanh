using BachHoaXanh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh._Repositories
{
    public class ImportRepository : Repository, IImportRepository
    {
        private List<Import> _imports;
        public ImportRepository() : base("import_note",
            new List<string> { "id",
                "staff_id",
                "received_date",
                "total",
                "supplier_id"})
        {
            _imports = SearchImport(new List<string> { });
        }

        public List<Import> convertToImport(List<List<string>> data)
        {
            return convert(data, row =>
            {
                try
                {
                    return new Import(
                    Convert.ToInt16(row[0]), // id
                    Convert.ToInt16(row[1]),
                    Convert.ToDateTime(row[2]),
                    Convert.ToDouble(row[3]),
                    Convert.ToInt16(row[4])
                    );
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error occurred in ImportRepository.convertToImport(): " + e.Message);
                }
                return new Import();
            });
        }

        public int Add(Import import)
        {
            try
            {
                return create(new List<object> {import.Id,
                    import.Staff_id,
                    import.Received_DateTime,
                    import.Total,
                    import.Supplier_id }
                );
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred in ImportRepository.addImport(): " + e.Message);
            }
            return 0;
        }

        public List<Import> SearchImport(List<string> conditions)
        {
            try
            {
                return convertToImport(read(conditions));
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred in ImportRepository.searchImport(): " + e.Message);
            }
            return new List<Import>();
        }

        public int GetAutoID()
        {
            return GetAutoID(SearchImport(new List<string> { }));
        }

        public Object GetValueByKey(Import import, string key)
        {
            return key switch
            {
                "id" => import.Id,
                "staff_id" => import.Staff_id,
                "received_date" => import.Received_DateTime,
                "total" => import.Total,
                "supplier_id" => import.Supplier_id,
                _ => new Object()
            };
        }

        public List<Import> FindObjectsBy(String key, Object value, IEnumerable<Import> objectList)
        {
            List<Import> objects = new List<Import>();
            foreach (Import import in objectList)
                if (GetValueByKey(import, key).Equals(value))
                    objects.Add(import);
            return objects;
        }

        public List<Import> FindImportsBy(Dictionary<string, Object> conditions)
        {
            List<Import> imports = new List<Import>();
            foreach (KeyValuePair<string, Object> entry in conditions)
                imports = FindObjectsBy(entry.Key, entry.Value, _imports);
            return imports;
        }

        public List<Import> FindImports(string key, string value)
        {
            List<Import> list = new List<Import>();

            foreach (Import import in _imports)
            {
                if (GetValueByKey(import, key)?.ToString()?.ToLower().Contains(value.ToLower()) == true)
                {
                    list.Add(import);
                }
            }

            return list;
        }

        public IEnumerable<Import> GetAll()
        {
            return _imports;
        }

    }
}
