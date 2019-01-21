using NUnit.Framework;
using ProductService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductServiceTests
{
    public class SpecialsRepositoryTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void GetAllReturnsAListOfMarkdowns()
        {
            IRepository<ISpecial> specialsRepository = new SpecialsRepository();

            var specialsList = specialsRepository.GetAll();

            Assert.NotNull(specialsList);
        }
    }
}
