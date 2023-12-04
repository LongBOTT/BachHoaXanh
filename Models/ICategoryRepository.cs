using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Models
{
    public interface ICategoryRepository
    {
        int Add(Category category);
        int Update(Category category);
        int Delete(List<string> conditions);
        int GetAutoID();
        List<Category> FindCategorysBy(Dictionary<string, Object> conditions);
        List<Category> FindCategorys(string key, string value);
        IEnumerable<Category> GetAll();
    }
}
