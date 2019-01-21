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

            MarkdownController markdownController = new MarkdownController(mockMarkdownRepository.Object);
            var result = markdownController.GetAllMarkdowns();
            var contentResult = result as ActionResult<IEnumerable<Markdown>>;

            Assert.NotNull(contentResult.Value);
        }
    }
}
