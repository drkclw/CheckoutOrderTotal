using ProductService.Models.Prices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.Models.Markdowns
{
    public class MarkdownDataAccessor : IDataAccessor<Markdown>
    {
        private IRepository<Markdown> _markdownRepository;
        private IRepository<Product> _priceRepository;

        public MarkdownDataAccessor(IRepository<Markdown> markdownRepository,
            IRepository<Product> priceRepository)
        {
            _markdownRepository = markdownRepository;
            _priceRepository = priceRepository;
        }

        public IList<Markdown> GetAll()
        {
            return _markdownRepository.GetAll();
        }

        public Markdown GetByProductName(string productName)
        {
            return _markdownRepository.GetByProductName(productName);
        }

        public float GetMarkdownAmount(string productName)
        {
            var markdown = _markdownRepository.GetByProductName(productName);

            if (markdown == null)
            {
                return 0;
            }
            else
            {
                return markdown.Amount;
            }
        }

        public string Save(Markdown saveThis)
        {
            var priceList = _priceRepository.GetAll();
            var priceDict = priceList.ToDictionary(p => p.ProductName, p => p);

            if (priceDict.ContainsKey(saveThis.ProductName))
            {
                if (saveThis.Amount < priceDict[saveThis.ProductName].Price)
                {
                    _markdownRepository.Save(saveThis);
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

        public void Delete(Markdown deleteThis)
        {
            throw new NotImplementedException();
        }

        public string Update(Markdown updateThis)
        {
            var markdownDict = _markdownRepository.GetAll().ToDictionary(p => p.ProductName, p => p);
            var priceDict = _priceRepository.GetAll().ToDictionary(p => p.ProductName, p => p);

            if (priceDict.ContainsKey(updateThis.ProductName))
            {
                if (markdownDict.ContainsKey(updateThis.ProductName))
                {
                    if (priceDict[updateThis.ProductName].Price > updateThis.Amount)
                    {
                        _markdownRepository.Update(updateThis);
                        return "Success.";
                    }
                    else
                    {
                        return "Error: Markdown must be smaller than price.";
                    }
                }
            }
            else
            {
                return "Error: Cannot update markdown for a product that doesn't have a price.";
            }

            return "";
        }
    }
}
