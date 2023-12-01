using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Models
{
    public interface IDiscountRepository
    {
        void Add(Discount discount);
        void Edit(Discount discount);
        IEnumerable<Discount> GetAll();
        IEnumerable<Discount> GetByValue();
    }
}
