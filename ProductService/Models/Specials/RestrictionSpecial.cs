using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.Models.Specials
{
    /// <summary>
    /// This class will accomodate specials with the format Buy N, get M of equal or lesser value for %X off
    /// </summary>
    public class RestrictionSpecial : ISpecial
    {
        public RestrictionSpecial(string productName, int purchaseQty, 
            bool isActive, int discountQty, float discountAmount, RestrictionType restrictionType)
        {
            this.ProductName = productName;
            this.PurchaseQty = purchaseQty;
            this.Type = SpecialType.Restriction;
            this.IsActive = isActive;
            this.DiscountQty = discountQty;
            this.DiscountAmount = discountAmount;
            this.RestrictionType = restrictionType;
        }

        public string ProductName { get; }

        public int PurchaseQty { get; }

        public SpecialType Type { get; }

        public bool IsActive { get; }

        public int DiscountQty { get; }

        public float DiscountAmount { get; }

        public RestrictionType RestrictionType { get; }
    }
}
