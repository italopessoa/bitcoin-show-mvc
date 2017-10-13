using System;
using System.Collections.Generic;
using BitcoinShow.Web.Models;
using BitcoinShow.Web.Repositories.Interface;

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
            throw new System.NotImplementedException();
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
            throw new System.NotImplementedException();
        }

        public void Update(Award award)
        {
            throw new System.NotImplementedException();
        }
    }
}
