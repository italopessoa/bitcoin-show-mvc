using System.Collections.Generic;
using BitcoinShow.Web.Models;

namespace BitcoinShow.Web.Repositories.Interface
{
    public interface IQuestionRepository
    {
        /// <summary>
        ///     Add new Question. The result will be store into the current param.
        /// </summary>
        /// <param name="question"> Question to be stored. </param>
        void Add(Question question);

        /// <summary>
        ///     Get all the Questions
        /// </summary>
        /// <returns> List of questions. </returns>
        List<Question> GetAll();

        /// <summary>
        ///     Search Question by Question Id.
        /// </summary>
        /// <param name="id"> Question Id. </param>
        /// <returns> The found Question. </returns>
        Question GetById(int id);

        /// <summary>
        ///     Get random Question based on the its level.
        /// </summary>
        /// <param name="level"> Question level. </param>
        /// <returns> The found Question. </returns>
        Question GetByLevel(QuestionLevelEnum level);

        /// <summary>
        ///     Delete question.
        /// </summary>
        /// <param name="id"> Question Id. </param>
        void Delete(int id);

        /// <summary>
        ///     Update question.
        /// </summary>
        /// <param name="question"> Question object with value to update. </param>
        void Update(Question question);
    }
}