using MediatR;
using TravelBookingPlatform.Application.DTOs;

namespace TravelBookingPlatform.Application.Commands;

public class CreateUserCommand : IRequest<UserResponseDto>
{
    public CreateUserDto CreateUserDto { get; set; }
}