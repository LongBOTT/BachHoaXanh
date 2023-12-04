using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Models
{
    public interface IReceipt_detailRepository
    {
        int Add(Receipt_detail receipt);
        int GetAutoID();
        List<Receipt_detail> FindReceipt_detailsBy(Dictionary<string, Object> conditions);
        List<Receipt_detail> FindReceipt_details(string key, string value);
        IEnumerable<Receipt_detail> GetAll();
    }
}
