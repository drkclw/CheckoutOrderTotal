using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.Models.Specials
{
    public class SpecialsValidator : IValidator<ISpecial>
    {
        public ValidationResponse Validate(ISpecial validateThis)
        {
            if (validateThis.Type == SpecialType.Price)
            {
                var priceSpecial = (PriceSpecial)validateThis;

                if (priceSpecial.Price > 0)
                {
                    if (priceSpecial.PurchaseQty >= 2)
                    {
                        return new ValidationResponse
                        {
                            IsValid = true,
                            Message = "Success."
                        };
                    }
                    else
                    {
                        return new ValidationResponse
                        {
                            IsValid = false,
                            Message = "Error: Purchase quantity must be bigger than 1."
                        };
                    }
                }
                else
                {
                    return new ValidationResponse
                    {
                        IsValid = false,
                        Message = "Error: Price must be bigger than 0."
                    };
                }
            }
            else if (validateThis.Type == SpecialType.Limit)
            {
                var limitSpecial = (LimitSpecial)validateThis;

                if (limitSpecial.Limit == 0)
                {
                    return new ValidationResponse
                    {
                        IsValid = false,
                        Message = "Error: Limit must be bigger than 0."
                    };
                }

                if (limitSpecial.DiscountAmount == 0)
                {
                    return new ValidationResponse
                    {
                        IsValid = false,
                        Message = "Error: Discount amount must be bigger than 0."
                    };
                }

                if (limitSpecial.PurchaseQty > limitSpecial.Limit)
                {
                    return new ValidationResponse
                    {
                        IsValid = false,
                        Message = "Error: Limit must be bigger than purchase quantity."
                    };
                }

                if (limitSpecial.Limit % (limitSpecial.PurchaseQty + limitSpecial.DiscountQty) > 0)
                {
                    return new ValidationResponse
                    {
                        IsValid = false,
                        Message = "Error: Limit must be a multiple of purchase quantity plus discount quantity."
                    };
                }

                return new ValidationResponse
                {
                    IsValid = true,
                    Message = "Success."
                };
            }
            else if (validateThis.Type == SpecialType.Restriction)
            {
                var restrictionSpecial = (RestrictionSpecial)validateThis;

                if (restrictionSpecial.DiscountAmount == 0)
                {
                    return new ValidationResponse
                    {
                        IsValid = false,
                        Message = "Error: Discount amount must be bigger than zero."
                    };
                }

                if (restrictionSpecial.DiscountQty == 0)
                {
                    return new ValidationResponse
                    {
                        IsValid = false,
                        Message = "Error: Discount quantity must be bigger than zero."
                    };
                }

                return new ValidationResponse
                {
                    IsValid = true,
                    Message = "Success."
                };
            }

            return new ValidationResponse
            {
                IsValid = false,
                Message = "Special type not found."
            };
        }
    }
}
