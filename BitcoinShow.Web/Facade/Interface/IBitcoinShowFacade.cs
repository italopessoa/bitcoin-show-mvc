using System.Collections.Generic;
using BitcoinShow.Web.Models;

namespace BitcoinShow.Web.Facade.Interface
{
    public interface IBitcoinShowFacade
    {
        /// <summary>
        ///     List all the questions.
        /// </summary>
        /// <returns> List of Questions. </returns>
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

        /// <summary>
        ///     Update Question.
        /// </summary>
        /// <param name="questionViewModel"> QuestionViewModel object with values to be updated. </param>
        void UpdateQuestion(QuestionViewModel questionViewModel);

        /// <summary>
        ///     Create a new award.
        /// </summary>
        /// <param name="awardViewModel"> AwardViewModel object. </param>
        /// <returns> AwardViewModel </returns>
        AwardViewModel CreateAward(AwardViewModel awardViewModel);

        /// <summary>
        ///     Find an award by Id.
        /// </summary>
        /// <param name="id"> Award Id. </param>
        /// <returns> AwardViewModel </returns>
        AwardViewModel GetAward(int id);

        /// <summary>
        ///     Get all the Awards.
        /// </summary>
        /// <returns> List<AwardViewModel> </returns>
        List<AwardViewModel> GetAwards();

        /// <summary>
        ///     Update Award
        /// </summary>
        /// <param name="awardViewModel"> AwardViewModel object with values to be updated. </param>
        void UpdateAward(AwardViewModel awardViewModel);

        /// <summary>
        ///     Delete a question.
        /// </summary>
        /// <param name="id"> Quesiton Id. </param>
        void DeleteAward(int id);

        /// <summary>
        ///     Get a random question based on its Level
        /// </summary>
        /// <param name="level"> Question Level. </param>
        /// <param name="exclude"> Questions Id that has already been selected before. </param>
        /// <returns> QuestionViewModel </returns>
        QuestionViewModel GetRandomQuestionByLevel(LevelEnum level, int[] exclude);
    }
}
