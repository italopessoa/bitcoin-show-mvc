using System;
using System.Collections.Generic;
using BitcoinShow.Web.Models;
using BitcoinShow.Web.Repositories;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using Xunit;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BitcoinShow.Test.Repositories
{
    public class QuestionRepositoryTest
    {
        // https://www.jankowskimichal.pl/en/2016/01/mocking-dbcontext-and-dbset-with-moq/
        [Fact]
        public void Add_Question_Without_Title_Error()
        {
            BitcoinShowDBContext context = DbContextFactory.GetContext();
            QuestionRepository repository = new QuestionRepository(context);

            Question option = new Question();
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => repository.Add(option));
            Assert.NotNull(ex);
            Assert.Equal(nameof(option.Title), ex.ParamName);
        }

        [Fact]
        public void Add_Question_Wit_Title_Greater_Than_Max_Error()
        {
            BitcoinShowDBContext context = DbContextFactory.GetContext();
            QuestionRepository repository = new QuestionRepository(context);

            Question option = new Question();
            option.Title = new String('a', 201);
            
            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(() => repository.Add(option));
            Assert.NotNull(ex);
            Assert.Equal(nameof(option.Title), ex.ParamName);
        }

        [Fact]
        public void Add_Question_Without_Answer_Error()
        {
            BitcoinShowDBContext context = DbContextFactory.GetContext();
            QuestionRepository repository = new QuestionRepository(context);

            Question option = new Question();
            option.Title = "How many times do you test your code?";
            
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => repository.Add(option));
            Assert.NotNull(ex);
            Assert.Equal(nameof(option.Answer), ex.ParamName);
        }

        [Fact]
        public void Add_Question_Success()
        {
            BitcoinShowDBContext context = DbContextFactory.GetContext();

            var options = RandomOptions(4).ToList();
            options.ForEach(o => 
            {
                context.Options.Add(o);
            });
            context.SaveChanges();

            QuestionRepository repository = new QuestionRepository(context);
            Question question = new Question();
            question.Answer = context.Options.First();
            question.Title = "Test question";

            repository.Add(question);
            Assert.True(question.Id > 0);
        }

        [Fact]
        public void GetAll_Questions_Success()
        {
            BitcoinShowDBContext context = DbContextFactory.GetContext();
            for (int i = 0; i < 1; i++)
            {
                context.Questions.Add(new Question
                { 
                    Title = $"Random Question {i + 1}",
                    Answer = new Option() { Text = $"Random Option {i}"}
                });
            }
            context.SaveChanges();

            QuestionRepository repository = new QuestionRepository(context);

            repository.GetAll().ForEach(q => 
            {
                Assert.NotNull(q);
                Assert.True(q.Id > 0);
                Assert.False(String.IsNullOrEmpty(q.Title));
                Assert.NotNull(q.Answer);
                Assert.True(q.Answer.Id > 0);
                Assert.False(String.IsNullOrEmpty(q.Answer.Text));
            });
        }

        [Fact]
        public void Get_Question_Not_Found_Error()
        {
            BitcoinShowDBContext context = DbContextFactory.GetContext();
            for (int i = 0; i < 10; i++)
            {
                context.Questions.Add(new Question
                { 
                    Title = $"Random Question {i + 1}",
                    Answer = new Option() { Id = i, Text = $"Random Option {i}"}
                });
            }
            context.SaveChanges();

            QuestionRepository repository = new QuestionRepository(context);

            Question actual = repository.Get(50);
            Assert.Null(actual);
        }

        [Fact]
        public void Get_Question_Success()
        {
            BitcoinShowDBContext context = DbContextFactory.GetContext();
            for (int i = 0; i < 98; i++)
            {
                context.Questions.Add(new Question
                { 
                    Title = $"Random Question {i + 1}",
                    Answer = new Option() { Id = i, Text = $"Random Option {i}"}
                });
            }
            context.SaveChanges();

            QuestionRepository repository = new QuestionRepository(context);

            Question actual = repository.Get(50);
            Assert.NotNull(actual);
            Assert.NotNull(actual.Answer);
        }

        [Fact]
        public void Delete_Question_Not_Found_Error()
        {
            BitcoinShowDBContext context = DbContextFactory.GetContext();

            QuestionRepository repository = new QuestionRepository(context);
            Exception ex = Assert.Throws<Exception>(() => repository.Delete(99999999));
            Assert.NotNull(ex);
            Assert.Equal("The current Question does not exist.", ex.Message);
        }

        [Fact]
        public void Delete_Question_Success()
        {
            var question = new Question
            {
                Title = "Delete_Question_Success"
            };
            BitcoinShowDBContext context = DbContextFactory.GetContext();
            context.Questions.Add(question);
            context.SaveChanges();
            int questionId = question.Id;

            QuestionRepository repository = new QuestionRepository(context);
            repository.Delete(questionId);
            Assert.Null(context.Questions.Find(questionId));
        }

        private IEnumerable<Option> RandomOptions(int nOptions)
        {
            for (int i = 0; i < nOptions; i++)
            {
                yield return new Option 
                { 
                    Text = $"Random Option {i + 1}"
                };
            }
        }

        private IEnumerable<Option> RandomOptionsWithId(int nOptions)
        {
            for (int i = 0; i < nOptions; i++)
            {
                yield return new Option 
                { 
                    Id = i + 1,
                    Text = $"Random Option {i + 1}"
                };
            }
        }
    }
}
