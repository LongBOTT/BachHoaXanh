using BachHoaXanh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh._Repositories
{
    public class ShipmentRepository : Repository, IShipmentRepository
    {
        private List<Shipment> _shipments;
        public ShipmentRepository() : base("shipment",
           new List<string> { "id",
                "product_id",
                "unit_price",
                "quantity",
                "remain",
                "mfg",
                "exp",
                "sku",
                "import_note_id"})
        {
            _shipments = SearchShipment(new List<string> { });
        }

        public List<Shipment> convertToShipment(List<List<string>> data)
        {
            return convert(data, row =>
            {
                try
                {
                    return new Shipment(
                    Convert.ToInt16(row[0]),
                    Convert.ToInt16(row[1]),
                    Convert.ToDouble(row[2]),
                    Convert.ToDouble(row[3]),
                    Convert.ToDouble(row[4]),
                    Convert.ToDateTime(row[5]),
                    Convert.ToDateTime(row[6]),
                    row[7],
                    Convert.ToInt16(row[8])
                    );
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error occurred in ShipmentRepository.convertToShipment(): " + e.Message);
                }
                return new Shipment();
            });
        }
        public List<Shipment> SearchShipment(List<string> conditions)
        {
            try
            {
                return convertToShipment(read(conditions));
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred in ShipmentRepository.searchShipment(): " + e.Message);
            }
            return new List<Shipment>();
        }

        public int GetAutoID()
        {
            return GetAutoID(_shipments);
        }

        public Object GetValueByKey(Shipment shipment, string key)
        {
            return key switch
            {
                "id" => shipment.Id,
                "product_id" => shipment.Product_id,
                "unit_price" => shipment.Unit_price,
                "quantity" => shipment.Quantity,
                "remain" => shipment.Remain,
                "mfg" => shipment.Mfg,
                "exp" => shipment.Exp,
                "sku" => shipment.Sku,
                "import_note_id" => shipment.Import_id,
                _ => new Object()
            };
        }

        public List<Shipment> FindObjectsBy(String key, Object value, IEnumerable<Shipment> objectList)
        {
            List<Shipment> objects = new List<Shipment>();
            foreach (Shipment shipment in objectList)
                if (GetValueByKey(shipment, key).Equals(value))
                    objects.Add(shipment);
            return objects;
        }

        public List<Shipment> FindShipmentsBy(Dictionary<string, Object> conditions)
        {
            List<Shipment> shipments = new List<Shipment>();
            foreach (KeyValuePair<string, Object> entry in conditions)
                shipments = FindObjectsBy(entry.Key, entry.Value, _shipments);
            return shipments;
        }

        public List<Shipment> FindShipments(string key, string value)
        {
            List<Shipment> list = new List<Shipment>();

            foreach (Shipment shipment in _shipments)
            {
                if (GetValueByKey(shipment, key)?.ToString()?.ToLower().Contains(value.ToLower()) == true)
                {
                    list.Add(shipment);
                }
            }

            return list;
        }

        public IEnumerable<Shipment> GetAll()
        {
            return _shipments;
        }
    }
}
