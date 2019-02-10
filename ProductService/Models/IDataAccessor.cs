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
        void Save(T saveThis);
        void Delete(T deleteThis);
        bool Update(T updateThis);
    }
}
