namespace TravelBookingPlatform.Application.Tests.Queries.Bookings;

public class GetAllBookingsQueryHandlerTests
{
    private readonly Mock<IBookingRepository> _bookingRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetAllBookingsQueryHandler _handler;

    public GetAllBookingsQueryHandlerTests()
    {
        _bookingRepositoryMock = new Mock<IBookingRepository>();
        _mapperMock = new Mock<IMapper>();
        _handler = new GetAllBookingsQueryHandler(
            _bookingRepositoryMock.Object,
            _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_BookingsExist_ReturnsBookingDtos()
    {
        // Arrange
        var bookings = new List<Booking>
        {
            new Booking { Id = 1, RoomId = 1 },
            new Booking { Id = 2, RoomId = 2 }
        };

        var bookingDtos = new List<BookingDto>
        {
            new BookingDto { Id = 1, RoomId = 1 },
            new BookingDto { Id = 2, RoomId = 2 }
        };

        _bookingRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(bookings);
        _mapperMock.Setup(mapper => mapper.Map<List<BookingDto>>(bookings)).Returns(bookingDtos);

        // Act
        var result = await _handler.Handle(new GetAllBookingsQuery(), CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Collection(result,
            item =>
            {
                Assert.Equal(1, item.Id);
                Assert.Equal(1, item.RoomId);
            },
            item =>
            {
                Assert.Equal(2, item.Id);
                Assert.Equal(2, item.RoomId);
            });

        // Verify
        _bookingRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_NoBookingsExist_ThrowsEntityNotFoundException()
    {
        // Arrange
        _bookingRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Booking>());

        // Act & Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(() => _handler.Handle(new GetAllBookingsQuery(), CancellationToken.None));
    }
}
