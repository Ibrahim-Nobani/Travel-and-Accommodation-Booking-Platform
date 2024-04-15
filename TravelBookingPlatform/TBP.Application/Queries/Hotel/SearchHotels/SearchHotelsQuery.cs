using MediatR;
using TravelBookingPlatform.Application.DTOs;
namespace TravelBookingPlatform.Application.Queries;

public class SearchHotelsQuery : IRequest<IEnumerable<HotelDto>>
{
    public SearchHotelCriteria SearchHotel { get; set; }
}