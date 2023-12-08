using BachHoaXanh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh._Repositories
{
    public class ExportRepository : Repository, IExportRepository
    {
        private List<Export> _exports;
        public ExportRepository() : base("export_note",
            new List<string> { "id",
                "staff_id",
                "invoice_date",
                "total",
                "reason"})
        {
            _exports = SearchExport(new List<string> { });
        }

        public List<Export> convertToExport(List<List<string>> data)
        {
            return convert(data, row =>
            {
                try
                {
                    return new Export(
                    Convert.ToInt16(row[0]), // id
                    Convert.ToInt16(row[1]), // staff_id
                    Convert.ToDateTime(row[2]), // invoice_date
                    Convert.ToDouble(row[3]), // total
                    row[4] // received
                    );
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error occurred in ExportRepository.convertToExport(): " + e.Message);
                }
                return new Export();
            });
        }

        public int Add(Export export)
        {
            try
            {
                return create(new List<object> {export.Id,
                    export.Staff_id,
                    export.Invoice_DateTime,
                    export.Total,
                    export.Reason}
                );
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred in ExportRepository.addExport(): " + e.Message);
            }
            return 0;
        }

        public List<Export> SearchExport(List<string> conditions)
        {
            try
            {
                return convertToExport(read(conditions));
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred in ExportRepository.searchExport(): " + e.Message);
            }
            return new List<Export>();
        }

        public int GetAutoID()
        {
            return GetAutoID(SearchExport(new List<string> { }));
        }

        public Object GetValueByKey(Export export, string key)
        {
            return key switch
            {
                "id" => export.Id,
                "staff_id" => export.Staff_id,
                "invoice_date" => export.Invoice_DateTime,
                "total" => export.Total,
                "reason" => export.Reason,
                _ => new Object()
            };
        }

        public List<Export> FindObjectsBy(String key, Object value, IEnumerable<Export> objectList)
        {
            List<Export> objects = new List<Export>();
            foreach (Export export in objectList)
                if (GetValueByKey(export, key).Equals(value))
                    objects.Add(export);
            return objects;
        }

        public List<Export> FindExportsBy(Dictionary<string, Object> conditions)
        {
            List<Export> exports = new List<Export>();
            foreach (KeyValuePair<string, Object> entry in conditions)
                exports = FindObjectsBy(entry.Key, entry.Value, _exports);
            return exports;
        }

        public List<Export> FindExports(string key, string value)
        {
            List<Export> list = new List<Export>();

            foreach (Export export in _exports)
            {
                if (GetValueByKey(export, key)?.ToString()?.ToLower().Contains(value.ToLower()) == true)
                {
                    list.Add(export);
                }
            }

            return list;
        }

        public IEnumerable<Export> GetAll()
        {
            return _exports;
        }

    }
}
