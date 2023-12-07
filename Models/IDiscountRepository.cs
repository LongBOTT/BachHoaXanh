using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Models
{
    public interface IDiscountRepository
    {
        int Add(Discount discount);
        int Update(Discount discount);
        int GetAutoID();
        List<Discount> FindDiscountsBy(Dictionary<string, Object> conditions);
        List<Discount> FindDiscounts(string key, string value);
        IEnumerable<Discount> GetAll();
    }
}
