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
        private Product invalidProduct;
        private Product nonExistentProduct;

        [SetUp]
        public void Setup()
        {
            validProduct = new Product
            {
                ProductName = "Can of soup",
                Price = 2.50f,
                Unit = Unit.EA
            };

            invalidProduct = new Product
            {
                ProductName = "Can of soup",
                Price = -1f,
                Unit = Unit.EA
            };

            nonExistentProduct = new Product
            {
                ProductName = "Bananas",
                Price = 5f,
                Unit = Unit.LBS
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

        [Test]
        public void AddingValidPriceReturnsSuccess()
        {
            Mock<IRepository<Product>> mockPriceRepository = new Mock<IRepository<Product>>();
            mockPriceRepository.Setup(x => x.Save(validProduct));
            
            PriceDataAccessor priceDataAccessor = new PriceDataAccessor(mockPriceRepository.Object);

            var result = priceDataAccessor.Save(validProduct);

            Assert.AreEqual(result, "Success.");
        }

        [Test]
        public void AddingInvalidPriceReturnsErrorMessage()
        {
            Mock<IRepository<Product>> mockPriceRepository = new Mock<IRepository<Product>>();
            mockPriceRepository.Setup(x => x.Save(invalidProduct));

            PriceDataAccessor priceDataAccessor = new PriceDataAccessor(mockPriceRepository.Object);

            var result = priceDataAccessor.Save(invalidProduct);            

            Assert.AreEqual(result, "Error: Price must be bigger than 0.");
        }

        [Test]
        public void UpdateValidExistingPriceReturnsSuccess()
        {
            Mock<IRepository<Product>> mockPriceRepository = new Mock<IRepository<Product>>();
            mockPriceRepository.Setup(x => x.GetAll()).Returns(productList);
            mockPriceRepository.Setup(x => x.Update(validProduct)).Returns(true);

            PriceDataAccessor priceDataAccessor = new PriceDataAccessor(mockPriceRepository.Object);

            var updateResult = priceDataAccessor.Update(validProduct);            

            Assert.AreEqual(updateResult, "Success.");
        }

        [Test]
        public void UpdateInvalidExistingPriceReturnsError()
        {
            Mock<IRepository<Product>> mockPriceRepository = new Mock<IRepository<Product>>();
            mockPriceRepository.Setup(x => x.Update(invalidProduct)).Returns(true);

            PriceDataAccessor priceDataAccessor = new PriceDataAccessor(mockPriceRepository.Object);

            var updateResult = priceDataAccessor.Update(invalidProduct);

            Assert.AreEqual(updateResult, "Error: Price must be bigger than 0.");
        }

        [Test]
        public void UpdateNonExistentPriceReturnsError()
        {
            Mock<IRepository<Product>> mockPriceRepository = new Mock<IRepository<Product>>();
            mockPriceRepository.Setup(x => x.GetAll()).Returns(productList);
            mockPriceRepository.Setup(x => x.Update(nonExistentProduct)).Returns(false);

            PriceDataAccessor priceDataAccessor = new PriceDataAccessor(mockPriceRepository.Object);

            var updateResult = priceDataAccessor.Update(nonExistentProduct);

            Assert.AreEqual(updateResult, "Product does not exist, create product before updating price.");
        }
    }
}
