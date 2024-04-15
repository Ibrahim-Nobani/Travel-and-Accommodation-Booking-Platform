using MediatR;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Application.DTOs;
namespace TravelBookingPlatform.Application.Commands;

public class UpdateCityCommand : IRequest<CityDto>
{
    public int CityId { get; set; }
    public UpdateCityDto UpdateCityDto { get; set; }
}