using AutoMapper;
using BitcoinShow.Web.Facade;
using BitcoinShow.Web.Facade.Interface;
using BitcoinShow.Web.Models;
using BitcoinShow.Web.Repositories.Interface;
using BitcoinShow.Web.Services.Interface;
using Moq;
using Xunit;

namespace BitcoinShow.Test.Facade
{
    public class BitcoinShowFacadeTest
    {
        public BitcoinShowFacadeTest()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<BitcoinShowProfile>();
            });
        }
        [Fact]
        public void QuestionService_GetRandom_Question_By_Level()
        {
            Mock<IQuestionService> mockQuestionService = new Mock<IQuestionService>(MockBehavior.Strict);
            mockQuestionService.Setup(s => s.GetByLevel(LevelEnum.Easy, null)).Returns(new Question());
            mockQuestionService.Setup(s => s.GetByLevel(LevelEnum.Hard, new int[] { 1 })).Returns(new Question());

            IBitcoinShowFacade facade = new BitcoinShowFacade(mockQuestionService.Object, null, null);
            QuestionViewModel question = facade.GetRandomQuestionByLevel(LevelEnum.Easy, null);
            QuestionViewModel question2 = facade.GetRandomQuestionByLevel(LevelEnum.Hard, new int[] { 1 });
            Assert.NotNull(question);
            Assert.NotNull(question2);

            mockQuestionService.Verify(s => s.GetByLevel(LevelEnum.Easy, null), Times.Once());
            mockQuestionService.Verify(s => s.GetByLevel(LevelEnum.Hard, new int[] { 1 }), Times.Once());
        }
    }
}
