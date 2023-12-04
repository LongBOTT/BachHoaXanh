using BachHoaXanh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh._Repositories
{
    public class ReceiptRepository : Repository, IReceiptRepository
    {
        private List<Receipt> _receipts;
        public ReceiptRepository() : base("receipt",
            new List<string> { "id",
                "staff_id",
                "invoice_date",
                "total",
                "received",
                "excess"})
        {
            _receipts = SearchReceipt(new List<string> { });
        }

        public List<Receipt> convertToReceipt(List<List<string>> data)
        {
            return convert(data, row =>
            {
                try
                {
                    return new Receipt(
                    Convert.ToInt16(row[0]), // id
                    Convert.ToInt16(row[1]), // staff_id
                    Convert.ToDateTime(row[2]), // invoice_date
                    Convert.ToDouble(row[3]), // total
                    Convert.ToDouble(row[4]), // received
                    Convert.ToDouble(row[5]) // excess
                    );
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error occurred in ReceiptRepository.convertToReceipt(): " + e.Message);
                }
                return new Receipt();
            });
        }

        public int Add(Receipt receipt)
        {
            try
            {
                return create(new List<object> {receipt.Id,
                    receipt.Staff_id,
                    receipt.Invoice_DateTime,
                    receipt.Total,
                    receipt.Received,
                    receipt.Excess}
                );
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred in ReceiptRepository.addReceipt(): " + e.Message);
            }
            return 0;
        }

        public List<Receipt> SearchReceipt(List<string> conditions)
        {
            try
            {
                return convertToReceipt(read(conditions));
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred in ReceiptRepository.searchReceipt(): " + e.Message);
            }
            return new List<Receipt>();
        }

        public int GetAutoID()
        {
            return GetAutoID(SearchReceipt(new List<string> { }));
        }

        public Object GetValueByKey(Receipt receipt, string key)
        {
            return key switch
            {
                "id" => receipt.Id,
                "staff_id" => receipt.Staff_id,
                "invoice_date" => receipt.Invoice_DateTime,
                "total" => receipt.Total,
                "received" => receipt.Received,
                "excess" => receipt.Excess,
                _ => new Object()
            };
        }

        public List<Receipt> FindObjectsBy(String key, Object value, IEnumerable<Receipt> objectList)
        {
            List<Receipt> objects = new List<Receipt>();
            foreach (Receipt receipt in objectList)
                if (GetValueByKey(receipt, key).Equals(value))
                    objects.Add(receipt);
            return objects;
        }

        public List<Receipt> FindReceiptsBy(Dictionary<string, Object> conditions)
        {
            List<Receipt> receipts = new List<Receipt>();
            foreach (KeyValuePair<string, Object> entry in conditions)
                receipts = FindObjectsBy(entry.Key, entry.Value, _receipts);
            return receipts;
        }

        public List<Receipt> FindReceipts(string key, string value)
        {
            List<Receipt> list = new List<Receipt>();

            foreach (Receipt receipt in _receipts)
            {
                if (GetValueByKey(receipt, key)?.ToString()?.ToLower().Contains(value.ToLower()) == true)
                {
                    list.Add(receipt);
                }
            }

            return list;
        }

        public IEnumerable<Receipt> GetAll()
        {
            return _receipts;
        }

    }
}
