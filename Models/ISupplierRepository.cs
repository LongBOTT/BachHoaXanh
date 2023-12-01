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
        int Edit(Supplier supplier);
        int Delete(int id);
        int GetAutoID();
        List<Supplier> FindSuppliersBy(Dictionary<string, Object> conditions);
        List<Supplier> FindSuppliers(string key, string value);
        IEnumerable<Supplier> GetAll();
    }
}
