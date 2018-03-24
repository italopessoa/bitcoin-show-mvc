using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BitcoinShow.Neo4j.Core;
using BitcoinShow.Neo4j.Repository;
using BitcoinShow.Neo4j.Repository.Interface;
using BitcoinShow.Neo4j.Service.Interface;
using Moq;
using Xunit;

namespace BitcoinShow.Neo4j.Service.Test
{
    public class Neo4jServiceTest
    {
        [Fact]
        public async Task CreateQuestionAsyncTest()
        {
            Mock<INeo4jRepository> repositoryMock = new Mock<INeo4jRepository>(MockBehavior.Strict);
            INeo4jService service = new Neo4jService(repositoryMock.Object);
            QuestionNode question = new QuestionNode
            {
                Title = "Question number 1",
                Type = QuestionType.MultipleChoice,
                Difficulty = QuestionDifficulty.Easy,
                CorrectAnswer = "correct",
                IncorrectAnswers = new List<object>() { "a", "b", "c" }
            };
            QuestionNode actual = await service.CreateQuestionAsync(question);
        }

        [Fact]
        public async void MatchQuestionByPropertiesAsyncTest()
        {
            Mock<INeo4jRepository> repositoryMock = new Mock<INeo4jRepository>(MockBehavior.Strict);
            INeo4jService service = new Neo4jService(repositoryMock.Object);
            await service.CreateQuestionAsync(null);
        }

        [Fact]
        public async void MatchQuestionByUUIDAsync()
        {
            Mock<INeo4jRepository> repositoryMock = new Mock<INeo4jRepository>(MockBehavior.Strict);
            INeo4jService service = new Neo4jService(repositoryMock.Object);
            await service.MatchQuestionByUUIDAsync(null);
        }

        [Fact]
        public async void DeleteQuestionByUUIDAsync()
        {
            Mock<INeo4jRepository> repositoryMock = new Mock<INeo4jRepository>(MockBehavior.Strict);
            INeo4jService service = new Neo4jService(repositoryMock.Object);
            await service.DeleteQuestionByUUIDAsync(null);
        }
    }
}
