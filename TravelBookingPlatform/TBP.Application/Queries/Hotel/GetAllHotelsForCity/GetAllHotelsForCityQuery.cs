using MediatR;
using TravelBookingPlatform.Application.DTOs;
namespace TravelBookingPlatform.Application.Queries;

public class GetAllHotelsForCityQuery : IRequest<IEnumerable<HotelDto>>
{
    public int CityId { get; set; }
}