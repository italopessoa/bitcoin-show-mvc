using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BitcoinShow.Neo4j.Core;
using BitcoinShow.Neo4j.Repository.Interface;
using BitcoinShow.Neo4j.Service.Interface;
using Neo4j.Map.Extension.Map;
using Neo4j.Map.Extension.Model;

namespace BitcoinShow.Neo4j.Service
{
    public class UserService : INeo4jService<UserNode>
    {
        private readonly INeo4jRepository _repository;

        public UserService(INeo4jRepository repository)
        {
            _repository = repository;
        }

        public Task<UserNode> CreateAsync(UserNode user)
        {
            if (user.Id > 0)
                throw new ArgumentException("Id value must be 0 on creation.", nameof(user.Id));
            if (!string.IsNullOrEmpty(user.UUID))
                throw new ArgumentException("UUID value must be empty on creation.", nameof(user.UUID));
            if (string.IsNullOrWhiteSpace(user.Name))
                throw new ArgumentNullException(nameof(user.Name), "Name cannot be null");

            return _repository.CreateCypherAsync<UserNode>(user.MapToCypher(CypherQueryType.Create));
        }

        public Task<bool> DeleteByUUIDAsync(string uuid)
        {
            throw new NotImplementedException();
        }

        public Task ExecuteQueryAsync(string query)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserNode>> MatchByPropertiesAsync(UserNode node)
        {
            throw new NotImplementedException();
        }

        public Task<UserNode> MatchByUUIDAsync(string uuid)
        {
            throw new NotImplementedException();
        }
    }
}
