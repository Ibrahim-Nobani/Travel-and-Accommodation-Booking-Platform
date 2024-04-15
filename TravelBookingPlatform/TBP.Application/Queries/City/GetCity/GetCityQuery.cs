using MediatR;
using TravelBookingPlatform.Application.DTOs;
namespace TravelBookingPlatform.Application.Queries;

public class GetCityQuery : IRequest<CityDto>
{
    public int CityId { get; set; }
}