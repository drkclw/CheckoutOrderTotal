using Moq;
using NUnit.Framework;
using ProductService.Models;
using ProductService.Models.Prices;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductServiceTests
{
    public class PriceDataAccessorTests
    {
        private Product validProduct;
        private List<Product> productList;

        [SetUp]
        public void Setup()
        {
            validProduct = new Product
            {
                ProductName = "Can of soup",
                Price = 2.50f,
                Unit = Unit.EA
            };

            productList = new List<Product>();
            productList.Add(validProduct);
        }

        [Test]
        public void GetAllPricesReturnsListOfMarkdowns()
        {            
            Mock<IRepository<Product>> mockPriceRepository = new Mock<IRepository<Product>>();
            mockPriceRepository.Setup(x => x.GetAll()).Returns(productList);

            PriceDataAccessor priceDataAccessor = new PriceDataAccessor(mockPriceRepository.Object);
            var prices = priceDataAccessor.GetAll();

            Assert.NotNull(prices);
        }
    }
}
