using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Models
{
    public interface IProductRepository
    {
        int Add(Product product);
        int Update(Product product);
        int Delete(List<string> conditions);
        int GetAutoID();
        List<Product> SearchProduct(List<string> conditions);
        List<Product> FindProductsBy(Dictionary<string, Object> conditions);
        List<Product> FindProducts(string key, string value);
        IEnumerable<Product> GetAll();
    }
}
