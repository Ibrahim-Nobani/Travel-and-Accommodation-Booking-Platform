using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace TravelBookingPlatform.Application.Commands;

public class CancelPaymentCommand : IRequest<IActionResult>
{
    public int BookingId { get; set; }
}