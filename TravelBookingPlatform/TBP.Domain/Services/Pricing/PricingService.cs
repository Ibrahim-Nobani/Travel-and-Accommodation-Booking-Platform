using TravelBookingPlatform.Domain.Interfaces;
namespace TravelBookingPlatform.Domain.Services;

public class PricingService : IPricingService
{
    public decimal CalculateTotalPrice(decimal roomPrice, DateTime checkInDate, DateTime checkOutDate)
    {
        var totalNights = (int)(checkOutDate - checkInDate).TotalDays;
        var totalPrice = roomPrice * totalNights;

        return totalPrice;
    }
}