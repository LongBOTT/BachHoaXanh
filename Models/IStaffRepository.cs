using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Models
{
    public interface IStaffRepository
    {
        int Add(Staff staff);
        int Update(Staff staff);
        int Delete(List<string> conditions);
        int GetAutoID();
        List<Staff> FindStaffsBy(Dictionary<string, Object> conditions);
        List<Staff> FindStaffs(string key, string value);
        IEnumerable<Staff> GetAll();
    }
}
