using MediatR;
using TravelBookingPlatform.Application.DTOs;
namespace TravelBookingPlatform.Application.Queries;

public class GetHotelQuery : IRequest<HotelDto>
{
    public int HotelId { get; set; }
}