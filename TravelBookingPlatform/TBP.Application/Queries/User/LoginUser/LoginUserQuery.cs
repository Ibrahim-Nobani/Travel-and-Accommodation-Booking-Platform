using MediatR;
using TravelBookingPlatform.Application.DTOs;
namespace TravelBookingPlatform.Application.Queries;

public class LoginUserQuery : IRequest<UserLoginResponseDto>
{
    public LoginUserDto LoginUserDto { get; set; }
}