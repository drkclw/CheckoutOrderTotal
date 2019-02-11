using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductService.Models.Markdowns;
using ProductService.Models.Prices;
using ProductService.Models;

namespace ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarkdownController : ControllerBase
    {
        private IDataAccessor<Markdown> _dataAccessor;

        public MarkdownController(IDataAccessor<Markdown> dataAccessor)
        {
            _dataAccessor = dataAccessor;
        }

        // GET api/allmarkdowns
        [HttpGet]
        [Route("allmarkdowns")]
        public ActionResult<IEnumerable<Markdown>> GetAllMarkdowns()
        {
            return _dataAccessor.GetAll().ToList();
        }

        [HttpPost]
        public ActionResult<string> AddMarkdown([FromBody] Markdown markdown)
        {
            return _dataAccessor.Save(markdown);
        }

        [HttpPut]
        public ActionResult<string> UpdateMarkdown([FromBody] Markdown markdown)
        {
            return _dataAccessor.Update(markdown);
        }

        [HttpGet("{productName}")]
        public ActionResult<float> GetMarkdown(string productName)
        {
            return _dataAccessor.GetAmountByProductName(productName);
        }
    }
}