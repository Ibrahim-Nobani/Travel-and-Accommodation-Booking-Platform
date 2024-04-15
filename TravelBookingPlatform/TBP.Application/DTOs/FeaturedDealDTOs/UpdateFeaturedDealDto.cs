namespace TravelBookingPlatform.Application.DTOs;

public class UpdateFeaturedDealDto
{
    public int RoomId { get; set; }

    public decimal OriginalPrice { get; set; }

    public decimal DiscountedPrice { get; set; }
}