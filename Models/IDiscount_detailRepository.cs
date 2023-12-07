using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Models
{
    public interface IDiscount_detailRepository
    {
        int Add(Discount_detail discount_Detail);
        int Update(Discount_detail discount_Detail);
        int GetAutoID();
        List<Discount_detail> FindDiscount_detailsBy(Dictionary<string, Object> conditions);
        List<Discount_detail> FindDiscount_details(string key, string value);
        IEnumerable<Discount_detail> GetAll();
    }
}
