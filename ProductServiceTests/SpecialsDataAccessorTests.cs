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
        private PriceSpecial zeroPriceSpecial;
        private PriceSpecial lessThanTwoQuantityPriceSpecial;

        [SetUp]
        public void Setup()
        {
            validPriceSpecial = new PriceSpecial("Can of soup", 2, true, 5);
            validLimitSpecial = new LimitSpecial("Can of beans", 2, true, 1, 0.5f, 4);
            zeroPriceSpecial = new PriceSpecial("Can of soup", 2, true, 0);
            lessThanTwoQuantityPriceSpecial = new PriceSpecial("Can of soup", 1, true, 5);

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
        public void AddingValidSpecialReturnsSuccess()
        {
            Mock<IRepository<ISpecial>> mockSpecialsRepository = new Mock<IRepository<ISpecial>>();
            mockSpecialsRepository.Setup(x => x.Save(validPriceSpecial));

            SpecialsDataAccessor specialsDataAccessor = new SpecialsDataAccessor(mockSpecialsRepository.Object);
            var result = specialsDataAccessor.Save(validPriceSpecial);

            Assert.AreEqual(result, "Success.");
        }

        [Test]
        public void AddingValidPriceSpecialReturnsSuccess()
        {
            Mock<IRepository<ISpecial>> mockSpecialsRepository = new Mock<IRepository<ISpecial>>();
            mockSpecialsRepository.Setup(x => x.Save(validPriceSpecial));

            SpecialsDataAccessor specialsDataAccessor = new SpecialsDataAccessor(mockSpecialsRepository.Object);
            var result = specialsDataAccessor.Save(validPriceSpecial);

            Assert.AreEqual(result, "Success.");
        }

        [Test]
        public void AddingPriceSpecialWithZeroPriceReturnsError()
        {
            Mock<IRepository<ISpecial>> mockSpecialsRepository = new Mock<IRepository<ISpecial>>();            

            SpecialsDataAccessor specialsDataAccessor = new SpecialsDataAccessor(mockSpecialsRepository.Object);
            var result = specialsDataAccessor.Save(zeroPriceSpecial);

            Assert.AreEqual(result, "Error: Price must be bigger than 0.");
        }

        [Test]
        public void AddingPriceSpecialWithPurchaseQuantityLessThanTwoReturnsError()
        {
            Mock<IRepository<ISpecial>> mockSpecialsRepository = new Mock<IRepository<ISpecial>>();

            SpecialsDataAccessor specialsDataAccessor = new SpecialsDataAccessor(mockSpecialsRepository.Object);
            var result = specialsDataAccessor.Save(lessThanTwoQuantityPriceSpecial);

            Assert.AreEqual(result, "Error: Purchase quantity must be bigger than 1.");
        }

        [Test]
        public void AddingValidLimitSpecialReturnsSuccess()
        {
            Mock<IRepository<ISpecial>> mockSpecialsRepository = new Mock<IRepository<ISpecial>>();

            SpecialsDataAccessor specialsDataAccessor = new SpecialsDataAccessor(mockSpecialsRepository.Object);
            var result = specialsDataAccessor.Save(validLimitSpecial);

            Assert.AreEqual(result, "Success.");
        }
    }
}
