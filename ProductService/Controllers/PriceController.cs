﻿using System;
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

        public PriceController(IRepository<Product> repository)
        {
            priceRepository = repository;
        }

        // GET api/prices
        [HttpGet]
        [Route("allprices")]
        public ActionResult<IEnumerable<Product>> GetAllPrices()
        {            
            return priceRepository.GetAll().ToList();
        }

        [HttpPost]
        public ActionResult<string> AddPrice([FromBody] Product product)
        {
            if (product.Price > 0)
            {
                priceRepository.Save(product);
                return "Success";
            }
            else
            {
                return "Error: Price must be bigger than 0.";
            }
        }

        [HttpPut]
        public ActionResult<string> UpdatePrice([FromBody] Product product)
        {
            if(product.Price < 0)
            {
                return "Error: Price must be bigger than 0.";
            }

            bool updateResult = priceRepository.Update(product);

            if(updateResult)
                return "Success";
            else
            {
                return "Product does not exist, create product before updating price.";
            }
        }
    }
}