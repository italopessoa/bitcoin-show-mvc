using System;
using System.Collections.Generic;
using BitcoinShow.Web.Models;
using BitcoinShow.Web.Repositories;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using Xunit;
using System.Linq;

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
            QuestionRepository repository = new QuestionRepository(context);

            var options = RandomOptions(4).ToList();
            options.ForEach(o => 
            {
                context.Options.Add(o);
            });
            context.SaveChanges();

            var expectedAnswer = context.Options.Find(2);

            Question expectedQuestion = new Question();
            expectedQuestion.Id = 1;
            expectedQuestion.Title = "Test question";
            expectedQuestion.Answer = expectedAnswer;

            Question question = new Question();
            question.Answer = context.Options.Find(2);
            question.Title = "Test question";

            repository.Add(question);
            Assert.Equal(expectedQuestion, question);
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
