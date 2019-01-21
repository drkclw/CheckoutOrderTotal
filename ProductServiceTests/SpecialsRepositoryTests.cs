using NUnit.Framework;
using ProductService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductServiceTests
{
    public class SpecialsRepositoryTests
    {
        private ISpecial priceSpecial;
        private ISpecial limitSpecialNoLimit;
        private ISpecial restrictionSpecialLesser;
        private ISpecial nonExistentPriceSpecial;
        private ISpecial updatedPriceSpecial;

        [SetUp]
        public void Setup()
        {
            priceSpecial = new PriceSpecial("Can of soup", 2, true, 5);

            limitSpecialNoLimit = new LimitSpecial("Can of soup", 2, true, 1, 0.5f, 0);

            restrictionSpecialLesser = new RestrictionSpecial("Can of soup", 2, true, 1, 0.5f, RestrictionType.Lesser);

            nonExistentPriceSpecial = new PriceSpecial("Can of beans", 2, true, 6);

            updatedPriceSpecial = new PriceSpecial("Can of soup", 2, true, 6);
        }

        [Test]
        public void GetAllReturnsAListOfMarkdowns()
        {
            IRepository<ISpecial> specialsRepository = new SpecialsRepository();

            var specialsList = specialsRepository.GetAll();

            Assert.NotNull(specialsList);
        }

        [Test]
        public void SaveAddsPriceSpecialToList()
        {
            IRepository<ISpecial> specialsRepository = new SpecialsRepository();

            specialsRepository.Save(priceSpecial);

            var priceList = specialsRepository.GetAll();

            Assert.AreEqual(priceList.Count, 1);
            Assert.AreEqual(priceList[0].Type, SpecialType.Price);
        }

        [Test]
        public void SaveAddsLimitSpecialToList()
        {
            IRepository<ISpecial> specialsRepository = new SpecialsRepository();

            specialsRepository.Save(limitSpecialNoLimit);

            var priceList = specialsRepository.GetAll();

            Assert.AreEqual(priceList.Count, 1);
            Assert.AreEqual(priceList[0].Type, SpecialType.Limit);
        }

        [Test]
        public void SaveAddsRestrictionSpecialToList()
        {
            IRepository<ISpecial> specialsRepository = new SpecialsRepository();

            specialsRepository.Save(restrictionSpecialLesser);

            var priceList = specialsRepository.GetAll();

            Assert.AreEqual(priceList.Count, 1);
            Assert.AreEqual(priceList[0].Type, SpecialType.Restriction);
        }

        [Test]
        public void GetSpecialsByProductNameWithExistingProductNameReturnsRightSpecial()
        {
            IRepository<ISpecial> specialsRepository = new SpecialsRepository();

            specialsRepository.Save(priceSpecial);

            var priceSpecialResult = (PriceSpecial)specialsRepository.GetByProductName("Can of soup");

            Assert.AreEqual(priceSpecialResult.ProductName, "Can of soup");
            Assert.AreEqual(priceSpecialResult.Price, 5);
        }

        [Test]
        public void GetSpecialsByProductNameWithNonExistentProductNameReturnsNull()
        {
            IRepository<ISpecial> specialsRepository = new SpecialsRepository();

            specialsRepository.Save(priceSpecial);

            var priceSpecialResult = (PriceSpecial)specialsRepository.GetByProductName("Can of beans");

            Assert.IsNull(priceSpecialResult);
        }

        [Test]
        public void UpdateWithExistingSpecialOfTheSameTypeReturnsTrue()
        {
            IRepository<ISpecial> specialsRepository = new SpecialsRepository();

            specialsRepository.Save(priceSpecial);
            bool updated = specialsRepository.Update(updatedPriceSpecial);

            Assert.IsTrue(updated);
        }
    }
}
