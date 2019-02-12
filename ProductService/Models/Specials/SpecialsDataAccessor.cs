using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.Models.Specials
{
    public class SpecialsDataAccessor : IDataAccessor<ISpecial>
    {
        private IRepository<ISpecial> _specialsRepository;

        public SpecialsDataAccessor(IRepository<ISpecial> specialsRepository)
        {
            _specialsRepository = specialsRepository;
        }

        public IList<ISpecial> GetAll()
        {
            return new List<ISpecial>();
        }

        public ISpecial GetByProductName(string productName)
        {
            throw new NotImplementedException();
        }

        public string Save(ISpecial saveThis)
        {
            throw new NotImplementedException();
        }

        public void Delete(ISpecial deleteThis)
        {
            throw new NotImplementedException();
        }

        public string Update(ISpecial updateThis)
        {
            throw new NotImplementedException();
        }

        public float GetAmountByProductName(string productName)
        {
            throw new NotImplementedException();
        }
    }
}
