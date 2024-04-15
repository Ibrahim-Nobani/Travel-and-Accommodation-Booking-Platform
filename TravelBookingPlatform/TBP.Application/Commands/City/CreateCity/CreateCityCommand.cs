using MediatR;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Application.DTOs;
namespace TravelBookingPlatform.Application.Commands;

public class CreateCityCommand : IRequest<CityDto>
{
    public CreateCityDto CreateCityDto { get; set; }
}