using BachHoaXanh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh._Repositories
{
    public class DiscountRepository : Repository,  IDiscountRepository
    {
        private List<Discount> _discounts;
        public DiscountRepository() : base("discount",
            new List<string> { "id",
                "percent",
                "start_date",
                "end_date", 
                "status"})
        {
            _discounts = SearchDiscount(new List<string> { });
        }

        public List<Discount> convertToDiscount(List<List<string>> data)
        {
            return convert(data, row =>
            {
                row[row.Count() - 1] = row[row.Count() - 1].Equals("0") ? "false" : "true";
                try
                {
                    return new Discount(
                    Convert.ToInt16(row[0]), // id
                    Convert.ToDouble(row[1]), //
                    Convert.ToDateTime(row[2]), //
                    Convert.ToDateTime(row[3]), //
                    Convert.ToBoolean(row[4]) //deleted
                    );
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error occurred in DiscountRepository.convertToDiscount(): " + e.Message);
                }
                return new Discount();
            });
        }

        public int Add(Discount discount)
        {
            try
            {
                return create(new List<object> {discount.Id,
                    discount.Percent,
                    discount.Start_DateTime,
                    discount.End_DateTime,
                    false }
                );
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred in DiscountRepository.addDiscount(): " + e.Message);
            }
            return 0;
        }

        public List<Discount> SearchDiscount(List<string> conditions)
        {
            try
            {
                return convertToDiscount(read(conditions));
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred in DiscountRepository.searchDiscount(): " + e.Message);
            }
            return new List<Discount>();
        }

        public int GetAutoID()
        {
            return GetAutoID(SearchDiscount(new List<string> { }));
        }

        public Object GetValueByKey(Discount discount, string key)
        {
            return key switch
            {
                "id" => discount.Id,
                "percent" => discount.Percent,
                "start_date" => discount.Start_DateTime,
                "end_date" => discount.End_DateTime,
                "status" => discount.Status,
                _ => new Object()
            };
        }

        public List<Discount> FindObjectsBy(String key, Object value, IEnumerable<Discount> objectList)
        {
            List<Discount> objects = new List<Discount>();
            foreach (Discount discount in objectList)
                if (GetValueByKey(discount, key).Equals(value))
                    objects.Add(discount);
            return objects;
        }

        public List<Discount> FindDiscountsBy(Dictionary<string, Object> conditions)
        {
            List<Discount> discounts = new List<Discount>();
            foreach (KeyValuePair<string, Object> entry in conditions)
                discounts = FindObjectsBy(entry.Key, entry.Value, _discounts);
            return discounts;
        }

        public List<Discount> FindDiscounts(string key, string value)
        {
            List<Discount> list = new List<Discount>();

            foreach (Discount discount in _discounts)
            {
                if (GetValueByKey(discount, key)?.ToString()?.ToLower().Contains(value.ToLower()) == true)
                {
                    list.Add(discount);
                }
            }

            return list;
        }


        public IEnumerable<Discount> GetAll()
        {
            return _discounts;
        }
    }
}
