using NUnit.Framework;
using ProductService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductServiceTests
{
    public class MarkdownRepositoryTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void GetAllReturnsAListOfMarkdowns()
        {
            IRepository<Markdown> markdownRepository = new MarkdownRepository();

            var priceList = markdownRepository.GetAll();

            Assert.NotNull(priceList);
        }
    }
}
