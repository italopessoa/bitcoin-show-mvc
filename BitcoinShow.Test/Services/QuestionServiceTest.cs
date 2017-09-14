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
            this._mockRepository.Setup(s => s.Add(newQuestion))
                .Throws(new ArgumentNullException(nameof(newQuestion.Title)));

            QuestionService service = new QuestionService(_mockRepository.Object);
            
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => service.Add(newQuestion));
            Assert.Equal(nameof(newQuestion.Title), (ex as ArgumentNullException).ParamName);
        }

        [Fact]
        public void Add_Question_With_Empty_Title_Error()
        {
            Question newQuestion = new Question();
            newQuestion.Title = "                                                     ";

            this._mockRepository = new Mock<IQuestionRepository>(MockBehavior.Loose);
            this._mockRepository.Setup(s => s.Add(newQuestion))
                .Throws(new ArgumentNullException(nameof(newQuestion.Title)));

            QuestionService service = new QuestionService(_mockRepository.Object);
            
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => service.Add(newQuestion));
            Assert.Equal(nameof(newQuestion.Title), ex.ParamName);
        }

        [Fact]
        public void Add_Question_With_Title_Less_Than_11_Char_Error()
        {
            Question newQuestion = new Question();
            newQuestion.Title = "Who are You?";
            
            this._mockRepository = new Mock<IQuestionRepository>(MockBehavior.Loose);
            this._mockRepository.Setup(s => s.Add(newQuestion))
                .Throws(new ArgumentOutOfRangeException(
                    nameof(newQuestion.Title),
                    newQuestion.Title,
                    "Question title should have at least 12 characters."
                    )
                );

            QuestionService service = new QuestionService(_mockRepository.Object);
            
            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(() => service.Add(newQuestion));
            Assert.Equal(nameof(newQuestion.Title), ex.ParamName);
        }

        [Fact]
        public void Add_Question_Without_Options_Error()
        {
            Question newQuestion = new Question();
            newQuestion.Title = "How many times to you test your code?";

            this._mockRepository = new Mock<IQuestionRepository>(MockBehavior.Loose);
            this._mockRepository.Setup(s => s.Add(newQuestion))
                .Throws(new ArgumentException("Cannot save question without options", nameof(newQuestion.QuestionOptions)));

            QuestionService service = new QuestionService(_mockRepository.Object);
            
            ArgumentException ex = Assert.Throws<ArgumentException>(() => service.Add(newQuestion));
            Assert.Equal(nameof(newQuestion.QuestionOptions), ex.ParamName);
        }

        [Fact]
        public void Add_Question_With_Options_Without_Answer()
        {
            Question newQuestion = new Question();
            newQuestion.Title = "How many times to you test your code?";
            newQuestion.QuestionOptions.Add(new QuestionOption() {Text = "1"});
            newQuestion.QuestionOptions.Add(new QuestionOption() {Text = "2"});
            newQuestion.QuestionOptions.Add(new QuestionOption() {Text = "3"});
            newQuestion.QuestionOptions.Add(new QuestionOption() {Text = "4"});

            this._mockRepository = new Mock<IQuestionRepository>(MockBehavior.Loose);
            this._mockRepository.Setup(s => s.Add(newQuestion))
                .Throws(new ArgumentNullException(nameof(newQuestion.Answer)));

            QuestionService service = new QuestionService(_mockRepository.Object);
            
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => service.Add(newQuestion));
            Assert.Equal(nameof(newQuestion.Answer), ex.ParamName);
        }

        [Fact]
        public void Add_Question_With_Invalid_Answer()
        {
            Question newQuestion = new Question();
            newQuestion.Title = "How many times to you test your code?";
            newQuestion.QuestionOptions.Add(new QuestionOption() {Text = "1"});
            newQuestion.QuestionOptions.Add(new QuestionOption() {Text = "2"});
            newQuestion.QuestionOptions.Add(new QuestionOption() {Text = "3"});
            newQuestion.QuestionOptions.Add(new QuestionOption() {Text = "4"});

            newQuestion.Answer = new QuestionOption() {Text = "5"};

            this._mockRepository = new Mock<IQuestionRepository>(MockBehavior.Loose);
            this._mockRepository.Setup(s => s.Add(newQuestion))
                .Throws(new ArgumentOutOfRangeException(nameof(newQuestion.Answer)));

            QuestionService service = new QuestionService(_mockRepository.Object);
            
            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(() => service.Add(newQuestion));
            Assert.Equal(nameof(newQuestion.Answer), ex.ParamName);
        }
    }
}
