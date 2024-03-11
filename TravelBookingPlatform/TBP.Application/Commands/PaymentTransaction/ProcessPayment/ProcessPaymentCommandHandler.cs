using MediatR;
using Microsoft.AspNetCore.Mvc;
using TravelBookingPlatform.Domain.Enums;
using TravelBookingPlatform.Application.Exceptions;
using TravelBookingPlatform.Application.Interfaces;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Infrastructure.Database;
using TravelBookingPlatform.Infrastructure.Interfaces;
using TravelBookingPlatform.Infrastructure.Models;
namespace TravelBookingPlatform.Application.Commands;

public class ProcessPaymentCommandHandler : IRequestHandler<ProcessPaymentCommand, IActionResult>
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IPaymentTransactionRepository _paymentTransactionRepository;
    private readonly IBraintreeService _braintreeService;
    private readonly IPaymentEmailService _emailService;
    private readonly IUnitOfWork _unitOfWork;

    public ProcessPaymentCommandHandler(IBookingRepository bookingRepository, IPaymentTransactionRepository paymentTransactionRepository, IBraintreeService braintreeService, IPaymentEmailService emailService, IUnitOfWork unitOfWork)
    {
        _bookingRepository = bookingRepository;
        _paymentTransactionRepository = paymentTransactionRepository;
        _braintreeService = braintreeService;
        _emailService = emailService;
        _unitOfWork = unitOfWork;
    }

    public async Task<IActionResult> Handle(ProcessPaymentCommand request, CancellationToken cancellationToken)
    {
        var booking = await _bookingRepository.GetByIdIncludingUserAsync(request.BookingId);

        if (booking == null)
        {
            throw new EntityNotFoundException(nameof(booking));
        }

        if (booking.Status == BookingStatus.Confirmed)
        {
            return new BadRequestObjectResult(new { Message = PaymentResponse.Done });
        }

        if (booking.Status == BookingStatus.Pending)
        {
            var paymentResult = await _braintreeService.ProcessPayment(request.PaymentMethodNonce, booking.TotalPrice);

            if (paymentResult.IsSuccess)
            {
                return await PaymentSuccessful(booking, paymentResult);
            }

            await PaymentFailure(booking, paymentResult);
        }

        return new BadRequestObjectResult(new { Message = PaymentResponse.Cancelled });
    }

    private async Task<IActionResult> PaymentSuccessful(Booking booking, TransactionResult paymentResult)
    {
        booking.Status = BookingStatus.Confirmed;
        _bookingRepository.UpdateAsync(booking);

        AddPaymentTransaction(booking, paymentResult);

        await _unitOfWork.SaveChangesAsync();

        await _emailService.SendPaymentConfirmationEmailAsync(booking.User.Email, booking.User.Username, booking.TotalPrice, paymentResult);
        return new OkObjectResult(new { Message = PaymentResponse.Success });
    }

    private async Task PaymentFailure(Booking booking, TransactionResult paymentResult)
    {
        booking.Status = BookingStatus.Cancelled;
        _bookingRepository.UpdateAsync(booking);

        await _unitOfWork.SaveChangesAsync();

        await _emailService.SendPaymentFailureEmailAsync(booking.User.Email, booking.User.Username, paymentResult);
    }

    private void AddPaymentTransaction(Booking booking, TransactionResult paymentResult)
    {
        var paymentTransaction = new PaymentTransaction
        {
            BookingId = booking.Id,
            TransactionId = paymentResult.TransactionId,
            Amount = booking.TotalPrice,
            PaymentDate = DateTime.Now
        };

        _paymentTransactionRepository.AddAsync(paymentTransaction);
    }
}