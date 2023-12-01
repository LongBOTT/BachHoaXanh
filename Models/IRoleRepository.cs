using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Models
{
    public interface IRoleRepository
    {
        List<Role> FindRolesBy(Dictionary<string, Object> conditions);
        List<Role> FindRoles(string key, string value);
        int GetAutoID();
        IEnumerable<Role> GetAll();
    }
}
