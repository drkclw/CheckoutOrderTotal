using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ProductService.Controllers;
using ProductService.Models;
using System;
using System.Collections.Generic;
using System.Text;
using ProductService.Models.Specials;

namespace ProductServiceTests
{
    public class SpecialsControllerTests
    {        
        private List<ISpecial> specialsList;
        private ISpecial validPriceSpecial;
        private SpecialRequest validPriceSpecialRequest;
        private SpecialRequest zeroPriceSpecialRequest;
        private SpecialRequest lessThanTwoQuantityPriceSpecialRequest;
        private LimitSpecial validLimitSpecial;
        private SpecialRequest validLimitSpecialRequest;
        private SpecialRequest limitSpecialWithoutLimitRequest;
        private SpecialRequest limitSpecialWithoutDiscountAmountRequest;
        private SpecialRequest limitSpecialWithoutDiscountQuantityRequest;
        private SpecialRequest limitSpecialWithLimitLessThanPurchaseQtyRequest;
        private SpecialRequest limitSpecialWithLimitNotAMultipleOfPurchaseQtyPlusDiscountQtyRequest;
        private RestrictionSpecial validRestrictionSpecial;
        private SpecialRequest validRestrictionSpecialRequest;
        private SpecialRequest restrictionSpecialWithZeroDiscountAmountRequest;
        private SpecialRequest restrictionSpecialWithZeroDiscountQtyRequest;

        [SetUp]
        public void Setup()
        {
            validPriceSpecial = new PriceSpecial("Can of soup", 2, true, 5);
            validPriceSpecialRequest = new SpecialRequest
            {
                ProductName = "Can of soup",
                PurchaseQty = 2,
                IsActive = true,                
                Price = 5,
                Type = SpecialType.Price
            };

            zeroPriceSpecialRequest = new SpecialRequest
            {
                ProductName = "Can of soup",
                PurchaseQty = 2,
                IsActive = true,
                Price = 0,
                Type = SpecialType.Price
            };

            lessThanTwoQuantityPriceSpecialRequest = new SpecialRequest
            {
                ProductName = "Can of soup",
                PurchaseQty = 1,
                IsActive = true,
                Price = 5,
                Type = SpecialType.Price
            };

            validLimitSpecial = new LimitSpecial("Can of beans", 2, true, 1, 0.5f, 6);

            validLimitSpecialRequest = new SpecialRequest
            {
                ProductName = "Can of beans",
                PurchaseQty = 2,
                IsActive = true,
                DiscountQty = 1,
                DiscountAmount = 0.5f,
                Limit = 6,
                Type = SpecialType.Limit
            };

            limitSpecialWithoutLimitRequest = new SpecialRequest
            {
                ProductName = "Can of beans",
                PurchaseQty = 2,
                IsActive = true,
                DiscountQty = 1,
                DiscountAmount = 0.5f,
                Limit = 0,
                Type = SpecialType.Limit
            };

            limitSpecialWithoutDiscountAmountRequest = new SpecialRequest
            {
                ProductName = "Can of beans",
                PurchaseQty = 2,
                IsActive = true,
                DiscountQty = 1,
                DiscountAmount = 0,
                Limit = 4,
                Type = SpecialType.Limit
            };

            limitSpecialWithoutDiscountQuantityRequest = new SpecialRequest
            {
                ProductName = "Can of beans",
                PurchaseQty = 2,
                IsActive = true,
                DiscountQty = 0,
                DiscountAmount = 0.5f,
                Limit = 4,
                Type = SpecialType.Limit
            };

            limitSpecialWithLimitLessThanPurchaseQtyRequest = new SpecialRequest
            {
                ProductName = "Can of beans",
                PurchaseQty = 2,
                IsActive = true,
                DiscountQty = 1,
                DiscountAmount = 0.5f,
                Limit = 1,
                Type = SpecialType.Limit
            };

            limitSpecialWithLimitNotAMultipleOfPurchaseQtyPlusDiscountQtyRequest = new SpecialRequest
            {
                ProductName = "Can of beans",
                PurchaseQty = 2,
                IsActive = true,
                DiscountQty = 1,
                DiscountAmount = 0.5f,
                Limit = 5,
                Type = SpecialType.Limit
            };

            validRestrictionSpecial = new RestrictionSpecial("Bananas", 2, true, 1, 0.5f, RestrictionType.Lesser);

            validRestrictionSpecialRequest = new SpecialRequest
            {
                ProductName = "Bananas",
                PurchaseQty = 2,
                IsActive = true,
                DiscountQty = 1,
                DiscountAmount = 0.5f,
                RestrictionType = RestrictionType.Lesser,
                Type = SpecialType.Restriction
            };

            restrictionSpecialWithZeroDiscountAmountRequest = new SpecialRequest
            {
                ProductName = "Bananas",
                PurchaseQty = 2,
                IsActive = true,
                DiscountQty = 1,
                DiscountAmount = 0,
                RestrictionType = RestrictionType.Lesser,
                Type = SpecialType.Restriction
            };

            restrictionSpecialWithZeroDiscountQtyRequest = new SpecialRequest
            {
                ProductName = "Bananas",
                PurchaseQty = 2,
                IsActive = true,
                DiscountQty = 0,
                DiscountAmount = 0.5f,
                RestrictionType = RestrictionType.Lesser,
                Type = SpecialType.Restriction
            };            

            specialsList = new List<ISpecial>();
            specialsList.Add(validPriceSpecial);
            specialsList.Add(validLimitSpecial);
        }

