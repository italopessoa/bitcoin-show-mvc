using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BitcoinShow.Neo4j.Service.Interface;
using BitcoinShow.Neo4j.Core;
using BitcoinShow.Neo4j.Repository.Interface;
using System.Linq;
using Neo4j.Map.Extension.Map;
using Neo4j.Map.Extension.Model;

namespace BitcoinShow.Neo4j.Service
{
    public class Neo4jService : INeo4jService
    {
        private readonly INeo4jRepository _repository;

        public Neo4jService(INeo4jRepository repository)
        {
            _repository = repository;
        }

        public async Task<QuestionNode> CreateQuestionAsync(QuestionNode question)
        {
            if (question.Id > 0)
                throw new ArgumentException("Id value must be 0 on creation.", nameof(question.Id));
            if (!string.IsNullOrEmpty(question.UUID))
                throw new ArgumentException("UUID value must be empty on creation.", nameof(question.UUID));
            if (question.IncorrectAnswers == null || !question.IncorrectAnswers.Any())
                throw new ArgumentException("There must be at least one incorrect answer.", nameof(question.IncorrectAnswers));
            if (string.IsNullOrWhiteSpace(question.CorrectAnswer) || String.IsNullOrEmpty(question.CorrectAnswer))
                throw new ArgumentException("There must be one correct answer.", nameof(question.CorrectAnswer));
            if (question.IncorrectAnswers.Contains(question.CorrectAnswer))
                throw new InvalidOperationException($"The correct answer \"{question.CorrectAnswer}\" can't be in the incorrect answers list.");

            return await _repository.CreateCypherAsync<QuestionNode>(question.MapToCypher(CypherQueryType.Create));
        }

        public Task<bool> DeleteQuestionByUUIDAsync(string uuid)
        {
            if (string.IsNullOrEmpty(uuid) || string.IsNullOrWhiteSpace(uuid))
                throw new ArgumentException("UUID can't be empty", nameof(uuid));

            throw new NotImplementedException();
        }

        public Task<List<QuestionNode>> MatchQuestionByPropertiesAsync(QuestionNode question)
        {
            throw new NotImplementedException();
        }

        public Task<QuestionNode> MatchQuestionByUUIDAsync(QuestionNode question)
        {
            if (string.IsNullOrEmpty(question.UUID) || string.IsNullOrWhiteSpace(question.UUID))
                throw new ArgumentException("UUID can't be empty", nameof(question.UUID));
            throw new NotImplementedException();
        }
    }
}
