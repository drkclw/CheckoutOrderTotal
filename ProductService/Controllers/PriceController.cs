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
    public class PriceController : ControllerBase
    {
        IRepository<Product> priceRepository;

        public PriceController()
        {

        }

        public PriceController(IRepository<Product> repository)
        {
            priceRepository = repository;
        }

        // GET api/prices
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAllPrices()
        {            
            return priceRepository.GetAll().ToList();
        }

        [HttpPost]
        public ActionResult<string> AddPrice([FromBody] Product product)
        {
            return "Success";
        }
    }
}