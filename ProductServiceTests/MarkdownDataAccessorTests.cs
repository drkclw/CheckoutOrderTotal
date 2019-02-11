using Moq;
using NUnit.Framework;
using ProductService.Models;
using ProductService.Models.Markdowns;
using ProductService.Models.Prices;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductServiceTests
{
    public class MarkdownDataAccessorTests
    {
        private Markdown validMarkdown;
        private List<Markdown> markdownList;
        private Markdown invalidMarkdown;
        private Product canOfSoup;
        private List<Product> productList;
        private Markdown markdownForNonExistentPrice;
        private Markdown nonExistentMarkdownWithoutExistingPrice;
        private Markdown nonExistentMarkdownWithExistingPrice;
        private Product bananas;

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

            markdownForNonExistentPrice = new Markdown
            {
                ProductName = "Apples",
                Amount = 2.55f
            };

            nonExistentMarkdownWithoutExistingPrice = new Markdown
            {
                ProductName = "Can of beans",
                Amount = 0.55f
            };

            nonExistentMarkdownWithExistingPrice = new Markdown
            {
                ProductName = "Bananas",
                Amount = 0.55f
            };

            bananas = new Product
            {
                ProductName = "Bananas",
                Price = 5f,
                Unit = Unit.LBS
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

            MarkdownDataAccessor markdownDataAccessor = new MarkdownDataAccessor(mockMarkdownRepository.Object,
                mockPriceRepository.Object);
            var markdowns = markdownDataAccessor.GetAll();

            Assert.NotNull(markdowns);
        }

        [Test]
        public void GetByProductNameWithExistingMarkdownReturnsMarkdown()
        {
            Mock<IRepository<Markdown>> mockMarkdownRepository = new Mock<IRepository<Markdown>>();
            mockMarkdownRepository.Setup(x => x.GetByProductName("Can of soup")).Returns(validMarkdown);

            Mock<IRepository<Product>> mockPriceRepository = new Mock<IRepository<Product>>();
            mockPriceRepository.Setup(x => x.GetAll()).Returns(productList);

            MarkdownDataAccessor markdownDataAccessor = new MarkdownDataAccessor(mockMarkdownRepository.Object,
                mockPriceRepository.Object);
            var markdown = markdownDataAccessor.GetByProductName("Can of soup");

            Assert.NotNull(markdown);
        }

        [Test]
        public void GetMarkdownAmountWithExistingMarkdownReturnsMarkdownAmount()
        {
            Mock<IRepository<Markdown>> mockMarkdownRepository = new Mock<IRepository<Markdown>>();
            mockMarkdownRepository.Setup(x => x.GetByProductName("Can of soup")).Returns(validMarkdown);

            Mock<IRepository<Product>> mockPriceRepository = new Mock<IRepository<Product>>();
            mockPriceRepository.Setup(x => x.GetAll()).Returns(productList);

            MarkdownDataAccessor markdownDataAccessor = new MarkdownDataAccessor(mockMarkdownRepository.Object,
                mockPriceRepository.Object);
            var markdownAmount = markdownDataAccessor.GetAmountByProductName("Can of soup");

            Assert.AreEqual(markdownAmount, 0.45f);
        }

        [Test]
        public void GetMarkdownAmountWithExistingMarkdownReturnsRightMarkdownAmount()
        {
            Mock<IRepository<Markdown>> mockMarkdownRepository = new Mock<IRepository<Markdown>>();
            mockMarkdownRepository.Setup(x => x.GetByProductName("Can of soup")).Returns(validMarkdown);

            Mock<IRepository<Product>> mockPriceRepository = new Mock<IRepository<Product>>();
            mockPriceRepository.Setup(x => x.GetAll()).Returns(productList);

            MarkdownDataAccessor markdownDataAccessor = new MarkdownDataAccessor(mockMarkdownRepository.Object,
                mockPriceRepository.Object);
            var markdownAmount = markdownDataAccessor.GetAmountByProductName("Can of soup");

            Assert.AreEqual(markdownAmount, validMarkdown.Amount);
        }

        [Test]
        public void GetMarkdownAmountWithNonExistentMarkdownReturnsZero()
        {
            Mock<IRepository<Markdown>> mockMarkdownRepository = new Mock<IRepository<Markdown>>();
            mockMarkdownRepository.Setup(x => x.GetByProductName("Bananas")).Returns((Markdown)null);

            Mock<IRepository<Product>> mockPriceRepository = new Mock<IRepository<Product>>();
            mockPriceRepository.Setup(x => x.GetAll()).Returns(productList);

            MarkdownDataAccessor markdownDataAccessor = new MarkdownDataAccessor(mockMarkdownRepository.Object,
                mockPriceRepository.Object);
            var markdownAmount = markdownDataAccessor.GetAmountByProductName("Bananas");

            Assert.AreEqual(markdownAmount, 0);
        }

        [Test]
        public void AddingValidMarkdownReturnsSuccess()
        {
            Mock<IRepository<Markdown>> mockMarkdownRepository = new Mock<IRepository<Markdown>>();
            mockMarkdownRepository.Setup(x => x.Save(validMarkdown));

            Mock<IRepository<Product>> mockPriceRepository = new Mock<IRepository<Product>>();
            mockPriceRepository.Setup(x => x.GetAll()).Returns(productList);

            MarkdownDataAccessor markdownDataAccessor = new MarkdownDataAccessor(mockMarkdownRepository.Object,
                mockPriceRepository.Object);

            var result = markdownDataAccessor.Save(validMarkdown);                 

            Assert.AreEqual(result, "Success.");
        }

        [Test]
        public void AddingInvalidMarkdownReturnsErrorMessage()
        {
            Mock<IRepository<Markdown>> mockMarkdownRepository = new Mock<IRepository<Markdown>>();
            mockMarkdownRepository.Setup(x => x.Save(invalidMarkdown));

            Mock<IRepository<Product>> mockPriceRepository = new Mock<IRepository<Product>>();
            mockPriceRepository.Setup(x => x.GetAll()).Returns(productList);

            MarkdownDataAccessor markdownDataAccessor = new MarkdownDataAccessor(mockMarkdownRepository.Object,
                mockPriceRepository.Object);

            var result = markdownDataAccessor.Save(invalidMarkdown);            

            Assert.AreEqual(result, "Error: Markdown must be smaller than price.");
        }

        [Test]
        public void AddingMarkdownForNonExistentReturnsErrorMessage()
        {
            Mock<IRepository<Markdown>> mockMarkdownRepository = new Mock<IRepository<Markdown>>();
            mockMarkdownRepository.Setup(x => x.Save(markdownForNonExistentPrice));

            Mock<IRepository<Product>> mockPriceRepository = new Mock<IRepository<Product>>();
            mockPriceRepository.Setup(x => x.GetAll()).Returns(productList);

            MarkdownDataAccessor markdownDataAccessor = new MarkdownDataAccessor(mockMarkdownRepository.Object,
                mockPriceRepository.Object);

            var result = markdownDataAccessor.Save(markdownForNonExistentPrice);

            Assert.AreEqual(result, "Error: Cannot add markdown for a product that doesn't have a price.");
        }

        [Test]
        public void UpdateValidExistingMarkdownReturnsSuccess()
        {
            Mock<IRepository<Markdown>> mockMarkdownRepository = new Mock<IRepository<Markdown>>();
            mockMarkdownRepository.Setup(x => x.Update(validMarkdown)).Returns(true);
            mockMarkdownRepository.Setup(x => x.GetAll()).Returns(markdownList);

            Mock<IRepository<Product>> mockPriceRepository = new Mock<IRepository<Product>>();
            mockPriceRepository.Setup(x => x.GetAll()).Returns(productList);

            MarkdownDataAccessor markdownDataAccessor = new MarkdownDataAccessor(mockMarkdownRepository.Object,
                mockPriceRepository.Object);

            var result = markdownDataAccessor.Update(validMarkdown);

            Assert.AreEqual(result, "Success.");
        }

        [Test]
        public void UpdateInvalidExistingMarkdownReturnsError()
        {
            Mock<IRepository<Markdown>> mockMarkdownRepository = new Mock<IRepository<Markdown>>();
            mockMarkdownRepository.Setup(x => x.Update(invalidMarkdown)).Returns(false);
            mockMarkdownRepository.Setup(x => x.GetAll()).Returns(markdownList);

            Mock<IRepository<Product>> mockPriceRepository = new Mock<IRepository<Product>>();
            mockPriceRepository.Setup(x => x.GetAll()).Returns(productList);

            MarkdownDataAccessor markdownDataAccessor = new MarkdownDataAccessor(mockMarkdownRepository.Object,
                mockPriceRepository.Object);

            var result = markdownDataAccessor.Update(invalidMarkdown);

            Assert.AreEqual(result, "Error: Markdown must be smaller than price.");
        }

        [Test]
        public void UpdateMarkdownForNonExistentPriceReturnsError()
        {
            Mock<IRepository<Markdown>> mockMarkdownRepository = new Mock<IRepository<Markdown>>();
            mockMarkdownRepository.Setup(x => x.Update(nonExistentMarkdownWithoutExistingPrice)).Returns(false);
            mockMarkdownRepository.Setup(x => x.GetAll()).Returns(markdownList);

            Mock<IRepository<Product>> mockPriceRepository = new Mock<IRepository<Product>>();
            mockPriceRepository.Setup(x => x.GetAll()).Returns(productList);

            MarkdownDataAccessor markdownDataAccessor = new MarkdownDataAccessor(mockMarkdownRepository.Object,
                mockPriceRepository.Object);

            var result = markdownDataAccessor.Update(nonExistentMarkdownWithoutExistingPrice);

            Assert.AreEqual(result, "Error: Cannot update markdown for a product that doesn't have a price.");
        }

        [Test]
        public void UpdateNonExistentMarkdownReturnsError()
        {
            Mock<IRepository<Markdown>> mockMarkdownRepository = new Mock<IRepository<Markdown>>();
            mockMarkdownRepository.Setup(x => x.Update(nonExistentMarkdownWithExistingPrice)).Returns(false);
            mockMarkdownRepository.Setup(x => x.GetAll()).Returns(markdownList);

            Mock<IRepository<Product>> mockPriceRepository = new Mock<IRepository<Product>>();
            mockPriceRepository.Setup(x => x.GetAll()).Returns(productList);

            MarkdownDataAccessor markdownDataAccessor = new MarkdownDataAccessor(mockMarkdownRepository.Object,
                mockPriceRepository.Object);

            var result = markdownDataAccessor.Update(nonExistentMarkdownWithExistingPrice);

            Assert.AreEqual(result, "Markdown does not exist, create markdown before updating price.");
        }
    }
}
