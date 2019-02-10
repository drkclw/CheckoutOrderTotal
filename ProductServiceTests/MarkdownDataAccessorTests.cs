using NUnit.Framework;
using ProductService.Models.Markdowns;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductServiceTests
{
    public class MarkdownDataAccessorTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void GetAllMarkdownsReturnsListOfMarkdowns()
        {
            MarkdownDataAccessor markdownDataAccessor = new MarkdownDataAccessor();
            var markdownList = markdownDataAccessor.GetAll();

            Assert.NotNull(markdownList);
        }
    }
}
