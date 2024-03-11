using MediatR;
using TravelBookingPlatform.Application.DTOs;
namespace TravelBookingPlatform.Application.Queries;

public class GetUserQuery : IRequest<UserResponseDto>
{
    public int UserId { get; set; }
}