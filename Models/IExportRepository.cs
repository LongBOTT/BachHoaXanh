using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Models
{
    public interface IExportRepository
    {
        int Add(Export export);
        int GetAutoID();
        List<Export> FindExportsBy(Dictionary<string, Object> conditions);
        List<Export> FindExports(string key, string value);
        IEnumerable<Export> GetAll();
    }
}
