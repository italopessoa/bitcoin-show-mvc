using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BitcoinShow.Neo4j.Service.Test
{
    public class Neo4jServiceTest
    {
        [Fact]
        public async Task CreateQuestionAsyncTest()
        {
            Neo4jService service = new Neo4jService();
            await service.CreateQuestionAsync(null);
        }

        [Fact]
        public async void MatchQuestionByPropertiesAsyncTest()
        {
            Neo4jService service = new Neo4jService();
            await service.CreateQuestionAsync(null);
        }

        [Fact]
        public async void MatchQuestionByUUIDAsync()
        {
            Neo4jService service = new Neo4jService();
            await service.MatchQuestionByUUIDAsync(null);
        }

        [Fact]
        public async void DeleteQuestionByUUIDAsync()
        {
            Neo4jService service = new Neo4jService();
            await service.DeleteQuestionByUUIDAsync(null);
        }
    }
}
