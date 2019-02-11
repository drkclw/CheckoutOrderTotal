using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.Models
{
    public interface IDataAccessor<T>
    {
        IList<T> GetAll();
        T GetByProductName(string productName);
        string Save(T saveThis);
        void Delete(T deleteThis);
        string Update(T updateThis);
        float GetAmountByProductName(string productName);
    }
}
