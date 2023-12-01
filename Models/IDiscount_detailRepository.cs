using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Models
{
    public interface IDiscount_detailRepository
    {
        void Add(Discount_detail discount_detail);
        void Edit(Discount_detail discount_detail);
        IEnumerable<Discount_detail> GetAll();
        IEnumerable<Discount_detail> GetByValue();
    }
}
