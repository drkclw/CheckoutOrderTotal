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

        [SetUp]
        public void Setup()
        {
            priceSpecial = new PriceSpecial("Can of soup", 2, SpecialType.Price, true, 5);
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
    }
}
