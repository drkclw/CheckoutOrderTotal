using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductService.Models;
using ProductService.Models.Specials;
using ProductService.Models.Prices;

namespace ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialsController : ControllerBase
    {
        private IDataAccessor<ISpecial> _specialsDataAccessor;

        public SpecialsController(IDataAccessor<ISpecial> specialsDataAccessor)
        {
            _specialsDataAccessor = specialsDataAccessor;
        }

        [HttpGet]
        [Route("allspecials")]
        public ActionResult<IEnumerable<ISpecial>> GetAllSpecials()
        {
            return _specialsDataAccessor.GetAll().ToList();
        }

        [HttpPost]
        public ActionResult<string> AddSpecial([FromBody] SpecialRequest specialRequest)
        {
            string result = string.Empty;
            if (specialRequest.Type == SpecialType.Price)
            {
                var priceSpecial = new PriceSpecial(specialRequest.ProductName, specialRequest.PurchaseQty,
                    specialRequest.IsActive, specialRequest.Price);
                result = _specialsDataAccessor.Save(priceSpecial);
            }
            else if (specialRequest.Type == SpecialType.Limit)
            {

                var limitSpecial = new LimitSpecial(specialRequest.ProductName, specialRequest.PurchaseQty,
                    specialRequest.IsActive, specialRequest.DiscountQty, specialRequest.DiscountAmount, specialRequest.Limit);
                result = _specialsDataAccessor.Save(limitSpecial);
            }
            else if (specialRequest.Type == SpecialType.Restriction)
            {
                var restrictionSpecial = new RestrictionSpecial(specialRequest.ProductName, specialRequest.PurchaseQty,
                    specialRequest.IsActive, specialRequest.DiscountQty, specialRequest.DiscountAmount,
                    specialRequest.RestrictionType);

                result = _specialsDataAccessor.Save(restrictionSpecial);
            }

            return result;
        }

        [HttpPut]
        public ActionResult<string> UpdateSpecial([FromBody] SpecialRequest specialRequest)
        {
            string result = string.Empty;
            if (specialRequest.Type == SpecialType.Price)
            {
                var priceSpecial = new PriceSpecial(specialRequest.ProductName, specialRequest.PurchaseQty,
                    specialRequest.IsActive, specialRequest.Price);
                result = _specialsDataAccessor.Update(priceSpecial);
            }            

            return result;
        }
    }
}