using BachHoaXanh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh._Repositories
{
    public class RoleRepository : Repository, IRoleRepository
    {
        private List<Role> _roles;
        public RoleRepository() : base("role",
           new List<string> { "id",
                "name", 
                "deleted"})
        {
            _roles = SearchRole(new List<string> { "deleted = 0" });
        }

        public List<Role> convertToRole(List<List<string>> data)
        {

            return convert(data, row =>
            {
                row[row.Count() - 1] = row[row.Count() - 1].Equals("0") ? "false" : "true";
                try
                {
                    return new Role(
                    Convert.ToInt16(row[0]),
                    row[1],
                    Convert.ToBoolean(row[2])
                    );
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error occurred in RoleRepository.convertToRole(): " + e.Message);
                }
                return new Role();
            });
        }
        public List<Role> SearchRole(List<string> conditions)
        {
            try
            {
                return convertToRole(read(conditions));
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred in RoleRepository.searchRole(): " + e.Message);
            }
            return new List<Role>();
        }

        public int GetAutoID()
        {
            return GetAutoID(SearchRole(new List<string> { }));
        }

        public Object GetValueByKey(Role role, string key)
        {
            return key switch
            {
                "id" => role.Id,
                "name" => role.Name,
                _ => new Object()
            };
        }

        public List<Role> FindObjectsBy(String key, Object value, IEnumerable<Role> objectList)
        {
            List<Role> objects = new List<Role>();
            foreach (Role role in objectList)
            {
                if (GetValueByKey(role, key).Equals(value))
                    objects.Add(role);
            }
            return objects;
        }

        public List<Role> FindRolesBy(Dictionary<string, Object> conditions)
        {
            List<Role> roles = new List<Role>();
            foreach (KeyValuePair<string, Object> entry in conditions)
                roles = FindObjectsBy(entry.Key, entry.Value, _roles);
            return roles;
        }

        public List<Role> FindRoles(string key, string value)
        {
            List<Role> list = new List<Role>();

            foreach (Role role in _roles)
            {
                if (GetValueByKey(role, key)?.ToString()?.ToLower().Contains(value.ToLower()) == true)
                {
                    list.Add(role);
                }
            }

            return list;
        }

        public IEnumerable<Role> GetAll()
        {
            return _roles;
        }
    }
}
