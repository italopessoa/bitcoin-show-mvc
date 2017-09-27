using System.Collections.Generic;
using BitcoinShow.Web.Models;
using BitcoinShow.Web.Repositories.Interface;

namespace BitcoinShow.Web.Repositories
{
    public class OptionRepository : IOptionRepository
    {
        private BitcoinShowDBContext _context;
        public OptionRepository(BitcoinShowDBContext context)
        {
            _context = context;
        }
        public void Add(Option newOption)
        {
            _context.Options.Add(newOption);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public Option Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Option option)
        {
            throw new System.NotImplementedException();
        }
    }
}