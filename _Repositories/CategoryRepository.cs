using BachHoaXanh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh._Repositories
{
    public class CategoryRepository : Repository, ICategoryRepository
    {
        private List<Category> _categories;
        public CategoryRepository() : base("category",
            new List<string> { "id",
                "name",
                "quantity",
                "deleted"})
        {
            _categories = SearchCategory(new List<string> { "deleted = 0" });
        }

        public List<Category> convertToCategory(List<List<string>> data)
        {
            return convert(data, row =>
            {
                row[row.Count() - 1] = row[row.Count() - 1].Equals("0") ? "false" : "true";
                try
                {
                    return new Category(
                    Convert.ToInt16(row[0]), // id
                    row[1], //name
                    Convert.ToDouble(row[2]), //quantity
                    Convert.ToBoolean(row[3]) //deleted
                    );
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error occurred in CategoryRepository.convertToCategory(): " + e.Message);
                }
                return new Category();
            });
        }

        public int Add(Category category)
        {
            try
            {
                return create(new List<object> {category.Id,
                    category.Name,
                    category.Quantity,
                    false }
                );
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred in CategoryRepository.addCategory(): " + e.Message);
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
                Console.WriteLine("Error occurred in CategoryRepository.deleteCategory(): " + e.Message);
            }
            return 0;
        }

        public int Update(Category category)
        {
            try
            {
                List<Object> updateValues = new List<object>();
                updateValues.Add(category.Id);
                updateValues.Add(category.Name);
                updateValues.Add(category.Quantity);
                updateValues.Add(category.Deleted);
                return update(updateValues, new List<string> { "id = " + category.Id });
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred in CategoryRepository.updateCategory(): " + e.Message);
            }
            return 0;
        }

        public List<Category> SearchCategory(List<string> conditions)
        {
            try
            {
                return convertToCategory(read(conditions));
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred in CategoryRepository.searchCategory(): " + e.Message);
            }
            return new List<Category>();
        }

        public int GetAutoID()
        {
            return GetAutoID(SearchCategory(new List<string> { }));
        }

        public Object GetValueByKey(Category category, string key)
        {
            return key switch
            {
                "id" => category.Id,
                "name" => category.Name,
                "quantity" => category.Quantity,
                _ => new Object()
            };
        }

        public List<Category> FindObjectsBy(String key, Object value, IEnumerable<Category> objectList)
        {
            List<Category> objects = new List<Category>();
            foreach (Category category in objectList)
                if (GetValueByKey(category, key).Equals(value))
                    objects.Add(category);
            return objects;
        }

        public List<Category> FindCategorysBy(Dictionary<string, Object> conditions)
        {
            List<Category> categorys = new List<Category>();
            foreach (KeyValuePair<string, Object> entry in conditions)
                categorys = FindObjectsBy(entry.Key, entry.Value, _categories);
            return categorys;
        }

        public List<Category> FindCategorys(string key, string value)
        {
            List<Category> list = new List<Category>();

            foreach (Category category in _categories)
            {
                if (GetValueByKey(category, key)?.ToString()?.ToLower().Contains(value.ToLower()) == true)
                {
                    list.Add(category);
                }
            }

            return list;
        }


        public IEnumerable<Category> GetAll()
        {
            return _categories;
        }

    }
}
