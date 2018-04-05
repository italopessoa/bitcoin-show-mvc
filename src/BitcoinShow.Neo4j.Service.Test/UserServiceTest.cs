using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BitcoinShow.Neo4j.Core;
using BitcoinShow.Neo4j.Repository.Interface;
using BitcoinShow.Neo4j.Service.Interface;
using Moq;
using Neo4j.Map.Extension.Map;
using Neo4j.Map.Extension.Model;
using Xunit;

namespace BitcoinShow.Neo4j.Service.Test
{
    public class UserServiceTest
    {
        [Fact]
        public async Task CreateAsyncAsync_WithId_Error_Test()
        {
            Mock<INeo4jRepository> repositoryMock = new Mock<INeo4jRepository>(MockBehavior.Strict);
            INeo4jService<UserNode> service = new UserService(repositoryMock.Object);
            UserNode user = new UserNode("user 1", Gender.Female)
            {
                Id = 1
            };
            ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(async () => await service.CreateAsync(user));
            Assert.NotNull(exception);
            Assert.Equal("Id", exception.ParamName);
            repositoryMock.Verify(m => m.CreateCypherAsync<UserNode>(It.IsAny<string>()), Times.Never());
        }

        [Fact]
        public async Task CreateQuestionAsync_WithUUID_Error_Test()
        {
            Mock<INeo4jRepository> repositoryMock = new Mock<INeo4jRepository>(MockBehavior.Strict);
            INeo4jService<UserNode> service = new UserService(repositoryMock.Object);
            UserNode user = new UserNode("user 1", Gender.Female)
            {
                UUID = Guid.NewGuid().ToString()
            };
            ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(async () => await service.CreateAsync(user));
            Assert.NotNull(exception);
            Assert.Equal("UUID", exception.ParamName);
            repositoryMock.Verify(m => m.CreateCypherAsync<UserNode>(It.IsAny<string>()), Times.Never());
        }

        [Fact]
        public async Task CreateQuestionAsync_WithoutName_Error_Test()
        {
            Mock<INeo4jRepository> repositoryMock = new Mock<INeo4jRepository>(MockBehavior.Strict);
            INeo4jService<UserNode> service = new UserService(repositoryMock.Object);
            UserNode user = new UserNode(string.Empty, Gender.Female);
            ArgumentNullException exception = await Assert.ThrowsAsync<ArgumentNullException>(async () => await service.CreateAsync(user));
            Assert.NotNull(exception);
            Assert.Equal("Name", exception.ParamName);
            repositoryMock.Verify(m => m.CreateCypherAsync<UserNode>(It.IsAny<string>()), Times.Never());
        }

        [Fact]
        public async Task CreateQuestionAsync_Test()
        {
            UserNode user = new UserNode("user 1", Gender.Female);
            Mock<INeo4jRepository> repositoryMock = new Mock<INeo4jRepository>(MockBehavior.Strict);
            repositoryMock.Setup(s => s.CreateCypherAsync<UserNode>(user.MapToCypher(CypherQueryType.Create)))
                .Returns(Task.FromResult(new UserNode("user 1", Gender.Female) { Id = 1 }));
            INeo4jService<UserNode> service = new UserService(repositoryMock.Object);
            UserNode actual = await service.CreateAsync(user);
            Assert.NotNull(actual);
            Assert.True(actual.Id > 0);
            Assert.Equal(user.Name, actual.Name);
            Assert.Equal(user.Gender, actual.Gender);
            repositoryMock.Verify(m => m.CreateCypherAsync<UserNode>(user.MapToCypher(CypherQueryType.Create)), Times.Once());
        }

        public async Task DeleteByUUIDAsync_Test()
        {
            Mock<INeo4jRepository> repositoryMock = new Mock<INeo4jRepository>(MockBehavior.Strict);
            INeo4jService<UserNode> service = new UserService(repositoryMock.Object);
            NotImplementedException exception = await Assert.ThrowsAsync<NotImplementedException>(async () => await service.DeleteByUUIDAsync(null));
            Assert.NotNull(exception);
        }

        public async Task ExecuteQueryAsync_Test()
        {
            Mock<INeo4jRepository> repositoryMock = new Mock<INeo4jRepository>(MockBehavior.Strict);
            INeo4jService<UserNode> service = new UserService(repositoryMock.Object);
            NotImplementedException exception = await Assert.ThrowsAsync<NotImplementedException>(async () => await service.ExecuteQueryAsync(null));
            Assert.NotNull(exception);
        }

        public async Task MatchByPropertiesAsync()
        {
            Mock<INeo4jRepository> repositoryMock = new Mock<INeo4jRepository>(MockBehavior.Strict);
            INeo4jService<UserNode> service = new UserService(repositoryMock.Object);
            NotImplementedException exception = await Assert.ThrowsAsync<NotImplementedException>(async () => await service.MatchByPropertiesAsync(null));
            Assert.NotNull(exception);
        }

        public async Task MatchByUUIDAsync()
        {
            Mock<INeo4jRepository> repositoryMock = new Mock<INeo4jRepository>(MockBehavior.Strict);
            INeo4jService<UserNode> service = new UserService(repositoryMock.Object);
            NotImplementedException exception = await Assert.ThrowsAsync<NotImplementedException>(async () => await service.MatchByUUIDAsync(null));
            Assert.NotNull(exception);
        }
    }
}
