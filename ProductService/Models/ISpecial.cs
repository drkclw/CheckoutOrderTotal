using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.Models
{
    public interface ISpecial
    {
        string ProductName { get; }

        int PurchaseQty { get; }

        SpecialType Type { get; }

        bool IsActive { get; }
    }
}
