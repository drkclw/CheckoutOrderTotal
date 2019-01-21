using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.Models
{
    /// <summary>
    /// This class will accomodate specials with the format buy Buy N items get M at %X off and 
    /// Buy N items get M at %X off limit Y
    /// </summary>
    public class LimitSpecial : ISpecial
    {
        public LimitSpecial(string ProductName, int PurchaseQty, 
            bool IsActive, int DiscountQty, float DiscountAmount, int Limit)
        {
            this.ProductName = ProductName;
            this.PurchaseQty = PurchaseQty;
            this.Type = SpecialType.Limit;
            this.IsActive = IsActive;
            this.DiscountQty = DiscountQty;
            this.DiscountAmount = DiscountAmount;
            this.Limit = Limit;
        }

        public string ProductName { get; }

        public int PurchaseQty { get; }

        public SpecialType Type { get; }

        public bool IsActive { get; }

        public int DiscountQty { get; }

        public float DiscountAmount { get; set; }

        public int Limit { get; set; }

    }
}
