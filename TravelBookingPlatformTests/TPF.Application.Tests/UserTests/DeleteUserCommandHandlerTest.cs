namespace TravelBookingPlatform.Application.Tests.Commands.Users;

public class DeleteUserCommandHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly DeleteUserCommandHandler _handler;

    public DeleteUserCommandHandlerTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new DeleteUserCommandHandler(
            _userRepositoryMock.Object,
            _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_ValidRequest_DeletesUser()
    {
        // Arrange
        var userId = 1;
        var user = new User { Id = userId };

        _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(user);

        // Act
        await _handler.Handle(new DeleteUserCommand { UserId = userId }, CancellationToken.None);

        // Assert
        _userRepositoryMock.Verify(repo => repo.DeleteAsync(user), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_NonExistentUser_ThrowsEntityNotFoundException()
    {
        // Arrange
        var userId = 1;
        _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync((User)null);

        // Assert
        Func<Task> act = async () => await _handler.Handle(new DeleteUserCommand { UserId = userId }, CancellationToken.None);

        // Act & Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(act);
    }
}
