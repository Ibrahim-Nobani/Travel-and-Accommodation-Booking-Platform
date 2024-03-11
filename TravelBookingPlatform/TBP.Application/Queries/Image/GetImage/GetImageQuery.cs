using MediatR;
using TravelBookingPlatform.Application.DTOs;
namespace TravelBookingPlatform.Application.Queries;

public class GetImageQuery : IRequest<ImageDto>
{
    public int ImageId { get; set; }
}