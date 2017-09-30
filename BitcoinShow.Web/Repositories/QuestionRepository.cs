using BitcoinShow.Web.Models;
using BitcoinShow.Web.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

namespace BitcoinShow.Web.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private BitcoinShowDBContext _context;
        public QuestionRepository(BitcoinShowDBContext context)
        {
            this._context = context;
        }

        public void Add(Question question)
        {
            throw new System.NotImplementedException();
        }
        
        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<Question> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Question Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Question quesiton)
        {
            throw new System.NotImplementedException();
        }
    }
}
