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
            if (saveThis.Type == SpecialType.Price)
            {
                var priceSpecial = (PriceSpecial)saveThis;

                if (priceSpecial.Price > 0)
                {
                    if (priceSpecial.PurchaseQty >= 2)
                    {
                        _specialsRepository.Save(saveThis);
                        return "Success.";
                    }
                    else
                    {
                        return "Error: Purchase quantity must be bigger than 1.";
                    }
                }
                else
                {
                    return "Error: Price must be bigger than 0.";
                }
            }
            else if (saveThis.Type == SpecialType.Limit)
            {
                var limitSpecial = (LimitSpecial)saveThis;

                if (limitSpecial.Limit == 0)
                {
                    return "Error: Limit must be bigger than 0.";
                }

                if (limitSpecial.DiscountAmount == 0)
                {
                    return "Error: Discount must be bigger than 0.";
                }

                if (limitSpecial.PurchaseQty > limitSpecial.Limit)
                {
                    return "Error: Limit must be bigger than purchase quantity.";
                }

                if(limitSpecial.Limit % (limitSpecial.PurchaseQty + limitSpecial.DiscountQty) > 0)
                {
                    return "Error: Limit must be a multiple of purchase quantity plus discount quantity.";
                }

                _specialsRepository.Save(saveThis);
                return "Success.";
            }else if (saveThis.Type == SpecialType.Restriction)
            {
                var restrictionSpecial = (RestrictionSpecial)saveThis;

                if(restrictionSpecial.DiscountAmount == 0)
                {
                    return "Error: Discount amount must be bigger than zero.";
                }

                _specialsRepository.Save(saveThis);
                return "Success.";
            }
            return "";
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
