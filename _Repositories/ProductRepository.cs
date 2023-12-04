using BachHoaXanh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh._Repositories
{
    public class ProductRepository : Repository, IProductRepository
    {
        private List<Product> _products;
        public ProductRepository() : base("product",
            new List<string> { "id",
               "name",
                "brand_id",
                "category_id",
                "unit",
                "cost",
                "quantity",
                "image",
                "barcode",
                "deleted"})
        {
            _products = SearchProduct(new List<string> { "deleted = 0" });
        }

        public List<Product> convertToProduct(List<List<string>> data)
        {
            return convert(data, row =>
            {
                row[row.Count() - 1] = row[row.Count() - 1].Equals("0") ? "false" : "true";
                try
                {
                    return new Product(
                    Convert.ToInt16(row[0]), // id
                    row[1], // name
                    Convert.ToInt16(row[2]), // brand_id
                    Convert.ToInt16(row[3]), // category_id
                    row[4], // unit
                    Convert.ToDouble(row[5]), //cost
                    Convert.ToDouble(row[6]),    // quantity
                    row[7],//image
                    row[8], //barcode
                    Convert.ToBoolean(row[9]) //deleted
                    );
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error occurred in ProductRepository.convertToProduct(): " + e.Message);
                }
                return new Product();
            });
        }

        public int Add(Product product)
        {
            try
            {
                return create(new List<object> {product.Id,
                    product.Name,
                    product.Brand_id,
                    product.Category_id,
                    product.Unit,
                    product.Cost,
                    product.Quantity,
                    product.Image,
                    product.Barcode,
                    false }
                );
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred in ProductRepository.addPRoduct(): " + e.Message);
            }
            return 0;
        }

        public int Delete(List<string> conditions)
        {
            try
            {
                List<Object> updateValues = new List<object>();
                updateValues.Add(true);
                return update(updateValues, conditions);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred in ProductRepository.deleteProduct(): " + e.Message);
            }
            return 0;
        }

        public int Update(Product product)
        {
            try
            {
                List<Object> updateValues = new List<object>();
                updateValues.Add(product.Id);
                updateValues.Add(product.Name);
                updateValues.Add(product.Brand_id);
                updateValues.Add(product.Category_id);
                updateValues.Add(product.Unit);
                updateValues.Add(product.Cost);
                updateValues.Add(product.Quantity);
                updateValues.Add(product.Image);
                updateValues.Add(product.Barcode);
                updateValues.Add(product.Deleted);
                return update(updateValues, new List<string> { "id = " + product.Id });
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred in ProductRepository.updateProduct(): " + e.Message);
            }
            return 0;
        }

        public List<Product> SearchProduct(List<string> conditions)
        {
            try
            {
                return convertToProduct(read(conditions));
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred in ProductRepository.searchProduct(): " + e.Message);
            }
            return new List<Product>();
        }

        public int GetAutoID()
        {
            return GetAutoID(SearchProduct(new List<string> { }));
        }

        public Object GetValueByKey(Product product, string key)
        {
            return key switch
            {
                "id" => product.Id,
                "name" => product.Name,
                "brand_id" => product.Brand_id,
                "category_id" => product.Category_id,
                "unit" => product.Unit,
                "cost" => product.Cost,
                "quantity" => product.Quantity,
                "image" => product.Image,
                "barcode" => product.Barcode,
                _ => new Object()
            };
        }

        public List<Product> FindObjectsBy(String key, Object value, IEnumerable<Product> objectList)
        {
            List<Product> objects = new List<Product>();
            foreach (Product product in objectList)
                if (GetValueByKey(product, key).Equals(value))
                    objects.Add(product);
            return objects;
        }

        public List<Product> FindProductsBy(Dictionary<string, Object> conditions)
        {
            List<Product> products = new List<Product>();
            foreach (KeyValuePair<string, Object> entry in conditions)
                products = FindObjectsBy(entry.Key, entry.Value, _products);
            return products;
        }

        public List<Product> FindProducts(string key, string value)
        {
            List<Product> list = new List<Product>();

            foreach (Product product in _products)
            {
                if (GetValueByKey(product, key)?.ToString()?.ToLower().Contains(value.ToLower()) == true)
                {
                    list.Add(product);
                }
            }

            return list;
        }


        public IEnumerable<Product> GetAll()
        {
            return _products;
        }

    }
}
