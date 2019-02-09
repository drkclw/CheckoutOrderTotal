using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ProductService.Controllers;
using ProductService.Models;
using System.Collections.Generic;
using ProductService.Models.Prices;

namespace ProductServiceTests
{
    public class PriceControllerTests
    {
        private Product validProduct;
        private Product invalidProduct;
        private Product nonExistentProduct;
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
            productList.Add(nonExistentProduct);
        }

        [Test]
        public void GetAllPricesReturnsListOfPrices()
        {
            Mock<IRepository<Product>> mockPriceRepository = new Mock<IRepository<Product>>();
            mockPriceRepository.Setup(x => x.GetAll()).Returns(productList);

            PriceController priceController = new PriceController(mockPriceRepository.Object);
            var result = priceController.GetAllPrices();
            var contentResult = result as ActionResult<IEnumerable<Product>>;

            Assert.NotNull(contentResult.Value);
        }

        [Test]
        public void AddingValidPriceReturnsSuccess()
        {
            Mock<IRepository<Product>> mockPriceRepository = new Mock<IRepository<Product>>();
            mockPriceRepository.Setup(x => x.Save(validProduct));

            PriceController priceController = new PriceController(mockPriceRepository.Object);
            
            var result = priceController.AddPrice(validProduct);
            var contentResult = result as ActionResult<string>;

            Assert.AreEqual(contentResult.Value, "Success");
        }

        [Test]
        public void AddingInvalidPriceReturnsErrorMessage()
        {
            Mock<IRepository<Product>> mockPriceRepository = new Mock<IRepository<Product>>();
            mockPriceRepository.Setup(x => x.Save(invalidProduct));

            PriceController priceController = new PriceController(mockPriceRepository.Object);
            
            var result = priceController.AddPrice(invalidProduct);
            var contentResult = result as ActionResult<string>;

            Assert.AreEqual(contentResult.Value, "Error: Price must be bigger than 0.");
        }
            
        [Test]
        public void UpdateValidExistingPriceReturnsSuccess()
        {            
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