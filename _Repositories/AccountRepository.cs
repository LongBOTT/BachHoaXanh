using BachHoaXanh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh._Repositories
{
    public class AccountRepository : Repository, IAccountRepository
    {
        private List<Account> _accounts;
        public AccountRepository() : base("account",
            new List<string> { "id",
                "username",
                "password",
                "role_id",
                "staff_id",
                "last_signed_in"})
        {
            _accounts = SearchAccount(new List<string> { });
        }

        public List<Account> convertToAccount(List<List<string>> data)
        {
            return convert(data, row =>
            {
                try
                {
                    return new Account(
                    Convert.ToInt16(row[0]), // id
                    row[1], // username
                    row[2], // password
                    Convert.ToInt16(row[3]), // role_id
                    Convert.ToInt16(row[4]), // staff_id
                    Convert.ToDateTime(row[5]) //last_signed_in
                    );
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error occurred in AccountRepository.convertToAccount(): " + e.Message);
                }
                return new Account();
            });
        }

        public int Add(Account account)
        {
            try
            {
                return create(new List<object> {account.Id,
                    account.Username,
                    account.Password,
                    account.RoleID,
                    account.StaffID,
                    account.Last_signed_in }
                );
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred in AccountRepository.addAccount(): " + e.Message);
            }
            return 0;
        }

        public int Delete(List<String> conditions)
        {
            try
            {
                return delete(conditions);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred in AccountRepository.deleteAccount(): " + e.Message);
            }
            return 0;
        }

        public int Update(Account account)
        {
            try
            {
                List<Object> updateValues = new List<object>();
                updateValues.Add(account.Id);
                updateValues.Add(account.Username);
                updateValues.Add(account.Password);
                updateValues.Add(account.RoleID);
                updateValues.Add(account.StaffID);
                updateValues.Add(account.Last_signed_in);
                return update(updateValues, new List<string> { "id = " + account.Id });
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred in AccountRepository.updateAccount(): " + e.Message);
            }
            return 0;
        }

        public List<Account> SearchAccount(List<string> conditions)
        {
            try
            {
                return convertToAccount(read(conditions));
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred in AccountRepository.searchAccount(): " + e.Message);
            }
            return new List<Account>();
        }

        public int GetAutoID()
        {
            return GetAutoID(SearchAccount(new List<string> { }));
        }

        public Object GetValueByKey(Account account, string key)
        {
            return key switch
            {
                "id" => account.Id,
                "username" => account.Username,
                "password" => account.Password,
                "role_id" => account.RoleID,
                "staff_id" => account.StaffID,
                "last_signed_in" => account.Last_signed_in,
                _ => new Object()
            };
        }

        public List<Account> FindObjectsBy(String key, Object value, IEnumerable<Account> objectList)
        {
            List<Account> objects = new List<Account>();
            foreach (Account account in objectList)
                if (GetValueByKey(account, key).Equals(value))
                    objects.Add(account);
            return objects;
        }

        public List<Account> FindAccountsBy(Dictionary<string, Object> conditions)
        {
            List<Account> accounts = new List<Account>();
            foreach (KeyValuePair<string, Object> entry in conditions)
                accounts = FindObjectsBy(entry.Key, entry.Value, _accounts);
            return accounts;
        }

        public List<Account> FindAccounts(string key, string value)
        {
            List<Account> list = new List<Account>();

            foreach (Account account in _accounts)
            {
                if (GetValueByKey(account, key)?.ToString()?.ToLower().Contains(value.ToLower()) == true)
                {
                    list.Add(account);
                }
            }

            return list;
        }

        public IEnumerable<Account> GetAll()
        {
            return _accounts;
        }

    }
}
