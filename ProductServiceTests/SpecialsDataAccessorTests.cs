﻿using Moq;
using NUnit.Framework;
using ProductService.Models;
using ProductService.Models.Specials;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductServiceTests
{
    public class SpecialsDataAccessorTests
    {
        private List<ISpecial> specialsList;
        private PriceSpecial validPriceSpecial;
        private LimitSpecial validLimitSpecial;

        [SetUp]
        public void Setup()
        {
            validPriceSpecial = new PriceSpecial("Can of soup", 2, true, 5);
            validLimitSpecial = new LimitSpecial("Can of beans", 2, true, 1, 0.5f, 4);

            specialsList = new List<ISpecial>();
            specialsList.Add(validPriceSpecial);
            specialsList.Add(validLimitSpecial);
        }

        [Test]
        public void GetAllSpecialsReturnsListOfSpecials()
        {
            Mock<IRepository<ISpecial>> mockSpecialsRepository = new Mock<IRepository<ISpecial>>();
            mockSpecialsRepository.Setup(x => x.GetAll()).Returns(specialsList);

            SpecialsDataAccessor specialsDataAccessor = new SpecialsDataAccessor(mockSpecialsRepository.Object);
            var specials = specialsDataAccessor.GetAll();

            Assert.NotNull(specials);
        }
    }
}