        [Test]
        public void GetAllSpecialsReturnsListOfSpecials()
        {
            Mock<IDataAccessor<ISpecial>> mockSpecialsDataAccessor = new Mock<IDataAccessor<ISpecial>>();
            mockSpecialsDataAccessor.Setup(x => x.GetAll()).Returns(specialsList);
            
            SpecialsController specialsController = new SpecialsController(mockSpecialsDataAccessor.Object);
            var result = specialsController.GetAllSpecials();
            var contentResult = result as ActionResult<IEnumerable<ISpecial>>;

            Assert.NotNull(contentResult.Value);
        }

        [Test]
        public void AddingValidSpecialReturnsSuccess()
        {
            Mock<IDataAccessor<ISpecial>> mockSpecialsDataAccessor = new Mock<IDataAccessor<ISpecial>>();
            mockSpecialsDataAccessor.Setup(x => x.Save(It.IsAny<ISpecial>())).Returns("Success.");

            SpecialsController specialsController = new SpecialsController(mockSpecialsDataAccessor.Object);

            var result = specialsController.AddSpecial(validPriceSpecialRequest);
            var contentResult = result as ActionResult<string>;

            Assert.AreEqual(contentResult.Value, "Success.");
        }

        [Test]
        public void AddingValidPriceSpecialReturnsSuccess()
        {
            Mock<IDataAccessor<ISpecial>> mockSpecialsDataAccessor = new Mock<IDataAccessor<ISpecial>>();
            mockSpecialsDataAccessor.Setup(x => x.Save(
                It.Is<PriceSpecial>(s => s.Price > 0 && s.PurchaseQty >= 2)))
                .Returns("Success.");

            SpecialsController specialsController = new SpecialsController(mockSpecialsDataAccessor.Object);

            var result = specialsController.AddSpecial(validPriceSpecialRequest);
            var contentResult = result as ActionResult<string>;

            Assert.AreEqual(contentResult.Value, "Success.");
        }

        [Test]
        public void AddingPriceSpecialWithZeroPriceReturnsError()
        {
            Mock<IDataAccessor<ISpecial>> mockSpecialsDataAccessor = new Mock<IDataAccessor<ISpecial>>();
            mockSpecialsDataAccessor.Setup(x => x.Save(
                It.Is<PriceSpecial>(s => s.Price == 0)))
                .Returns("Error: Price must be bigger than 0.");

            SpecialsController specialsController = new SpecialsController(mockSpecialsDataAccessor.Object);

            var result = specialsController.AddSpecial(zeroPriceSpecialRequest);
            var contentResult = result as ActionResult<string>;

            Assert.AreEqual(contentResult.Value, "Error: Price must be bigger than 0.");
        }

        [Test]
        public void AddingPriceSpecialWithPurchaseQuantityLessThanTwoReturnsError()
        {
            Mock<IDataAccessor<ISpecial>> mockSpecialsDataAccessor = new Mock<IDataAccessor<ISpecial>>();
            mockSpecialsDataAccessor.Setup(x => x.Save(
                It.Is<PriceSpecial>(s => s.PurchaseQty < 2)))
                .Returns("Error: Purchase quantity must be bigger than 1.");

            SpecialsController specialsController = new SpecialsController(mockSpecialsDataAccessor.Object);

            var result = specialsController.AddSpecial(lessThanTwoQuantityPriceSpecialRequest);
            var contentResult = result as ActionResult<string>;

            Assert.AreEqual(contentResult.Value, "Error: Purchase quantity must be bigger than 1.");
        }

