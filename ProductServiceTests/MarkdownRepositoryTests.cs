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
    }
}
