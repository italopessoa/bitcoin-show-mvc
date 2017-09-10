using BitcoinShow.Web.Repositories.Interface;
using BitcoinShow.Web.Services.Interface;

namespace BitcoinShow.Web.Services
{
    public class QuestionService : IQuestionService
    {
        private IQuestionRepository repository { get; set; }
        public QuestionService(IQuestionRepository repository)
        {
            this.repository = repository;
        }

        public string Get(string question)
        {
            return this.repository.Get(question);
        }
    }
}