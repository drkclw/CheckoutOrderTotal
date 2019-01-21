﻿using Microsoft.AspNetCore.Mvc;
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
    }
}
