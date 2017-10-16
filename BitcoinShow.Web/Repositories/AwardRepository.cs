using System;
using System.Collections.Generic;
using BitcoinShow.Web.Models;
using BitcoinShow.Web.Repositories.Interface;
using System.Linq;

namespace BitcoinShow.Web.Repositories
{
    public class AwardRepository : IAwardRepository
    {
        private readonly BitcoinShowDBContext _context;
        public AwardRepository(BitcoinShowDBContext context)
        {
            _context = context;
        }
        public Award Add(decimal successValue, decimal failValue, decimal quitValue, LevelEnum level)
        {
            Award award = new Award
            {
                Success = successValue,
                Fail = failValue,
                Quit = quitValue,
                Level = level
            };

            _context.Awards.Add(award);
            _context.SaveChanges();
            return award;
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public Award Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<Award> GetAll()
        {
            return _context.Awards.ToList();
        }

        public void Update(Award award)
        {
            throw new System.NotImplementedException();
        }
    }
}
