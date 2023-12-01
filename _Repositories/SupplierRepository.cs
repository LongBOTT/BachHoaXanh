using BachHoaXanh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh._Repositories
{
    public class SupplierRepository : Repository, ISupplierRepository
    {
        private List<Supplier> _suppliers;
        public SupplierRepository() : base("supplier",
            new List<string> { "id",
                "name",
                "phone",
                "address",
                "email",
                "deleted"})
        {
            _suppliers = SearchSupplier(new List<string> { "deleted = 0" });
        }

        public List<Supplier> convertToSupplier(List<List<string>> data)
        {
            return convert(data, row =>
            {
                row[row.Count() - 1] = row[row.Count() - 1].Equals("0") ? "false" : "true";
                try
                {
                    return new Supplier(
                    Convert.ToInt16(row[0]), // id
                    row[1], // name
                    row[2], // phone
                    row[3], // address
                    row[4], // email
                    Convert.ToBoolean(row[5]) // deleted
                    );
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error occurred in SupplierRepository.convertToSupplier(): " + e.Message);
                }
                return new Supplier();
            });
        }

        public int Add(Supplier supplier)
        {
            try
            {
                return create(new List<object> {supplier.Id,
                    supplier.Name,
                    supplier.Phone,
                    supplier.Address,
                    supplier.Email,
                    false }
                );
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred in SupplierRepository.addMon(): " + e.Message);
            }
            return 0;
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public int Edit(Supplier supplier)
        {
            throw new NotImplementedException();
        }

        public List<Supplier> SearchSupplier(List<string> conditions)
        {
            try
            {
                return convertToSupplier(read(conditions));
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred in SupplierRepository.searchSupplier(): " + e.Message);
            }
            return new List<Supplier>();
        }

        public int GetAutoID()
        {
            return GetAutoID(_suppliers);
        }

        public Object GetValueByKey(Supplier supplier, string key)
        {
            return key switch
            {
                "id" => supplier.Id,
                "name" => supplier.Name,
                "phone" => supplier.Phone,
                "address" => supplier.Address,
                "email" => supplier.Email,
                _ => new Object()
            };
        }

        public List<Supplier> FindObjectsBy(String key, Object value, IEnumerable<Supplier> objectList)
        {
            List<Supplier> objects = new List<Supplier>();
            foreach (Supplier supplier in objectList)
                if (GetValueByKey(supplier, key).Equals(value))
                    objects.Add(supplier);
            return objects;
        }

        public List<Supplier> FindSuppliersBy(Dictionary<string, Object> conditions)
        {
            List<Supplier> suppliers = new List<Supplier>();
            foreach (KeyValuePair<string, Object> entry in conditions)
                suppliers = FindObjectsBy(entry.Key, entry.Value, _suppliers);
            return suppliers;
        }

        public List<Supplier> FindSuppliers(string key, string value)
        {
            List<Supplier> list = new List<Supplier>();

            foreach (Supplier supplier in _suppliers)
            {
                if (GetValueByKey(supplier, key)?.ToString()?.ToLower().Contains(value.ToLower()) == true)
                {
                    list.Add(supplier);
                }
            }

            return list;
        }


        public IEnumerable<Supplier> GetAll()
        {
            return _suppliers;
        }

    }
}
