using System.Collections.Generic;
using System.Threading.Tasks;
using BitcoinShow.Neo4j.Core;

namespace BitcoinShow.Neo4j.Service.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface INeo4jService
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
