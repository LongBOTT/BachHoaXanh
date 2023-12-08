using BachHoaXanh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh._Repositories
{
    public class Export_detailRepository : Repository, IExport_detailRepository
    {
        private List<Export_detail> _export_details;
        public Export_detailRepository() : base("export_detail",
            new List<string> { "export_note_id",
                "shipment_id",
                "quantity",
                "total"})
        {
            _export_details = SearchExport_detail(new List<string> { });
        }

        public List<Export_detail> convertToExport_detail(List<List<string>> data)
        {
            return convert(data, row =>
            {
                try
                {
                    return new Export_detail(
                    Convert.ToInt16(row[0]), // export_id
                    Convert.ToInt16(row[1]), // product_id
                    Convert.ToDouble(row[2]), // quantity
                    Convert.ToDouble(row[3]) // total
                    );
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error occurred in Export_detailRepository.convertToExport_detail(): " + e.Message);
                }
                return new Export_detail();
            });
        }

        public int Add(Export_detail export_detail)
        {
            try
            {
                return create(new List<object> {export_detail.Export_id,
                    export_detail.Shipment_id,
                    export_detail.Quantity,
                    export_detail.Total}
                );
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred in Export_detailRepository.addExport_detail(): " + e.Message);
            }
            return 0;
        }

        public List<Export_detail> SearchExport_detail(List<string> conditions)
        {
            try
            {
                return convertToExport_detail(read(conditions));
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred in Export_detailRepository.searchExport_detail(): " + e.Message);
            }
            return new List<Export_detail>();
        }

        public int GetAutoID()
        {
            return GetAutoID(SearchExport_detail(new List<string> { }));
        }

        public Object GetValueByKey(Export_detail export_detail, string key)
        {
            return key switch
            {
                "export_note_id" => export_detail.Export_id,
                "shipment_id" => export_detail.Shipment_id,
                "quantity" => export_detail.Quantity,
                "total" => export_detail.Total,
                _ => new Object()
            };
        }

        public List<Export_detail> FindObjectsBy(String key, Object value, IEnumerable<Export_detail> objectList)
        {
            List<Export_detail> objects = new List<Export_detail>();
            foreach (Export_detail export_detail in objectList)
                if (GetValueByKey(export_detail, key).Equals(value))
                    objects.Add(export_detail);
            return objects;
        }

        public List<Export_detail> FindExport_detailsBy(Dictionary<string, Object> conditions)
        {
            List<Export_detail> export_details = new List<Export_detail>();
            foreach (KeyValuePair<string, Object> entry in conditions)
                export_details = FindObjectsBy(entry.Key, entry.Value, _export_details);
            return export_details;
        }

        public List<Export_detail> FindExport_details(string key, string value)
        {
            List<Export_detail> list = new List<Export_detail>();

            foreach (Export_detail export_detail in _export_details)
            {
                if (GetValueByKey(export_detail, key)?.ToString()?.ToLower().Contains(value.ToLower()) == true)
                {
                    list.Add(export_detail);
                }
            }

            return list;
        }

        public IEnumerable<Export_detail> GetAll()
        {
            return _export_details;
        }

    }
}
