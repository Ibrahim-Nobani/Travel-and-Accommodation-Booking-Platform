using MediatR;
using TravelBookingPlatform.Application.DTOs;
namespace TravelBookingPlatform.Application.Commands;

public class UpdateUserCommand : IRequest<UserResponseDto>
{
    public int UserId { get; set; }
    public UpdateUserDto UpdateUserDto { get; set; }
}