using System;
using System.Threading.Tasks;
using BitcoinShow.Web.Models;
using BitcoinShow.Web.Repositories.Interface;
using BitcoinShow.Web.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;


namespace BitcoinShow.Test.Services
{
    public class OptionServiceTest
    {
        [Fact]
        public void Add_Option_Without_Text_Error()
        {
            Mock<IOptionRepository> mockRepository = new Mock<IOptionRepository>(MockBehavior.Strict);

            Option option = new Option();
            option.Text = null;
            mockRepository.Setup(s => s.Add(option))
                .Throws(new ArgumentNullException(nameof(option.Text)));

            option.Text = String.Empty;
            mockRepository.Setup(s => s.Add(option))
                .Throws(new ArgumentNullException(nameof(option.Text)));

            OptionService service = new OptionService(mockRepository.Object);

            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => service.Add(null));
            Assert.NotNull(ex);
            Assert.Equal(nameof(option.Text), ex.ParamName);

            ex = Assert.Throws<ArgumentNullException>(() => service.Add(String.Empty));
            Assert.NotNull(ex);
            Assert.Equal(nameof(option.Text), ex.ParamName);

            mockRepository.Verify(m => m.Add(It.IsAny<Option>()), Times.AtLeast(2));
        }

        [Fact]
        public void Add_Option_With_Text_Greater_Than_Max_Size_Error()
        {
            Mock<IOptionRepository> mockRepository = new Mock<IOptionRepository>(MockBehavior.Strict);

            Option option = new Option();
            option.Text = new String('A', 201);

            mockRepository.Setup(s => s.Add(option))
                .Throws(new ArgumentOutOfRangeException(nameof(option.Text)));

            OptionService service = new OptionService(mockRepository.Object);

            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(() => service.Add(option.Text));

            Assert.NotNull(ex);
            Assert.Equal(nameof(option.Text), ex.ParamName);
            mockRepository.Verify(m => m.Add(It.IsAny<Option>()), Times.Once());
        }

        [Fact]
        public void Add_Option_Success()
        {
            string text = "Add_Option_Success";
            Mock<IOptionRepository> mockRepository = new Mock<IOptionRepository>(MockBehavior.Strict);

            Option newOption = new Option();
            newOption.Text = text;

            mockRepository.Setup(s => s.Add(newOption))
                .Callback<Option>(o => o.Id = 1);

            Option expected = new Option();
            expected.Id = 1;
            expected.Text = text;

            OptionService service = new OptionService(mockRepository.Object);
            var actual = service.Add(text);

            Assert.NotNull(actual);
            Assert.Equal(expected, actual);
            mockRepository.Verify(m => m.Add(It.IsAny<Option>()), Times.Once());
        }

        [Fact]
        public void Get_Option_By_Id_Not_Found()
        {
            Mock<IOptionRepository> mockRepository = new Mock<IOptionRepository>(MockBehavior.Strict);

            Option option = null;
            mockRepository.Setup(s => s.Get(1)).Returns(option);

            OptionService service = new OptionService(mockRepository.Object);
            var actual = service.Get(1);

            Assert.Null(actual);
            mockRepository.Verify(m => m.Get(It.IsAny<int>()), Times.Once());
        }

        [Fact]
        public void Get_Option_By_Id_Success()
        {
            Mock<IOptionRepository> mockRepository = new Mock<IOptionRepository>(MockBehavior.Strict);

            mockRepository.Setup(s => s.Get(1)).Returns(new Option { Id = 1, Text = "Donald Trump" });

            OptionService service = new OptionService(mockRepository.Object);
            Option expected = new Option { Id = 1, Text = "Donald Trump" };

            var actual = service.Get(1);

            Assert.NotNull(actual);
            Assert.Equal(expected, actual);
            mockRepository.Verify(m => m.Get(It.IsAny<int>()), Times.Once());
        }

        [Fact]
        public void Update_Option_Without_Text_Error()
        {
            Mock<IOptionRepository> mockRepository = new Mock<IOptionRepository>(MockBehavior.Strict);

            Option option = new Option();
            option.Text = null;
            mockRepository.Setup(s => s.Update(option))
                .Throws(new ArgumentNullException(nameof(option.Text)));

            option.Text = String.Empty;
            mockRepository.Setup(s => s.Update(option))
                .Throws(new ArgumentNullException(nameof(option.Text)));

            OptionService service = new OptionService(mockRepository.Object);

            Option optionToUpdate = new Option { Text = String.Empty };

            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => service.Update(optionToUpdate));
            Assert.NotNull(ex);
            Assert.Equal(nameof(option.Text), ex.ParamName);

