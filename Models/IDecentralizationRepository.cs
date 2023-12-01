using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Models
{
    public interface IDecentralizationRepository
    {
        void Add(Decentralization decentralization);
        void Edit(Decentralization decentralization);
        IEnumerable<Decentralization> GetAll();
        IEnumerable<Decentralization> GetByValue();
    }
}
