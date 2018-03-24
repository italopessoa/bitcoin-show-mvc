﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BitcoinShow.Neo4j.Service.Interface;
using BitcoinShow.Neo4j.Core;
using BitcoinShow.Neo4j.Repository.Interface;

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
