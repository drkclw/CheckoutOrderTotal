using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.Models.Prices
{
    public class PriceDataAccessor : IDataAccessor<Product>
    {
        private IRepository<Product> _productRepository;

        public PriceDataAccessor(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public IList<Product> GetAll()
        {
            return _productRepository.GetAll();
        }
        public Product GetByProductName(string productName)
        {
            return _productRepository.GetByProductName(productName);
        }

        public string Save(Product saveThis)
        {
            throw new NotImplementedException();
        }

        public void Delete(Product deleteThis)
        {
            throw new NotImplementedException();
        }

        public string Update(Product updateThis)
        {
            throw new NotImplementedException();
        }
    }
}
