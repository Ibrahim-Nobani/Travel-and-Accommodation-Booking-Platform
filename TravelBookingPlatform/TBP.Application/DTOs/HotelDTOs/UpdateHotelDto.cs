namespace TravelBookingPlatform.Application.DTOs;

public class UpdateHotelDto
{
    public string Name { get; set; }

    public int? StarRating { get; set; }

    public string Location { get; set; }

    public string ThumbnailImageUrl { get; set; }

    public int CityId { get; set; }

    public string Owner { get; set; }
}