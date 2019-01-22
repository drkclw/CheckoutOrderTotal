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
    public class SpecialsControllerTests
    {        
        private List<ISpecial> specialsList;
        private PriceSpecial validPriceSpecial;

        [SetUp]
        public void Setup()
        {
            validPriceSpecial = new PriceSpecial("Can of soup", 2, true, 5);

            specialsList = new List<ISpecial>();
            specialsList.Add(validPriceSpecial);
        }

        [Test]
        public void GetAllSpecialsReturnsListOfSpecials()
        {
            Mock<IRepository<ISpecial>> mockSpecialsRepository = new Mock<IRepository<ISpecial>>();
            mockSpecialsRepository.Setup(x => x.GetAll()).Returns(specialsList);
            
            SpecialsController specialsController = new SpecialsController(mockSpecialsRepository.Object);
            var result = specialsController.GetAllSpecials();
            var contentResult = result as ActionResult<IEnumerable<ISpecial>>;

            Assert.NotNull(contentResult.Value);
        }
    }
}
