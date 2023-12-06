using BachHoaXanh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh._Repositories
{
    public class Discount_detailRepository : Repository, IDiscount_detailRepository
    {
        private List<Discount_detail> _discounts;
        public Discount_detailRepository() : base("discount_detail",
            new List<string> { "discount_id",
                "product_id",
                "status"})
        {
            _discounts = SearchDiscount_detail(new List<string> { });
        }

        public List<Discount_detail> convertToDiscount_detail(List<List<string>> data)
        {
            return convert(data, row =>
            {
                row[row.Count() - 1] = row[row.Count() - 1].Equals("0") ? "false" : "true";
                try
                {
                    return new Discount_detail(
                    Convert.ToInt16(row[0]), // id
                    Convert.ToInt16(row[1]), //
                    Convert.ToBoolean(row[2]) //deleted
                    );
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error occurred in Discount_detailRepository.convertToDiscount_detail(): " + e.Message);
                }
                return new Discount_detail();
            });
        }

        public int Add(Discount_detail discount)
        {
            try
            {
                return create(new List<object> {discount.Discount_id,
                    discount.Product_id,
                    false }
                );
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred in Discount_detailRepository.addDiscount_detail(): " + e.Message);
            }
            return 0;
        }

        public List<Discount_detail> SearchDiscount_detail(List<string> conditions)
        {
            try
            {
                return convertToDiscount_detail(read(conditions));
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred in Discount_detailRepository.searchDiscount_detail(): " + e.Message);
            }
            return new List<Discount_detail>();
        }

        public int GetAutoID()
        {
            return GetAutoID(SearchDiscount_detail(new List<string> { }));
        }

        public Object GetValueByKey(Discount_detail discount, string key)
        {
            return key switch
            {
                "discount_id" => discount.Discount_id,
                "product_id" => discount.Product_id,
                "status" => discount.Status,
                _ => new Object()
            };
        }

        public List<Discount_detail> FindObjectsBy(String key, Object value, IEnumerable<Discount_detail> objectList)
        {
            List<Discount_detail> objects = new List<Discount_detail>();
            foreach (Discount_detail discount in objectList)
                if (GetValueByKey(discount, key).Equals(value))
                    objects.Add(discount);
            return objects;
        }

        public List<Discount_detail> FindDiscount_detailsBy(Dictionary<string, Object> conditions)
        {
            List<Discount_detail> discounts = new List<Discount_detail>();
            foreach (KeyValuePair<string, Object> entry in conditions)
                discounts = FindObjectsBy(entry.Key, entry.Value, _discounts);
            return discounts;
        }

        public List<Discount_detail> FindDiscount_details(string key, string value)
        {
            List<Discount_detail> list = new List<Discount_detail>();

            foreach (Discount_detail discount in _discounts)
            {
                if (GetValueByKey(discount, key)?.ToString()?.ToLower().Contains(value.ToLower()) == true)
                {
                    list.Add(discount);
                }
            }

            return list;
        }


        public IEnumerable<Discount_detail> GetAll()
        {
            return _discounts;
        }

    }
}
