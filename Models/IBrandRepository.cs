using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Models
{
    public interface IBrandRepository
    {
        void Add(Brand brand);
        void Edit(Brand brand);
        void Delete(int id);
        IEnumerable<Brand> GetAll();
    }
}
