using System.Collections.Generic;
using BitcoinShow.Web.Models;

namespace BitcoinShow.Web.Facade.Interface
{
    public interface IBitcoinShowFacade
    {
        /// <summary>
        ///     List all the questions.
        /// </summary>
        /// <returns> List of Questions.!-- </returns>
        List<QuestionViewModel> GetAllQuestions();

        /// <summary>
        ///     Create a new Question
        /// </summary>
        /// <param name="question"> Question to save.!-- </param>
        /// <returns></returns>
        QuestionViewModel CreateQuestion(QuestionViewModel question);

        /// <summary>
        ///     Get a Question by Id
        /// </summary>
        /// <param name="id"> Question id</param>
        /// <returns> Question </returns>
        QuestionViewModel GetQuestion(int id);
    }
}
