using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Models
{
    public interface IShipmentRepository
    {
        int Add(Shipment shipment);
        int GetAutoID();
        List<Shipment> FindShipmentsBy(Dictionary<string, Object> conditions);
        List<Shipment> FindShipments(string key, string value);
        List<Shipment> SearchShipment(List<string> conditions);
        IEnumerable<Shipment> GetAll();
    }
}
