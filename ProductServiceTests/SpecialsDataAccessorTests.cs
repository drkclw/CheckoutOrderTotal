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
        private LimitSpecial limitSpecialWithoutLimit;
        private LimitSpecial limitSpecialWithoutDiscountAmount;
        private LimitSpecial limitSpecialWithoutDiscountQuantity;
        private LimitSpecial limitSpecialWithLimitLessThanPurchaseQty;
        private LimitSpecial limitSpecialWithLimitNotAMultipleOfPurchaseQtyPlusDiscountQty;
        private RestrictionSpecial validRestrictionSpecial;
        private RestrictionSpecial restrictionSpecialWithZeroDiscountAmount;
        private RestrictionSpecial restrictionSpecialWithZeroDiscountQty;
        private PriceSpecial nonExistentPriceSpecial;
        private ValidationResponse successValidationResponse;
        private ValidationResponse zeroPriceValidationResponse;
        private ValidationResponse purchaseQuantityLessThanTwoValidationResponse;
        private ValidationResponse zeroLimitValidationResponse;
        private ValidationResponse zeroDiscountAmountValidationResponse;
        private ValidationResponse limitLessThanPurchaseQuantityValidationResponse;
        private ValidationResponse limitNotAMultipleOfPurchaseQtyPlusDiscountQtyValidationResponse;
        private ValidationResponse zeroDiscountQuantityValidationResponse;

        [SetUp]
        public void Setup()
        {
            validPriceSpecial = new PriceSpecial("Can of soup", 2, true, 5);
            validLimitSpecial = new LimitSpecial("Can of beans", 2, true, 1, 0.5f, 6);
            zeroPriceSpecial = new PriceSpecial("Can of soup", 2, true, 0);
            lessThanTwoQuantityPriceSpecial = new PriceSpecial("Can of soup", 1, true, 5);
            limitSpecialWithoutLimit = new LimitSpecial("Can of beans", 2, true, 1, 0.5f, 0);
            limitSpecialWithoutDiscountAmount = new LimitSpecial("Can of beans", 2, true, 1, 0, 4);
            limitSpecialWithoutDiscountQuantity = new LimitSpecial("Can of beans", 2, true, 0, 0.5f, 4);
            limitSpecialWithLimitLessThanPurchaseQty = new LimitSpecial("Can of beans", 2, true, 1, 0.5f, 1);
            limitSpecialWithLimitNotAMultipleOfPurchaseQtyPlusDiscountQty = new LimitSpecial("Can of beans", 2, true, 1, 0.5f, 5);
            validRestrictionSpecial = new RestrictionSpecial("Bananas", 2, true, 1, 0.5f, RestrictionType.Lesser);
            restrictionSpecialWithZeroDiscountAmount = new RestrictionSpecial("Bananas", 2, true, 1, 0, RestrictionType.Lesser);
            restrictionSpecialWithZeroDiscountQty = new RestrictionSpecial("Bananas", 2, true, 0, 0.5f, RestrictionType.Lesser);
            nonExistentPriceSpecial = new PriceSpecial("Milk", 2, true, 5);
            successValidationResponse = new ValidationResponse
            {
                IsValid = true,
                Message = "Success."
            };

            zeroPriceValidationResponse = new ValidationResponse
            {
                IsValid = false,
                Message = "Error: Price must be bigger than 0."
            };

            purchaseQuantityLessThanTwoValidationResponse = new ValidationResponse
            {
                IsValid = false,
                Message = "Error: Purchase quantity must be bigger than 1."
            };

            zeroLimitValidationResponse = new ValidationResponse
            {
                IsValid = false,
                Message = "Error: Limit must be bigger than 0."
            };

            zeroDiscountAmountValidationResponse = new ValidationResponse
            {
                IsValid = false,
                Message = "Error: Discount amount must be bigger than zero."
            };

            limitLessThanPurchaseQuantityValidationResponse = new ValidationResponse
            {
                IsValid = false,
                Message = "Error: Limit must be bigger than purchase quantity."
            };

            limitNotAMultipleOfPurchaseQtyPlusDiscountQtyValidationResponse = new ValidationResponse
            {
                IsValid = false,
                Message = "Error: Limit must be a multiple of purchase quantity plus discount quantity."
            };

            zeroDiscountQuantityValidationResponse = new ValidationResponse
            {
                IsValid = false,
                Message = "Error: Discount quantity must be bigger than zero."
            };

            specialsList = new List<ISpecial>();
            specialsList.Add(validPriceSpecial);
            specialsList.Add(validLimitSpecial);
        }

        [Test]
        public void GetAllSpecialsReturnsListOfSpecials()
        {
            Mock<IRepository<ISpecial>> mockSpecialsRepository = new Mock<IRepository<ISpecial>>();
            mockSpecialsRepository.Setup(x => x.GetAll()).Returns(specialsList);

            Mock<IValidator<ISpecial>> mockSpecialsValidator = new Mock<IValidator<ISpecial>>();

            SpecialsDataAccessor specialsDataAccessor = new SpecialsDataAccessor(mockSpecialsRepository.Object,
                mockSpecialsValidator.Object);
            var specials = specialsDataAccessor.GetAll();

            Assert.NotNull(specials);
        }

        [Test]
        public void GetByProductNameWithExistingSpecialReturnsSpecial()
        {
            Mock<IRepository<ISpecial>> mockSpecialsRepository = new Mock<IRepository<ISpecial>>();
            mockSpecialsRepository.Setup(x => x.GetByProductName("Can of soup")).Returns(validPriceSpecial);

            Mock<IValidator<ISpecial>> mockSpecialsValidator = new Mock<IValidator<ISpecial>>();

            SpecialsDataAccessor specialsDataAccessor = new SpecialsDataAccessor(mockSpecialsRepository.Object,
                mockSpecialsValidator.Object);
            var special = specialsDataAccessor.GetByProductName("Can of soup");

            Assert.NotNull(special);
        }

        [Test]
        public void GetByProductNameWithNonExistentSpecialReturnsNull()
        {
            Mock<IRepository<ISpecial>> mockSpecialsRepository = new Mock<IRepository<ISpecial>>();
            mockSpecialsRepository.Setup(x => x.GetByProductName("Bananas")).Returns((ISpecial)null);

            Mock<IValidator<ISpecial>> mockSpecialsValidator = new Mock<IValidator<ISpecial>>();

            SpecialsDataAccessor specialsDataAccessor = new SpecialsDataAccessor(mockSpecialsRepository.Object,
                mockSpecialsValidator.Object);
            var special = specialsDataAccessor.GetByProductName("Bananas");

            Assert.IsNull(special);
        }

        [Test]
        public void AddingValidSpecialReturnsSuccess()
        {
            Mock<IRepository<ISpecial>> mockSpecialsRepository = new Mock<IRepository<ISpecial>>();
            mockSpecialsRepository.Setup(x => x.Save(validPriceSpecial));

            Mock<IValidator<ISpecial>> mockSpecialsValidator = new Mock<IValidator<ISpecial>>();
            mockSpecialsValidator.Setup(x => x.Validate(validPriceSpecial)).Returns(successValidationResponse);

            SpecialsDataAccessor specialsDataAccessor = new SpecialsDataAccessor(mockSpecialsRepository.Object,
                mockSpecialsValidator.Object);
            var result = specialsDataAccessor.Save(validPriceSpecial);

            Assert.AreEqual(result, "Success.");
        }

        [Test]
        public void AddingValidPriceSpecialReturnsSuccess()
        {
            Mock<IRepository<ISpecial>> mockSpecialsRepository = new Mock<IRepository<ISpecial>>();
            mockSpecialsRepository.Setup(x => x.Save(validPriceSpecial));

            Mock<IValidator<ISpecial>> mockSpecialsValidator = new Mock<IValidator<ISpecial>>();
            mockSpecialsValidator.Setup(x => x.Validate(validPriceSpecial)).Returns(successValidationResponse);

            SpecialsDataAccessor specialsDataAccessor = new SpecialsDataAccessor(mockSpecialsRepository.Object,
                mockSpecialsValidator.Object);
            var result = specialsDataAccessor.Save(validPriceSpecial);

            Assert.AreEqual(result, "Success.");
        }

        [Test]
        public void AddingPriceSpecialWithZeroPriceReturnsError()
        {
            Mock<IRepository<ISpecial>> mockSpecialsRepository = new Mock<IRepository<ISpecial>>();

            Mock<IValidator<ISpecial>> mockSpecialsValidator = new Mock<IValidator<ISpecial>>();
            mockSpecialsValidator.Setup(x => x.Validate(zeroPriceSpecial)).Returns(zeroPriceValidationResponse);

            SpecialsDataAccessor specialsDataAccessor = new SpecialsDataAccessor(mockSpecialsRepository.Object,
                mockSpecialsValidator.Object);
            var result = specialsDataAccessor.Save(zeroPriceSpecial);

            Assert.AreEqual(result, "Error: Price must be bigger than 0.");
        }

        [Test]
        public void AddingPriceSpecialWithPurchaseQuantityLessThanTwoReturnsError()
        {
            Mock<IRepository<ISpecial>> mockSpecialsRepository = new Mock<IRepository<ISpecial>>();

            Mock<IValidator<ISpecial>> mockSpecialsValidator = new Mock<IValidator<ISpecial>>();
            mockSpecialsValidator.Setup(x => x.Validate(lessThanTwoQuantityPriceSpecial)).Returns(purchaseQuantityLessThanTwoValidationResponse);

            SpecialsDataAccessor specialsDataAccessor = new SpecialsDataAccessor(mockSpecialsRepository.Object,
                mockSpecialsValidator.Object);
            var result = specialsDataAccessor.Save(lessThanTwoQuantityPriceSpecial);

            Assert.AreEqual(result, "Error: Purchase quantity must be bigger than 1.");
        }

        [Test]
        public void AddingValidLimitSpecialReturnsSuccess()
        {
            Mock<IRepository<ISpecial>> mockSpecialsRepository = new Mock<IRepository<ISpecial>>();

            Mock<IValidator<ISpecial>> mockSpecialsValidator = new Mock<IValidator<ISpecial>>();
            mockSpecialsValidator.Setup(x => x.Validate(validLimitSpecial)).Returns(successValidationResponse);

            SpecialsDataAccessor specialsDataAccessor = new SpecialsDataAccessor(mockSpecialsRepository.Object,
                mockSpecialsValidator.Object);
            var result = specialsDataAccessor.Save(validLimitSpecial);

            Assert.AreEqual(result, "Success.");
        }

        [Test]
        public void AddingLimitSpecialWithoutLimitReturnsError()
        {
            Mock<IRepository<ISpecial>> mockSpecialsRepository = new Mock<IRepository<ISpecial>>();

            Mock<IValidator<ISpecial>> mockSpecialsValidator = new Mock<IValidator<ISpecial>>();
            mockSpecialsValidator.Setup(x => x.Validate(limitSpecialWithoutLimit)).Returns(zeroLimitValidationResponse);

            SpecialsDataAccessor specialsDataAccessor = new SpecialsDataAccessor(mockSpecialsRepository.Object,
                mockSpecialsValidator.Object);
            var result = specialsDataAccessor.Save(limitSpecialWithoutLimit);

            Assert.AreEqual(result, "Error: Limit must be bigger than 0.");
        }

        [Test]
        public void AddingLimitSpecialWithoutDiscountAmountReturnsError()
        {
            Mock<IRepository<ISpecial>> mockSpecialsRepository = new Mock<IRepository<ISpecial>>();

            Mock<IValidator<ISpecial>> mockSpecialsValidator = new Mock<IValidator<ISpecial>>();
            mockSpecialsValidator.Setup(x => x.Validate(limitSpecialWithoutDiscountAmount))
                .Returns(zeroDiscountAmountValidationResponse);

            SpecialsDataAccessor specialsDataAccessor = new SpecialsDataAccessor(mockSpecialsRepository.Object,
                mockSpecialsValidator.Object);
            var result = specialsDataAccessor.Save(limitSpecialWithoutDiscountAmount);

            Assert.AreEqual(result, "Error: Discount amount must be bigger than zero.");
        }

        [Test]
        public void AddingLimitSpecialWithoutDiscountQuantityReturnsError()
        {
            Mock<IRepository<ISpecial>> mockSpecialsRepository = new Mock<IRepository<ISpecial>>();

            Mock<IValidator<ISpecial>> mockSpecialsValidator = new Mock<IValidator<ISpecial>>();
            mockSpecialsValidator.Setup(x => x.Validate(limitSpecialWithoutDiscountQuantity))
                .Returns(zeroDiscountQuantityValidationResponse);

            SpecialsDataAccessor specialsDataAccessor = new SpecialsDataAccessor(mockSpecialsRepository.Object,
                mockSpecialsValidator.Object);
            var result = specialsDataAccessor.Save(limitSpecialWithoutDiscountQuantity);

            Assert.AreEqual(result, "Error: Discount quantity must be bigger than zero.");
        }

        [Test]
        public void AddingLimitSpecialWithSmallerLimitThanPurchaseQuantityReturnsError()
        {
            Mock<IRepository<ISpecial>> mockSpecialsRepository = new Mock<IRepository<ISpecial>>();

            Mock<IValidator<ISpecial>> mockSpecialsValidator = new Mock<IValidator<ISpecial>>();
            mockSpecialsValidator.Setup(x => x.Validate(limitSpecialWithLimitLessThanPurchaseQty))
                .Returns(limitLessThanPurchaseQuantityValidationResponse);

            SpecialsDataAccessor specialsDataAccessor = new SpecialsDataAccessor(mockSpecialsRepository.Object,
                mockSpecialsValidator.Object);
            var result = specialsDataAccessor.Save(limitSpecialWithLimitLessThanPurchaseQty);

            Assert.AreEqual(result, "Error: Limit must be bigger than purchase quantity.");
        }

        [Test]
        public void AddingLimitSpecialWithLimitNotAMultipleOfPurchaseQtyPlusDiscountQtyReturnsError()
        {
            Mock<IRepository<ISpecial>> mockSpecialsRepository = new Mock<IRepository<ISpecial>>();

            Mock<IValidator<ISpecial>> mockSpecialsValidator = new Mock<IValidator<ISpecial>>();
            mockSpecialsValidator.Setup(x => x.Validate(limitSpecialWithLimitNotAMultipleOfPurchaseQtyPlusDiscountQty))
                .Returns(limitNotAMultipleOfPurchaseQtyPlusDiscountQtyValidationResponse);
            
            SpecialsDataAccessor specialsDataAccessor = new SpecialsDataAccessor(mockSpecialsRepository.Object,
                mockSpecialsValidator.Object);
            var result = specialsDataAccessor.Save(limitSpecialWithLimitNotAMultipleOfPurchaseQtyPlusDiscountQty);

            Assert.AreEqual(result, "Error: Limit must be a multiple of purchase quantity plus discount quantity.");
        }

        [Test]
        public void AddingValidRestrictionSpecialReturnsSuccess()
        {
            Mock<IRepository<ISpecial>> mockSpecialsRepository = new Mock<IRepository<ISpecial>>();
            mockSpecialsRepository.Setup(x => x.Save(validRestrictionSpecial));

            Mock<IValidator<ISpecial>> mockSpecialsValidator = new Mock<IValidator<ISpecial>>();
            mockSpecialsValidator.Setup(x => x.Validate(validRestrictionSpecial)).Returns(successValidationResponse);

            SpecialsDataAccessor specialsDataAccessor = new SpecialsDataAccessor(mockSpecialsRepository.Object,
                mockSpecialsValidator.Object);
            var result = specialsDataAccessor.Save(validRestrictionSpecial);

            Assert.AreEqual(result, "Success.");
        }

        [Test]
        public void AddingRestrictionSpecialWithZeroDiscountAmountReturnsError()
        {
            Mock<IRepository<ISpecial>> mockSpecialsRepository = new Mock<IRepository<ISpecial>>();

            Mock<IValidator<ISpecial>> mockSpecialsValidator = new Mock<IValidator<ISpecial>>();
            mockSpecialsValidator.Setup(x => x.Validate(restrictionSpecialWithZeroDiscountAmount)).Returns(zeroDiscountAmountValidationResponse);

            SpecialsDataAccessor specialsDataAccessor = new SpecialsDataAccessor(mockSpecialsRepository.Object,
                mockSpecialsValidator.Object);
            var result = specialsDataAccessor.Save(restrictionSpecialWithZeroDiscountAmount);

            Assert.AreEqual(result, "Error: Discount amount must be bigger than zero.");
        }

        [Test]
        public void AddingRestrictionSpecialWithZeroDiscountQtyReturnsError()
        {
            Mock<IRepository<ISpecial>> mockSpecialsRepository = new Mock<IRepository<ISpecial>>();

            Mock<IValidator<ISpecial>> mockSpecialsValidator = new Mock<IValidator<ISpecial>>();
            mockSpecialsValidator.Setup(x => x.Validate(restrictionSpecialWithZeroDiscountQty)).Returns(zeroDiscountQuantityValidationResponse);

            SpecialsDataAccessor specialsDataAccessor = new SpecialsDataAccessor(mockSpecialsRepository.Object,
                mockSpecialsValidator.Object);
            var result = specialsDataAccessor.Save(restrictionSpecialWithZeroDiscountQty);

            Assert.AreEqual(result, "Error: Discount quantity must be bigger than zero.");
        }

        [Test]
        public void UpdateExistingValidSpecialReturnsSuccess()
        {
            Mock<IRepository<ISpecial>> mockSpecialsRepository = new Mock<IRepository<ISpecial>>();
            mockSpecialsRepository.Setup(x => x.GetByProductName("Can of soup")).Returns(validPriceSpecial);
            mockSpecialsRepository.Setup(x => x.Update(validPriceSpecial)).Returns(true);

            Mock<IValidator<ISpecial>> mockSpecialsValidator = new Mock<IValidator<ISpecial>>();
            mockSpecialsValidator.Setup(x => x.Validate(validPriceSpecial))
                .Returns(successValidationResponse);

            SpecialsDataAccessor specialsDataAccessor = new SpecialsDataAccessor(mockSpecialsRepository.Object,
                mockSpecialsValidator.Object);
            var result = specialsDataAccessor.Update(validPriceSpecial);

            Assert.AreEqual(result, "Success.");
        }

        [Test]
        public void UpdateNonExistentSpecialReturnsError()
        {
            Mock<IRepository<ISpecial>> mockSpecialsRepository = new Mock<IRepository<ISpecial>>();
            mockSpecialsRepository.Setup(x => x.GetByProductName("Milk")).Returns((ISpecial)null);

            Mock<IValidator<ISpecial>> mockSpecialsValidator = new Mock<IValidator<ISpecial>>();

            SpecialsDataAccessor specialsDataAccessor = new SpecialsDataAccessor(mockSpecialsRepository.Object,
                mockSpecialsValidator.Object);
            var result = specialsDataAccessor.Update(nonExistentPriceSpecial);

            Assert.AreEqual(result, "Error: Special does not exist, please create special before updating.");
        }

        [Test]
        public void UpdatePriceSpecialWithPriceSpecialWithZeroPriceReturnsError()
        {
            Mock<IRepository<ISpecial>> mockSpecialsRepository = new Mock<IRepository<ISpecial>>();
            mockSpecialsRepository.Setup(x => x.GetByProductName("Can of soup")).Returns(validPriceSpecial);

            Mock<IValidator<ISpecial>> mockSpecialsValidator = new Mock<IValidator<ISpecial>>();
            mockSpecialsValidator.Setup(x => x.Validate(zeroPriceSpecial)).Returns(zeroPriceValidationResponse);

            SpecialsDataAccessor specialsDataAccessor = new SpecialsDataAccessor(mockSpecialsRepository.Object,
                mockSpecialsValidator.Object);
            var result = specialsDataAccessor.Update(zeroPriceSpecial);

            Assert.AreEqual(result, "Error: Price must be bigger than 0.");
        }

        [Test]
        public void UpdatePriceSpecialWithPriceSpecialWithPurchaseQuantityLessThanTwoReturnsError()
        {
            Mock<IRepository<ISpecial>> mockSpecialsRepository = new Mock<IRepository<ISpecial>>();
            mockSpecialsRepository.Setup(x => x.GetByProductName("Can of soup")).Returns(validPriceSpecial);

            Mock<IValidator<ISpecial>> mockSpecialsValidator = new Mock<IValidator<ISpecial>>();
            mockSpecialsValidator.Setup(x => x.Validate(lessThanTwoQuantityPriceSpecial)).Returns(purchaseQuantityLessThanTwoValidationResponse);

            SpecialsDataAccessor specialsDataAccessor = new SpecialsDataAccessor(mockSpecialsRepository.Object,
                mockSpecialsValidator.Object);
            var result = specialsDataAccessor.Update(lessThanTwoQuantityPriceSpecial);

            Assert.AreEqual(result, "Error: Purchase quantity must be bigger than 1.");
        }

        [Test]
        public void UpdateValidLimitSpecialReturnsSuccess()
        {
            Mock<IRepository<ISpecial>> mockSpecialsRepository = new Mock<IRepository<ISpecial>>();
            mockSpecialsRepository.Setup(x => x.GetByProductName("Can of beans")).Returns(validLimitSpecial);

            Mock<IValidator<ISpecial>> mockSpecialsValidator = new Mock<IValidator<ISpecial>>();
            mockSpecialsValidator.Setup(x => x.Validate(validLimitSpecial)).Returns(successValidationResponse);

            SpecialsDataAccessor specialsDataAccessor = new SpecialsDataAccessor(mockSpecialsRepository.Object,
                mockSpecialsValidator.Object);
            var result = specialsDataAccessor.Update(validLimitSpecial);

            Assert.AreEqual(result, "Success.");
        }

        [Test]
        public void UpdateLimitSpecialWithLimitSpecialWithoutLimitReturnsError()
        {
            Mock<IRepository<ISpecial>> mockSpecialsRepository = new Mock<IRepository<ISpecial>>();
            mockSpecialsRepository.Setup(x => x.GetByProductName("Can of beans")).Returns(validLimitSpecial);

            Mock<IValidator<ISpecial>> mockSpecialsValidator = new Mock<IValidator<ISpecial>>();
            mockSpecialsValidator.Setup(x => x.Validate(limitSpecialWithoutLimit)).Returns(zeroLimitValidationResponse);

            SpecialsDataAccessor specialsDataAccessor = new SpecialsDataAccessor(mockSpecialsRepository.Object,
                mockSpecialsValidator.Object);
            var result = specialsDataAccessor.Save(limitSpecialWithoutLimit);

            Assert.AreEqual(result, "Error: Limit must be bigger than 0.");
        }

        [Test]
        public void UpdateLimitSpecialWithLimitSpecialWithoutDiscountAmountReturnsError()
        {
            Mock<IRepository<ISpecial>> mockSpecialsRepository = new Mock<IRepository<ISpecial>>();
            mockSpecialsRepository.Setup(x => x.GetByProductName("Can of beans")).Returns(validLimitSpecial);

            Mock<IValidator<ISpecial>> mockSpecialsValidator = new Mock<IValidator<ISpecial>>();
            mockSpecialsValidator.Setup(x => x.Validate(limitSpecialWithoutDiscountAmount))
                .Returns(zeroDiscountAmountValidationResponse);

            SpecialsDataAccessor specialsDataAccessor = new SpecialsDataAccessor(mockSpecialsRepository.Object,
                mockSpecialsValidator.Object);
            var result = specialsDataAccessor.Save(limitSpecialWithoutDiscountAmount);

            Assert.AreEqual(result, "Error: Discount amount must be bigger than zero.");
        }

        [Test]
        public void UpdateLimitSpecialWithLimitSpecialWithoutDiscountQuantityReturnsError()
        {
            Mock<IRepository<ISpecial>> mockSpecialsRepository = new Mock<IRepository<ISpecial>>();
            mockSpecialsRepository.Setup(x => x.GetByProductName("Can of beans")).Returns(validLimitSpecial);

            Mock<IValidator<ISpecial>> mockSpecialsValidator = new Mock<IValidator<ISpecial>>();
            mockSpecialsValidator.Setup(x => x.Validate(limitSpecialWithoutDiscountQuantity))
                .Returns(zeroDiscountQuantityValidationResponse);

            SpecialsDataAccessor specialsDataAccessor = new SpecialsDataAccessor(mockSpecialsRepository.Object,
                mockSpecialsValidator.Object);
            var result = specialsDataAccessor.Save(limitSpecialWithoutDiscountQuantity);

            Assert.AreEqual(result, "Error: Discount quantity must be bigger than zero.");
        }
    }
}
