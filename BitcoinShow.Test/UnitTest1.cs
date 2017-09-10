using System;
using BitcoinShow.Web.Repositories.Interface;
using BitcoinShow.Web.Services;
using BitcoinShow.Web.Services.Interface;
using Moq;
using Xunit;


namespace BitcoinShow.Test
{
    public class UnitTest1
    {
        private Mock<IQuestionRepository> mockService;
        
        [Fact]
        public void Test1()
        {
            mockService = new Mock<IQuestionRepository>(MockBehavior.Strict);
            mockService.Setup(s => s.Get(String.Empty)).Returns(() => "sdasdas");

            QuestionService service = new QuestionService(mockService.Object);
            service.Get("ksdjfskdjfs");
        }
    }
}
