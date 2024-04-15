using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace TravelBookingPlatform.Application.Commands;

public class ProcessPaymentCommand : IRequest<IActionResult>
{
    public int BookingId { get; set; }
    public string PaymentMethodNonce { get; set; }
}
