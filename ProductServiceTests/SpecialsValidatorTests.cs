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
        private PriceSpecial lessThanTwoQuantityPriceSpecial;
        private LimitSpecial validLimitSpecial;
        private LimitSpecial limitSpecialWithoutLimit;

        [SetUp]
        public void Setup()
        {
            validPriceSpecial = new PriceSpecial("Can of soup", 2, true, 5);
            zeroPriceSpecial = new PriceSpecial("Can of soup", 2, true, 0);
            lessThanTwoQuantityPriceSpecial = new PriceSpecial("Can of soup", 1, true, 5);
            validLimitSpecial = new LimitSpecial("Can of beans", 2, true, 1, 0.5f, 6);
            limitSpecialWithoutLimit = new LimitSpecial("Can of beans", 2, true, 1, 0.5f, 0);
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
        public void ValidatePriceSpecialWithZeroPriceReturnsError()
        {
            IValidator<ISpecial> specialsValidator = new SpecialsValidator();

            var result = specialsValidator.Validate(zeroPriceSpecial);

            Assert.AreEqual(result.IsValid, false);
            Assert.AreEqual(result.Message, "Error: Price must be bigger than 0.");
        }

        [Test]
        public void ValidatePriceSpecialWithPurchaseQuantityLessThanTwoReturnsError()
        {
            IValidator<ISpecial> specialsValidator = new SpecialsValidator();

            var result = specialsValidator.Validate(lessThanTwoQuantityPriceSpecial);

            Assert.AreEqual(result.IsValid, false);
            Assert.AreEqual(result.Message, "Error: Purchase quantity must be bigger than 1.");
        }

        [Test]
        public void ValidateValidLimitSpecialReturnsSuccess()
        {
            IValidator<ISpecial> specialsValidator = new SpecialsValidator();

            var result = specialsValidator.Validate(validLimitSpecial);

            Assert.AreEqual(result.IsValid, true);
            Assert.AreEqual(result.Message, "Success.");
        }

        [Test]
        public void ValidateLimitSpecialWithoutLimitReturnsError()
        {
            IValidator<ISpecial> specialsValidator = new SpecialsValidator();

            var result = specialsValidator.Validate(limitSpecialWithoutLimit);

            Assert.AreEqual(result.IsValid, false);
            Assert.AreEqual(result.Message, "Error: Limit must be bigger than 0.");
        }
    }
}
