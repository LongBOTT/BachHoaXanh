using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Models
{
    public interface IExport_detailRepository
    {
        int Add(Export_detail export_Detail);
        int GetAutoID();
        List<Export_detail> FindExport_detailsBy(Dictionary<string, Object> conditions);
        List<Export_detail> FindExport_details(string key, string value);
        IEnumerable<Export_detail> GetAll();
    }
}
