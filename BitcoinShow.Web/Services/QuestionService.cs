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
            return _repository.GetAll();
        }

        public Question Get(int id)
        {
            return _repository.Get(id);
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
