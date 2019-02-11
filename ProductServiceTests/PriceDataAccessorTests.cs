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
        public void GetAllPricesReturnsListOfPrices()
        {            
            Mock<IRepository<Product>> mockPriceRepository = new Mock<IRepository<Product>>();
            mockPriceRepository.Setup(x => x.GetAll()).Returns(productList);

            PriceDataAccessor priceDataAccessor = new PriceDataAccessor(mockPriceRepository.Object);
            var prices = priceDataAccessor.GetAll();

            Assert.NotNull(prices);
        }

        [Test]
        public void GetByProductNameWithExistingPriceReturnsPrice()
        {
            Mock<IRepository<Product>> mockPriceRepository = new Mock<IRepository<Product>>();
            mockPriceRepository.Setup(x => x.GetByProductName("Can of soup")).Returns(validProduct);

            PriceDataAccessor priceDataAccessor = new PriceDataAccessor(mockPriceRepository.Object);
            var price = priceDataAccessor.GetByProductName("Can of soup");

            Assert.NotNull(price);
        }

        [Test]
        public void GetPriceAmountWithExistingPriceReturnsPriceAmount()
        {            
            Mock<IRepository<Product>> mockPriceRepository = new Mock<IRepository<Product>>();
            mockPriceRepository.Setup(x => x.GetByProductName("Can of soup")).Returns(validProduct);

            PriceDataAccessor priceDataAccessor = new PriceDataAccessor(mockPriceRepository.Object);

            var priceAmount = priceDataAccessor.GetAmountByProductName("Can of soup");

            Assert.AreEqual(priceAmount, 2.50f);
        }

        [Test]
        public void GetPriceAmountWithExistingPriceReturnsRightPriceAmount()
        {
            Mock<IRepository<Product>> mockPriceRepository = new Mock<IRepository<Product>>();
            mockPriceRepository.Setup(x => x.GetByProductName("Can of soup")).Returns(validProduct);

            PriceDataAccessor priceDataAccessor = new PriceDataAccessor(mockPriceRepository.Object);
            var priceAmount = priceDataAccessor.GetAmountByProductName("Can of soup");

            Assert.AreEqual(priceAmount, validProduct.Price);
        }

        [Test]
        public void GetPriceAmountWithNonExistentPriceReturnsZero()
        {
            Mock<IRepository<Product>> mockPriceRepository = new Mock<IRepository<Product>>();
            mockPriceRepository.Setup(x => x.GetByProductName("Bananas")).Returns((Product)null);

            PriceDataAccessor priceDataAccessor = new PriceDataAccessor(mockPriceRepository.Object);
            var priceAmount = priceDataAccessor.GetAmountByProductName("Bananas");

            Assert.AreEqual(priceAmount, 0);
        }
    }
}
