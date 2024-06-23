using Application.DTOs.UserDtos;
using Moq;
using Newtonsoft.Json;
using PresentationCreatorAPI.Application.Common.Exceptions;
using PresentationCreatorAPI.Application.Common.Utils;
using PresentationCreatorAPI.Application.DTOs.UserDtos;
using PresentationCreatorAPI.Application.Interfaces;
using PresentationCreatorAPI.Application.Services;
using PresentationCreatorAPI.Data.Interfaces;
using PresentationCreatorAPI.Domain.Entites;

#pragma warning disable
namespace PresentationCreatorAPI.Tests;

[TestFixture]
public class UserServiceTests
{
    private Mock<IUnitOfWork> _unitOfWorkMock;
    private Mock<IRedisService> _redisServiceMock;
    private UserService _userService;

    [SetUp]
    public void Setup()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _redisServiceMock = new Mock<IRedisService>();
        _userService = new UserService(_unitOfWorkMock.Object, _redisServiceMock.Object);
    }

    [Test]
    public async Task DeleteAsync_ShouldThrowBadRequestException_WhenDeletingAdminUser()
    {
        var userId = 1;

        Assert.ThrowsAsync<StatusCodeException>(async () => await _userService.DeleteAsync(userId));
    }

    [Test]
    public async Task DeleteAsync_ShouldThrowNotFoundException_WhenUserNotFound()
    {
        var userId = 100;
        _unitOfWorkMock.Setup(x => x.User.GetByIdIncludeAsync(userId)).ReturnsAsync((User)null);

        Assert.ThrowsAsync<StatusCodeException>(async () => await _userService.DeleteAsync(userId));
    }

    [Test]
    public async Task GetAllAsync_ShouldReturnSerializedUsers_WhenCacheIsNull()
    {
        var users = new List<User>
        {
            new User { Id = 1, FullName = "John"},
            new User { Id = 2, FullName = "Jane" }
        };
        _unitOfWorkMock.Setup(x => x.User.GetAllAsync()).ReturnsAsync(users);
        _redisServiceMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync((string)null);

        var result = await _userService.GetAllAsync(new PaginationParams());

        var expectedJson = JsonConvert.SerializeObject(users.Select(u => (UserDto)u), Formatting.Indented);
        Assert.AreEqual(expectedJson, result);
    }

    [Test]
    public async Task GetByIdAsync_ShouldThrowNotFoundException_WhenUserNotFound()
    {
        var userId = 100;
        _unitOfWorkMock.Setup(x => x.User.GetByIdIncludeAsync(userId)).ReturnsAsync((User)null);

        Assert.ThrowsAsync<StatusCodeException>(async () => await _userService.GetByIdAsync(userId));
    }

    [Test]
    public async Task GetByIdAsync_ShouldReturnUserDto_WhenUserFound()
    {
        var userId = 5;
        var user = new User { Id = userId, FullName = "John" };
        _unitOfWorkMock.Setup(x => x.User.GetByIdIncludeAsync(userId)).ReturnsAsync(user);

        var result = await _userService.GetByIdAsync(userId);

        Assert.AreEqual(userId, result.Id);
        Assert.AreEqual("John", result.FullName);
    }

    [Test]
    public async Task GetByPhoneNumberAsync_ShouldThrowNotFoundException_WhenUserNotFound()
    {
        var phoneNumber = "1234567890";
        _unitOfWorkMock.Setup(x => x.User.GetByPhoneNumberAsync(phoneNumber)).ReturnsAsync((User)null);

        Assert.ThrowsAsync<StatusCodeException>(async () => await _userService.GetByPhoneNumberAsync(phoneNumber));
    }

    [Test]
    public async Task GetByPhoneNumberAsync_ShouldReturnUserDto_WhenUserFound()
    {
        var phoneNumber = "1234567890";
        var user = new User { Id = 5, FullName = "John", PhoneNumber = phoneNumber };
        _unitOfWorkMock.Setup(x => x.User.GetByPhoneNumberAsync(phoneNumber)).ReturnsAsync(user);

        var result = await _userService.GetByPhoneNumberAsync(phoneNumber);

        Assert.AreEqual(5, result.Id);
        Assert.AreEqual("John", result.FullName);
    }

    [Test]
    public async Task GetUserAsync_ShouldThrowNotFoundException_WhenUserNotFound()
    {
        var userId = 100;
        _unitOfWorkMock.Setup(x => x.User.GetByIdIncludeAsync(userId)).ReturnsAsync((User)null);

        Assert.ThrowsAsync<StatusCodeException>(async () => await _userService.GetUserAsync(userId));
    }

    [Test]
    public async Task GetUserAsync_ShouldReturnUserDto_WhenUserFound()
    {
        var userId = 5;
        var user = new User { Id = userId, FullName = "John" };
        _unitOfWorkMock.Setup(x => x.User.GetByIdIncludeAsync(userId)).ReturnsAsync(user);

        var result = await _userService.GetUserAsync(userId);

        Assert.AreEqual(userId, result.Id);
        Assert.AreEqual("John", result.FullName);
    }

    [Test]
    public async Task UpdateAsync_ShouldThrowNotFoundException_WhenUserNotFound()
    {
        var userId = 100;
        var updateUserDto = new UpdateUserDto { FullName = "UpdatedJohn" };
        _unitOfWorkMock.Setup(x => x.User.GetByIdIncludeAsync(userId)).ReturnsAsync((User)null);

        Assert.ThrowsAsync<StatusCodeException>(async () => await _userService.UpdateAsync(userId, updateUserDto));
    }
}