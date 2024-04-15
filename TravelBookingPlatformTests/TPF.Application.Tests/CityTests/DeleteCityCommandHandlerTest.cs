namespace TravelBookingPlatform.Application.Tests.Commands.Cities;

public class DeleteCityCommandHandlerTests
{
    private readonly Mock<ICityRepository> _cityRepositoryMock = new Mock<ICityRepository>();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();

    [Fact]
    public async Task Handle_ValidCity_DeletesCity()
    {
        // Arrange
        var cityId = 1;
        var city = new City { Id = cityId, Name = "Test City" };

        _cityRepositoryMock.Setup(repo => repo.GetByIdAsync(cityId)).ReturnsAsync(city);

        var handler = new DeleteCityCommandHandler(_cityRepositoryMock.Object, _unitOfWorkMock.Object);

        // Act
        await handler.Handle(new DeleteCityCommand { CityId = cityId }, CancellationToken.None);

        // Assert
        _cityRepositoryMock.Verify(repo => repo.DeleteAsync(city), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_NonExistingCity_ThrowsEntityNotFoundException()
    {
        // Arrange
        var cityId = 1;

        _cityRepositoryMock.Setup(repo => repo.GetByIdAsync(cityId)).ReturnsAsync((City)null);

        var handler = new DeleteCityCommandHandler(_cityRepositoryMock.Object, _unitOfWorkMock.Object);

        // Act & Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(() => handler.Handle(new DeleteCityCommand { CityId = cityId }, CancellationToken.None));

        // Verify
        _cityRepositoryMock.Verify(repo => repo.DeleteAsync(It.IsAny<City>()), Times.Never);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Never);
    }
}
