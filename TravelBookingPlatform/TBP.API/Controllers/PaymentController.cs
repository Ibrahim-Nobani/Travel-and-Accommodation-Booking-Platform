using MediatR;
using Microsoft.AspNetCore.Mvc;
using TravelBookingPlatform.Application.DTOs;
using TravelBookingPlatform.Application.Commands;
using TravelBookingPlatform.Application.Queries;
namespace TravelBookingPlatform.API.Controllers;

[ApiController]
[Route("api/payments")]
public class PaymentController : ControllerBase
{
    private readonly IMediator _mediator;

    public PaymentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("process")]
    public async Task<IActionResult> ProcessPayment([FromBody] ProcessPaymentRequest processPaymentRequest)
    {
        var processPaymentCommand = new ProcessPaymentCommand
        {
            BookingId = processPaymentRequest.BookingId,
            PaymentMethodNonce = processPaymentRequest.PaymentMethodNonce
        };

        var paymentResult = await _mediator.Send(processPaymentCommand);
        return paymentResult;
    }

    [HttpPost("cancel/{bookingId}")]
    public async Task<IActionResult> CancelPayment([FromRoute] int bookingId)
    {
        var cancelPaymentCommand = new CancelPaymentCommand
        {
            BookingId = bookingId
        };

        var refundResult = await _mediator.Send(cancelPaymentCommand);
        return refundResult;
    }

    [HttpGet("{paymentTransactionId}")]
    public async Task<IActionResult> GetPaymentTransaction([FromRoute] int paymentTransactionId)
    {
        var getPaymentTransactionCommand = new GetPaymentTransactionQuery
        {
            PaymentTransactionId = paymentTransactionId
        };

        var paymentTransaction = await _mediator.Send(getPaymentTransactionCommand);
        return Ok(paymentTransaction);
    }
}
