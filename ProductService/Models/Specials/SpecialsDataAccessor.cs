using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.Models.Specials
{
    public class SpecialsDataAccessor : IDataAccessor<ISpecial>
    {
        private IRepository<ISpecial> _specialsRepository;
        private IValidator<ISpecial> _specialsValidator;

        public SpecialsDataAccessor(IRepository<ISpecial> specialsRepository, 
            IValidator<ISpecial> specialsValidator)
        {
            _specialsRepository = specialsRepository;
            _specialsValidator = specialsValidator;
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
            var validationResponse = _specialsValidator.Validate(saveThis);
            if (validationResponse.IsValid)
            {
                _specialsRepository.Save(saveThis);
                return validationResponse.Message;
            }
            else
            {
                return validationResponse.Message;
            }
        }

        public void Delete(ISpecial deleteThis)
        {
            throw new NotImplementedException();
        }

        public string Update(ISpecial updateThis)
        {
            var existingSpecial = _specialsRepository.GetByProductName(updateThis.ProductName);
            if (existingSpecial != null)
            {
                var validationResponse = _specialsValidator.Validate(updateThis);

                if (validationResponse.IsValid)
                {
                    _specialsRepository.Update(updateThis);
                    return validationResponse.Message;
                }
                else
                {
                    return validationResponse.Message;
                }
            }
            else
            {
                return "Error: Special does not exist, please create special before updating.";
            }

        }        
    }
}
