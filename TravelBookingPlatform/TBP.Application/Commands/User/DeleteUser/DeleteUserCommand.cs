using MediatR;

namespace TravelBookingPlatform.Application.Commands;

public class DeleteUserCommand : IRequest<Task>
{
    public int UserId { get; set; }
}