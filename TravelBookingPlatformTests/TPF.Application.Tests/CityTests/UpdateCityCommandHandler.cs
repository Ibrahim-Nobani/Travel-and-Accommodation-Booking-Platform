namespace TravelBookingPlatform.Application.Tests.Commands.Cities;

public class UpdateCityCommandHandlerTests
{
    private readonly Mock<ICityRepository> _cityRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly UpdateCityCommandHandler _handler;

    public UpdateCityCommandHandlerTests()
    {
        _cityRepositoryMock = new Mock<ICityRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _handler = new UpdateCityCommandHandler(_cityRepositoryMock.Object, _unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ValidRequest_ReturnsCityDto()
    {
        // Arrange
        var updateCityDto = new UpdateCityDto
        {
            Name = "Updated City",
            Country = "Updated Country",
            ThumbnailImageUrl = "updated.jpg",
            PostOffice = "Updated Post Office"
        };

        var city = new City
        {
            Id = 1,
            Name = "Old City",
            Country = "Old Country",
            ThumbnailImageUrl = "old.jpg",
            PostOffice = "Old Post Office",
        };

        var cityDto = new CityDto
        {
            Id = 1,
            Name = "Updated City",
            Country = "Updated Country",
            ThumbnailImageUrl = "updated.jpg",
            PostOffice = "Updated Post Office"
        };

        _cityRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(city);
        _mapperMock.Setup(mapper => mapper.Map(updateCityDto, city)).Returns(city);
        _cityRepositoryMock.Setup(repo => repo.UpdateAsync(city)).Verifiable();
        _unitOfWorkMock.Setup(uow => uow.SaveChangesAsync()).Returns(Task.FromResult(0));
        _mapperMock.Setup(mapper => mapper.Map<CityDto>(city)).Returns(cityDto);

        // Act
        var result = await _handler.Handle(new UpdateCityCommand { CityId = 1, UpdateCityDto = updateCityDto }, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(cityDto, result);

        // Verify
        _cityRepositoryMock.Verify(repo => repo.UpdateAsync(city), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_NonExistingCity_ThrowsEntityNotFoundException()
    {
        // Arrange
        var cityId = 1;
        var updateCityDto = new UpdateCityDto { Name = "Updated City Name" };

        _cityRepositoryMock.Setup(repo => repo.GetByIdAsync(cityId)).ReturnsAsync((City)null);

        // Act & Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(() => _handler.Handle(new UpdateCityCommand { CityId = cityId, UpdateCityDto = updateCityDto }, CancellationToken.None));

        // Verify
        _cityRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<City>()), Times.Never);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Never);
    }
}
