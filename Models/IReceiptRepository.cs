using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Models
{
    public interface IReceiptRepository
    {
        int Add(Receipt receipt);
        int GetAutoID();
        List<Receipt> FindReceiptsBy(Dictionary<string, Object> conditions);
        List<Receipt> FindReceipts(string key, string value);
        IEnumerable<Receipt> GetAll();
    }
}
