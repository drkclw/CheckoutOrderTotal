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
    [Route("api/[controller]")]
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
        [Route("allprices")]
        public ActionResult<IEnumerable<Product>> GetAllPrices()
        {            
            return _priceDataAccessor.GetAll().ToList();
        }

        [HttpPost]
        public ActionResult<string> AddPrice([FromBody] Product product)
        {
            return _priceDataAccessor.Save(product);
        }

        [HttpPut]
        public ActionResult<string> UpdatePrice([FromBody] Product product)
        {
            return _priceDataAccessor.Update(product);
        }

        [HttpGet("{productName}")]
        public ActionResult<float> GetPrice(string productName)
        {
            return _priceDataAccessor.GetAmountByProductName(productName);
        }
    }
}