            optionToUpdate = new Option { Text = null };
            ex = Assert.Throws<ArgumentNullException>(() => service.Update(optionToUpdate));
            Assert.NotNull(ex);
            Assert.Equal(nameof(option.Text), ex.ParamName);

            mockRepository.Verify(m => m.Update(It.IsAny<Option>()), Times.AtLeast(2));
        }

        [Fact]
        public void Update_Option_NonExistent_Error()
        {
            Mock<IOptionRepository> mockRepository = new Mock<IOptionRepository>(MockBehavior.Strict);

            Option option666 = new Option { Id = 666, Text = "new text. 666" };
            mockRepository.Setup(s => s.Update(option666))
                .Throws(new DbUpdateException("The current option does not exist.", new NullReferenceException()));

            Option option = new Option { Text = "new text. option without id" };
            mockRepository.Setup(s => s.Update(option))
                .Throws(new DbUpdateException("The current option does not exist.", new NullReferenceException()));

            OptionService service = new OptionService(mockRepository.Object);

            Option optionToUpdate = new Option { Id = 666, Text = "new text. 666" };
            DbUpdateException ex = Assert.Throws<DbUpdateException>(() => service.Update(optionToUpdate));
            Assert.NotNull(ex);
            Assert.Equal("The current option does not exist.", ex.Message);

            optionToUpdate = new Option { Text = "new text. option without id" };
            ex = Assert.Throws<DbUpdateException>(() => service.Update(optionToUpdate));
            Assert.NotNull(ex);
            Assert.Equal("The current option does not exist.", ex.Message);

            mockRepository.Verify(m => m.Update(It.IsAny<Option>()), Times.AtLeast(2));
        }

        [Fact]
        public void Update_Option_With_Text_Greater_Than_Max_Size_Error()
        {
            Mock<IOptionRepository> mockRepository = new Mock<IOptionRepository>(MockBehavior.Strict);

            Option option = new Option { Id = 1, Text = new String('B', 201) };
            mockRepository.Setup(s => s.Update(option))
                .Throws(new ArgumentOutOfRangeException(nameof(option.Text)));

            OptionService service = new OptionService(mockRepository.Object);

            Option optionToUpdate = new Option { Id = 1, Text = new String('B', 201) };
            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(() => service.Update(optionToUpdate));
            Assert.NotNull(ex);
            Assert.Equal(nameof(option.Text), ex.ParamName);

            mockRepository.Verify(m => m.Update(It.IsAny<Option>()), Times.Once());
        }

        [Fact]
        public void Update_Option_Success()
        {
            Mock<IOptionRepository> mockRepository = new Mock<IOptionRepository>(MockBehavior.Strict);

            mockRepository.Setup(s => s.Update(new Option { Id = 1, Text = "Updated" }))
                .Callback<Option>(o => o.Text = "Updated");

            OptionService service = new OptionService(mockRepository.Object);

            var expected = new Option { Id = 1, Text = "Updated" };
            var actual = service.Update(new Option { Id = 1, Text = "Updated" });
            Assert.NotNull(actual);
            Assert.Equal(expected, actual);

            mockRepository.Verify(m => m.Update(It.IsAny<Option>()), Times.Once());
        }

        [Fact]
        public void Delete_Option_Not_Found_Error()
        {
            Mock<IOptionRepository> mockRepository = new Mock<IOptionRepository>(MockBehavior.Strict);

            mockRepository.Setup(s => s.Delete(666))
                .Throws(new DbUpdateException("The current option does not exist.", new NullReferenceException()));

            OptionService service = new OptionService(mockRepository.Object);

            DbUpdateException ex = Assert.Throws<DbUpdateException>(() => service.Delete(666));

            Assert.NotNull(ex);
            Assert.Equal("The current option does not exist.", ex.Message);

            mockRepository.Verify(m => m.Delete(It.IsAny<int>()), Times.Once());
        }

        [Fact]
        public void Delete_Option_Success()
        {
            Mock<IOptionRepository> mockRepository = new Mock<IOptionRepository>(MockBehavior.Loose);

            mockRepository.Setup(s => s.Delete(1));

            OptionService service = new OptionService(mockRepository.Object);

            service.Delete(1);

            mockRepository.Verify(m => m.Delete(It.IsAny<int>()), Times.Once());
        }
    }
}
