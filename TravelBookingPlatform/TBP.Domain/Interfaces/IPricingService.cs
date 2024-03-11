namespace TravelBookingPlatform.Domain.Interfaces;

public interface IPricingService
{
    decimal CalculateTotalPrice(decimal roomPrice, DateTime checkInDate, DateTime checkOutDate);
}