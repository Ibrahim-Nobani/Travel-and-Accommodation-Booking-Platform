using MediatR;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Application.DTOs;

namespace TravelBookingPlatform.Application.Commands;

public class CreateImageForHotelCommand : IRequest<ImageDto>
{
    public int HotelId { get; set; }
    public CreateImageDto CreateImageDto { get; set; }
}