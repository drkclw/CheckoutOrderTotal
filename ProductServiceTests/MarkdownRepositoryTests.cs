using NUnit.Framework;
using ProductService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductServiceTests
{
    public class MarkdownRepositoryTests
    {
        private Markdown markdown;
        [SetUp]
        public void Setup()
        {
            markdown = new Markdown
            {
                ProductName = "Can of soup",
                Amount = 0.20f
            };
        }

        [Test]
        public void GetAllReturnsAListOfMarkdowns()
        {
            IRepository<Markdown> markdownRepository = new MarkdownRepository();

            var priceList = markdownRepository.GetAll();

            Assert.NotNull(priceList);
        }

        [Test]
        public void SaveAddsMarkdownToList()
        {
            IRepository<Markdown> markdownRepository = new MarkdownRepository();

            markdownRepository.Save(markdown);

            var priceList = markdownRepository.GetAll();

            Assert.AreEqual(priceList.Count, 1);
        }

        [Test]
        public void SaveAddsRightMarkdownToList()
        {
            IRepository<Markdown> markdownRepository = new MarkdownRepository();

            markdownRepository.Save(markdown);

            var markdownList = markdownRepository.GetAll();

            Assert.AreEqual(markdownList[0].ProductName, "Can of soup");
            Assert.AreEqual(markdownList[0].Amount, 0.2f);
        }

        [Test]
        public void GetByProductNameWithExistingProductNameReturnsRightMarkdown()
        {
            IRepository<Markdown> markdownRepository = new MarkdownRepository();

            markdownRepository.Save(markdown);

            var markdownResult = markdownRepository.GetByProductName("Can of soup");

            Assert.AreEqual(markdownResult.ProductName, "Can of soup");
            Assert.AreEqual(markdownResult.Amount, 0.2f);
        }
    }
}
