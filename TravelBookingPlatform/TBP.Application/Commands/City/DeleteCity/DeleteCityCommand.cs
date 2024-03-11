using MediatR;
namespace TravelBookingPlatform.Application.Commands;

public class DeleteCityCommand : IRequest<Task>
{
    public int CityId { get; set; }
}