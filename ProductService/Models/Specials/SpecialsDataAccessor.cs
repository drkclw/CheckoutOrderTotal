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
            return _specialsRepository.GetAll();
        }

        public ISpecial GetByProductName(string productName)
        {
            return _specialsRepository.GetByProductName(productName);
        }

        public float GetAmountByProductName(string productName)
        {
            throw new NotImplementedException();
        }

        public string Save(ISpecial saveThis)
        {
            return "Success.";
        }

        public void Delete(ISpecial deleteThis)
        {
            throw new NotImplementedException();
        }

        public string Update(ISpecial updateThis)
        {
            throw new NotImplementedException();
        }        
    }
}
