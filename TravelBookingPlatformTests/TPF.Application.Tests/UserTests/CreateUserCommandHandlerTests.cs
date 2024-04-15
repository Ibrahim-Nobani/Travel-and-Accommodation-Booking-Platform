namespace TravelBookingPlatform.Application.Tests.Commands.Users;

public class CreateUserCommandHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IPasswordHashService> _passwordHashServiceMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly CreateUserCommandHandler _handler;

    public CreateUserCommandHandlerTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _mapperMock = new Mock<IMapper>();
        _passwordHashServiceMock = new Mock<IPasswordHashService>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new CreateUserCommandHandler(
            _userRepositoryMock.Object,
            _mapperMock.Object,
            _passwordHashServiceMock.Object,
            _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_ValidRequest_ReturnsUserResponseDto()
    {
        // Arrange
        var createUserDto = new CreateUserDto
        {
            Username = "testuser",
            Email = "test@example.com",
            Password = "password123"
        };

        var user = new User { Username = "testuser", Email = "test@example.com", RoleId = (int)UserRoles.User };
        var userResponseDto = new UserResponseDto { Username = "testuser", Email = "test@example.com" };

        _userRepositoryMock.Setup(repo => repo.IsUsernameTakenAsync(createUserDto.Username))
                           .ReturnsAsync(false);
        _userRepositoryMock.Setup(repo => repo.IsEmailTakenAsync(createUserDto.Email))
                           .ReturnsAsync(false);
        _mapperMock.Setup(mapper => mapper.Map<User>(createUserDto))
                   .Returns(user);
        _unitOfWorkMock.Setup(uow => uow.SaveChangesAsync()).Returns(Task.FromResult(0));
        _mapperMock.Setup(mapper => mapper.Map<UserResponseDto>(user))
                   .Returns(userResponseDto);
        _passwordHashServiceMock.Setup(service => service.HashPassword(createUserDto.Password))
                                .Returns("hashedpassword123");

        // Act
        var result = await _handler.Handle(new CreateUserCommand { CreateUserDto = createUserDto }, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("testuser", result.Username);
        Assert.Equal("test@example.com", result.Email);

        // Verify
        _userRepositoryMock.Verify(repo => repo.AddAsync(user), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ExistingUsername_ThrowsDuplicateUsernameException()
    {
        // Arrange
        var createUserDto = new CreateUserDto { Username = "user1", Email = "test@example.com", Password = "password123" };
        _userRepositoryMock.Setup(repo => repo.IsUsernameTakenAsync(createUserDto.Username))
                           .ReturnsAsync(true);

        // Act
        Func<Task> act = async () => await _handler.Handle(new CreateUserCommand { CreateUserDto = createUserDto }, CancellationToken.None);

        // Assert
        await Assert.ThrowsAsync<DuplicateUsernameException>(act);
    }

    [Fact]
    public async Task Handle_ExistingEmail_ThrowsDuplicateEmailException()
    {
        // Arrange
        var createUserDto = new CreateUserDto { Username = "testuser", Email = "user177k@example.com", Password = "password123" };
        _userRepositoryMock.Setup(repo => repo.IsUsernameTakenAsync(createUserDto.Username))
                           .ReturnsAsync(false);
        _userRepositoryMock.Setup(repo => repo.IsEmailTakenAsync(createUserDto.Email))
                           .ReturnsAsync(true);

        // Act
        Func<Task> act = async () => await _handler.Handle(new CreateUserCommand { CreateUserDto = createUserDto }, CancellationToken.None);

        // Assert
        await Assert.ThrowsAsync<DuplicateEmailException>(act);
    }

}
