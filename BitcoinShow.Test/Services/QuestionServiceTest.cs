using Xunit;
using Moq;
using BitcoinShow.Web.Repositories.Interface;
using BitcoinShow.Web.Models;
using System;
using BitcoinShow.Web.Services;

namespace BitcoinShow.Test.Services
{
    public class QuestionServiceTest
    {
        private Mock<IQuestionRepository> _mockRepository;

        [Fact]
        public void Add_Question_Without_Title_Error()
        {
            Question newQuestion = new Question();

            this._mockRepository = new Mock<IQuestionRepository>(MockBehavior.Strict);
            this._mockRepository.Setup(s => s.Add(newQuestion)).Throws(new ArgumentNullException("Title"));

            QuestionService service = new QuestionService(_mockRepository.Object);
            
            Exception ex = Assert.Throws<ArgumentNullException>(() => service.Add(newQuestion));
            Assert.IsType<ArgumentNullException>(ex);
            Assert.Equal("Value cannot be null.\r\nParameter name: Title", ex.Message);
        }

        [Fact]
        public void Add_Question_With_Empty_Title_Error()
        {
            Question newQuestion = new Question();
            newQuestion.Title = "                                                     ";

            this._mockRepository = new Mock<IQuestionRepository>(MockBehavior.Loose);
            this._mockRepository.Setup(s => s.Add(newQuestion)).Throws(new ArgumentNullException("Title"));

            QuestionService service = new QuestionService(_mockRepository.Object);
            
            Exception ex = Assert.Throws<ArgumentNullException>(() => service.Add(newQuestion));
            Assert.IsType<ArgumentNullException>(ex);
            Assert.Equal("Value cannot be null.\r\nParameter name: Title", ex.Message);
        }
    }
}
