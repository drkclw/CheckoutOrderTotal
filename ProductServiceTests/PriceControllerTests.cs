using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ProductService.Controllers;
using ProductService.Models;
using System.Collections.Generic;

namespace ProductServiceTests
{
    public class PriceControllerTests
    {
        private Product validProduct;
        private Product invalidProduct;
        private Product nonExistentProduct;

        [SetUp]
        public void Setup()
        {
            validProduct = new Product
            {
                ProductName = "Can of soup",
                Price = 2.50f
            };

            invalidProduct = new Product
            {
                ProductName = "Can of soup",
                Price = -1f
            };

            nonExistentProduct = new Product
            {
                ProductName = "Bananas",
                Price = 5f
            };
        }

        [Test]
        public void GetAllPricesReturnsListOfPrices()
        {
            PriceController priceController = new PriceController(new PriceRepository());
            var result = priceController.GetAllPrices();
            var contentResult = result as ActionResult<IEnumerable<Product>>;

            Assert.NotNull(contentResult.Value);
        }

        [Test]
        public void AddingValidPriceReturnsSuccess()
        {
            PriceController priceController = new PriceController(new PriceRepository());
            
            var result = priceController.AddPrice(validProduct);
            var contentResult = result as ActionResult<string>;

            Assert.AreEqual(contentResult.Value, "Success");
        }

        [Test]
        public void AddingInvalidPriceReturnsErrorMessage()
        {
            PriceController priceController = new PriceController(new PriceRepository());
            
            var result = priceController.AddPrice(invalidProduct);
            var contentResult = result as ActionResult<string>;

            Assert.AreEqual(contentResult.Value, "Error: Price must be bigger than 0.");
        }

        [Test]
        public void ValidPriceIsAddedToTheList()
        {
            var priceRepository = new PriceRepository();
            PriceController priceController = new PriceController(priceRepository);
           
            var addResult = priceController.AddPrice(validProduct);
            var addContentResult = addResult as ActionResult<string>;

            var getAllPricesResult = priceController.GetAllPrices();
            var getAllPricesContentResult = getAllPricesResult as ActionResult<IEnumerable<Product>>;
            var priceList = new List<Product>();
            priceList.AddRange(getAllPricesContentResult.Value);            

            Assert.AreEqual(priceList[0].ProductName, "Can of soup");
        }

        [Test]
        public void UpdateValidExistingPriceReturnsSuccess()
        {
            //var priceRepository = new PriceRepository();
            Mock<IRepository<Product>> mockPriceRepository = new Mock<IRepository<Product>>();
            mockPriceRepository.Setup(x => x.Update(validProduct)).Returns(true);

            PriceController priceController = new PriceController(mockPriceRepository.Object);
            
            var updateResult = priceController.UpdatePrice(validProduct);
            var updateContentResult = updateResult as ActionResult<string>;

            Assert.AreEqual(updateContentResult.Value, "Success");
        }

        [Test]
        public void UpdateInvalidExistingPriceReturnsError()
        {
            Mock<IRepository<Product>> mockPriceRepository = new Mock<IRepository<Product>>();
            mockPriceRepository.Setup(x => x.Update(invalidProduct)).Returns(true);

            PriceController priceController = new PriceController(mockPriceRepository.Object);

            var updateResult = priceController.UpdatePrice(invalidProduct);
            var updateContentResult = updateResult as ActionResult<string>;

            Assert.AreEqual(updateContentResult.Value, "Error: Price must be bigger than 0.");
        }

        [Test]
        public void UpdateNonExistentPriceReturnsError()
        {
            Mock<IRepository<Product>> mockPriceRepository = new Mock<IRepository<Product>>();
            mockPriceRepository.Setup(x => x.Update(nonExistentProduct)).Returns(false);

            PriceController priceController = new PriceController(mockPriceRepository.Object);

            var updateResult = priceController.UpdatePrice(nonExistentProduct);
            var updateContentResult = updateResult as ActionResult<string>;

            Assert.AreEqual(updateContentResult.Value, "Product does not exist, create product before updating price.");
        }

        [Test]
        public void GetPriceForSpecificProductReturnsPrice()
        {
            Mock<IRepository<Product>> mockPriceRepository = new Mock<IRepository<Product>>();
            mockPriceRepository.Setup(x => x.GetByProductName(validProduct.ProductName)).Returns(validProduct);

            PriceController priceController = new PriceController(mockPriceRepository.Object);

            string productName = "Can of soup";

            var priceResult = priceController.GetPrice(productName);
            var priceContentResult = priceResult as ActionResult<float>;

            Assert.AreEqual(priceContentResult.Value, 2.5f);
        }

        [Test]
        public void GetPriceForNonExistentProductReturnsZero()
        {
            Mock<IRepository<Product>> mockPriceRepository = new Mock<IRepository<Product>>();
            mockPriceRepository.Setup(x => x.GetByProductName(nonExistentProduct.ProductName)).Returns((Product)null);

            PriceController priceController = new PriceController(mockPriceRepository.Object);

            string productName = "Bananas";

            var priceResult = priceController.GetPrice(productName);
            var priceContentResult = priceResult as ActionResult<float>;

            Assert.AreEqual(priceContentResult.Value, 0);
        }
    }
}