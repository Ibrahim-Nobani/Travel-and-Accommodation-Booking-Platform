namespace TravelBookingPlatform.Application.Tests.Commands.Cities;


public class CreateCityCommandHandlerTests
{
    private readonly Mock<ICityRepository> _cityRepositoryMock = new Mock<ICityRepository>();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();
    private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();

    [Fact]
    public async Task Handle_ValidCity_ReturnsCreatedCityDto()
    {
        // Arrange
        var createCityDto = new CreateCityDto { Name = "Test City", Country = "Test Country", PostOffice = "Test Post Office", ThumbnailImageUrl = "test.jpg" };
        var city = new City { Name = "Test City", Country = "Test Country", PostOffice = "Test Post Office", ThumbnailImageUrl = "test.jpg" };
        var cityDto = new CityDto { Id = 1, Name = "Test City", Country = "Test Country", PostOffice = "Test Post Office", ThumbnailImageUrl = "test.jpg" };

        _mapperMock.Setup(mapper => mapper.Map<City>(createCityDto)).Returns(city);
        _mapperMock.Setup(mapper => mapper.Map<CityDto>(city)).Returns(cityDto);

        var handler = new CreateCityCommandHandler(_cityRepositoryMock.Object, _unitOfWorkMock.Object, _mapperMock.Object);

        // Act
        var result = await handler.Handle(new CreateCityCommand { CreateCityDto = createCityDto }, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<CityDto>(result);
        Assert.Equal(cityDto, result);

        // Verify
        _cityRepositoryMock.Verify(repo => repo.AddAsync(city), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Once);
    }
}
