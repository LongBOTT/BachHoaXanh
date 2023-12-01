using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Models
{
    public interface IAccountRepository
    {
        int Add(Account account);
        int Update(Account account);
        int Delete(List<string> conditions);
        int GetAutoID();
        List<Account> FindAccountsBy(Dictionary<string, Object> conditions);
        List<Account> FindAccounts(string key, string value);
        IEnumerable<Account> GetAll();
    }
}
