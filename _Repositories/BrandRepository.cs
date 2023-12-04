using BachHoaXanh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh._Repositories
{
    public class BrandRepository : Repository, IBrandRepository
    {
        private List<Brand> _brands;
        public BrandRepository() : base("brand",
            new List<string> { "id",
                "name",
                "supplier_id",
                "deleted"})
        {
            _brands = SearchBrand(new List<string> { "deleted = 0" });
        }

        public List<Brand> convertToBrand(List<List<string>> data)
        {
            return convert(data, row =>
            {
                row[row.Count() - 1] = row[row.Count() - 1].Equals("0") ? "false" : "true";
                try
                {
                    return new Brand(
                    Convert.ToInt16(row[0]), // id
                    row[1], //name
                    Convert.ToInt16(row[2]), //supplier_id
                    Convert.ToBoolean(row[3]) //deleted
                    );
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error occurred in BrandRepository.convertToBrand(): " + e.Message);
                }
                return new Brand();
            });
        }

        public int Add(Brand brand)
        {
            try
            {
                return create(new List<object> {brand.Id,
                    brand.Name,
                    brand.Supplier_id,
                    false }
                );
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred in BrandRepository.addBrand(): " + e.Message);
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
                Console.WriteLine("Error occurred in BrandRepository.deleteBrand(): " + e.Message);
            }
            return 0;
        }

        public int Update(Brand brand)
        {
            try
            {
                List<Object> updateValues = new List<object>();
                updateValues.Add(brand.Id);
                updateValues.Add(brand.Name);
                updateValues.Add(brand.Supplier_id);
                updateValues.Add(brand.Deleted);
                return update(updateValues, new List<string> { "id = " + brand.Id });
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred in BrandRepository.updateBrand(): " + e.Message);
            }
            return 0;
        }

        public List<Brand> SearchBrand(List<string> conditions)
        {
            try
            {
                return convertToBrand(read(conditions));
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred in BrandRepository.searchBrand(): " + e.Message);
            }
            return new List<Brand>();
        }

        public int GetAutoID()
        {
            return GetAutoID(SearchBrand(new List<string> { }));
        }

        public Object GetValueByKey(Brand brand, string key)
        {
            return key switch
            {
                "id" => brand.Id,
                "name" => brand.Name,
                "supplier_id" => brand.Supplier_id,
                _ => new Object()
            };
        }

        public List<Brand> FindObjectsBy(String key, Object value, IEnumerable<Brand> objectList)
        {
            List<Brand> objects = new List<Brand>();
            foreach (Brand brand in objectList)
                if (GetValueByKey(brand, key).Equals(value))
                    objects.Add(brand);
            return objects;
        }

        public List<Brand> FindBrandsBy(Dictionary<string, Object> conditions)
        {
            List<Brand> brands = new List<Brand>();
            foreach (KeyValuePair<string, Object> entry in conditions)
                brands = FindObjectsBy(entry.Key, entry.Value, _brands);
            return brands;
        }

        public List<Brand> FindBrands(string key, string value)
        {
            List<Brand> list = new List<Brand>();

            foreach (Brand brand in _brands)
            {
                if (GetValueByKey(brand, key)?.ToString()?.ToLower().Contains(value.ToLower()) == true)
                {
                    list.Add(brand);
                }
            }

            return list;
        }


        public IEnumerable<Brand> GetAll()
        {
            return _brands;
        }

    }
}
