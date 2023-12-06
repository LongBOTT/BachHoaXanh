using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Models
{
    public interface IStatisticsRepository
    {
        List<List<string>> ExcuteQuerry(string conditions);
    }
}
