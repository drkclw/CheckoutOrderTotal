using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.Models.Specials
{
    /// <summary>
    /// This class will accomodate specials with the format buy N for $X.
    /// </summary>
    public class PriceSpecial : ISpecial
    {
        public PriceSpecial(string ProductName, int PurchaseQty,
            bool IsActive, float Price)
        {
            this.ProductName = ProductName;
            this.PurchaseQty = PurchaseQty;
            this.Type = SpecialType.Price;
            this.IsActive = IsActive;
            this.Price = Price;
        }

        public string ProductName { get; }        

        public int PurchaseQty { get; }

        public SpecialType Type { get; }

        public bool IsActive { get; }

        public float Price { get; }
    }
}
