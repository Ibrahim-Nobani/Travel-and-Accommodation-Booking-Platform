namespace TravelBookingPlatform.Application.Tests.Commands.Payments;

public class ProcessPaymentCommandHandlerTests
{
    private readonly Mock<IBookingRepository> _bookingRepositoryMock;
    private readonly Mock<IPaymentTransactionRepository> _paymentTransactionRepositoryMock;
    private readonly Mock<IBraintreeService> _braintreeServiceMock;
    private readonly Mock<IPaymentEmailService> _emailServiceMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly ProcessPaymentCommandHandler _handler;

    public ProcessPaymentCommandHandlerTests()
    {
        _bookingRepositoryMock = new Mock<IBookingRepository>();
        _paymentTransactionRepositoryMock = new Mock<IPaymentTransactionRepository>();
        _braintreeServiceMock = new Mock<IBraintreeService>();
        _emailServiceMock = new Mock<IPaymentEmailService>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new ProcessPaymentCommandHandler(
            _bookingRepositoryMock.Object,
            _paymentTransactionRepositoryMock.Object,
            _braintreeServiceMock.Object,
            _emailServiceMock.Object,
            _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_BookingNotFound_ThrowsEntityNotFoundException()
    {
        // Arrange
        var bookingId = 1;
        _bookingRepositoryMock.Setup(repo => repo.GetByIdIncludingUserAsync(bookingId)).ReturnsAsync((Booking)null);

        // Act & Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(() => _handler.Handle(new ProcessPaymentCommand { BookingId = bookingId }, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ConfirmedBooking_ReturnsBadRequest()
    {
        // Arrange
        var bookingId = 1;
        var booking = new Booking { Id = bookingId, Status = BookingStatus.Confirmed };
        _bookingRepositoryMock.Setup(repo => repo.GetByIdIncludingUserAsync(bookingId)).ReturnsAsync(booking);

        // Act
        var result = await _handler.Handle(new ProcessPaymentCommand { BookingId = bookingId }, CancellationToken.None) as BadRequestObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var message = result.Value.GetType().GetProperty("Message")?.GetValue(result.Value)?.ToString();
        Assert.Equal(PaymentResponse.Done, message);
    }


    [Fact]
    public async Task Handle_PendingBooking_PaymentSuccessful_ReturnsOk()
    {
        // Arrange
        var bookingId = 1;
        var userId = 1;
        var roomId = 1;
        var booking = new Booking { Id = bookingId, Status = BookingStatus.Pending, TotalPrice = 100, UserId = userId, RoomId = roomId };
        var paymentResult = new TransactionResult { IsSuccess = true, TransactionId = "aaa" };
        var user = new User { Id = userId, Email = "test@example.com", Username = "testuser" };
        booking.User = user;

        _bookingRepositoryMock.Setup(repo => repo.GetByIdIncludingUserAsync(bookingId))
                              .ReturnsAsync(booking);
        _braintreeServiceMock.Setup(service => service.ProcessPayment(It.IsAny<string>(), booking.TotalPrice))
                             .ReturnsAsync(paymentResult);
        _paymentTransactionRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<PaymentTransaction>()));
        _unitOfWorkMock.Setup(uow => uow.SaveChangesAsync()).Returns(Task.FromResult(0));
        _emailServiceMock.Setup(service => service.SendPaymentConfirmationEmailAsync(user.Email, user.Username, booking.TotalPrice, paymentResult))
                         .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(new ProcessPaymentCommand { BookingId = bookingId, PaymentMethodNonce = "nonce" }, CancellationToken.None) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);

        // Verify
        _bookingRepositoryMock.Verify(repo => repo.UpdateAsync(booking), Times.Once);
        _paymentTransactionRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<PaymentTransaction>()), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Once);
        _emailServiceMock.Verify(service => service.SendPaymentConfirmationEmailAsync(user.Email, user.Username, booking.TotalPrice, paymentResult), Times.Once);
    }

    [Fact]
    public async Task Handle_PendingBooking_PaymentFailure_ReturnsBadRequest()
    {
        // Arrange
        var bookingId = 1;
        var userId = 1;
        var roomId = 1;
        var booking = new Booking { Id = bookingId, Status = BookingStatus.Pending, TotalPrice = 100, UserId = userId, RoomId = roomId };
        var paymentResult = new TransactionResult { IsSuccess = false, TransactionId = null }; // Simulate payment failure
        var user = new User { Id = userId, Email = "test@example.com", Username = "testuser" };
        booking.User = user;

        _bookingRepositoryMock.Setup(repo => repo.GetByIdIncludingUserAsync(bookingId))
                              .ReturnsAsync(booking);
        _braintreeServiceMock.Setup(service => service.ProcessPayment(It.IsAny<string>(), booking.TotalPrice))
                             .ReturnsAsync(paymentResult);
        _paymentTransactionRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<PaymentTransaction>()));
        _unitOfWorkMock.Setup(uow => uow.SaveChangesAsync()).Returns(Task.FromResult(0));
        _emailServiceMock.Setup(service => service.SendPaymentFailureEmailAsync(user.Email, user.Username, paymentResult))
                         .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(new ProcessPaymentCommand { BookingId = bookingId, PaymentMethodNonce = "nonce" }, CancellationToken.None) as BadRequestObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);

        // Verify
        _bookingRepositoryMock.Verify(repo => repo.UpdateAsync(booking), Times.Once);
        _paymentTransactionRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<PaymentTransaction>()), Times.Never); // Ensure no payment transaction is added in case of failure
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Once);
        _emailServiceMock.Verify(service => service.SendPaymentFailureEmailAsync(user.Email, user.Username, paymentResult), Times.Once);
    }
}
