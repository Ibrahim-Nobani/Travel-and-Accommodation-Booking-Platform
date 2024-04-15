using MediatR;
using TravelBookingPlatform.Application.DTOs;
namespace TravelBookingPlatform.Application.Queries;

public class GetAllHotelsQuery : IRequest<IEnumerable<HotelDto>> { }