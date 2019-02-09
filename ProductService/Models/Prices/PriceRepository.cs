using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.Models.Prices
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
            var productDict = priceList.ToDictionary(p => p.ProductName, p => p);

            if (productDict.ContainsKey(productName))
            {
                return productDict[productName];
            }
            else
            {
                return null;
            }
        }
        public void Save(Product saveThis)
        {
            priceList.Add(saveThis);
        }
        public void Delete(Product deleteThis)
        {
            throw new NotImplementedException();
        }

        public bool Update(Product updateThis)
        {
            var productDict = priceList.ToDictionary(p => p.ProductName, p => p);

            if (productDict.ContainsKey(updateThis.ProductName))
            {
                productDict[updateThis.ProductName].Price = updateThis.Price;
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}
