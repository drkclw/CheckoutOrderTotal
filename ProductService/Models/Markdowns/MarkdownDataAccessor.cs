using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.Models.Markdowns
{
    public class MarkdownDataAccessor : IDataAccessor<Markdown>
    {
        private IRepository<Markdown> _markdownRepository;
        
        public MarkdownDataAccessor(IRepository<Markdown> markdownRepository)
        {
            _markdownRepository = markdownRepository;
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
