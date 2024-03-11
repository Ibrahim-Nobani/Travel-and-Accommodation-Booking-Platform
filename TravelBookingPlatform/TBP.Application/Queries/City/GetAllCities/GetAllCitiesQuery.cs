using MediatR;
using TravelBookingPlatform.Application.DTOs;
namespace TravelBookingPlatform.Application.Queries;

public class GetAllCitiesQuery : IRequest<IEnumerable<CityDto>> { }