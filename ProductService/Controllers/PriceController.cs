using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductService.Models;
using ProductService.Models.Prices;

namespace ProductService.Controllers
{
    [Route("price-management/")]
    [ApiController]
    public class PriceController : ControllerBase
    {
        IDataAccessor<Product> _priceDataAccessor;

        public PriceController(IDataAccessor<Product> priceDataAccessor)
        {
            _priceDataAccessor = priceDataAccessor;
        }

        // GET api/allprices
        [HttpGet]
        [Route("prices")]
        public ActionResult<IEnumerable<Product>> GetAllPrices()
        {            
            return _priceDataAccessor.GetAll().ToList();
        }

        [HttpPost]
        [Route("prices")]
        public ActionResult<string> AddPrice([FromBody] Product product)
        {
            return _priceDataAccessor.Save(product);
        }

        [HttpPut]
        [Route("price")]
        public ActionResult<string> UpdatePrice([FromBody] Product product)
        {
            return _priceDataAccessor.Update(product);
        }

        [HttpGet("{productName}")]
        [Route("price")]
        public ActionResult<float> GetPrice([FromQuery]string productName)
        {
            return _priceDataAccessor.GetAmountByProductName(productName);
        }
    }
}