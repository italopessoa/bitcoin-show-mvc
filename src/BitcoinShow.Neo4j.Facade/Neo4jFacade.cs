using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BitcoinShow.Neo4j.Core;
using BitcoinShow.Neo4j.Facade.Interface;
using BitcoinShow.Neo4j.Facade.Model;

namespace BitcoinShow.Neo4j.Facade
{
    public class Neo4jFacade : INeo4jFacade
    {
        public Neo4jFacade()
        {
        }

        public async Task<QuestionNode> CreateQuestionAsync(QuestionNode question)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteQuestionByUUIDAsync(string uuid)
        {
            throw new NotImplementedException();
        }

        public Task<List<QuestionNode>> MatchQuestionByPropertiesAsync(QuestionNode question)
        {
            throw new NotImplementedException();
        }

        public Task<QuestionNode> MatchQuestionByUUIDAsync(QuestionNode question)
        {
            throw new NotImplementedException();
        }
    }
}
