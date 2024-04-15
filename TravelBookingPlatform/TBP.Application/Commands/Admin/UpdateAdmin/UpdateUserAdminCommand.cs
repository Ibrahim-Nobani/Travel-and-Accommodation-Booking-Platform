using MediatR;
using TravelBookingPlatform.Application.DTOs;

public class UpdateUserAdminCommand : IRequest<UserAdminResponseDto>
{
    public int UserId { get; set; }
    public UpdateUserAdminDto UpdateUserAdminDto { get; set; }
}