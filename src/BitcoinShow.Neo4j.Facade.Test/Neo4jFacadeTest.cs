using System;
using System.Threading.Tasks;
using Xunit;

namespace BitcoinShow.Neo4j.Facade.Test
{
    public class Neo4jFacadeTest
    {
        [Fact]
        public async Task CreateQuestionAsyncTest()
        {
            Neo4jFacade facade = new Neo4jFacade();
            await facade.CreateQuestionAsync(null);
        }

        [Fact]
        public async void MatchQuestionByPropertiesAsyncTest()
        {
            Neo4jFacade facade = new Neo4jFacade();
            await facade.CreateQuestionAsync(null);
        }

        [Fact]
        public async void MatchQuestionByUUIDAsync()
        {
            Neo4jFacade facade = new Neo4jFacade();
            await facade.MatchQuestionByUUIDAsync(null);
        }

        [Fact]
        public async void DeleteQuestionByUUIDAsync()
        {
            Neo4jFacade facade = new Neo4jFacade();
            await facade.DeleteQuestionByUUIDAsync(null);
        }
    }
}
