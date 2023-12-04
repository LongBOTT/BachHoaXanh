using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Models
{
    public interface ISupplierRepository
    {
        int Add(Supplier supplier);
        int Update(Supplier supplier);
        int Delete(List<string> conditions);
        int GetAutoID();
        List<Supplier> FindSuppliersBy(Dictionary<string, Object> conditions);
        List<Supplier> FindSuppliers(string key, string value);
        IEnumerable<Supplier> GetAll();
    }
}
