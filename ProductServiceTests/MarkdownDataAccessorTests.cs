using Moq;
using NUnit.Framework;
using ProductService.Models;
using ProductService.Models.Markdowns;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductServiceTests
{
    public class MarkdownDataAccessorTests
    {
        private Markdown validMarkdown;
        private List<Markdown> markdownList;

        [SetUp]
        public void Setup()
        {
            validMarkdown = new Markdown
            {
                ProductName = "Can of soup",
                Amount = 0.45f
            };

            markdownList = new List<Markdown>();
            markdownList.Add(validMarkdown);
        }

        [Test]
        public void GetAllMarkdownsReturnsListOfMarkdowns()
        {
            Mock<IRepository<Markdown>> mockMarkdownRepository = new Mock<IRepository<Markdown>>();
            mockMarkdownRepository.Setup(x => x.GetAll()).Returns(markdownList);

            MarkdownDataAccessor markdownDataAccessor = new MarkdownDataAccessor(mockMarkdownRepository.Object);
            var markdowns = markdownDataAccessor.GetAll();

            Assert.NotNull(markdowns);
        }
    }
}
