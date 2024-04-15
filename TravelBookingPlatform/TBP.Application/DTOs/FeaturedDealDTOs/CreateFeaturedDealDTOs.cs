namespace TravelBookingPlatform.Application.DTOs;

public class CreateFeaturedDealDto
{
    public int RoomId { get; set; }

    public decimal OriginalPrice { get; set; }

    public decimal DiscountedPrice { get; set; }
}