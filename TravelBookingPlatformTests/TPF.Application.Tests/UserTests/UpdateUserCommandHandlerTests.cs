namespace TravelBookingPlatform.Application.Tests.Commands.Users;

public class UpdateUserCommandHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IPasswordHashService> _passwordHashServiceMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly UpdateUserCommandHandler _handler;

    public UpdateUserCommandHandlerTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _mapperMock = new Mock<IMapper>();
        _passwordHashServiceMock = new Mock<IPasswordHashService>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new UpdateUserCommandHandler(
            _userRepositoryMock.Object,
            _mapperMock.Object,
            _passwordHashServiceMock.Object,
            _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_ValidRequest_ReturnsUserResponseDto()
    {
        // Arrange
        var updateUserDto = new UpdateUserDto
        {
            Username = "updatedUser",
            Email = "updated@example.com",
            Password = "newpassword123"
        };

        var user = new User
        {
            Id = 1,
            Username = "oldUser",
            Email = "old@example.com",
            PasswordHash = "oldPasswordHash"
        };

        var userResponseDto = new UserResponseDto
        {
            Id = 1,
            Username = "updatedUser",
            Email = "updated@example.com"
        };

        _userRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(user);
        _userRepositoryMock.Setup(repo => repo.IsUsernameTakenAsync(updateUserDto.Username))
                           .ReturnsAsync(false);
        _userRepositoryMock.Setup(repo => repo.IsEmailTakenAsync(updateUserDto.Email))
                           .ReturnsAsync(false);

        _mapperMock.Setup(mapper => mapper.Map(updateUserDto, user)).Returns(user);
        _passwordHashServiceMock.Setup(service => service.HashPassword(updateUserDto.Password))
                                .Returns("newPasswordHash");

        _userRepositoryMock.Setup(repo => repo.UpdateAsync(user)).Verifiable();
        _unitOfWorkMock.Setup(uow => uow.SaveChangesAsync()).Returns(Task.FromResult(0));
        _mapperMock.Setup(mapper => mapper.Map<UserResponseDto>(user)).Returns(userResponseDto);

        // Act
        var result = await _handler.Handle(new UpdateUserCommand { UserId = 1, UpdateUserDto = updateUserDto }, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(userResponseDto.Id, result.Id);
        Assert.Equal(userResponseDto.Username, result.Username);
        Assert.Equal(userResponseDto.Email, result.Email);

        // Verify
        _userRepositoryMock.Verify(repo => repo.UpdateAsync(user), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_NonExistentUser_ThrowsEntityNotFoundException()
    {
        // Arrange
        _userRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((User)null);

        // Act
        Func<Task> act = async () => await _handler.Handle(new UpdateUserCommand { UserId = 1, UpdateUserDto = new UpdateUserDto() }, CancellationToken.None);

        // Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(act);
    }

    [Fact]
    public async Task Handle_ExistingUsername_ThrowsDuplicateUsernameException()
    {
        // Arrange
        var updateUserDto = new UpdateUserDto { Username = "existingUser" };
        var existingUser = new User { Id = 1, Username = "otherUser" };
        _userRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(existingUser);
        _userRepositoryMock.Setup(repo => repo.IsUsernameTakenAsync(updateUserDto.Username))
                           .ReturnsAsync(true);

        // Act
        Func<Task> act = async () => await _handler.Handle(new UpdateUserCommand { UserId = 1, UpdateUserDto = updateUserDto }, CancellationToken.None);

        // Assert
        await Assert.ThrowsAsync<DuplicateUsernameException>(act);
    }

    [Fact]
    public async Task Handle_ExistingEmail_ThrowsDuplicateEmailException()
    {
        // Arrange
        var updateUserDto = new UpdateUserDto { Email = "existing@example.com" };
        var existingUser = new User { Id = 1, Email = "other@example.com" }; // Use a different email than the one being updated
        _userRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(existingUser);
        _userRepositoryMock.Setup(repo => repo.IsEmailTakenAsync(updateUserDto.Email))
                           .ReturnsAsync(true); // Simulate existing email

        // Act
        Func<Task> act = async () => await _handler.Handle(new UpdateUserCommand { UserId = 1, UpdateUserDto = updateUserDto }, CancellationToken.None);

        // Assert
        await Assert.ThrowsAsync<DuplicateEmailException>(act);
    }
}
