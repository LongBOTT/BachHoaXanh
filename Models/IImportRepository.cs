using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Models
{
    public interface IImportRepository
    {
        int Add(Import import);
        int GetAutoID();
        List<Import> FindImportsBy(Dictionary<string, Object> conditions);
        List<Import> FindImports(string key, string value);
        IEnumerable<Import> GetAll();
    }
}
