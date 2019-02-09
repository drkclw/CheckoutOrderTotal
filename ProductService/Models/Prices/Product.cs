using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.Models.Prices
{
    public class Product
    {
        public string ProductName { get; set; }

        public float Price { get; set; }

        public Unit Unit { get; set; }
    }
}
