using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Models
{
    public interface IStaffRepository
    {
        void Add();
        void Edit(Staff staff);
        int Delete(Staff staff);
        int GetAutoID();
        List<Staff> FindStaffsBy(Dictionary<string, Object> conditions);
        List<Staff> FindStaffs(string key, string value);
        IEnumerable<Staff> GetAll();
    }
}
