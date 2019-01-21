using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.Models
{
    public interface ISpecial
    {
        string ProductName { get; set; }

        int PurchaseQty { get; set; }

        SpecialType Type { get; set; }

        bool IsActive { get; set; }
    }
}
