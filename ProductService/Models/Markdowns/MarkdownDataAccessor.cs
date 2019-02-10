using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.Models.Markdowns
{
    public class MarkdownDataAccessor : IDataAccessor<Markdown>
    {
        public IList<Markdown> GetAll()
        {
            return new List<Markdown>();
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
