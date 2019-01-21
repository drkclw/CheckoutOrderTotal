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
    public class MarkdownController : ControllerBase
    {
        private IRepository<Markdown> markdownRepository;

        public MarkdownController(IRepository<Markdown> repository)
        {
            markdownRepository = repository;
        }

        // GET api/allprices
        [HttpGet]
        [Route("allmarkdowns")]
        public ActionResult<IEnumerable<Markdown>> GetAllMarkdowns()
        {
            return markdownRepository.GetAll().ToList();
        }

        [HttpPost]
        public ActionResult<string> AddMarkdown([FromBody] Markdown markdown)
        {                        
            return "Success";            
        }
    }
}