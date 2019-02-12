using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.Models.Prices
{
    public class PriceDataAccessor : IDataAccessor<Product>
    {
        private IRepository<Product> _priceRepository;

        public PriceDataAccessor(IRepository<Product> priceRepository)
        {
            _priceRepository = priceRepository;
        }

        public IList<Product> GetAll()
        {
            return _priceRepository.GetAll();
        }

        public Product GetByProductName(string productName)
        {
            return _priceRepository.GetByProductName(productName);
        }

        public float GetAmountByProductName(string productName)
        {
            var price = _priceRepository.GetByProductName(productName);

            if (price != null)
            {
                return price.Price;
            }
            else
            {
                return 0;
            }
        }

        public string Save(Product saveThis)
        {
            if (saveThis.Price > 0)
            {
                _priceRepository.Save(saveThis);
                return "Success.";
            }            
            else
            {
                return "Error: Price must be bigger than 0.";
            }        
        }

        public void Delete(Product deleteThis)
        {
            throw new NotImplementedException();
        }

        public string Update(Product updateThis)
        {            
            if (updateThis.Price > 0)
            {
                var existingPrice = _priceRepository.GetAll().FirstOrDefault(p => p.ProductName == updateThis.ProductName);
                if (existingPrice != null)
                {
                    _priceRepository.Update(updateThis);
                    return "Success.";
                }
                else
                {
                    return "Product does not exist, create product before updating price.";
                }
            }
            else
            {
                return "Error: Price must be bigger than 0.";
            }
        }
    }
}
