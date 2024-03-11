using MediatR;
using TravelBookingPlatform.Application.DTOs;

namespace TravelBookingPlatform.Application.Commands;

public class UpdateImageCommand : IRequest<ImageDto>
{
    public int ImageId { get; set; }
    public UpdateImageDto UpdateImageDto { get; set; }
}