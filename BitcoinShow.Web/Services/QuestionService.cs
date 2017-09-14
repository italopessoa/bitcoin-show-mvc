using System.Collections.Generic;
using BitcoinShow.Web.Models;
using BitcoinShow.Web.Repositories.Interface;
using BitcoinShow.Web.Services.Interface;

namespace BitcoinShow.Web.Services
{
    public class QuestionService : IQuestionService
    {
        private IQuestionRepository _repository { get; set; }
        public QuestionService(IQuestionRepository repository)
        {
            this._repository = repository;
        }

        public void Add(Question question)
        {
            _repository.Add(question);            
        }

        public List<Question> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Question GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Question GetByLevel(QuestionLevelEnum level)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Question quesiton)
        {
            throw new System.NotImplementedException();
        }
    }
}