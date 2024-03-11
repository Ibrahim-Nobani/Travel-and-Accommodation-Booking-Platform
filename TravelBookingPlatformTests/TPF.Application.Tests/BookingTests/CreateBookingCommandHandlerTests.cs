namespace TravelBookingPlatform.Application.Tests.Commands.Bookings;

public class CreateBookingCommandHandlerTests
{
    private readonly Mock<IBookingRepository> _bookingRepositoryMock;
    private readonly Mock<IRoomRepository> _roomRepositoryMock;
    private readonly Mock<IPricingService> _pricingServiceMock;
    private readonly Mock<IRoomAvailabilityService> _roomAvailabilityServiceMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly CreateBookingCommandHandler _handler;

    public CreateBookingCommandHandlerTests()
    {
        _bookingRepositoryMock = new Mock<IBookingRepository>();
        _roomRepositoryMock = new Mock<IRoomRepository>();
        _pricingServiceMock = new Mock<IPricingService>();
        _roomAvailabilityServiceMock = new Mock<IRoomAvailabilityService>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _handler = new CreateBookingCommandHandler(
            _bookingRepositoryMock.Object,
            _roomRepositoryMock.Object,
            _roomAvailabilityServiceMock.Object,
            _unitOfWorkMock.Object,
            _mapperMock.Object,
            _pricingServiceMock.Object);
    }

    [Fact]
    public async Task Handle_ValidRequest_ReturnsBookingDto()
    {
        // Arrange
        var createBookingDto = new CreateBookingDto
        {
            RoomId = 1,
            CheckInDate = DateTime.Today.AddDays(1),
            CheckOutDate = DateTime.Today.AddDays(3)
        };

        var room = new Room { Id = 1, Price = 100, Availability = true };

        var booking = new Booking
        {
            RoomId = createBookingDto.RoomId,
            CheckInDate = createBookingDto.CheckInDate,
            CheckOutDate = createBookingDto.CheckOutDate,
            TotalPrice = 200,
            Status = BookingStatus.Pending
        };

        var bookingDto = new BookingDto
        {
            RoomId = createBookingDto.RoomId,
            CheckInDate = createBookingDto.CheckInDate,
            CheckOutDate = createBookingDto.CheckOutDate,
            TotalPrice = 200,
            Status = BookingStatus.Pending
        };

        _roomRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(room);
        _roomAvailabilityServiceMock.Setup(service => service.IsRoomAvailable(room.Id, createBookingDto.CheckInDate, createBookingDto.CheckOutDate))
                                    .ReturnsAsync(true);
        _mapperMock.Setup(mapper => mapper.Map<Booking>(createBookingDto)).Returns(booking);
        _pricingServiceMock.Setup(service => service.CalculateTotalPrice(room.Price, createBookingDto.CheckInDate, createBookingDto.CheckOutDate))
                           .Returns(200);
        _mapperMock.Setup(mapper => mapper.Map<BookingDto>(booking)).Returns(bookingDto);

        // Act
        var result = await _handler.Handle(new CreateBookingCommand { CreateBookingDto = createBookingDto }, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(bookingDto.RoomId, result.RoomId);
        Assert.Equal(bookingDto.CheckInDate, result.CheckInDate);
        Assert.Equal(bookingDto.CheckOutDate, result.CheckOutDate);
        Assert.Equal(bookingDto.TotalPrice, result.TotalPrice);
        Assert.Equal(bookingDto.Status, result.Status);

        // Verify
        _roomRepositoryMock.Verify(repo => repo.GetByIdAsync(1), Times.Once);
        _roomAvailabilityServiceMock.Verify(service => service.IsRoomAvailable(room.Id, createBookingDto.CheckInDate, createBookingDto.CheckOutDate), Times.Once);
        _bookingRepositoryMock.Verify(repo => repo.AddAsync(booking), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_NonExistentRoom_ThrowsEntityNotFoundException()
    {
        // Arrange
        var createBookingDto = new CreateBookingDto { RoomId = 1 };
        _roomRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Room)null);

        // Act & Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(() => _handler.Handle(new CreateBookingCommand { CreateBookingDto = createBookingDto }, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_RoomNotAvailable_ThrowsRoomNotAvailableException()
    {
        // Arrange
        var createBookingDto = new CreateBookingDto { RoomId = 1 };
        var room = new Room { Id = 1 };
        _roomRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(room);
        _roomAvailabilityServiceMock.Setup(service => service.IsRoomAvailable(room.Id, It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                                    .ReturnsAsync(false);

        // Act & Assert
        await Assert.ThrowsAsync<RoomNotAvailableException>(() => _handler.Handle(new CreateBookingCommand { CreateBookingDto = createBookingDto }, CancellationToken.None));
    }
}
