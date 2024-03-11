using MediatR;
using TravelBookingPlatform.Application.DTOs;
namespace TravelBookingPlatform.Application.Queries;

public class GetImagesForHotelQuery : IRequest<IEnumerable<ImageDto>>
{
    public int HotelId { get; set; }
}