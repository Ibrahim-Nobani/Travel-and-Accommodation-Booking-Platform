using MediatR;
using TravelBookingPlatform.Application.DTOs;
namespace TravelBookingPlatform.Application.Commands;

public class CreateUserAdminCommand : IRequest<UserAdminResponseDto>
{
    public CreateUserAdminDto CreateUserAdminDto { get; set; }
}