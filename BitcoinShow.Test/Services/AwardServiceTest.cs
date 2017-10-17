using System;
using System.Collections.Generic;
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

            mockRepository.Verify(r => r.Add(3, 1, 2, LevelEnum.Easy), Times.Never());
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

            mockRepository.Verify(r => r.Add(3, 1, 2, LevelEnum.Easy), Times.Never());
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

            mockRepository.Verify(r => r.Add(3, 1, 2, LevelEnum.Easy), Times.Never());
        }

        [Fact]
        public void Add_Award_SuccessValue_Minor_than_FailValue_Error()
        {
            Mock<IAwardRepository> mockRepository = new Mock<IAwardRepository>(MockBehavior.Strict);
            IAwardService service = new AwardService(mockRepository.Object);

            ArgumentException ex = Assert.Throws<ArgumentException>(() => service.Add(2, 3, 1, LevelEnum.Easy));

            Assert.NotNull(ex);
            Assert.Equal("successValue can't be minor than failValue.", ex.Message);

            mockRepository.Verify(r => r.Add(3, 1, 2, LevelEnum.Easy), Times.Never());
        }

        [Fact]
        public void Add_Award_SuccessValue_Minor_than_QuitValue_Error()
        {
            Mock<IAwardRepository> mockRepository = new Mock<IAwardRepository>(MockBehavior.Strict);
            IAwardService service = new AwardService(mockRepository.Object);

            ArgumentException ex = Assert.Throws<ArgumentException>(() => service.Add(2, 1, 3, LevelEnum.Easy));

            Assert.NotNull(ex);
            Assert.Equal("successValue can't be minor than quitValue.", ex.Message);

            mockRepository.Verify(r => r.Add(3, 1, 2, LevelEnum.Easy), Times.Never());
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

            mockRepository.Verify(r => r.Add(3, 1, 2, LevelEnum.Easy), Times.Never());
        }

        [Fact]
        public void Add_Award_QuitValue_Minor_than_FailValue_Error()
        {
            Mock<IAwardRepository> mockRepository = new Mock<IAwardRepository>(MockBehavior.Strict);
            IAwardService service = new AwardService(mockRepository.Object);

            ArgumentException ex = Assert.Throws<ArgumentException>(() => service.Add(3, 2, 1, LevelEnum.Easy));
            Assert.NotNull(ex);
            Assert.Equal("quitValue can't be minor than failValue.", ex.Message);

            mockRepository.Verify(r => r.Add(3, 1, 2, LevelEnum.Easy), Times.Never());
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
            mockRepository.Verify(r => r.Add(3, 1, 2, LevelEnum.Easy), Times.Once());
            Assert.Equal(expected, actual);

        }

        [Fact]
        public void Update_Award_SuccessValue_Minor_or_Equal_Zero_Error()
        {
            Mock<IAwardRepository> mockRepository = new Mock<IAwardRepository>(MockBehavior.Strict);
            IAwardService service = new AwardService(mockRepository.Object);

            var award = new Award
            {
                Id = 1,
                Success = -1,
                Fail = 0,
                Quit = 0,
                Level = LevelEnum.Easy
            };

            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(() => service.Update(award));

            Assert.NotNull(ex);
            Assert.Equal("successValue", ex.ParamName);
            Assert.Equal(-1M, ex.ActualValue);
            Assert.Equal($"The value must be greater than zero.\r\nParameter name: {"successValue"}\r\nActual value was {-1M}.", ex.Message);

            award.Success = 0;
            ex = Assert.Throws<ArgumentOutOfRangeException>(() => service.Update(award));

            Assert.NotNull(ex);
            Assert.Equal("successValue", ex.ParamName);
            Assert.Equal(0M, ex.ActualValue);
            Assert.Equal($"The value must be greater than zero.\r\nParameter name: {"successValue"}\r\nActual value was {0M}.", ex.Message);

            mockRepository.Verify(r => r.Update(It.IsAny<Award>()), Times.Never());
        }

        [Fact]
        public void Update_Award_FailValue_Minor_than_Zero_Error()
        {
            Mock<IAwardRepository> mockRepository = new Mock<IAwardRepository>(MockBehavior.Strict);
            IAwardService service = new AwardService(mockRepository.Object);

            var award = new Award
            {
                Id = 1,
                Success = 1,
                Fail = -1,
                Quit = 0,
                Level = LevelEnum.Easy
            };

            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(() => service.Update(award));

            Assert.NotNull(ex);
            Assert.Equal("failValue", ex.ParamName);
            Assert.Equal(-1M, ex.ActualValue);
            Assert.Equal($"The value must be greater than or equal to zero.\r\nParameter name: {"failValue"}\r\nActual value was {-1M}.", ex.Message);

            mockRepository.Verify(r => r.Update(It.IsAny<Award>()), Times.Never());
        }

        [Fact]
        public void Update_Award_QuitValue_Minor_than_Zero_Error()
        {
            Mock<IAwardRepository> mockRepository = new Mock<IAwardRepository>(MockBehavior.Strict);
            IAwardService service = new AwardService(mockRepository.Object);

            var award = new Award
            {
                Id = 1,
                Success = 1,
                Fail = 1,
                Quit = -1,
                Level = LevelEnum.Easy
            };

            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(() => service.Update(award));

            Assert.NotNull(ex);
            Assert.Equal("quitValue", ex.ParamName);
            Assert.Equal(-1M, ex.ActualValue);
            Assert.Equal($"The value must be greater than or equal to zero.\r\nParameter name: {"quitValue"}\r\nActual value was {-1M}.", ex.Message);

            mockRepository.Verify(r => r.Update(It.IsAny<Award>()), Times.Never());
        }

        [Fact]
        public void Update_Award_SuccessValue_Minor_than_FailValue_Error()
        {
            Mock<IAwardRepository> mockRepository = new Mock<IAwardRepository>(MockBehavior.Strict);
            IAwardService service = new AwardService(mockRepository.Object);

            var award = new Award
            {
                Id = 1,
                Success = 2,
                Fail = 3,
                Quit = 1,
                Level = LevelEnum.Easy
            };

            ArgumentException ex = Assert.Throws<ArgumentException>(() => service.Update(award));

            Assert.NotNull(ex);
            Assert.Equal("successValue can't be minor than failValue.", ex.Message);

            mockRepository.Verify(r => r.Update(It.IsAny<Award>()), Times.Never());
        }

        [Fact]
        public void Update_Award_SuccessValue_Minor_than_QuitValue_Error()
        {
            Mock<IAwardRepository> mockRepository = new Mock<IAwardRepository>(MockBehavior.Strict);
            IAwardService service = new AwardService(mockRepository.Object);

            var award = new Award
            {
                Id = 1,
                Success = 2,
                Fail = 1,
                Quit = 3,
                Level = LevelEnum.Easy
            };

            ArgumentException ex = Assert.Throws<ArgumentException>(() => service.Update(award));

            Assert.NotNull(ex);
            Assert.Equal("successValue can't be minor than quitValue.", ex.Message);

            mockRepository.Verify(r => r.Update(It.IsAny<Award>()), Times.Never());
        }

        [Fact]
        public void Update_Award_Equal_Values_Error()
        {
            Mock<IAwardRepository> mockRepository = new Mock<IAwardRepository>(MockBehavior.Strict);
            IAwardService service = new AwardService(mockRepository.Object);

            var award = new Award
            {
                Id = 1,
                Success = 1,
                Fail = 1,
                Quit = 2,
                Level = LevelEnum.Easy
            };

            ArgumentException ex = Assert.Throws<ArgumentException>(() => service.Update(award));
            Assert.NotNull(ex);
            Assert.Equal("Award values can't be equal.", ex.Message);

            award.Success = 1;
            award.Fail = 2;
            award.Quit = 1;

            ex = Assert.Throws<ArgumentException>(() => service.Update(award));
            Assert.NotNull(ex);
            Assert.Equal("Award values can't be equal.", ex.Message);

            award.Success = 2;
            award.Fail = 1;
            award.Quit = 1;

            ex = Assert.Throws<ArgumentException>(() => service.Update(award));
            Assert.NotNull(ex);
            Assert.Equal("Award values can't be equal.", ex.Message);

            mockRepository.Verify(r => r.Add(3, 1, 2, LevelEnum.Easy), Times.Never());
        }

        [Fact]
        public void Update_Award_QuitValue_Minor_than_FailValue_Error()
        {
            Mock<IAwardRepository> mockRepository = new Mock<IAwardRepository>(MockBehavior.Strict);
            IAwardService service = new AwardService(mockRepository.Object);

            var award = new Award
            {
                Id = 1,
                Success = 3,
                Fail = 2,
                Quit = 1,
                Level = LevelEnum.Easy
            };

            ArgumentException ex = Assert.Throws<ArgumentException>(() => service.Update(award));
            Assert.NotNull(ex);
            Assert.Equal("quitValue can't be minor than failValue.", ex.Message);

            mockRepository.Verify(r => r.Update(It.IsAny<Award>()), Times.Never());
        }

        [Fact]
        public void Update_Award_Success()
        {
            var award = new Award
            {
                Id = 1,
                Success = 3,
                Fail = 1,
                Quit = 2,
                Level = LevelEnum.Easy
            };

            Mock<IAwardRepository> mockRepository = new Mock<IAwardRepository>(MockBehavior.Strict);
            mockRepository.Setup(s => s.Update(award));

            IAwardService service = new AwardService(mockRepository.Object);

            service.Update(award);

            mockRepository.Verify(r => r.Update(It.IsAny<Award>()), Times.Once());
        }

        [Fact]
        public void GetAll_Award_Success()
        {
            var expected = new List<Award>
            {
                new Award { Id = 1, Success = 3, Fail = 1, Quit = 2, Level = LevelEnum.Hard },
                new Award { Id = 2, Success = 4, Fail = 2, Quit = 3, Level = LevelEnum.Medium }
            };
            Mock<IAwardRepository> mockRepository = new Mock<IAwardRepository>(MockBehavior.Strict);
            mockRepository.Setup(s => s.GetAll()).Returns(expected);

            IAwardService service = new AwardService(mockRepository.Object);
            List<Award> actual = service.GetAll();

            Assert.Equal(expected, actual);

            mockRepository.Verify(r => r.GetAll(), Times.Once());
        }

        [Fact]
        public void Get_Award_Success()
        {
            var expected = new Award
            {
                Id = 1,
                Success = 3,
                Fail = 1,
                Quit = 2,
                Level = LevelEnum.Hard
            };
            Award expectedNull = null;
            Mock<IAwardRepository> mockRepository = new Mock<IAwardRepository>(MockBehavior.Strict);
            mockRepository.Setup(s => s.Get(1)).Returns(expected);
            mockRepository.Setup(s => s.Get(0)).Returns(expectedNull);

            IAwardService service = new AwardService(mockRepository.Object);
            Award actual = service.Get(1);

            Assert.Equal(expected, actual);
            Assert.Null(service.Get(0));

            mockRepository.Verify(r => r.Get(1), Times.Once());
            mockRepository.Verify(r => r.Get(0), Times.Once());
        }

        [Fact]
        public void Delete_Award_Success()
        {
            Award notFoundAward = null;
            Mock<IAwardRepository> mockRepository = new Mock<IAwardRepository>(MockBehavior.Strict);
            mockRepository.Setup(r => r.Delete(0)).Throws(new InvalidOperationException("There's no Award with ID value equal to 0"));
            mockRepository.Setup(r => r.Delete(1));
            mockRepository.Setup(r => r.Get(0)).Returns(notFoundAward);
            mockRepository.Setup(r => r.Get(1)).Returns(new Award());

            IAwardService service = new AwardService(mockRepository.Object);
            service.Delete(1);
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => service.Delete(0));
            Assert.NotNull(ex);
            Assert.Equal(ex.Message, "There's no Award with ID value equal to 0");

            mockRepository.Verify(r => r.Delete(1), Times.Once());

            mockRepository.Verify(r => r.Get(It.IsAny<int>()), Times.AtLeast(2));
            mockRepository.Verify(r => r.Delete(0), Times.Never());
            mockRepository.Verify(r => r.Delete(1), Times.Once());
        }
    }
}
