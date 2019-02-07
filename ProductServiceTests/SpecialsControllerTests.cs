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
        private SpecialRequest validPriceSpecialRequest;
        private SpecialRequest zeroPriceSpecialRequest;

        [SetUp]
        public void Setup()
        {
            validPriceSpecial = new PriceSpecial("Can of soup", 2, true, 5);
            validPriceSpecialRequest = new SpecialRequest
            {
                ProductName = "Can of soup",
                PurchaseQty = 2,
                IsActive = true,                
                Price = 5,
                Type = SpecialType.Price
            };

            zeroPriceSpecialRequest = new SpecialRequest
            {
                ProductName = "Can of soup",
                PurchaseQty = 2,
                IsActive = true,
                Price = 0,
                Type = SpecialType.Price
            };

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

        [Test]
        public void AddingValidSpecialReturnsSuccess()
        {
            Mock<IRepository<ISpecial>> mockSpecialsRepository = new Mock<IRepository<ISpecial>>();
            mockSpecialsRepository.Setup(x => x.Save(validPriceSpecial));
            
            SpecialsController specialsController = new SpecialsController(mockSpecialsRepository.Object);

            var result = specialsController.AddSpecial(validPriceSpecialRequest);
            var contentResult = result as ActionResult<string>;

            Assert.AreEqual(contentResult.Value, "Success.");
        }

        [Test]
        public void AddingValidPriceSpecialReturnsSuccess()
        {
            Mock<IRepository<ISpecial>> mockSpecialsRepository = new Mock<IRepository<ISpecial>>();
            mockSpecialsRepository.Setup(x => x.Save(validPriceSpecial));

            SpecialsController specialsController = new SpecialsController(mockSpecialsRepository.Object);

            var result = specialsController.AddSpecial(validPriceSpecialRequest);
            var contentResult = result as ActionResult<string>;

            Assert.AreEqual(contentResult.Value, "Success.");
        }

        [Test]
        public void AddingPriceSpecialWithZeroPriceReturnsError()
        {
            Mock<IRepository<ISpecial>> mockSpecialsRepository = new Mock<IRepository<ISpecial>>();
            mockSpecialsRepository.Setup(x => x.Save(validPriceSpecial));

            SpecialsController specialsController = new SpecialsController(mockSpecialsRepository.Object);

            var result = specialsController.AddSpecial(zeroPriceSpecialRequest);
            var contentResult = result as ActionResult<string>;

            Assert.AreEqual(contentResult.Value, "Error: Price must be bigger than 0.");
        }
    }
}
