using BachHoaXanh.Models;
using BachHoaXanh.Views.InterfaceView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh._Repositories
{
    public class StatisticRepository : MySQL, IStatisticsRepository
    {
        public StatisticRepository() { }

        public List<List<string>> ExcuteQuerry(string conditions)
        {
            return executeQuery(conditions);
        }
    }
}
