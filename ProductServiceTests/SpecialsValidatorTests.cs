using NUnit.Framework;
using ProductService.Models;
using ProductService.Models.Specials;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductServiceTests
{
    public class SpecialsValidatorTests
    {
        private PriceSpecial validPriceSpecial;
        private PriceSpecial zeroPriceSpecial;

        [SetUp]
        public void Setup()
        {
            validPriceSpecial = new PriceSpecial("Can of soup", 2, true, 5);
            zeroPriceSpecial = new PriceSpecial("Can of soup", 2, true, 0);
        }

        [Test]
        public void ValidateValidPriceSpecialReturnsSuccess()
        {
            IValidator<ISpecial> specialsValidator = new SpecialsValidator();

            var result = specialsValidator.Validate(validPriceSpecial);

            Assert.AreEqual(result.IsValid, true);
            Assert.AreEqual(result.Message, "Success.");
        }

        [Test]
        public void ValidateValidPriceSpecialWithZeroPriceReturnsError()
        {
            IValidator<ISpecial> specialsValidator = new SpecialsValidator();

            var result = specialsValidator.Validate(zeroPriceSpecial);

            Assert.AreEqual(result.IsValid, false);
            Assert.AreEqual(result.Message, "Error: Price must be bigger than 0.");
        }
    }
}
