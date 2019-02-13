using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.Models
{
    public class ValidationResponse
    {
        public bool IsValid { get; set; }
        public string Message { get; set; }
    }
}
