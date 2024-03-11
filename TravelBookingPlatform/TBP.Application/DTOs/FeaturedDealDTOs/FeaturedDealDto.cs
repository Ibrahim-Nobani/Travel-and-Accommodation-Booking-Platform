namespace TravelBookingPlatform.Application.DTOs;

public class FeaturedDealDto
{
    public int Id { get; set; }

    public int RoomId { get; set; }

    public decimal OriginalPrice { get; set; }

    public decimal DiscountedPrice { get; set; }
}