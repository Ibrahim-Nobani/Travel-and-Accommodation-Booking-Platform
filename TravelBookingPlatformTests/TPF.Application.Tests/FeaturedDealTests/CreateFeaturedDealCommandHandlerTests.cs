namespace TravelBookingPlatform.Application.Tests.Commands.FeaturedDeals;

public class CreateFeaturedDealCommandHandlerTests
{
    private readonly Mock<IFeaturedDealRepository> _mockFeaturedDealRepository;
    private readonly Mock<IRoomRepository> _mockRoomRepository;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly Mock<IMapper> _mockMapper;
    private readonly CreateFeaturedDealCommandHandler _handler;

    public CreateFeaturedDealCommandHandlerTests()
    {
        _mockFeaturedDealRepository = new Mock<IFeaturedDealRepository>();
        _mockRoomRepository = new Mock<IRoomRepository>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockMapper = new Mock<IMapper>();
        _handler = new CreateFeaturedDealCommandHandler(_mockFeaturedDealRepository.Object, _mockRoomRepository.Object, _mockUnitOfWork.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task Handle_ValidRequest_ReturnsFeaturedDealDto()
    {
        // Arrange
        var createFeaturedDealDto = new CreateFeaturedDealDto
        {
            RoomId = 1,
            OriginalPrice = 200,
            DiscountedPrice = 150
        };

        var roomExists = true;
        _mockRoomRepository.Setup(repo => repo.CheckIfExistsByIdAsync(createFeaturedDealDto.RoomId)).ReturnsAsync(roomExists);

        var isRoomAlreadyFeatured = false;
        _mockFeaturedDealRepository.Setup(repo => repo.IsRoomAlreadyFeaturedDealAsync(createFeaturedDealDto.RoomId)).ReturnsAsync(isRoomAlreadyFeatured);

        var featuredDeal = new FeaturedDeal { Id = 1, RoomId = createFeaturedDealDto.RoomId, OriginalPrice = createFeaturedDealDto.OriginalPrice, DiscountedPrice = createFeaturedDealDto.DiscountedPrice };
        _mockMapper.Setup(mapper => mapper.Map<FeaturedDeal>(createFeaturedDealDto)).Returns(featuredDeal);

        var expectedFeaturedDealDto = new FeaturedDealDto { Id = featuredDeal.Id, RoomId = featuredDeal.RoomId, OriginalPrice = featuredDeal.OriginalPrice, DiscountedPrice = featuredDeal.DiscountedPrice };
        _mockMapper.Setup(mapper => mapper.Map<FeaturedDealDto>(featuredDeal)).Returns(expectedFeaturedDealDto);

        // Act
        var result = await _handler.Handle(new CreateFeaturedDealCommand { CreateFeaturedDealDto = createFeaturedDealDto }, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<FeaturedDealDto>(result);
        Assert.Equal(expectedFeaturedDealDto, result);

        _mockFeaturedDealRepository.Verify(repo => repo.AddAsync(featuredDeal), Times.Once);
        _mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_NonExistentRoom_ThrowsEntityNotFoundException()
    {
        // Arrange
        var createFeaturedDealDto = new CreateFeaturedDealDto { RoomId = 1 };

        var roomExists = false;
        _mockRoomRepository.Setup(repo => repo.CheckIfExistsByIdAsync(createFeaturedDealDto.RoomId)).ReturnsAsync(roomExists);

        // Act & Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(() => _handler.Handle(new CreateFeaturedDealCommand { CreateFeaturedDealDto = createFeaturedDealDto }, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_RoomAlreadyFeatured_ThrowsRoomAlreadyFeaturedException()
    {
        // Arrange
        var createFeaturedDealDto = new CreateFeaturedDealDto { RoomId = 1 };

        var roomExists = true;
        _mockRoomRepository.Setup(repo => repo.CheckIfExistsByIdAsync(createFeaturedDealDto.RoomId)).ReturnsAsync(roomExists);

        var isRoomAlreadyFeatured = true;
        _mockFeaturedDealRepository.Setup(repo => repo.IsRoomAlreadyFeaturedDealAsync(createFeaturedDealDto.RoomId)).ReturnsAsync(isRoomAlreadyFeatured);

        // Act & Assert
        await Assert.ThrowsAsync<RoomAlreadyFeaturedException>(() => _handler.Handle(new CreateFeaturedDealCommand { CreateFeaturedDealDto = createFeaturedDealDto }, CancellationToken.None));
    }
}