using Xunit;
using Moq;
using BitcoinShow.Web.Repositories.Interface;
using BitcoinShow.Web.Models;
using System;
using BitcoinShow.Web.Services;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
        public void Add_Question_Wit_Title_Greater_Than_Max_Error()
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
        public void Add_Question_Without_Options_Error()
        {
            Question newQuestionNullOptions = new Question();
            newQuestionNullOptions.Title = "How many times do you test your code?";

            Question newQuestionZeroOptions = new Question();
            newQuestionZeroOptions.Title = "How many times do you test your code?";
            newQuestionZeroOptions.Options = new List<Option>();

            Mock<IQuestionRepository> mockRepository = new Mock<IQuestionRepository>(MockBehavior.Strict);
            mockRepository.Setup(s => s.Add(newQuestionNullOptions))
                .Throws(new ArgumentException("A question needs options", nameof(newQuestionNullOptions.Options)));

            mockRepository.Setup(s => s.Add(newQuestionZeroOptions))
                .Throws(new ArgumentException("A question needs options", nameof(newQuestionZeroOptions.Options)));

            QuestionService service = new QuestionService(mockRepository.Object);
            
            ArgumentException ex = Assert.Throws<ArgumentException>(() => service.Add(newQuestionNullOptions));
            Assert.Equal(nameof(newQuestionNullOptions.Options), ex.ParamName);

            ex = Assert.Throws<ArgumentException>(() => service.Add(newQuestionZeroOptions));
            Assert.Equal(nameof(newQuestionZeroOptions.Options), ex.ParamName);

            mockRepository.Verify(m =>m.Add(It.IsAny<Question>()), Times.AtLeast(2));
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

        [Fact]
        public void Add_Question_Success()
        {
            List<Option> options = new List<Option>
            {
                new Option {Id = 1, Text = "Option A"},
                new Option {Id = 2, Text = "Option B"},
                new Option {Id = 3, Text = "Option C"},
                new Option {Id = 4, Text = "Option D"}
            };
            Option answer = options[2];
            Question newQuestion = new Question("What was the score of the game?", answer, options);

            Mock<IQuestionRepository> mockRepository = new Mock<IQuestionRepository>(MockBehavior.Strict);
            mockRepository.Setup(s => s.Add(newQuestion))
            .Callback<Question>(q => {
                q.Id = 1;
                q.Options.ForEach(o => {
                    o.QuestionId = q.Id;
                    o.Question = q;
                });
            });

            Question expectedQuestion = new Question();
            List<Option> expectedOptions = new List<Option>
            {
                new Option {Id = 1, Text = "Option A", QuestionId = expectedQuestion.Id, Question = expectedQuestion},
                new Option {Id = 2, Text = "Option B", QuestionId = expectedQuestion.Id, Question = expectedQuestion},
                new Option {Id = 3, Text = "Option C", QuestionId = expectedQuestion.Id, Question = expectedQuestion},
                new Option {Id = 4, Text = "Option D", QuestionId = expectedQuestion.Id, Question = expectedQuestion}
            };

            Option expectedAnswer = options[2];
            expectedQuestion.Title = "What was the score of the game?";
            expectedQuestion.Answer = expectedAnswer;
            expectedQuestion.Options = expectedOptions;
            expectedQuestion.Id = 1;

            QuestionService service = new QuestionService(mockRepository.Object);
            service.Add(newQuestion);

            Assert.Equal(expectedQuestion, newQuestion);

            mockRepository.Verify(m => m.Add(It.IsAny<Question>()),Times.Once());
        }

        [Fact]
        public void GetAll_Questions_Empty()
        {
            Mock<IQuestionRepository> mockRepository = new Mock<IQuestionRepository>(MockBehavior.Strict);
            mockRepository.Setup(s =>s.GetAll()).Returns(new List<Question>());

            QuestionService service = new QuestionService(mockRepository.Object);
            var actual = service.GetAll();

            Assert.Equal(new List<Question>(), actual);
            mockRepository.Verify(m=>m.GetAll(),Times.Once());
        }

        [Fact]
        public void GetAll_Questions_Success()
        {
            Mock<IQuestionRepository> mockRepository = new Mock<IQuestionRepository>(MockBehavior.Strict);
            mockRepository.Setup(s => s.GetAll()).Returns(this.RandomQuestions(10));
            var expected = this.RandomQuestions(10);

            QuestionService service = new QuestionService(mockRepository.Object);
            var actual = service.GetAll();

            Assert.Equal(expected, actual);
            mockRepository.Verify(m=>m.GetAll(),Times.Once());
        }

        [Fact]
        public void Get_Question_Not_Found_Error()
        {
            Mock<IQuestionRepository> mockRepository = new Mock<IQuestionRepository>(MockBehavior.Strict);
            Question question = null;
            mockRepository.Setup(s => s.Get(100)).Returns(question);

            QuestionService service = new QuestionService(mockRepository.Object);

            Question actual = service.Get(100);
            Assert.Null(actual);

            mockRepository.Verify(m => m.Get(It.IsAny<int>()), Times.Once());
        }

        [Fact]
        public void Get_Question_Success()
        {
            Mock<IQuestionRepository> mockRepository = new Mock<IQuestionRepository>(MockBehavior.Strict);
            mockRepository.Setup(s => s.Get(8)).Returns(this.RandomQuestions(10).Find(q=>q.Id == 8));

            Question expected = this.RandomQuestions(10).Find(q => q.Id == 8);
            QuestionService service = new QuestionService(mockRepository.Object);

            Question actual = service.Get(8);
            Assert.NotNull(actual);
            Assert.Equal(expected, actual);

            mockRepository.Verify(m => m.Get(It.IsAny<int>()), Times.Once());
        }

        [Fact]
        public void Delete_Question_Not_Found_Error()
        {
            Mock<IQuestionRepository> mockRepository = new Mock<IQuestionRepository>(MockBehavior.Strict);
            mockRepository.Setup(s => s.Delete(1))
                .Throws(new DbUpdateException("The current Question does not exist.", new NullReferenceException()));

            QuestionService service = new QuestionService(mockRepository.Object);

            DbUpdateException ex  = Assert.Throws<DbUpdateException>(() => service.Delete(1));

            Assert.NotNull(ex);
            Assert.Equal("The current Question does not exist.", ex.Message);

            mockRepository.Verify(m => m.Delete(It.IsAny<int>()), Times.Once());
        }

        [Fact]
        public void Delete_Question_Success()
        {
            Mock<IQuestionRepository> mockRepository = new Mock<IQuestionRepository>(MockBehavior.Strict);
            mockRepository.Setup(s => s.Delete(1));

            QuestionService service = new QuestionService(mockRepository.Object);

            service.Delete(1);
            mockRepository.Verify(m => m.Delete(It.IsAny<int>()), Times.Once());
        }


        [Fact]
        public void Update_Question_Without_Title_Error()
        {
            Question question = new Question
            {
                Id = 1,
                Title = string.Empty,
            };

            List<Option> options = this.RandomOptions(4, question).ToList();
            question.Answer = options[0];
            question.Options = options;

            Mock<IQuestionRepository> mockRepository = new Mock<IQuestionRepository>(MockBehavior.Strict);
            mockRepository.Setup(s => s.Update(question))
                .Throws(new ArgumentNullException(nameof(question.Title)));

            QuestionService service = new QuestionService(mockRepository.Object);

            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => service.Update(question));

            Assert.NotNull(ex);
            Assert.Equal(nameof(question.Title), ex.ParamName);
            mockRepository.Verify(m => m.Update(It.IsAny<Question>()), Times.Once());
        }

        [Fact]
        public void Update_Question_Without_Title_Greater_Than_Max_Error()
        {
            Question question = new Question
            {
                Id = 1,
                Title = new String('a',201),
            };

            List<Option> options = this.RandomOptions(4, question).ToList();
            question.Answer = options[0];
            question.Options = options;

            Mock<IQuestionRepository> mockRepository = new Mock<IQuestionRepository>(MockBehavior.Strict);
            mockRepository.Setup(s => s.Update(question))
                .Throws(new ArgumentOutOfRangeException(nameof(question.Title)));

            QuestionService service = new QuestionService(mockRepository.Object);

            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(() => service.Update(question));

            Assert.NotNull(ex);
            Assert.Equal(nameof(question.Title), ex.ParamName);
            mockRepository.Verify(m => m.Update(It.IsAny<Question>()), Times.Once());
        }

        [Fact]
        public void Update_Question_Without_Answer_Error()
        {
            Question question = new Question
            {
                Id = 1,
                Title = Guid.NewGuid().ToString(),
            };
            
            List<Option> options = this.RandomOptions(4, question).ToList();
            question.Options = options;

            Mock<IQuestionRepository> mockRepository = new Mock<IQuestionRepository>(MockBehavior.Strict);
            mockRepository.Setup(s => s.Update(question))
                .Throws(new ArgumentNullException(nameof(question.Answer)));

            QuestionService service = new QuestionService(mockRepository.Object);

            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => service.Update(question));

            Assert.NotNull(ex);
            Assert.Equal(nameof(question.Answer), ex.ParamName);
            mockRepository.Verify(m => m.Update(It.IsAny<Question>()), Times.Once());
        }

        [Fact]
        public void Update_Question_With_Answer_Out_Of_Options_Error()
        {
            Question question = new Question
            {
                Id = 1,
                Title = Guid.NewGuid().ToString()
            };
            
            List<Option> options = this.RandomOptions(4, question).ToList();
            question.Options = options;
            question.Answer = new Option { Id = 10, Text = "Invalid option", Question = question, QuestionId = question.Id };

            Mock<IQuestionRepository> mockRepository = new Mock<IQuestionRepository>(MockBehavior.Strict);
            mockRepository.Setup(s => s.Update(question))
                .Throws(new ArgumentException("The options list does not contain the current Answer object."));

            QuestionService service = new QuestionService(mockRepository.Object);

            ArgumentException ex = Assert.Throws<ArgumentException>(() => service.Update(question));

            Assert.NotNull(ex);
            Assert.Equal("The options list does not contain the current Answer object.", ex.Message);
            mockRepository.Verify(m => m.Update(It.IsAny<Question>()), Times.Once());
        }

        private List<Question> RandomQuestions(int n)
        {
            List<Question> questions = new List<Question>();

            int lastOptionId = 0;
            for (int i = 1; i <= n; i++)
            {
                Question q = new Question();
                q.Title = $"Question {i}";
                q.Options = new List<Option>();

                for (int j = 0; j < 4; j++)
                {
                    Option o = new Option();
                    o.Id = ++lastOptionId;
                    o.Text = $"{q.Title} Option {j}";
                    o.QuestionId = q.Id;
                    o.Question = q;
                    q.Options.Add(o);
                }
                q.Answer = q.Options[3];
                q.Id = i;
                questions.Add(q);
            }

            return questions;
        }

        private IEnumerable<Option> RandomOptions(int nOptions, Question question)
        {
            for (int i = 0; i < nOptions; i++)
            {
                yield return new Option 
                { 
                    Id = i,
                    Question = question,
                    QuestionId = question.Id
                };
            }
        }
    }
}
