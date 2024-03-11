using MediatR;
namespace TravelBookingPlatform.Application.Commands;

public class DeleteImageCommand : IRequest<Task>
{
    public int ImageId { get; set; }
}