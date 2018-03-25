using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BitcoinShow.Neo4j.Core;
using BitcoinShow.Neo4j.Repository;
using BitcoinShow.Neo4j.Repository.Interface;
using BitcoinShow.Neo4j.Service.Interface;
using Moq;
using Neo4j.Map.Extension.Map;
using Neo4j.Map.Extension.Model;
using Xunit;

namespace BitcoinShow.Neo4j.Service.Test
{
    public class QuestionServiceTest
    {
        [Fact]
        public async Task CreateQuestionAsync_WithId_Error_Test()
        {
            Mock<INeo4jRepository> repositoryMock = new Mock<INeo4jRepository>(MockBehavior.Strict);
            INeo4jService<QuestionNode> service = new QuestionService(repositoryMock.Object);
            QuestionNode question = new QuestionNode("question 1", default(QuestionDifficulty), default(QuestionType), " correct", new List<object> { "incorrect" });
            question.Id = 1;
            ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(async () => await service.CreateAsync(question));
            Assert.NotNull(exception);
            Assert.Equal(nameof(question.Id), exception.ParamName);
            repositoryMock.Verify(m => m.CreateCypherAsync<QuestionNode>(It.IsAny<string>()), Times.Never());
        }

        [Fact]
        public async Task CreateQuestionAsync_WithUUID_Error_Test()
        {
            Mock<INeo4jRepository> repositoryMock = new Mock<INeo4jRepository>(MockBehavior.Strict);
            INeo4jService<QuestionNode> service = new QuestionService(repositoryMock.Object);
            QuestionNode question = new QuestionNode("question 1", default(QuestionDifficulty), default(QuestionType), " correct", new List<object> { "incorrect" });
            question.UUID = Guid.NewGuid().ToString();
            ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(async () => await service.CreateAsync(question));
            Assert.NotNull(exception);
            Assert.Equal(nameof(question.UUID), exception.ParamName);
            repositoryMock.Verify(m => m.CreateCypherAsync<QuestionNode>(It.IsAny<string>()), Times.Never());
        }

        [Fact]
        public async Task CreateQuestionAsync_WithoutIncorrectAnswer_Error_Test()
        {
            Mock<INeo4jRepository> repositoryMock = new Mock<INeo4jRepository>(MockBehavior.Strict);
            INeo4jService<QuestionNode> service = new QuestionService(repositoryMock.Object);
            QuestionNode question = new QuestionNode("question 1", default(QuestionDifficulty), default(QuestionType), "correct", null);
            ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(async () => await service.CreateAsync(question));
            Assert.NotNull(exception);
            Assert.Equal(nameof(question.IncorrectAnswers), exception.ParamName);
            repositoryMock.Verify(m => m.CreateCypherAsync<QuestionNode>(It.IsAny<string>()), Times.Never());
        }

        [Fact]
        public async Task CreateQuestionAsync_WithoutAnswer_Error_Test()
        {
            Mock<INeo4jRepository> repositoryMock = new Mock<INeo4jRepository>(MockBehavior.Strict);
            INeo4jService<QuestionNode> service = new QuestionService(repositoryMock.Object);
            QuestionNode question = new QuestionNode("question 1", default(QuestionDifficulty), default(QuestionType), string.Empty, new List<object>() { "answer" });
            ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(async () => await service.CreateAsync(question));
            Assert.NotNull(exception);
            Assert.Equal(nameof(question.CorrectAnswer), exception.ParamName);
            repositoryMock.Verify(m => m.CreateCypherAsync<QuestionNode>(It.IsAny<string>()), Times.Never());
        }

        [Fact]
        public async Task CreateQuestionAsync_WithoutInvalidIncorrectAnswer_Error_Test()
        {
            Mock<INeo4jRepository> repositoryMock = new Mock<INeo4jRepository>(MockBehavior.Strict);
            INeo4jService<QuestionNode> service = new QuestionService(repositoryMock.Object);
            QuestionNode question = new QuestionNode("question 1", default(QuestionDifficulty), default(QuestionType), "a", new List<object>() { "a", "b" });
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(async () => await service.CreateAsync(question));
            Assert.NotNull(exception);
            Assert.Equal($"The correct answer \"{question.CorrectAnswer}\" can't be in the incorrect answers list.", exception.Message);
            repositoryMock.Verify(m => m.CreateCypherAsync<QuestionNode>(It.IsAny<string>()), Times.Never());
        }

        [Fact]
        public async Task CreateQuestionAsyncTest()
        {
            QuestionNode question = new QuestionNode("question 1", QuestionDifficulty.Easy, QuestionType.MultipleChoice, "a", new List<object>() { "b", "c", "d" });
            Mock<INeo4jRepository> repositoryMock = new Mock<INeo4jRepository>(MockBehavior.Strict);
            QuestionNode expected = new QuestionNode("question 1", QuestionDifficulty.Easy, QuestionType.MultipleChoice, "a", new List<object>() { "b", "c", "d" })
            {
                Id = 1
            };
            repositoryMock.Setup(s => s.CreateCypherAsync<QuestionNode>(question.MapToCypher(CypherQueryType.Create)))
                .Returns(Task.FromResult(expected));

            INeo4jService<QuestionNode> service = new QuestionService(repositoryMock.Object);
            QuestionNode actual = await service.CreateAsync(question);

            Assert.NotNull(actual);
            Assert.True(actual.Id > 0);
            Assert.Equal(expected.Title, actual.Title);
            Assert.Equal(expected.Type, actual.Type);
            Assert.Equal(expected.Difficulty, actual.Difficulty);
            Assert.Equal(expected.CorrectAnswer, actual.CorrectAnswer);
            Assert.Equal(expected.IncorrectAnswers, actual.IncorrectAnswers);
            repositoryMock.Verify(m => m.CreateCypherAsync<QuestionNode>(It.IsAny<string>()), Times.Once());
        }

        [Fact(Skip = "todo")]
        public async void MatchQuestionByPropertiesAsyncTest()
        {
            Mock<INeo4jRepository> repositoryMock = new Mock<INeo4jRepository>(MockBehavior.Strict);
            INeo4jService<QuestionNode> service = new QuestionService(repositoryMock.Object);
            await service.CreateAsync(null);
        }

        [Fact(Skip = "todo")]
        public async void MatchQuestionByUUIDAsync()
        {
            Mock<INeo4jRepository> repositoryMock = new Mock<INeo4jRepository>(MockBehavior.Strict);
            INeo4jService<QuestionNode> service = new QuestionService(repositoryMock.Object);
            await service.MatchByUUIDAsync(null);
        }

        [Fact(Skip = "todo")]
        public async void DeleteQuestionByUUIDAsync()
        {
            Mock<INeo4jRepository> repositoryMock = new Mock<INeo4jRepository>(MockBehavior.Strict);
            INeo4jService<QuestionNode> service = new QuestionService(repositoryMock.Object);
            await service.DeleteByUUIDAsync(null);
        }
    }
}
