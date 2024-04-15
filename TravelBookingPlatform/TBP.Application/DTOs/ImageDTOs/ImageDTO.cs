using TravelBookingPlatform.Domain.Enums;
namespace TravelBookingPlatform.Application.DTOs;

public class ImageDto
{
    public int Id { get; set; }

    public string ImageUrl { get; set; }

    public int EntityId { get; set; }

    public ImageType ImageType { get; set; }
}