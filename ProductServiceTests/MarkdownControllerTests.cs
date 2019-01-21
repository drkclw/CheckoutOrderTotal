using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ProductService.Controllers;
using ProductService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductServiceTests
{
    public class MarkdownControllerTests
    {
        private Markdown validMarkdown;
        private List<Markdown> markdownList;
        private List<Product> productList;
        private Product canOfSoup;
        private Product bananas;
        private Markdown invalidMarkdown;
        private Markdown markdownForNonExistentPrice;
        private Markdown nonExistentMarkdownWithExistingPrice;
        private Markdown nonExistentMarkdownWithoutExistingPrice;

        [SetUp]
        public void Setup()
        {
            validMarkdown = new Markdown
            {
                ProductName = "Can of soup",
                Amount = 0.45f
            };

            canOfSoup = new Product
            {
                ProductName = "Can of soup",
                Price = 2.50f,
                Unit = Unit.EA
            };

            invalidMarkdown = new Markdown
            {
                ProductName = "Can of soup",
                Amount = 2.55f
            };

            bananas = new Product
            {
                ProductName = "Bananas",
                Price = 5f,
                Unit = Unit.LBS
            };

            markdownForNonExistentPrice = new Markdown
            {
                ProductName = "Apples",
                Amount = 2.55f
            };

            nonExistentMarkdownWithExistingPrice = new Markdown
            {
                ProductName = "Bananas",
                Amount = 0.55f
            };

            nonExistentMarkdownWithoutExistingPrice = new Markdown
            {
                ProductName = "Can of beans",
                Amount = 0.55f
            };

            markdownList = new List<Markdown>();
            markdownList.Add(validMarkdown);

            productList = new List<Product>();
            productList.Add(canOfSoup);
            productList.Add(bananas);
        }

        [Test]
        public void GetAllMarkdownsReturnsListOfMarkdowns()
        {
            Mock<IRepository<Markdown>> mockMarkdownRepository = new Mock<IRepository<Markdown>>();
            mockMarkdownRepository.Setup(x => x.GetAll()).Returns(markdownList);

            Mock<IRepository<Product>> mockPriceRepository = new Mock<IRepository<Product>>();
            mockPriceRepository.Setup(x => x.GetAll()).Returns(productList);

            MarkdownController markdownController = new MarkdownController(mockMarkdownRepository.Object, 
                mockPriceRepository.Object);
            var result = markdownController.GetAllMarkdowns();
            var contentResult = result as ActionResult<IEnumerable<Markdown>>;

            Assert.NotNull(contentResult.Value);
        }

        [Test]
        public void AddingValidMarkdownReturnsSuccess()
        {
            Mock<IRepository<Markdown>> mockMarkdownRepository = new Mock<IRepository<Markdown>>();
            mockMarkdownRepository.Setup(x => x.Save(validMarkdown));

            Mock<IRepository<Product>> mockPriceRepository = new Mock<IRepository<Product>>();
            mockPriceRepository.Setup(x => x.GetAll()).Returns(productList);

            MarkdownController markdownController = new MarkdownController(mockMarkdownRepository.Object,
                mockPriceRepository.Object);

            var result = markdownController.AddMarkdown(validMarkdown);
            var contentResult = result as ActionResult<string>;

            Assert.AreEqual(contentResult.Value, "Success");
        }

        [Test]
        public void AddingInvalidMarkdownReturnsErrorMessage()
        {
            Mock<IRepository<Markdown>> mockMarkdownRepository = new Mock<IRepository<Markdown>>();
            mockMarkdownRepository.Setup(x => x.Save(invalidMarkdown));

            Mock<IRepository<Product>> mockPriceRepository = new Mock<IRepository<Product>>();
            mockPriceRepository.Setup(x => x.GetAll()).Returns(productList);

            MarkdownController markdownController = new MarkdownController(mockMarkdownRepository.Object,
                mockPriceRepository.Object);

            var result = markdownController.AddMarkdown(invalidMarkdown);
            var contentResult = result as ActionResult<string>;

            Assert.AreEqual(contentResult.Value, "Error: Markdown must be smaller than price.");
        }

        [Test]
        public void AddingMarkdownForNonExistentReturnsErrorMessage()
        {
            Mock<IRepository<Markdown>> mockMarkdownRepository = new Mock<IRepository<Markdown>>();
            mockMarkdownRepository.Setup(x => x.Save(markdownForNonExistentPrice));

            Mock<IRepository<Product>> mockPriceRepository = new Mock<IRepository<Product>>();
            mockPriceRepository.Setup(x => x.GetAll()).Returns(productList);

            MarkdownController markdownController = new MarkdownController(mockMarkdownRepository.Object,
                mockPriceRepository.Object);

            var result = markdownController.AddMarkdown(markdownForNonExistentPrice);
            var contentResult = result as ActionResult<string>;

            Assert.AreEqual(contentResult.Value, "Error: Cannot add markdown for a product that doesn't have a price.");
        }

        [Test]
        public void UpdateValidExistingMarkdownReturnsSuccess()
        {
            Mock<IRepository<Markdown>> mockMarkdownRepository = new Mock<IRepository<Markdown>>();
            mockMarkdownRepository.Setup(x => x.Update(validMarkdown)).Returns(true);

            Mock<IRepository<Product>> mockPriceRepository = new Mock<IRepository<Product>>();
            mockPriceRepository.Setup(x => x.GetAll()).Returns(productList);

            MarkdownController markdownController = new MarkdownController(mockMarkdownRepository.Object,
                mockPriceRepository.Object);

            var result = markdownController.UpdateMarkdown(validMarkdown);
            var contentResult = result as ActionResult<string>;

            Assert.AreEqual(contentResult.Value, "Success.");
        }

        [Test]
        public void UpdateInvalidExistingMarkdownReturnsError()
        {
            Mock<IRepository<Markdown>> mockMarkdownRepository = new Mock<IRepository<Markdown>>();
            mockMarkdownRepository.Setup(x => x.Update(invalidMarkdown)).Returns(true);

            Mock<IRepository<Product>> mockPriceRepository = new Mock<IRepository<Product>>();
            mockPriceRepository.Setup(x => x.GetAll()).Returns(productList);

            MarkdownController markdownController = new MarkdownController(mockMarkdownRepository.Object,
                mockPriceRepository.Object);

            var result = markdownController.UpdateMarkdown(invalidMarkdown);
            var contentResult = result as ActionResult<string>;

            Assert.AreEqual(contentResult.Value, "Error: Markdown must be smaller than price.");
        }

        [Test]
        public void UpdateMarkdownForNonExistentPriceReturnsError()
        {
            Mock<IRepository<Markdown>> mockMarkdownRepository = new Mock<IRepository<Markdown>>();
            mockMarkdownRepository.Setup(x => x.Update(nonExistentMarkdownWithoutExistingPrice)).Returns(false);

            Mock<IRepository<Product>> mockPriceRepository = new Mock<IRepository<Product>>();
            mockPriceRepository.Setup(x => x.GetAll()).Returns(productList);

            MarkdownController markdownController = new MarkdownController(mockMarkdownRepository.Object,
                mockPriceRepository.Object);

            var result = markdownController.UpdateMarkdown(nonExistentMarkdownWithoutExistingPrice);
            var contentResult = result as ActionResult<string>;

            Assert.AreEqual(contentResult.Value, "Error: Cannot update markdown for a product that doesn't have a price.");
        }

        [Test]
        public void UpdateNonExistentMarkdownReturnsError()
        {
            Mock<IRepository<Markdown>> mockMarkdownRepository = new Mock<IRepository<Markdown>>();
            mockMarkdownRepository.Setup(x => x.Update(nonExistentMarkdownWithExistingPrice)).Returns(false);

            Mock<IRepository<Product>> mockPriceRepository = new Mock<IRepository<Product>>();
            mockPriceRepository.Setup(x => x.GetAll()).Returns(productList);

            MarkdownController markdownController = new MarkdownController(mockMarkdownRepository.Object,
                mockPriceRepository.Object);

            var result = markdownController.UpdateMarkdown(nonExistentMarkdownWithExistingPrice);
            var contentResult = result as ActionResult<string>;

            Assert.AreEqual(contentResult.Value, "Markdown does not exist, create markdown before updating price.");
        }

        [Test]
        public void GetExistentMarkdownReturnsMarkdown()
        {
            Mock<IRepository<Markdown>> mockMarkdownRepository = new Mock<IRepository<Markdown>>();
            mockMarkdownRepository.Setup(x => x.GetByProductName(validMarkdown.ProductName)).Returns(validMarkdown);

            Mock<IRepository<Product>> mockPriceRepository = new Mock<IRepository<Product>>();
            mockPriceRepository.Setup(x => x.GetAll()).Returns(productList);

            MarkdownController markdownController = new MarkdownController(mockMarkdownRepository.Object,
                mockPriceRepository.Object);

            string productName = "Can of soup";

            var result = markdownController.GetMarkdown(productName);
            var contentResult = result as ActionResult<float>;

            Assert.AreEqual(contentResult.Value, 0.45f);
        }
    }
}
