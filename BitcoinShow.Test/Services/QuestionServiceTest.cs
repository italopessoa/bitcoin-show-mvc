using Xunit;
using Moq;
using BitcoinShow.Web.Repositories.Interface;
using BitcoinShow.Web.Models;
using System;
using BitcoinShow.Web.Services;
using System.Collections.Generic;

namespace BitcoinShow.Test.Services
{
    public class QuestionServiceTest
    {

        [Fact]
        public void Add_Question_Without_Title_Error()
        {
            Mock<IQuestionRepository> mockRepository = new Mock<IQuestionRepository>(MockBehavior.Strict);
            Question newQuestion = new Question();
            mockRepository.Setup(s => s.Add(newQuestion))
                .Throws(new ArgumentNullException(nameof(newQuestion.Title)));

            QuestionService service = new QuestionService(mockRepository.Object);
            
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => service.Add(newQuestion));
            Assert.Equal(nameof(newQuestion.Title), (ex as ArgumentNullException).ParamName);

            mockRepository.Verify(m =>m.Add(It.IsAny<Question>()), Times.Once());
        }

        [Fact]
        public void Add_Question_Without_Title_Greater_Than_Max_Error()
        {
            Mock<IQuestionRepository> mockRepository = new Mock<IQuestionRepository>(MockBehavior.Strict);
            Question newQuestion = new Question();
            newQuestion.Title = new String('a',201);
            mockRepository.Setup(s => s.Add(newQuestion))
                .Throws(new ArgumentOutOfRangeException(nameof(newQuestion.Title)));

            QuestionService service = new QuestionService(mockRepository.Object);
            
            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(() => service.Add(newQuestion));
            Assert.Equal(nameof(newQuestion.Title), ex.ParamName);

            mockRepository.Verify(m =>m.Add(It.IsAny<Question>()), Times.Once());
        }

        [Fact]
        public void Add_Question_Without_Answer_Error()
        {
            Question newQuestion = new Question();
            newQuestion.Title = "How many times do you test your code?";

            Mock<IQuestionRepository> mockRepository = new Mock<IQuestionRepository>(MockBehavior.Strict);
            mockRepository.Setup(s => s.Add(newQuestion))
                .Throws(new ArgumentNullException(nameof(newQuestion.Answer)));

            QuestionService service = new QuestionService(mockRepository.Object);
            
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => service.Add(newQuestion));
            Assert.Equal(nameof(newQuestion.Answer), ex.ParamName);
            
            mockRepository.Verify(m =>m.Add(It.IsAny<Question>()), Times.Once());
        }

        [Fact]
        public void Add_Question_With_Options_Without_Answer_Error()
        {
            Question newQuestionNullOptions = new Question();
            newQuestionNullOptions.Title = "How many times do you test your code?";

            Question newQuestionZeroOptions = new Question();
            newQuestionZeroOptions.Title = "How many times do you test your code?";
            newQuestionZeroOptions.Options = new List<Option>();

            Mock<IQuestionRepository> mockRepository = new Mock<IQuestionRepository>(MockBehavior.Strict);
            mockRepository.Setup(s => s.Add(newQuestionNullOptions))
                .Throws(new ArgumentException("A question needs options", nameof(newQuestionNullOptions.Options)));

            mockRepository.Setup(s => s.Add(newQuestionNullOptions))
                .Throws(new ArgumentException("A question needs options", nameof(newQuestionNullOptions.Options)));

            QuestionService service = new QuestionService(mockRepository.Object);
            
            ArgumentException ex = Assert.Throws<ArgumentException>(() => service.Add(newQuestionNullOptions));
            Assert.Equal(nameof(newQuestionNullOptions.Answer), ex.ParamName);

            ex = Assert.Throws<ArgumentException>(() => service.Add(newQuestionZeroOptions));
            Assert.Equal(nameof(newQuestionZeroOptions.Answer), ex.ParamName);

            mockRepository.Verify(m =>m.Add(It.IsAny<Question>()), Times.Once());
        }

        [Fact]
        public void Add_Question_With_Answer_Out_Of_Options_Error()
        {
            Option answer = new Option { Id = 5, Text = "Invalid option" };
            
            List<Option> options = new List<Option>
            {
                new Option {Id = 1, Text = "Option A"},
                new Option {Id = 2, Text = "Option B"},
                new Option {Id = 3, Text = "Option C"},
                new Option {Id = 4, Text = "Option D"}
            };

            Question newQuestion = new Question("question",answer,options);

            Mock<IQuestionRepository> mockRepository = new Mock<IQuestionRepository>(MockBehavior.Strict);
            mockRepository.Setup(s => s.Add(newQuestion))
                .Throws(new ArgumentException("The options list does not contain the current Answer object."));

            QuestionService service = new QuestionService(mockRepository.Object);
            
            ArgumentException ex = Assert.Throws<ArgumentException>(() => service.Add(newQuestion));
            Assert.Equal("The options list does not contain the current Answer object.", ex.Message);

            mockRepository.Verify(m =>m.Add(It.IsAny<Question>()), Times.Once());
        }
    }
}
