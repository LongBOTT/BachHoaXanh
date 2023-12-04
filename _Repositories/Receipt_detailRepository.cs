using BachHoaXanh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh._Repositories
{
    public class Receipt_detailRepository : Repository, IReceipt_detailRepository
    {
        private List<Receipt_detail> _receipt_details;
        public Receipt_detailRepository() : base("receipt_detail",
            new List<string> { "receipt_id",
                "product_id",
                "quantity",
                "total"})
        {
            _receipt_details = SearchReceipt_detail(new List<string> { });
        }

        public List<Receipt_detail> convertToReceipt_detail(List<List<string>> data)
        {
            return convert(data, row =>
            {
                try
                {
                    return new Receipt_detail(
                    Convert.ToInt16(row[0]), // receipt_id
                    Convert.ToInt16(row[1]), // product_id
                    Convert.ToDouble(row[2]), // quantity
                    Convert.ToDouble(row[3]) // total
                    );
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error occurred in Receipt_detailRepository.convertToReceipt_detail(): " + e.Message);
                }
                return new Receipt_detail();
            });
        }

        public int Add(Receipt_detail receipt_detail)
        {
            try
            {
                return create(new List<object> {receipt_detail.Receipt_id,
                    receipt_detail.Product_id,
                    receipt_detail.Quantity,
                    receipt_detail.Total}
                );
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred in Receipt_detailRepository.addReceipt_detail(): " + e.Message);
            }
            return 0;
        }

        public List<Receipt_detail> SearchReceipt_detail(List<string> conditions)
        {
            try
            {
                return convertToReceipt_detail(read(conditions));
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred in Receipt_detailRepository.searchReceipt_detail(): " + e.Message);
            }
            return new List<Receipt_detail>();
        }

        public int GetAutoID()
        {
            return GetAutoID(SearchReceipt_detail(new List<string> { }));
        }

        public Object GetValueByKey(Receipt_detail receipt_detail, string key)
        {
            return key switch
            {
                "receipt_id" => receipt_detail.Receipt_id,
                "product_id" => receipt_detail.Product_id,
                "quantity" => receipt_detail.Quantity,
                "total" => receipt_detail.Total,
                _ => new Object()
            };
        }

        public List<Receipt_detail> FindObjectsBy(String key, Object value, IEnumerable<Receipt_detail> objectList)
        {
            List<Receipt_detail> objects = new List<Receipt_detail>();
            foreach (Receipt_detail receipt_detail in objectList)
                if (GetValueByKey(receipt_detail, key).Equals(value))
                    objects.Add(receipt_detail);
            return objects;
        }

        public List<Receipt_detail> FindReceipt_detailsBy(Dictionary<string, Object> conditions)
        {
            List<Receipt_detail> receipt_details = new List<Receipt_detail>();
            foreach (KeyValuePair<string, Object> entry in conditions)
                receipt_details = FindObjectsBy(entry.Key, entry.Value, _receipt_details);
            return receipt_details;
        }

        public List<Receipt_detail> FindReceipt_details(string key, string value)
        {
            List<Receipt_detail> list = new List<Receipt_detail>();

            foreach (Receipt_detail receipt_detail in _receipt_details)
            {
                if (GetValueByKey(receipt_detail, key)?.ToString()?.ToLower().Contains(value.ToLower()) == true)
                {
                    list.Add(receipt_detail);
                }
            }

            return list;
        }

        public IEnumerable<Receipt_detail> GetAll()
        {
            return _receipt_details;
        }

    }
}
