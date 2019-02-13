using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.Models
{
    public interface IValidator<T>
    {
        ValidationResponse Validate(T validateThis);
    }
}
