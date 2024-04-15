using Microsoft.EntityFrameworkCore;
namespace TravelBookingPlatform.Domain.Entities;

[Keyless]
public class FeaturedDealView
{
    public int FeaturedDealId { get; set; }
    public int RoomId { get; set; }
    public int RoomNumber { get; set; }
    public string RoomThumbnail { get; set; }
    public string HotelName { get; set; }
    public string HotelLocation { get; set; }
    public decimal OriginalPrice { get; set; }
    public decimal DiscountedPrice { get; set; }
}
