using Microsoft.EntityFrameworkCore;
namespace TravelBookingPlatform.Domain.Entities;

[Keyless]
public class RecentlyVisitedHotelView
{
    public int UserVisitId { get; set; }

    public int UserId { get; set; }

    public int HotelId { get; set; }

    public DateTime VisitDateTime { get; set; }

    public string HotelName { get; set; }

    public string ThumbnailImageUrl { get; set; }

    public int CityId { get; set; }

    public string City { get; set; }

    public int? StarRating { get; set; }
}