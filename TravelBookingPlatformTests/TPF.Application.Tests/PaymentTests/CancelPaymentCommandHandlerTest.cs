namespace TravelBookingPlatform.Application.Tests.Commands.Payments;

public class CancelPaymentCommandHandlerTests
{
    private readonly Mock<IBookingRepository> _bookingRepositoryMock = new Mock<IBookingRepository>();
    private readonly Mock<IBraintreeService> _braintreeServiceMock = new Mock<IBraintreeService>();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();

    [Fact]
    public async Task Handle_ValidBookingAndSuccessfulRefund_ReturnsOk()
    {
        // Arrange
        var bookingId = 1;
        var booking = new Booking { Id = bookingId, Status = BookingStatus.Confirmed };
        var paymentTransaction = new PaymentTransaction { TransactionId = "transactionId", Amount = 100 };
        booking.PaymentTransaction = paymentTransaction;

        _bookingRepositoryMock.Setup(repo => repo.GetByIdIncludingPaymentTransactionAsync(bookingId))
                              .ReturnsAsync(booking);
        _braintreeServiceMock.Setup(service => service.ProcessRefundAsync(paymentTransaction.TransactionId, paymentTransaction.Amount))
                             .ReturnsAsync(true);

        var handler = new CancelPaymentCommandHandler(_bookingRepositoryMock.Object, _braintreeServiceMock.Object, _unitOfWorkMock.Object);

        // Act
        var result = await handler.Handle(new CancelPaymentCommand { BookingId = bookingId }, CancellationToken.None) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        var message = result.Value.GetType().GetProperty("Message")?.GetValue(result.Value)?.ToString();
        Assert.Equal(PaymentRefundResponse.Success, message);

        // Verify
        _bookingRepositoryMock.Verify(repo => repo.UpdateAsync(booking), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_RefundFailure_ReturnsBadRequest()
    {
        // Arrange
        var bookingId = 1;
        var booking = new Booking { Id = bookingId, Status = BookingStatus.Confirmed };
        var paymentTransaction = new PaymentTransaction { TransactionId = "transactionId", Amount = 100 };
        booking.PaymentTransaction = paymentTransaction;

        _bookingRepositoryMock.Setup(repo => repo.GetByIdIncludingPaymentTransactionAsync(bookingId))
                              .ReturnsAsync(booking);
        _braintreeServiceMock.Setup(service => service.ProcessRefundAsync(paymentTransaction.TransactionId, paymentTransaction.Amount))
                             .ReturnsAsync(false);

        var handler = new CancelPaymentCommandHandler(_bookingRepositoryMock.Object, _braintreeServiceMock.Object, _unitOfWorkMock.Object);

        // Act
        var result = await handler.Handle(new CancelPaymentCommand { BookingId = bookingId }, CancellationToken.None) as BadRequestObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var message = result.Value.GetType().GetProperty("Message")?.GetValue(result.Value)?.ToString();
        Assert.Equal(PaymentRefundResponse.Failed, message);
    }

    [Fact]
    public async Task Handle_BookingWithoutPaymentTransaction_ReturnsEntityNotFoundException()
    {
        // Arrange
        var bookingId = 1;
        var booking = new Booking { Id = bookingId, Status = BookingStatus.Confirmed };
        //booking.PaymentTransaction = null; // Simulate no payment transaction

        _bookingRepositoryMock.Setup(repo => repo.GetByIdIncludingPaymentTransactionAsync(bookingId))
                              .ReturnsAsync(booking);

        var handler = new CancelPaymentCommandHandler(_bookingRepositoryMock.Object, _braintreeServiceMock.Object, _unitOfWorkMock.Object);

        // Act & Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(() => handler.Handle(new CancelPaymentCommand { BookingId = bookingId }, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_AlreadyCancelledBooking_ReturnsBadRequest()
    {
        // Arrange
        var bookingId = 1;
        var booking = new Booking { Id = bookingId, Status = BookingStatus.Cancelled };
        var paymentTransaction = new PaymentTransaction { TransactionId = "transactionId", Amount = 100 };
        booking.PaymentTransaction = paymentTransaction;

        _bookingRepositoryMock.Setup(repo => repo.GetByIdIncludingPaymentTransactionAsync(bookingId))
                              .ReturnsAsync(booking);

        var handler = new CancelPaymentCommandHandler(_bookingRepositoryMock.Object, _braintreeServiceMock.Object, _unitOfWorkMock.Object);

        // Act
        var result = await handler.Handle(new CancelPaymentCommand { BookingId = bookingId }, CancellationToken.None) as BadRequestObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var message = result.Value.GetType().GetProperty("Message")?.GetValue(result.Value)?.ToString();
        Assert.Equal(PaymentRefundResponse.Denied, message);
    }
}