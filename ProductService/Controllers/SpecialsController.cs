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
        public ActionResult<string> AddSpecial([FromBody] ISpecial special)
        {
            return "Success.";
        }
    }
}