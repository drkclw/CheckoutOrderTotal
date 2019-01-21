using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.Models
{
    public class MarkdownRepository : IRepository<Markdown>
    {
        private IList<Markdown> markdownList;

        public MarkdownRepository()
        {
            markdownList = new List<Markdown>();
        }

        public IList<Markdown> GetAll()
        {
            return markdownList;
        }

        public Markdown GetByProductName(string productName)
        {
            var markdownDict = markdownList.ToDictionary(p => p.ProductName, p => p);

            if (markdownDict.ContainsKey(productName))
            {
                return markdownDict[productName];
            }
            else
            {
                return null;
            }
        }

        public void Save(Markdown saveThis)
        {
            markdownList.Add(saveThis);
        }
        public void Delete(Markdown deleteThis)
        {
            throw new NotImplementedException();
        }

        public bool Update(Markdown updateThis)
        {
            var markdownDict = markdownList.ToDictionary(p => p.ProductName, p => p);
            
            markdownDict[updateThis.ProductName].Amount = updateThis.Amount;
            return true;            
        }
    }
}