        [Test]
        public void AddingValidLimitSpecialReturnsSuccess()
        {
            Mock<IDataAccessor<ISpecial>> mockSpecialsDataAccessor = new Mock<IDataAccessor<ISpecial>>();
            mockSpecialsDataAccessor.Setup(x => x.Save(
                It.Is<LimitSpecial>(s => s.Type == SpecialType.Limit && s.Limit > 0 
                && s.DiscountAmount > 0 && s.Limit > s.PurchaseQty 
                && (s.Limit % (s.PurchaseQty + s.DiscountQty) == 0))))
                .Returns("Success.");

            SpecialsController specialsController = new SpecialsController(mockSpecialsDataAccessor.Object);

            var result = specialsController.AddSpecial(validLimitSpecialRequest);
            var contentResult = result as ActionResult<string>;

            Assert.AreEqual(contentResult.Value, "Success.");
        }

        [Test]
        public void AddingLimitSpecialWithoutLimitReturnsError()
        {
            Mock<IDataAccessor<ISpecial>> mockSpecialsDataAccessor = new Mock<IDataAccessor<ISpecial>>();
            mockSpecialsDataAccessor.Setup(x => x.Save(
                It.Is<LimitSpecial>(s => s.Limit == 0)))
                .Returns("Error: Limit must be bigger than 0.");

            SpecialsController specialsController = new SpecialsController(mockSpecialsDataAccessor.Object);

            var result = specialsController.AddSpecial(limitSpecialWithoutLimitRequest);
            var contentResult = result as ActionResult<string>;

            Assert.AreEqual(contentResult.Value, "Error: Limit must be bigger than 0.");
        }

        [Test]
        public void AddingLimitSpecialWithoutDiscountAmountReturnsError()
        {
            Mock<IDataAccessor<ISpecial>> mockSpecialsDataAccessor = new Mock<IDataAccessor<ISpecial>>();
            mockSpecialsDataAccessor.Setup(x => x.Save(
                It.Is<LimitSpecial>(s => s.DiscountAmount == 0)))
                .Returns("Error: Discount amount must be bigger than zero.");

            SpecialsController specialsController = new SpecialsController(mockSpecialsDataAccessor.Object);

            var result = specialsController.AddSpecial(limitSpecialWithoutDiscountAmountRequest);
            var contentResult = result as ActionResult<string>;

            Assert.AreEqual(contentResult.Value, "Error: Discount amount must be bigger than zero.");
        }

        [Test]
        public void AddingLimitSpecialWithoutDiscountQuantityReturnsError()
        {
            Mock<IDataAccessor<ISpecial>> mockSpecialsDataAccessor = new Mock<IDataAccessor<ISpecial>>();
            mockSpecialsDataAccessor.Setup(x => x.Save(
                It.Is<LimitSpecial>(s => s.DiscountQty == 0)))
                .Returns("Error: Discount quantity must be bigger than zero.");

            SpecialsController specialsController = new SpecialsController(mockSpecialsDataAccessor.Object);

            var result = specialsController.AddSpecial(limitSpecialWithoutDiscountQuantityRequest);
            var contentResult = result as ActionResult<string>;

            Assert.AreEqual(contentResult.Value, "Error: Discount quantity must be bigger than zero.");
        }

        [Test]
        public void AddingLimitSpecialWithSmallerLimitThanPurchaseQuantityReturnsError()
        {
            Mock<IDataAccessor<ISpecial>> mockSpecialsDataAccessor = new Mock<IDataAccessor<ISpecial>>();
            mockSpecialsDataAccessor.Setup(x => x.Save(
                It.Is<LimitSpecial>(s => s.Limit < s.PurchaseQty)))
                .Returns("Error: Limit must be bigger than purchase quantity.");

            SpecialsController specialsController = new SpecialsController(mockSpecialsDataAccessor.Object);

            var result = specialsController.AddSpecial(limitSpecialWithLimitLessThanPurchaseQtyRequest);
            var contentResult = result as ActionResult<string>;

            Assert.AreEqual(contentResult.Value, "Error: Limit must be bigger than purchase quantity.");
        }

