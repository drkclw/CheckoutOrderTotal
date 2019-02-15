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
    [Route("markdown-management/")]
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
        [Route("markdowns")]
        public ActionResult<IEnumerable<Markdown>> GetAllMarkdowns()
        {
            return _dataAccessor.GetAll().ToList();
        }

        [HttpPost]
        [Route("markdowns")]
        public ActionResult<string> AddMarkdown([FromBody] Markdown markdown)
        {
            return _dataAccessor.Save(markdown);
        }

        [HttpPut]
        [Route("markdown")]
        public ActionResult<string> UpdateMarkdown([FromBody] Markdown markdown)
        {
            return _dataAccessor.Update(markdown);
        }

        [HttpGet("{productName}")]
        [Route("markdown")]
        public ActionResult<float> GetMarkdown([FromQuery]string productName)
        {
            return _dataAccessor.GetAmountByProductName(productName);
        }
    }
}