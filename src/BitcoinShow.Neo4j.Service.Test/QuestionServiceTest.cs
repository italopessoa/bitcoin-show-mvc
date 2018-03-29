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
            QuestionNode question = new QuestionNode("question 1", default(QuestionDifficulty), default(QuestionType), "correct", new List<object> { "incorrect" });
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
            QuestionNode question = new QuestionNode("question 1", default(QuestionDifficulty), default(QuestionType), "correct", new List<object> { "incorrect" });
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

        [Fact]
        public async void MatchQuestionByPropertiesAsync_Error_Test()
        {
            QuestionNode question = null;
            Mock<INeo4jRepository> repositoryMock = new Mock<INeo4jRepository>(MockBehavior.Strict);
            INeo4jService<QuestionNode> service = new QuestionService(repositoryMock.Object);
            ArgumentNullException exception = await Assert.ThrowsAsync<ArgumentNullException>(async () => await service.MatchByPropertiesAsync(question));
            Assert.NotNull(exception);
            Assert.Equal("question", exception.ParamName);
            repositoryMock.Verify(m => m.MatchSingleKeyCypherAsync<QuestionNode>(It.IsAny<string>()), Times.Never());
        }

        [Fact]
        public async void MatchQuestionByPropertiesAsyncTest()
        {
            QuestionNode question1 = new QuestionNode("question", QuestionDifficulty.Hard, QuestionType.Boolean, "false", new List<object> { "true" });
            QuestionNode question2 = new QuestionNode("question 2", QuestionDifficulty.Medium, QuestionType.Boolean, "true", new List<object> { "false" })
            {
                UUID = Guid.NewGuid().ToString()
            };
            QuestionNode question3 = new QuestionNode()
            {
                Title = "question"
            };
            List<QuestionNode> questions = new List<QuestionNode>
            {
                question1,
                question2,
                question3
            };

            Mock<INeo4jRepository> repositoryMock = new Mock<INeo4jRepository>(MockBehavior.Strict);
            repositoryMock.Setup(s => s.MatchSingleKeyCypherAsync<QuestionNode>(question3.MapToCypher(CypherQueryType.Match))).
                Returns(Task.FromResult(questions));

            INeo4jService<QuestionNode> service = new QuestionService(repositoryMock.Object);
            List<QuestionNode> actual = await service.MatchByPropertiesAsync(question3);
            Assert.NotNull(actual);
            Assert.NotEmpty(actual);
            Assert.Equal(questions, actual);
            repositoryMock.Verify(m => m.MatchSingleKeyCypherAsync<QuestionNode>(question3.MapToCypher(CypherQueryType.Match)), Times.Once());
        }

        [Fact]
        public async void MatchQuestionByUUIDAsync_EmptyUUID_Error_Test()
        {
            Mock<INeo4jRepository> repositoryMock = new Mock<INeo4jRepository>(MockBehavior.Strict);
            INeo4jService<QuestionNode> service = new QuestionService(repositoryMock.Object);
            ArgumentNullException exception = await Assert.ThrowsAsync<ArgumentNullException>(async () => await service.MatchByUUIDAsync(string.Empty));
            Assert.NotNull(exception);
            Assert.Equal("uuid", exception.ParamName);
            repositoryMock.Verify(m => m.MatchLabelByUUIDCypherAsync<QuestionNode>(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        }

        [Fact]
        public async void MatchQuestionByUUIDAsync()
        {
            string uuid = Guid.NewGuid().ToString();
            QuestionNode expected = new QuestionNode("question 2", QuestionDifficulty.Medium, QuestionType.Boolean, "true", new List<object> { "false" })
            {
                UUID = uuid
            };
            Mock<INeo4jRepository> repositoryMock = new Mock<INeo4jRepository>(MockBehavior.Strict);
            repositoryMock.Setup(s => s.MatchLabelByUUIDCypherAsync<QuestionNode>("Question", uuid)).
                Returns(Task.FromResult(expected));

            INeo4jService<QuestionNode> service = new QuestionService(repositoryMock.Object);
            QuestionNode actual = await service.MatchByUUIDAsync(uuid);
            Assert.NotNull(actual);
            Assert.Equal(expected, actual);
            repositoryMock.Verify(m => m.MatchLabelByUUIDCypherAsync<QuestionNode>("Question", uuid), Times.Once());
        }

        [Fact]
        public async void DeleteQuestionByUUIDAsync_EmptyUUID_Error_Test()
        {
            Mock<INeo4jRepository> repositoryMock = new Mock<INeo4jRepository>(MockBehavior.Strict);
            INeo4jService<QuestionNode> service = new QuestionService(repositoryMock.Object);
            ArgumentNullException exception = await Assert.ThrowsAsync<ArgumentNullException>(async () => await service.DeleteByUUIDAsync(null));
            Assert.NotNull(exception);
            Assert.Equal("uuid", exception.ParamName);
            repositoryMock.Verify(m => m.DeleteLabelByUUIDCypherAsync(It.IsAny<string>()), Times.Never());
        }

        [Fact]
        public async void DeleteQuestionByUUIDAsync_False_Test()
        {
            string uuid = "b6d1eca0-2fe4-11e8-bcfe-2cd05a628834";
            Mock<INeo4jRepository> repositoryMock = new Mock<INeo4jRepository>(MockBehavior.Strict);
            repositoryMock.Setup(s => s.DeleteLabelByUUIDCypherAsync(uuid)).ThrowsAsync(new Exception());
            INeo4jService<QuestionNode> service = new QuestionService(repositoryMock.Object);
            Assert.False(await service.DeleteByUUIDAsync(uuid));
            repositoryMock.Verify(m => m.DeleteLabelByUUIDCypherAsync(uuid), Times.Once());
        }

        [Fact]
        public async void DeleteQuestionByUUIDAsync_Test()
        {
            string uuid = "b6d1eca0-2fe4-11e8-bcfe-2cd05a628834";
            Mock<INeo4jRepository> repositoryMock = new Mock<INeo4jRepository>(MockBehavior.Strict);
            repositoryMock.Setup(s => s.DeleteLabelByUUIDCypherAsync(uuid)).Returns(Task.FromResult(0));
            INeo4jService<QuestionNode> service = new QuestionService(repositoryMock.Object);
            Assert.True(await service.DeleteByUUIDAsync(uuid));
            repositoryMock.Verify(m => m.DeleteLabelByUUIDCypherAsync(uuid), Times.Once());
        }
    }
}
