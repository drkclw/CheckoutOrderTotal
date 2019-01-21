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
            throw new NotImplementedException();
        }

        public void Save(Markdown saveThis)
        {
            throw new NotImplementedException();
        }
        public void Delete(Markdown deleteThis)
        {
            throw new NotImplementedException();
        }

        public bool Update(Markdown updateThis)
        {
            throw new NotImplementedException();
        }
    }
}
