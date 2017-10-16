using System;
using BitcoinShow.Web.Models;
using BitcoinShow.Web.Repositories.Interface;
using BitcoinShow.Web.Services;
using BitcoinShow.Web.Services.Interface;
using Moq;
using Xunit;

namespace BitcoinShow.Test.Services
{
    public class AwardServiceTest
    {
        [Fact]
        public void Add_Award_SuccessValue_Minor_or_Equal_Zero_Error()
        {
            Mock<IAwardRepository> mockRepository = new Mock<IAwardRepository>(MockBehavior.Strict);
            IAwardService service = new AwardService(mockRepository.Object);

            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(() => service.Add(-1, 0, 0, LevelEnum.Easy));

            Assert.NotNull(ex);
            Assert.Equal("successValue", ex.ParamName);
            Assert.Equal(-1M, ex.ActualValue);
            Assert.Equal($"The value must be greater than zero.\r\nParameter name: {"successValue"}\r\nActual value was {-1M}.", ex.Message);

            ex = Assert.Throws<ArgumentOutOfRangeException>(() => service.Add(0, 0, 0, LevelEnum.Easy));

            Assert.NotNull(ex);
            Assert.Equal("successValue", ex.ParamName);
            Assert.Equal(0M, ex.ActualValue);
            Assert.Equal($"The value must be greater than zero.\r\nParameter name: {"successValue"}\r\nActual value was {0M}.", ex.Message);
            
            mockRepository.Verify(r => r.Add(3,1,2, LevelEnum.Easy),Times.Never());
        }

        [Fact]
        public void Add_Award_FailValue_Minor_than_Zero_Error()
        {
            Mock<IAwardRepository> mockRepository = new Mock<IAwardRepository>(MockBehavior.Strict);
            IAwardService service = new AwardService(mockRepository.Object);

            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(() => service.Add(1, -1, 0, LevelEnum.Easy));

            Assert.NotNull(ex);
            Assert.Equal("failValue", ex.ParamName);
            Assert.Equal(-1M, ex.ActualValue);
            Assert.Equal($"The value must be greater than or equal to zero.\r\nParameter name: {"failValue"}\r\nActual value was {-1M}.", ex.Message); 

            mockRepository.Verify(r => r.Add(3,1,2, LevelEnum.Easy),Times.Never());
        }

        [Fact]
        public void Add_Award_QuitValue_Minor_than_Zero_Error()
        {
            Mock<IAwardRepository> mockRepository = new Mock<IAwardRepository>(MockBehavior.Strict);
            IAwardService service = new AwardService(mockRepository.Object);

            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(() => service.Add(1, 1, -1, LevelEnum.Easy));

            Assert.NotNull(ex);
            Assert.Equal("quitValue", ex.ParamName);
            Assert.Equal(-1M, ex.ActualValue);
            Assert.Equal($"The value must be greater than or equal to zero.\r\nParameter name: {"quitValue"}\r\nActual value was {-1M}.", ex.Message);

            mockRepository.Verify(r => r.Add(3,1,2, LevelEnum.Easy),Times.Never());
        }

        [Fact]
        public void Add_Award_SuccessValue_Minor_than_FailValue_Error()
        {
            Mock<IAwardRepository> mockRepository = new Mock<IAwardRepository>(MockBehavior.Strict);
            IAwardService service = new AwardService(mockRepository.Object);

            ArgumentException ex = Assert.Throws<ArgumentException>(() => service.Add(2, 3, 1, LevelEnum.Easy));

            Assert.NotNull(ex);
            Assert.Equal("successValue can't be minor than failValue.", ex.Message);

            mockRepository.Verify(r => r.Add(3,1,2, LevelEnum.Easy),Times.Never());
        }

        [Fact]
        public void Add_Award_SuccessValue_Minor_than_QuitValue_Error()
        {
            Mock<IAwardRepository> mockRepository = new Mock<IAwardRepository>(MockBehavior.Strict);
            IAwardService service = new AwardService(mockRepository.Object);

            ArgumentException ex = Assert.Throws<ArgumentException>(() => service.Add(2, 1, 3, LevelEnum.Easy));

            Assert.NotNull(ex);
            Assert.Equal("successValue can't be minor than quitValue.", ex.Message);

            mockRepository.Verify(r => r.Add(3,1,2, LevelEnum.Easy),Times.Never());
        }

        [Fact]
        public void Add_Award_Equal_Values_Error()
        {
            Mock<IAwardRepository> mockRepository = new Mock<IAwardRepository>(MockBehavior.Strict);
            IAwardService service = new AwardService(mockRepository.Object);

            ArgumentException ex = Assert.Throws<ArgumentException>(() => service.Add(1, 1, 2, LevelEnum.Easy));
            Assert.NotNull(ex);
            Assert.Equal("Award values can't be equal.", ex.Message); 

            ex = Assert.Throws<ArgumentException>(() => service.Add(1, 2, 1, LevelEnum.Easy));
            Assert.NotNull(ex);
            Assert.Equal("Award values can't be equal.", ex.Message); 

            ex = Assert.Throws<ArgumentException>(() => service.Add(2, 1, 1, LevelEnum.Easy));
            Assert.NotNull(ex);
            Assert.Equal("Award values can't be equal.", ex.Message);

            mockRepository.Verify(r => r.Add(3,1,2, LevelEnum.Easy),Times.Never());
        }
        
        [Fact]
        public void Add_Award_QuitValue_Minor_than_FailValue_Error()
        {
            Mock<IAwardRepository> mockRepository = new Mock<IAwardRepository>(MockBehavior.Strict);
            IAwardService service = new AwardService(mockRepository.Object);

            ArgumentException ex = Assert.Throws<ArgumentException>(() => service.Add(3, 2, 1, LevelEnum.Easy));
            Assert.NotNull(ex);
            Assert.Equal("quitValue can't be minor than failValue.", ex.Message);

            mockRepository.Verify(r => r.Add(3,1,2, LevelEnum.Easy),Times.Never());
        }

        [Fact]
        public void Add_Award_Success()
        {
            var expected = new Award
            {
                Id = 1,
                Success = 3,
                Fail = 1,
                Quit = 2,
                Level = LevelEnum.Easy
            };

            Mock<IAwardRepository> mockRepository = new Mock<IAwardRepository>(MockBehavior.Strict);
            mockRepository.Setup(s => s.Add(3, 1, 2, LevelEnum.Easy)).Returns(expected);
            IAwardService service = new AwardService(mockRepository.Object);

            Award actual = service.Add(3, 1, 2, LevelEnum.Easy);
            Assert.Equal(expected,actual);

            mockRepository.Verify(r => r.Add(3,1,2, LevelEnum.Easy),Times.Once());
        }

        [Fact]
        public void Update_Award_SuccessValue_Minor_or_Equal_Zero_Error()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void Update_Award_FailValue_Minor_than_Zero_Error()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void Update_Award_QuitValue_Minor_than_Zero_Error()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void Update_Award_SuccessValue_Minor_than_FailValue_Error()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void Update_Award_SuccessValue_Minor_than_QuitValue_Error()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void Update_Award_Equal_Values_Error()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void Update_Award_QuitValue_Minor_than_FailValue_Error()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void Update_Award_Success()
        {
            throw new NotImplementedException();
        }
    }
}
