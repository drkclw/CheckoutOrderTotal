using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using ProductService.Controllers;
using ProductService.Models;
using System.Collections.Generic;

namespace Tests
{
    public class Tests
    {
        private Product validProduct;
        private Product invalidProduct;

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
        public void UpdateExistingPriceReturnsSuccess()
        {
            var priceRepository = new PriceRepository();
            PriceController priceController = new PriceController(priceRepository);

            var updateResult = priceController.UpdatePrice(validProduct);
            var updateContentResult = updateResult as ActionResult<string>;

            Assert.AreEqual(updateContentResult.Value, "Success");
        }
    }
}