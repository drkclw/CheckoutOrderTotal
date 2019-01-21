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
        private IRepository<Markdown> _markdownRepository;
        private IRepository<Product> _priceRepository;

        public MarkdownController(IRepository<Markdown> markdownRepository, 
            IRepository<Product> priceRepository)
        {
            _markdownRepository = markdownRepository;
            _priceRepository = priceRepository;
        }

        // GET api/allprices
        [HttpGet]
        [Route("allmarkdowns")]
        public ActionResult<IEnumerable<Markdown>> GetAllMarkdowns()
        {
            return _markdownRepository.GetAll().ToList();
        }

        [HttpPost]
        public ActionResult<string> AddMarkdown([FromBody] Markdown markdown)
        {
            var priceList = _priceRepository.GetAll();
            var priceDict = priceList.ToDictionary(p => p.ProductName, p => p);            

            if (priceDict.ContainsKey(markdown.ProductName))
            {
                if (markdown.Amount < priceDict[markdown.ProductName].Price)
                {
                    _markdownRepository.Save(markdown);
                    return "Success";
                }
                else
                {
                    return "Error: Markdown must be smaller than price.";
                }
            }
            else
            {
                return "Error: Cannot add markdown for a product that doesn't have a price.";
            }
        }

        [HttpPut]
        public ActionResult<string> UpdateMarkdown([FromBody] Markdown markdown)
        {
            var priceDict = _priceRepository.GetAll().ToDictionary(p => p.ProductName, p => p);

            if (priceDict.ContainsKey(markdown.ProductName))
            {
                if (priceDict[markdown.ProductName].Price > markdown.Amount)
                {
                    if (_markdownRepository.Update(markdown))
                        return "Success.";
                    else
                        return "Markdown does not exist, create markdown before updating price.";
                }
                else
                {
                    return "Error: Markdown must be smaller than price.";
                }
            }
            else
            {
                return "Error: Cannot update markdown for a product that doesn't have a price.";
            }
        }

        [HttpGet("{productName}")]
        public ActionResult<float> GetMarkdown(string productName)
        {
            var markdown = _markdownRepository.GetByProductName(productName);
            return markdown.Amount;
        }
    }
}