        [Test]
        public void AddingLimitSpecialWithWithLimitNotAMultipleOfPurchaseQtyPlusDiscountQtyReturnsError()
        {
            Mock<IDataAccessor<ISpecial>> mockSpecialsDataAccessor = new Mock<IDataAccessor<ISpecial>>();
            mockSpecialsDataAccessor.Setup(x => x.Save(
                It.Is<LimitSpecial>(s => s.Limit % (s.PurchaseQty + s.DiscountQty) != 0)))
                .Returns("Error: Limit must be a multiple of purchase quantity plus discount quantity.");

            SpecialsController specialsController = new SpecialsController(mockSpecialsDataAccessor.Object);

            var result = specialsController.AddSpecial(limitSpecialWithLimitLessThanPurchaseQtyRequest);
            var contentResult = result as ActionResult<string>;

            Assert.AreEqual(contentResult.Value, 
                "Error: Limit must be a multiple of purchase quantity plus discount quantity.");
        }

        [Test]
        public void AddingValidRestrictionSpecialReturnsSuccess()
        {
            Mock<IDataAccessor<ISpecial>> mockSpecialsDataAccessor = new Mock<IDataAccessor<ISpecial>>();
            mockSpecialsDataAccessor.Setup(x => x.Save(
                It.Is<RestrictionSpecial>(s => s.DiscountAmount > 0 && s.DiscountQty > 0)))
                .Returns("Success.");

            SpecialsController specialsController = new SpecialsController(mockSpecialsDataAccessor.Object);

            var result = specialsController.AddSpecial(validRestrictionSpecialRequest);
            var contentResult = result as ActionResult<string>;

            Assert.AreEqual(contentResult.Value, "Success.");
        }

        [Test]
        public void AddingRestrictionSpecialWithZeroDiscountAmountReturnsError()
        {
            Mock<IDataAccessor<ISpecial>> mockSpecialsDataAccessor = new Mock<IDataAccessor<ISpecial>>();
            mockSpecialsDataAccessor.Setup(x => x.Save(
                It.Is<RestrictionSpecial>(s => s.DiscountAmount == 0)))
                .Returns("Error: Discount amount must be bigger than zero.");

            SpecialsController specialsController = new SpecialsController(mockSpecialsDataAccessor.Object);

            var result = specialsController.AddSpecial(restrictionSpecialWithZeroDiscountAmountRequest);
            var contentResult = result as ActionResult<string>;

            Assert.AreEqual(contentResult.Value, "Error: Discount amount must be bigger than zero.");
        }

        [Test]
        public void AddingRestrictionSpecialWithZeroDiscountQtyReturnsError()
        {
            Mock<IDataAccessor<ISpecial>> mockSpecialsDataAccessor = new Mock<IDataAccessor<ISpecial>>();
            mockSpecialsDataAccessor.Setup(x => x.Save(
                It.Is<RestrictionSpecial>(s => s.DiscountQty == 0)))
                .Returns("Error: Discount quantity must be bigger than zero.");

            SpecialsController specialsController = new SpecialsController(mockSpecialsDataAccessor.Object);

            var result = specialsController.AddSpecial(restrictionSpecialWithZeroDiscountQtyRequest);
            var contentResult = result as ActionResult<string>;


            Assert.AreEqual(contentResult.Value, "Error: Discount quantity must be bigger than zero.");
        }

        [Test]
        public void AddingValidSpecialForProductThatAlreadyHasSpecialReturnsError()
        {
            Mock<IDataAccessor<ISpecial>> mockSpecialsDataAccessor = new Mock<IDataAccessor<ISpecial>>();
            mockSpecialsDataAccessor.Setup(x => x.Save(
                It.Is<PriceSpecial>(s => s.Price > 0 && s.PurchaseQty >= 2)))
                .Returns("Error: A special already exists for this product, update special instead.");

            SpecialsController specialsController = new SpecialsController(mockSpecialsDataAccessor.Object);

            var result = specialsController.AddSpecial(validPriceSpecialRequest);
            var contentResult = result as ActionResult<string>;

            Assert.AreEqual(contentResult.Value, "Error: A special already exists for this product, update special instead.");
        }

