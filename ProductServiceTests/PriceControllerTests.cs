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
            Mock<IDataAccessor<Product>> mockPriceDataAccessor = new Mock<IDataAccessor<Product>>();
            mockPriceDataAccessor.Setup(x => x.GetAll()).Returns(productList);

            PriceController priceController = new PriceController(mockPriceDataAccessor.Object);
            var result = priceController.GetAllPrices();
            var contentResult = result as ActionResult<IEnumerable<Product>>;

            Assert.NotNull(contentResult.Value);
        }

        [Test]
        public void AddingValidPriceReturnsSuccess()
        {
            Mock<IDataAccessor<Product>> mockPriceDataAccessor = new Mock<IDataAccessor<Product>>();
            mockPriceDataAccessor.Setup(x => x.Save(validProduct)).Returns("Success.");

            PriceController priceController = new PriceController(mockPriceDataAccessor.Object);
            
            var result = priceController.AddPrice(validProduct);
            var contentResult = result as ActionResult<string>;

            Assert.AreEqual(contentResult.Value, "Success.");
        }

        [Test]
        public void AddingInvalidPriceReturnsErrorMessage()
        {
            Mock<IDataAccessor<Product>> mockPriceDataAccessor = new Mock<IDataAccessor<Product>>();
            mockPriceDataAccessor.Setup(x => x.Save(invalidProduct))
                .Returns("Error: Price must be bigger than 0.");

            PriceController priceController = new PriceController(mockPriceDataAccessor.Object);
            
            var result = priceController.AddPrice(invalidProduct);
            var contentResult = result as ActionResult<string>;

            Assert.AreEqual(contentResult.Value, "Error: Price must be bigger than 0.");
        }
            
        [Test]
        public void UpdateValidExistingPriceReturnsSuccess()
        {
            Mock<IDataAccessor<Product>> mockPriceDataAccessor = new Mock<IDataAccessor<Product>>();
            mockPriceDataAccessor.Setup(x => x.Update(validProduct)).Returns("Success.");

            PriceController priceController = new PriceController(mockPriceDataAccessor.Object);
            
            var updateResult = priceController.UpdatePrice(validProduct);
            var updateContentResult = updateResult as ActionResult<string>;

            Assert.AreEqual(updateContentResult.Value, "Success.");
        }

        [Test]
        public void UpdateInvalidExistingPriceReturnsError()
        {
            Mock<IDataAccessor<Product>> mockPriceDataAccessor = new Mock<IDataAccessor<Product>>();
            mockPriceDataAccessor.Setup(x => x.Update(invalidProduct))
                .Returns("Error: Price must be bigger than 0.");

            PriceController priceController = new PriceController(mockPriceDataAccessor.Object);

            var updateResult = priceController.UpdatePrice(invalidProduct);
            var updateContentResult = updateResult as ActionResult<string>;

            Assert.AreEqual(updateContentResult.Value, "Error: Price must be bigger than 0.");
        }

        [Test]
        public void UpdateNonExistentPriceReturnsError()
        {
            Mock<IDataAccessor<Product>> mockPriceDataAccessor = new Mock<IDataAccessor<Product>>();
            mockPriceDataAccessor.Setup(x => x.Update(nonExistentProduct))
                .Returns("Product does not exist, create product before updating price.");

            PriceController priceController = new PriceController(mockPriceDataAccessor.Object);

            var updateResult = priceController.UpdatePrice(nonExistentProduct);
            var updateContentResult = updateResult as ActionResult<string>;

            Assert.AreEqual(updateContentResult.Value, "Product does not exist, create product before updating price.");
        }

        [Test]
        public void GetPriceForSpecificProductReturnsPrice()
        {
            Mock<IDataAccessor<Product>> mockPriceDataAccessor = new Mock<IDataAccessor<Product>>();
            mockPriceDataAccessor.Setup(x => x.GetAmountByProductName(validProduct.ProductName)).Returns(2.5f);

            PriceController priceController = new PriceController(mockPriceDataAccessor.Object);

            string productName = "Can of soup";

            var priceResult = priceController.GetPrice(productName);
            var priceContentResult = priceResult as ActionResult<float>;

            Assert.AreEqual(priceContentResult.Value, 2.5f);
        }

        [Test]
        public void GetPriceForNonExistentProductReturnsZero()
        {
            Mock<IDataAccessor<Product>> mockPriceDataAccessor = new Mock<IDataAccessor<Product>>();
            mockPriceDataAccessor.Setup(x => x.GetAmountByProductName(nonExistentProduct.ProductName)).Returns(0);

            PriceController priceController = new PriceController(mockPriceDataAccessor.Object);

            string productName = "Bananas";

            var priceResult = priceController.GetPrice(productName);
            var priceContentResult = priceResult as ActionResult<float>;

            Assert.AreEqual(priceContentResult.Value, 0);
        }
    }
}