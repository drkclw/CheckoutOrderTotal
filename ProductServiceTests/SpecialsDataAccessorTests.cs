using Moq;
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

        [Test]
        public void GetByProductNameWithExistingSpecialReturnsSpecial()
        {
            Mock<IRepository<ISpecial>> mockSpecialsRepository = new Mock<IRepository<ISpecial>>();
            mockSpecialsRepository.Setup(x => x.GetByProductName("Can of soup")).Returns(validPriceSpecial);

            SpecialsDataAccessor specialsDataAccessor = new SpecialsDataAccessor(mockSpecialsRepository.Object);
            var special = specialsDataAccessor.GetByProductName("Can of soup");

            Assert.NotNull(special);
        }

        [Test]
        public void GetByProductNameWithNonExistentSpecialReturnsNull()
        {
            Mock<IRepository<ISpecial>> mockSpecialsRepository = new Mock<IRepository<ISpecial>>();
            mockSpecialsRepository.Setup(x => x.GetByProductName("Bananas")).Returns((ISpecial)null);

            SpecialsDataAccessor specialsDataAccessor = new SpecialsDataAccessor(mockSpecialsRepository.Object);
            var special = specialsDataAccessor.GetByProductName("Bananas");

            Assert.IsNull(special);
        }

        [Test]
        public void AddingValidSpeciaReturnsSuccess()
        {
            Mock<IRepository<ISpecial>> mockSpecialsRepository = new Mock<IRepository<ISpecial>>();
            mockSpecialsRepository.Setup(x => x.Save(validPriceSpecial));

            SpecialsDataAccessor specialsDataAccessor = new SpecialsDataAccessor(mockSpecialsRepository.Object);
            var result = specialsDataAccessor.Save(validPriceSpecial);

            Assert.AreEqual(result, "Success.");
        }
    }
}
