using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Models
{
    public interface ICategoryRepository
    {
        void Add(Category category);
        void Edit(Category category);
        void Delete(int id);
        IEnumerable<Category> GetAll();
        IEnumerable<Category> GetByValue();
    }
}
