﻿using NUnit.Framework;
using ProductService.Models;
using System;
using System.Collections.Generic;
using System.Text;
using ProductService.Models.Prices;

namespace ProductServiceTests
{
    public class PriceRepositoryTests
    {
        private Product product;
        private Product product2;
        private Product updatedProduct;

        [SetUp]
        public void Setup()
        {
            product = new Product
            {
                ProductName = "Can of soup",
                Price = 2.50f,
                Unit = Unit.EA
            };

            product2 = new Product
            {
                ProductName = "Bananas",
                Price = 5f,
                Unit = Unit.LBS
            };

            updatedProduct = new Product
            {
                ProductName = "Can of soup",
                Price = 2.80f,
                Unit = Unit.EA
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

        [Test]
        public void GetByProductNameWithNonExistentProductNameReturnsNull()
        {
            IRepository<Product> priceRepository = new PriceRepository();

            priceRepository.Save(product);

            var price = priceRepository.GetByProductName("Bananas");

            Assert.IsNull(price);
        }

        [Test]
        public void UpdateWithExistingPriceReturnsTrue()
        {
            IRepository<Product> priceRepository = new PriceRepository();

            priceRepository.Save(product);
            bool updated = priceRepository.Update(updatedProduct);            

            Assert.IsTrue(updated);
        }        

        [Test]
        public void UpdateWithExistingPriceUpdatesRightData()
        {
            IRepository<Product> priceRepository = new PriceRepository();

            priceRepository.Save(product);
            bool updated = priceRepository.Update(updatedProduct);
            var priceList = priceRepository.GetAll();

            Assert.IsTrue(updated);
            Assert.AreEqual(priceList[0].ProductName, "Can of soup");
            Assert.AreEqual(priceList[0].Price, 2.8f);
        }
    }
}
