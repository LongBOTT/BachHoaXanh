using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Models
{
    public interface ICustomerRepository
    {
        int Add(Customer customer);
        int Edit(Customer customer);
        int Delete(int id);
        IEnumerable<Customer> GetAll();
    }
}
