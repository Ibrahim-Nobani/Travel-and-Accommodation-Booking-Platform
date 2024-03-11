using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using TravelBookingPlatform.Application.Commands;
using TravelBookingPlatform.Domain.Enums;
using TravelBookingPlatform.Application.Exceptions;
using TravelBookingPlatform.Application.Interfaces;
using TravelBookingPlatform.Infrastructure.Database;
using TravelBookingPlatform.Infrastructure.Interfaces;
using TravelBookingPlatform.Domain.Entities;

public class CancelPaymentCommandHandler : IRequestHandler<CancelPaymentCommand, IActionResult>
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IBraintreeService _braintreeService;
    private readonly IUnitOfWork _unitOfWork;

    public CancelPaymentCommandHandler(IBookingRepository bookingRepository, IBraintreeService braintreeService, IUnitOfWork unitOfWork)
    {
        _bookingRepository = bookingRepository;
        _braintreeService = braintreeService;
        _unitOfWork = unitOfWork;
    }

    public async Task<IActionResult> Handle(CancelPaymentCommand request, CancellationToken cancellationToken)
    {
        var booking = await _bookingRepository.GetByIdIncludingPaymentTransactionAsync(request.BookingId);

        if (booking == null)
        {
            throw new EntityNotFoundException(nameof(Booking));
        }

        if (booking.PaymentTransaction == null)
        {
            throw new EntityNotFoundException(nameof(PaymentTransaction));
        }

        if (booking.Status == BookingStatus.Cancelled || string.IsNullOrEmpty(booking.PaymentTransaction.TransactionId))
        {
            return new BadRequestObjectResult(new { Message = PaymentRefundResponse.Denied });
        }

        var result = await _braintreeService.ProcessRefundAsync(booking.PaymentTransaction.TransactionId, booking.PaymentTransaction.Amount);

        if (!result)
        {
            return new BadRequestObjectResult(new { Message = PaymentRefundResponse.Failed });
        }

        booking.Status = BookingStatus.Cancelled;
        _bookingRepository.UpdateAsync(booking);

        await _unitOfWork.SaveChangesAsync();

        return new OkObjectResult(new { Message = PaymentRefundResponse.Success });
    }
}
