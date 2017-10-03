using System;
using System.Collections.Generic;
using BitcoinShow.Web.Models;
using BitcoinShow.Web.Repositories;
using Moq;
using Xunit;

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
            Assert.Equal("The title has to many characters.", ex.Message);
        }

        [Fact]
        public void Add_Question_Without_Answer_Error()
        {
            BitcoinShowDBContext context = DbContextFactory.GetContext();
            QuestionRepository repository = new QuestionRepository(context);

            Question option = new Question();
            option.Title = "How many times do you test your code?";
            
            ArgumentException ex = Assert.Throws<ArgumentException>(() => repository.Add(option));
            Assert.NotNull(ex);
            Assert.Equal(nameof(option.Title), ex.ParamName);
            Assert.Equal("You must provide Answer navigation property value.", ex.Message);
        }

        [Fact]
        public void Add_Question_Without_Options_Error()
        {
            BitcoinShowDBContext context = DbContextFactory.GetContext();
            QuestionRepository repository = new QuestionRepository(context);

            Question newQuestionNullOptions = new Question();
            newQuestionNullOptions.Title = "How many times do you test your code?";
            newQuestionNullOptions.Answer = new Option() {Id = 1,Text= "Option 1"};

            Question newQuestionZeroOptions = new Question();
            newQuestionZeroOptions.Title = "How many times do you test your code?";
            newQuestionZeroOptions.Answer = new Option() {Id = 1,Text= "Option 1"};
            newQuestionZeroOptions.Options = new List<Option>();

            ArgumentException ex = Assert.Throws<ArgumentException>(() => repository.Add(newQuestionNullOptions));
            Assert.NotNull(ex);
            Assert.Equal("At least two options are required.", ex.Message);
        }

        [Fact]
        public void Add_Question_With_Answer_Out_Of_Options_Error()
        {
            BitcoinShowDBContext context = DbContextFactory.GetContext();
            QuestionRepository repository = new QuestionRepository(context);

            Option answer = new Option { Id = 5, Text = "Invalid option" };
            
            List<Option> options = new List<Option>
            {
                new Option {Id = 1, Text = "Option A"},
                new Option {Id = 2, Text = "Option B"},
                new Option {Id = 3, Text = "Option C"},
                new Option {Id = 4, Text = "Option D"}
            };

            Question newQuestion = new Question("question",answer,options);
            
            ArgumentException ex = Assert.Throws<ArgumentException>(() => repository.Add(newQuestion));
            Assert.Equal("The options list does not contain the current Answer object.", ex.Message);
        }

        [Fact]
        public void Add_Question_Success()
        {
        }
    }
}