        [Test]
        public void UpdateValidPriceSpecialReturnsSuccess()
        {
            Mock<IDataAccessor<ISpecial>> mockSpecialsDataAccessor = new Mock<IDataAccessor<ISpecial>>();
            mockSpecialsDataAccessor.Setup(x => x.Update(
                It.Is<PriceSpecial>(s => s.Price > 0 && s.PurchaseQty >= 2)))
                .Returns("Success.");

            SpecialsController specialsController = new SpecialsController(mockSpecialsDataAccessor.Object);

            var result = specialsController.UpdateSpecial(validPriceSpecialRequest);
            var contentResult = result as ActionResult<string>;

            Assert.AreEqual(contentResult.Value, "Success.");
        }

        [Test]
        public void UpdatePriceSpecialWithZeroPriceReturnsError()
        {
            Mock<IDataAccessor<ISpecial>> mockSpecialsDataAccessor = new Mock<IDataAccessor<ISpecial>>();
            mockSpecialsDataAccessor.Setup(x => x.Update(
                It.Is<PriceSpecial>(s => s.Price == 0)))
                .Returns("Error: Price must be bigger than 0.");

            SpecialsController specialsController = new SpecialsController(mockSpecialsDataAccessor.Object);

            var result = specialsController.UpdateSpecial(zeroPriceSpecialRequest);
            var contentResult = result as ActionResult<string>;

            Assert.AreEqual(contentResult.Value, "Error: Price must be bigger than 0.");
        }

        [Test]
        public void UpdatePriceSpecialWithPurchaseQuantityLessThanTwoReturnsError()
        {
            Mock<IDataAccessor<ISpecial>> mockSpecialsDataAccessor = new Mock<IDataAccessor<ISpecial>>();
            mockSpecialsDataAccessor.Setup(x => x.Update(
                It.Is<PriceSpecial>(s => s.PurchaseQty < 2)))
                .Returns("Error: Purchase quantity must be bigger than 1.");

            SpecialsController specialsController = new SpecialsController(mockSpecialsDataAccessor.Object);

            var result = specialsController.UpdateSpecial(lessThanTwoQuantityPriceSpecialRequest);
            var contentResult = result as ActionResult<string>;

            Assert.AreEqual(contentResult.Value, "Error: Purchase quantity must be bigger than 1.");
        }

        [Test]
        public void UpdateValidLimitSpecialReturnsSuccess()
        {
            Mock<IDataAccessor<ISpecial>> mockSpecialsDataAccessor = new Mock<IDataAccessor<ISpecial>>();
            mockSpecialsDataAccessor.Setup(x => x.Update(
                It.Is<LimitSpecial>(s => s.Type == SpecialType.Limit && s.Limit > 0
                && s.DiscountAmount > 0 && s.Limit > s.PurchaseQty
                && (s.Limit % (s.PurchaseQty + s.DiscountQty) == 0))))
                .Returns("Success.");

            SpecialsController specialsController = new SpecialsController(mockSpecialsDataAccessor.Object);

            var result = specialsController.UpdateSpecial(validLimitSpecialRequest);
            var contentResult = result as ActionResult<string>;

            Assert.AreEqual(contentResult.Value, "Success.");
        }

        [Test]
        public void UpdateLimitSpecialWithoutLimitReturnsError()
        {
            Mock<IDataAccessor<ISpecial>> mockSpecialsDataAccessor = new Mock<IDataAccessor<ISpecial>>();
            mockSpecialsDataAccessor.Setup(x => x.Save(
                It.Is<LimitSpecial>(s => s.Limit == 0)))
                .Returns("Error: Limit must be bigger than 0.");

            SpecialsController specialsController = new SpecialsController(mockSpecialsDataAccessor.Object);

            var result = specialsController.AddSpecial(limitSpecialWithoutLimitRequest);
            var contentResult = result as ActionResult<string>;

            Assert.AreEqual(contentResult.Value, "Error: Limit must be bigger than 0.");
        }

        [Test]
        public void UpdateLimitSpecialWithoutDiscountAmountReturnsError()
        {
            Mock<IDataAccessor<ISpecial>> mockSpecialsDataAccessor = new Mock<IDataAccessor<ISpecial>>();
            mockSpecialsDataAccessor.Setup(x => x.Update(
                It.Is<LimitSpecial>(s => s.DiscountAmount == 0)))
                .Returns("Error: Discount amount must be bigger than zero.");

            SpecialsController specialsController = new SpecialsController(mockSpecialsDataAccessor.Object);

            var result = specialsController.UpdateSpecial(limitSpecialWithoutDiscountAmountRequest);
            var contentResult = result as ActionResult<string>;

            Assert.AreEqual(contentResult.Value, "Error: Discount amount must be bigger than zero.");
        }

