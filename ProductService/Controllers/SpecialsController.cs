using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductService.Models;

namespace ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialsController : ControllerBase
    {
        private IRepository<ISpecial> _specialsRepository;

        public SpecialsController(IRepository<ISpecial> specialsRepository)
        {
            _specialsRepository = specialsRepository;
        }

        [HttpGet]
        [Route("allspecials")]
        public ActionResult<IEnumerable<ISpecial>> GetAllSpecials()
        {
            return _specialsRepository.GetAll().ToList();
        }

        [HttpPost]
        public ActionResult<string> AddSpecial([FromBody] SpecialRequest specialRequest)
        {
            if(specialRequest.Type == SpecialType.Price)
            {
                var priceSpecial = new PriceSpecial(specialRequest.ProductName, specialRequest.PurchaseQty,
                    specialRequest.IsActive, specialRequest.Price);
                if(priceSpecial.PurchaseQty > 1)
                {
                    if(priceSpecial.Price > 0)
                    {
                        _specialsRepository.Save(priceSpecial);
                        return "Success.";
                    }
                    else
                    {
                        return "Error: Price must be bigger than 0.";
                    }
                }
                else
                {
                    return "Error: Purchase quantity must be bigger than 1.";
                }
            }else if(specialRequest.Type == SpecialType.Limit)
            {
                if (specialRequest.Limit > 0)
                {
                    var limitSpecial = new LimitSpecial(specialRequest.ProductName, specialRequest.PurchaseQty,
                        specialRequest.IsActive, specialRequest.DiscountQty, specialRequest.DiscountAmount, specialRequest.Limit);

                    _specialsRepository.Save(limitSpecial);
                    return "Success.";
                }
                else
                {
                    return "Error: Limit must be bigger than 0.";
                }
            }
            return "Special type not found.";
        }
    }
}