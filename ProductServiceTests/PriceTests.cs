using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using ProductService.Controllers;
using ProductService.Models;
using System.Collections.Generic;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
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

            var product = new Product
            {
                ProductName = "Can of soup",
                Price = 2.50f
            };
            var result = priceController.AddPrice(product);
            var contentResult = result as ActionResult<string>;

            Assert.AreEqual(contentResult.Value, "Success");
        }

        [Test]
        public void AddingInvalidPriceReturnsErrorMessage()
        {
            PriceController priceController = new PriceController(new PriceRepository());

            var product = new Product
            {
                ProductName = "Can of soup",
                Price = -1f
            };
            var result = priceController.AddPrice(product);
            var contentResult = result as ActionResult<string>;

            Assert.AreEqual(contentResult.Value, "Error: Price must be bigger than 0.");
        }

        [Test]
        public void ValidPriceIsAddedToTheList()
        {
            var priceRepository = new PriceRepository();
            PriceController priceController = new PriceController(priceRepository);

            var product = new Product
            {
                ProductName = "Can of soup",
                Price = 2.5f
            };
            var addResult = priceController.AddPrice(product);
            var addContentResult = addResult as ActionResult<string>;

            var getAllPricesResult = priceController.GetAllPrices();
            var getAllPricesContentResult = getAllPricesResult as ActionResult<IEnumerable<Product>>;
            var priceList = new List<Product>();
            priceList.AddRange(getAllPricesContentResult.Value);            

            Assert.AreEqual(priceList[0].ProductName, "Can of soup");
        }
    }
}