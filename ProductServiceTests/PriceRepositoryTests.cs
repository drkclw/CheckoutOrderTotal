using NUnit.Framework;
using ProductService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductServiceTests
{
    public class PriceRepositoryTests
    {
        private Product product;
        [SetUp]
        public void Setup()
        {
            product = new Product
            {
                ProductName = "Can of soup",
                Price = 2.50f
            };
        }

        [Test]
        public void GetAllReturnsAListOfPrices()
        {
            IRepository<Product> priceRepository = new PriceRepository();

            var priceList = priceRepository.GetAll();

            Assert.NotNull(priceList);
        }

        [Test]
        public void SaveAddsPriceToList()
        {
            IRepository<Product> priceRepository = new PriceRepository();

            priceRepository.Save(product);

            var priceList = priceRepository.GetAll();

            Assert.AreEqual(priceList.Count, 1);
        }

        [Test]
        public void SaveAddsRightDataToList()
        {
            IRepository<Product> priceRepository = new PriceRepository();

            priceRepository.Save(product);

            var priceList = priceRepository.GetAll();

            Assert.AreEqual(priceList[0].ProductName, "Can of soup");
            Assert.AreEqual(priceList[0].Price, 2.5f);
        }

        [Test]
        public void GetByProductNameWithExistingProductNameReturnsRightPrice()
        {
            IRepository<Product> priceRepository = new PriceRepository();

            priceRepository.Save(product);

            var price = priceRepository.GetByProductName("Can of soup");

            Assert.AreEqual(price.ProductName, "Can of soup");
            Assert.AreEqual(price.Price, 2.5f);
        }
    }
}
