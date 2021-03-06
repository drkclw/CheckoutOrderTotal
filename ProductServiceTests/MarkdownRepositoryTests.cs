﻿using NUnit.Framework;
using ProductService.Models;
using System;
using System.Collections.Generic;
using System.Text;
using ProductService.Models.Markdowns;

namespace ProductServiceTests
{
    public class MarkdownRepositoryTests
    {
        private Markdown markdown;
        private Markdown updatedMarkdown;
        private Markdown updatedNonExistentMarkdown;

        [SetUp]
        public void Setup()
        {
            markdown = new Markdown
            {
                ProductName = "Can of soup",
                Amount = 0.20f
            };

            updatedMarkdown = new Markdown
            {
                ProductName = "Can of soup",
                Amount = 0.45f
            };

            updatedNonExistentMarkdown = new Markdown
            {
                ProductName = "Bananas",
                Amount = 0.45f
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
        public void GetMarkdownByProductNameWithExistingProductNameReturnsRightMarkdown()
        {
            IRepository<Markdown> markdownRepository = new MarkdownRepository();

            markdownRepository.Save(markdown);

            var markdownResult = markdownRepository.GetByProductName("Can of soup");

            Assert.AreEqual(markdownResult.ProductName, "Can of soup");
            Assert.AreEqual(markdownResult.Amount, 0.2f);
        }

        [Test]
        public void GetMarkdownByProductNameWithNonExistentProductNameReturnsNull()
        {
            IRepository<Markdown> markdownRepository = new MarkdownRepository();

            markdownRepository.Save(markdown);

            var markdownResult = markdownRepository.GetByProductName("Bananas");

            Assert.IsNull(markdownResult);
        }

        [Test]
        public void UpdateWithExistingMarkdownReturnsTrue()
        {
            IRepository<Markdown> markdownRepository = new MarkdownRepository();

            markdownRepository.Save(markdown);
            bool updated = markdownRepository.Update(updatedMarkdown);

            Assert.IsTrue(updated);
        }        

        [Test]
        public void UpdateWithExistingMarkdownUpdatesRightMarkdown()
        {
            IRepository<Markdown> markdownRepository = new MarkdownRepository();

            markdownRepository.Save(markdown);
            bool updated = markdownRepository.Update(updatedMarkdown);
            var markdownList = markdownRepository.GetAll();

            Assert.IsTrue(updated);
            Assert.AreEqual(markdownList[0].ProductName, "Can of soup");
            Assert.AreEqual(markdownList[0].Amount, 0.45f);
        }
    }
}
