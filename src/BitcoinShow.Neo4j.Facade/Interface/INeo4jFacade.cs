using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BitcoinShow.Neo4j.Facade.Model;

namespace BitcoinShow.Neo4j.Facade.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface INeo4jFacade
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        Task<QuestionNode> CreateQuestionAsync(QuestionNode question);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        Task<QuestionNode> MatchQuestionByUUIDAsync(QuestionNode question);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        Task<List<QuestionNode>> MatchQuestionByPropertiesAsync(QuestionNode question);

        Task<bool> DeleteQuestionByUUIDAsync(string uuid);
    }
}
