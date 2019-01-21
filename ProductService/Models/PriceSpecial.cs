using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.Models
{
    public class PriceSpecial : ISpecial
    {
        public PriceSpecial(string ProductName, int PurchaseQty, SpecialType Type,
            bool IsActive, float Price)
        {
            this.ProductName = ProductName;
            this.PurchaseQty = PurchaseQty;
            this.Type = Type;
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
