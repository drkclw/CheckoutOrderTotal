using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.Models
{
    public class PriceRepository : IRepository<Product>
    {
        private IList<Product> priceList;

        public PriceRepository()
        {
            priceList = new List<Product>();
        }

        public IList<Product> GetAll()
        {
            return priceList;
        }

        public Product GetByProductName(string productName)
        {
            throw new NotImplementedException();
        }
        public void Save(Product saveThis)
        {
            priceList.Add(saveThis);
        }
        public void Delete(Product deleteThis)
        {
            throw new NotImplementedException();
        }
    }

}
