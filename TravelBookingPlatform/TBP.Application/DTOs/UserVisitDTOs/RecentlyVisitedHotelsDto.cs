namespace TravelBookingPlatform.Application.DTOs;

public class RecentlyVisitedHotelDto
{
    public DateTime VisitDateTime { get; set; }
    public string HotelName { get; set; }
    public string ThumbnailImageUrl { get; set; }
    public string City { get; set; }
    public int? StarRating { get; set; }
}