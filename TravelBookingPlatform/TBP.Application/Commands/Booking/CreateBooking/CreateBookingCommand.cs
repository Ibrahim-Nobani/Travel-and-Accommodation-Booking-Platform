using MediatR;
using TravelBookingPlatform.Application.DTOs;
namespace TravelBookingPlatform.Application.Commands;

public class CreateBookingCommand: IRequest<BookingDto>
{
    public CreateBookingDto CreateBookingDto { get; set; }
}