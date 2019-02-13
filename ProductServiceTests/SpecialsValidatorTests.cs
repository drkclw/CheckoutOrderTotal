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
        private LimitSpecial limitSpecialWithoutDiscountAmount;
        private LimitSpecial limitSpecialWithoutDiscountQuantity;
        private LimitSpecial limitSpecialWithLimitLessThanPurchaseQty;
        private LimitSpecial limitSpecialWithLimitNotAMultipleOfPurchaseQtyPlusDiscountQty;
        private RestrictionSpecial validRestrictionSpecial;
        private RestrictionSpecial restrictionSpecialWithZeroDiscountAmount;
        private RestrictionSpecial restrictionSpecialWithZeroDiscountQuantity;

        [SetUp]
        public void Setup()
        {
            validPriceSpecial = new PriceSpecial("Can of soup", 2, true, 5);
            zeroPriceSpecial = new PriceSpecial("Can of soup", 2, true, 0);
            lessThanTwoQuantityPriceSpecial = new PriceSpecial("Can of soup", 1, true, 5);
            validLimitSpecial = new LimitSpecial("Can of beans", 2, true, 1, 0.5f, 6);
            limitSpecialWithoutLimit = new LimitSpecial("Can of beans", 2, true, 1, 0.5f, 0);
            limitSpecialWithoutDiscountAmount = new LimitSpecial("Can of beans", 2, true, 1, 0, 4);
            limitSpecialWithoutDiscountQuantity = new LimitSpecial("Can of beans", 2, true, 0, 0.5f, 4);
            limitSpecialWithLimitLessThanPurchaseQty = new LimitSpecial("Can of beans", 2, true, 1, 0.5f, 1);
            limitSpecialWithLimitNotAMultipleOfPurchaseQtyPlusDiscountQty = new LimitSpecial("Can of beans", 2, true, 1, 0.5f, 5);
            validRestrictionSpecial = new RestrictionSpecial("Bananas", 2, true, 1, 0.5f, RestrictionType.Lesser);
            restrictionSpecialWithZeroDiscountAmount = new RestrictionSpecial("Bananas", 2, true, 1, 0, RestrictionType.Lesser);
            restrictionSpecialWithZeroDiscountQuantity = new RestrictionSpecial("Bananas", 2, true, 0, 0.5f, RestrictionType.Lesser);
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

        [Test]
        public void ValidateLimitSpecialWithoutDiscountAmountReturnsError()
        {
            IValidator<ISpecial> specialsValidator = new SpecialsValidator();

            var result = specialsValidator.Validate(limitSpecialWithoutDiscountAmount);

            Assert.AreEqual(result.IsValid, false);
            Assert.AreEqual(result.Message, "Error: Discount amount must be bigger than zero.");
        }

        [Test]
        public void ValidateLimitSpecialWithoutDiscountQuantityReturnsError()
        {
            IValidator<ISpecial> specialsValidator = new SpecialsValidator();

            var result = specialsValidator.Validate(limitSpecialWithoutDiscountQuantity);

            Assert.AreEqual(result.IsValid, false);
            Assert.AreEqual(result.Message, "Error: Discount quantity must be bigger than zero.");
        }

        [Test]
        public void ValidateLimitSpecialWithSmallerLimitThanPurchaseQuantityReturnsError()
        {
            IValidator<ISpecial> specialsValidator = new SpecialsValidator();

            var result = specialsValidator.Validate(limitSpecialWithLimitLessThanPurchaseQty);

            Assert.AreEqual(result.IsValid, false);
            Assert.AreEqual(result.Message, "Error: Limit must be bigger than purchase quantity.");
        }

        [Test]
        public void ValidateLimitSpecialWithLimitNotAMultipleOfPurchaseQtyPlusDiscountQtyReturnsError()
        {
            IValidator<ISpecial> specialsValidator = new SpecialsValidator();

            var result = specialsValidator.Validate(limitSpecialWithLimitNotAMultipleOfPurchaseQtyPlusDiscountQty);

            Assert.AreEqual(result.IsValid, false);
            Assert.AreEqual(result.Message, "Error: Limit must be a multiple of purchase quantity plus discount quantity.");
        }

        [Test]
        public void ValidateValidRestrictionSpecialReturnsSuccess()
        {
            IValidator<ISpecial> specialsValidator = new SpecialsValidator();

            var result = specialsValidator.Validate(validRestrictionSpecial);

            Assert.AreEqual(result.IsValid, true);
            Assert.AreEqual(result.Message, "Success.");
        }

        [Test]
        public void ValidateRestrictionSpecialWithZeroDiscountAmountReturnsError()
        {
            IValidator<ISpecial> specialsValidator = new SpecialsValidator();

            var result = specialsValidator.Validate(restrictionSpecialWithZeroDiscountAmount);

            Assert.AreEqual(result.IsValid, false);
            Assert.AreEqual(result.Message, "Error: Discount amount must be bigger than zero.");
        }

        [Test]
        public void ValidateRestrictionSpecialWithZeroDiscountQuantityReturnsError()
        {
            IValidator<ISpecial> specialsValidator = new SpecialsValidator();

            var result = specialsValidator.Validate(restrictionSpecialWithZeroDiscountQuantity);

            Assert.AreEqual(result.IsValid, false);
            Assert.AreEqual(result.Message, "Error: Discount quantity must be bigger than zero.");
        }
    }
}
