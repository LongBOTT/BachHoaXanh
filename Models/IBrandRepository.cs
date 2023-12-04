using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Models
{
    public interface IBrandRepository
    {
        int Add(Brand brand);
        int Update(Brand brand);
        int Delete(List<string> conditions);
        int GetAutoID();
        List<Brand> FindBrandsBy(Dictionary<string, Object> conditions);
        List<Brand> FindBrands(string key, string value);
        IEnumerable<Brand> GetAll();
    }
}
