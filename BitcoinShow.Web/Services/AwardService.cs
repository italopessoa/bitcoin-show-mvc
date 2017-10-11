using System.Collections.Generic;
using BitcoinShow.Web.Models;
using BitcoinShow.Web.Repositories.Interface;
using BitcoinShow.Web.Services.Interface;

namespace BitcoinShow.Web.Services
{
    public class AwardService : IAwardService
    {
        private readonly IAwardRepository _repository;
        public AwardService(IAwardRepository repository)
        {
            _repository = repository;
        }
        public Award Add(bool success, bool fail, bool quit, LevelEnum level)
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
