using System;
using System.Linq;
using BitcoinShow.Web.Models;
using BitcoinShow.Web.Repositories;
using BitcoinShow.Web.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Moq;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace BitcoinShow.Test.Repositories
{
    public class OptionRepositoryTest
    {
        [Fact]
        public void Add_Option_Without_Text_Error()
        {
            var options = new DbContextOptionsBuilder<BitcoinShowDBContext>()
                .UseInMemoryDatabase(System.Guid.NewGuid().ToString())
                .Options;
            var context = new BitcoinShowDBContext(options);
            OptionRepository repository = new OptionRepository(context);

            Option option = new Option();
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => repository.Add(option));

            Assert.NotNull(ex);
            Assert.Equal(nameof(option.Text), ex.ParamName);
        }

        [Fact]
        public void Add_Option_With_Text_Greater_Than_Max_Size_Error()
        {
            var options = new DbContextOptionsBuilder<BitcoinShowDBContext>()
               .UseInMemoryDatabase(System.Guid.NewGuid().ToString())
               .Options;
            var context = new BitcoinShowDBContext(options);
            OptionRepository repository = new OptionRepository(context);

            Option option = new Option();

            option.Text = new String('A', 201);
            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(() => repository.Add(option));

            Assert.NotNull(ex);
            Assert.Equal(nameof(option.Text), ex.ParamName);
            Assert.Equal(0, context.Options.Count());
        }

        [Fact]
        public void Add_Option_Success()
        {
            string text = "Add_Option_Success";
            var options2 = new DbContextOptionsBuilder<BitcoinShowDBContext>()
               .UseInMemoryDatabase("asd")
               .Options;
            var context2 = new BitcoinShowDBContext(options2);
            OptionRepository repository = new OptionRepository(context2);

            Option actual = new Option();
            actual.Text = text;

            repository.Add(actual);
            Assert.NotNull(actual);
            Assert.True(0 < actual.Id);
            Assert.Equal(text, actual.Text);
            Assert.Equal(1, context2.Options.Count());
        }

        [Fact]
        public void Get_Option_By_Id_Not_Found()
        {
            var options = new DbContextOptionsBuilder<BitcoinShowDBContext>()
                    .UseInMemoryDatabase(System.Guid.NewGuid().ToString())
                    .Options;
            var context = new BitcoinShowDBContext(options);

            OptionRepository repository = new OptionRepository(context);

            var option = repository.Get(100);
            Assert.Null(option);
        }

        [Fact]
        public void Get_Option_By_Id_Success()
        {
            var options = new DbContextOptionsBuilder<BitcoinShowDBContext>()
                    .UseInMemoryDatabase(System.Guid.NewGuid().ToString())
                    .Options;
            var context = new BitcoinShowDBContext(options);
            var expected = new Option() { Text = "New option" };
            context.Options.Add(expected);
            context.SaveChanges();
            OptionRepository repository = new OptionRepository(context);

            var actual = repository.Get(expected.Id);
            Assert.NotNull(actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Update_Option_Without_Text_Error()
        {
            var options = new DbContextOptionsBuilder<BitcoinShowDBContext>()
                .UseInMemoryDatabase(System.Guid.NewGuid().ToString())
                .Options;
            var context = new BitcoinShowDBContext(options);
            context.Options.Add(new Option() { Text = "New option" });
            context.SaveChanges();

            OptionRepository repository = new OptionRepository(context);

            Option updatedOption = new Option();
            updatedOption.Id = 1;

            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => repository.Update(updatedOption));

            Assert.NotNull(ex);
            Assert.Equal(nameof(updatedOption.Text), ex.ParamName);
        }

        [Fact]
        public void Update_Option_NonExistent_Error()
        {
            var options = new DbContextOptionsBuilder<BitcoinShowDBContext>()
                .UseInMemoryDatabase(System.Guid.NewGuid().ToString())
                .Options;
            var context = new BitcoinShowDBContext(options);

            OptionRepository repository = new OptionRepository(context);

            Option updatedOption = new Option();
            updatedOption.Id = 1;
            updatedOption.Text = "Update";

            Exception ex = Assert.Throws<DbUpdateException>(() => repository.Update(updatedOption));

            Assert.NotNull(ex);
            Assert.Equal("The current option does not exists.", ex.Message);
        }

        [Fact]
        public void Update_Option_With_Text_Greater_Than_Max_Size_Error()
        {
            var options = new DbContextOptionsBuilder<BitcoinShowDBContext>()
                .UseInMemoryDatabase(System.Guid.NewGuid().ToString())
                .Options;
            var context = new BitcoinShowDBContext(options);
            var newOption = new Option() { Text = "New option" };
            context.Options.Add(newOption);
            context.SaveChanges();

            OptionRepository repository = new OptionRepository(context);

            Option updatedOption = new Option();
            updatedOption.Id = newOption.Id;
            updatedOption.Text = new String('B', 201);

            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(() => repository.Update(updatedOption));

            Assert.NotNull(ex);
            Assert.Equal(nameof(updatedOption.Text), ex.ParamName);
        }

        [Fact]
        public void Update_Option_Success()
        {
            var options = new DbContextOptionsBuilder<BitcoinShowDBContext>()
               .UseInMemoryDatabase(System.Guid.NewGuid().ToString())
               .Options;
            var context = new BitcoinShowDBContext(options);
            var newOption = new Option() { Text = "New option" };
            context.Options.Add(newOption);
            context.SaveChanges();

            OptionRepository repository = new OptionRepository(context);
            Option expected = new Option();
            expected.Id = newOption.Id;
            expected.Text = "Update option";

            Option actual = new Option();
            actual.Id = newOption.Id;
            actual.Text = "Update option";

            repository.Update(actual);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Delete_Option_Not_Found_Error()
        {
            var options = new DbContextOptionsBuilder<BitcoinShowDBContext>()
               .UseInMemoryDatabase(System.Guid.NewGuid().ToString())
               .Options;
            var context = new BitcoinShowDBContext(options);

            OptionRepository repository = new OptionRepository(context);

            Exception ex = Assert.Throws<DbUpdateException>(() => repository.Delete(1));

            Assert.NotNull(ex);
            Assert.Equal("The current option does not exists.", ex.Message);
        }

        [Fact]
        public void Delete_Option_Success()
        {
            var options = new DbContextOptionsBuilder<BitcoinShowDBContext>()
               .UseInMemoryDatabase(System.Guid.NewGuid().ToString())
               .Options;
            var context = new BitcoinShowDBContext(options);

            Option deleteOption = new Option();
            deleteOption.Text = "delete";
            context.Options.Add(deleteOption);
            context.SaveChanges();

            OptionRepository repository = new OptionRepository(context);

            repository.Delete(deleteOption.Id);
        }
    }
}