using Chat.BLL.Services;
using Chat.BLL.Services.Contracts;
using Chat.Common.Entities;
using Chat.DAL.Repositories.Contracts;
using Moq;
using System.Reflection.Metadata;

namespace Chat.BLL.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1Async()
        {

            // Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(a => a.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new User
            {
                Id = new Guid("3cd3b98a-65e5-4b29-868a-730c97439f58"),
                Name = "Mitya",
                Messages = new List<Message>()
            });
            var userService = new UserService(userRepositoryMock.Object);

            // Act
            var result = await userService.CheckIfExistsAsync(new Guid("3cd3b98a-65e5-4b29-868a-730c97439f58"));

            // Assert
            Assert.Equal(true, result);
        }
    }
}