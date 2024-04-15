namespace TravelBookingPlatform.Application.Tests.Queries.Hotels;

public class SearchHotelsQueryHandlerTests
{
    private readonly Mock<IHotelRepository> _mockHotelRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly SearchHotelsQueryHandler _handler;

    public SearchHotelsQueryHandlerTests()
    {
        _mockHotelRepository = new Mock<IHotelRepository>();
        _mockMapper = new Mock<IMapper>();
        _handler = new SearchHotelsQueryHandler(_mockHotelRepository.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task Handle_ValidSearch_ReturnsHotelDtos()
    {
        // Arrange
        var hotels = new List<Hotel>
            {
                new Hotel { Name = "Hotel 1", StarRating = 4, Location = "Location 1", City = new City { Name = "City 1" }, Owner = "Owner 1" },
                new Hotel { Name = "Hotel 2", StarRating = 5, Location = "Location 2", City = new City { Name = "City 2" }, Owner = "Owner 2" }
            };

        var searchCriteria = new SearchHotelCriteria
        {
            Name = "Hotel",
            StarRating = 4,
            Location = "Location",
            CityName = "City",
            Owner = "Owner"
        };

        _mockHotelRepository.Setup(repo => repo.SearchAsync(searchCriteria)).ReturnsAsync(hotels);

        var expectedHotelDtos = hotels.Select(hotel => new HotelDto
        {
            Name = hotel.Name,
            StarRating = hotel.StarRating,
            Location = hotel.Location,
            Owner = hotel.Owner
        }).ToList();

        _mockMapper.Setup(mapper => mapper.Map<List<HotelDto>>(hotels)).Returns(expectedHotelDtos);

        var query = new SearchHotelsQuery { SearchHotel = searchCriteria };

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedHotelDtos, result);
    }

    [Fact]
    public async Task Handle_NoHotelsFound_ThrowsEntityNotFoundException()
    {
        // Arrange
        _mockHotelRepository.Setup(repo => repo.SearchAsync(It.IsAny<SearchHotelCriteria>())).ReturnsAsync(new List<Hotel>());

        var query = new SearchHotelsQuery { SearchHotel = new SearchHotelCriteria() };

        // Act & Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(() => _handler.Handle(query, CancellationToken.None));
    }
}