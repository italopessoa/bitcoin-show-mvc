using BitcoinShow.Web.Models;
using BitcoinShow.Web.Repositories;
using BitcoinShow.Web.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace BitcoinShow.Test.Repositories
{
    public class OptionRepositoryTest
    {
        [Fact]
        public void Add_Option_Without_Text_Error2()
        {
            // var mockSet = new Mock<DbSet<Option>>(); 
 
            // var mockContext = new Mock<BitcoinShowDBContext>(); 
            // mockContext.Setup(m => m.Options).Returns(mockSet.Object); 
 
            // Option newOption = new Option();
            // newOption.Text = "teste";
            // OptionRepository repository = new OptionRepository(mockContext.Object);            
            // repository.Add(newOption); 
 
            // mockSet.Verify(m => m.Add(It.IsAny<Option>()), Times.Once()); 
            // mockContext.Verify(m => m.SaveChanges(), Times.Once()); 

            var options = new DbContextOptionsBuilder<BitcoinShowDBContext>()
                .UseInMemoryDatabase(System.Guid.NewGuid().ToString())
                .Options;
            var context = new BitcoinShowDBContext(options);
            OptionRepository repository = new OptionRepository(context);
            
            Option expected = new Option();
            expected.Text = "teste";
            expected.Id = 1;

            Option actual = new Option();
            actual.Text = "teste";

            repository.Add(actual);

            Assert.True(actual.Id > 0, "The actual Id is not greater than 0");
            Assert.Equal(expected,actual);
        }

        [Fact]
        public void Add_Option_Without_Text_Error()
        {
            throw new System.NotImplementedException();
        }

        [Fact]
        public void Add_Option_With_Text_Greater_Than_Max_Size_Error()
        {
            throw new System.NotImplementedException();
        }

        [Fact]
        public void Add_Option_With_Text_Repeated_Letter_Error()
        {
            throw new System.NotImplementedException();
        }

        [Fact]
        public void Add_Option_Success()
        {
            throw new System.NotImplementedException();
        }

        [Fact]
        public void Update_Option_Without_Text_Error()
        {
            throw new System.NotImplementedException();
        }

        [Fact]
        public void Update_Option_With_Text_Repeated_Letter_Error()
        {
            throw new System.NotImplementedException();
        }

        [Fact]
        public void Update_Option_Success()
        {
            throw new System.NotImplementedException();
        }

        [Fact]
        public void Delete_Option_Not_Found_Error()
        {
            throw new System.NotImplementedException();
        }

        [Fact]
        public void Delete_Option_Success()
        {
            throw new System.NotImplementedException();
        }
    }
}