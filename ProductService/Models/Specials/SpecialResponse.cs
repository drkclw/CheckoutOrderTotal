using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.Models.Specials
{
    public class SpecialResponse
    {
        public string ProductName { get; set; }

        public int PurchaseQty { get; set; }

        public SpecialType Type { get; set; }

        public bool IsActive { get; set; }

        public float Price { get; set; }

        public int DiscountQty { get; set; }

        public float DiscountAmount { get; set; }

        public int Limit { get; set; }

        public RestrictionType RestrictionType { get; set; }
    }
}
