using MediatR;

namespace TravelBookingPlatform.Application.Commands;

public class DeleteHotelAdminCommand : IRequest<Task>
{
    public int HotelId { get; set; }
}