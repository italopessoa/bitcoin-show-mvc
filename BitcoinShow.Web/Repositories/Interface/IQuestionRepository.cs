using System.Collections.Generic;
using BitcoinShow.Web.Models;

namespace BitcoinShow.Web.Repositories.Interface
{
    public interface IQuestionRepository
    {
        Question Add(Question question);

        List<Question> GetAll();

        Question GetById(int id);

        Question GetByLevel(QuestionLevelEnum level);

        void Delete(int id);

        void Update(Question quesiton);
    }
}