        [Test]
        public void UpdateLimitSpecialWithoutDiscountQuantityReturnsError()
        {
            Mock<IDataAccessor<ISpecial>> mockSpecialsDataAccessor = new Mock<IDataAccessor<ISpecial>>();
            mockSpecialsDataAccessor.Setup(x => x.Update(
                It.Is<LimitSpecial>(s => s.DiscountQty == 0)))
                .Returns("Error: Discount quantity must be bigger than zero.");

            SpecialsController specialsController = new SpecialsController(mockSpecialsDataAccessor.Object);

            var result = specialsController.UpdateSpecial(limitSpecialWithoutDiscountQuantityRequest);
            var contentResult = result as ActionResult<string>;

            Assert.AreEqual(contentResult.Value, "Error: Discount quantity must be bigger than zero.");
        }

        [Test]
        public void UpdateLimitSpecialWithSmallerLimitThanPurchaseQuantityReturnsError()
        {
            Mock<IDataAccessor<ISpecial>> mockSpecialsDataAccessor = new Mock<IDataAccessor<ISpecial>>();
            mockSpecialsDataAccessor.Setup(x => x.Update(
                It.Is<LimitSpecial>(s => s.Limit < s.PurchaseQty)))
                .Returns("Error: Limit must be bigger than purchase quantity.");

            SpecialsController specialsController = new SpecialsController(mockSpecialsDataAccessor.Object);

            var result = specialsController.UpdateSpecial(limitSpecialWithLimitLessThanPurchaseQtyRequest);
            var contentResult = result as ActionResult<string>;

            Assert.AreEqual(contentResult.Value, "Error: Limit must be bigger than purchase quantity.");
        }

        [Test]
        public void UpdateLimitSpecialWithWithLimitNotAMultipleOfPurchaseQtyPlusDiscountQtyReturnsError()
        {
            Mock<IDataAccessor<ISpecial>> mockSpecialsDataAccessor = new Mock<IDataAccessor<ISpecial>>();
            mockSpecialsDataAccessor.Setup(x => x.Update(
                It.Is<LimitSpecial>(s => s.Limit % (s.PurchaseQty + s.DiscountQty) != 0)))
                .Returns("Error: Limit must be a multiple of purchase quantity plus discount quantity.");

            SpecialsController specialsController = new SpecialsController(mockSpecialsDataAccessor.Object);

            var result = specialsController.UpdateSpecial(limitSpecialWithLimitLessThanPurchaseQtyRequest);
            var contentResult = result as ActionResult<string>;

            Assert.AreEqual(contentResult.Value,
                "Error: Limit must be a multiple of purchase quantity plus discount quantity.");
        }

        [Test]
        public void UpdateValidRestrictionSpecialReturnsSuccess()
        {
            Mock<IDataAccessor<ISpecial>> mockSpecialsDataAccessor = new Mock<IDataAccessor<ISpecial>>();
            mockSpecialsDataAccessor.Setup(x => x.Update(
                It.Is<RestrictionSpecial>(s => s.DiscountAmount > 0 && s.DiscountQty > 0)))
                .Returns("Success.");

            SpecialsController specialsController = new SpecialsController(mockSpecialsDataAccessor.Object);

            var result = specialsController.UpdateSpecial(validRestrictionSpecialRequest);
            var contentResult = result as ActionResult<string>;

            Assert.AreEqual(contentResult.Value, "Success.");
        }

        [Test]
        public void UpdateRestrictionSpecialWithZeroDiscountAmountReturnsError()
        {
            Mock<IDataAccessor<ISpecial>> mockSpecialsDataAccessor = new Mock<IDataAccessor<ISpecial>>();
            mockSpecialsDataAccessor.Setup(x => x.Update(
                It.Is<RestrictionSpecial>(s => s.DiscountAmount == 0)))
                .Returns("Error: Discount amount must be bigger than zero.");

            SpecialsController specialsController = new SpecialsController(mockSpecialsDataAccessor.Object);

            var result = specialsController.UpdateSpecial(restrictionSpecialWithZeroDiscountAmountRequest);
            var contentResult = result as ActionResult<string>;

            Assert.AreEqual(contentResult.Value, "Error: Discount amount must be bigger than zero.");
        }
    }
}
