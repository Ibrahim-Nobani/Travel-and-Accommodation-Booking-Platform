namespace TravelBookingPlatform.Domain.Tests;

public class PricingServiceTests
{
    private readonly IPricingService _pricingService;

    public PricingServiceTests()
    {
        _pricingService = new PricingService();
    }

    [Theory]
    [InlineData(100, "2024-03-10", "2024-03-15", 500)]
    [InlineData(150.75, "2024-04-20", "2024-04-22", 301.5)]
    [InlineData(75.5, "2024-05-01", "2024-05-05", 302)]
    public void CalculateTotalPrice_ValidInput_ReturnsCorrectTotalPrice(decimal roomPrice, string checkInDateString, string checkOutDateString, decimal expectedTotalPrice)
    {
        // Arrange
        var checkInDate = DateTime.Parse(checkInDateString);
        var checkOutDate = DateTime.Parse(checkOutDateString);

        // Act
        var totalPrice = _pricingService.CalculateTotalPrice(roomPrice, checkInDate, checkOutDate);

        // Assert
        Assert.Equal(expectedTotalPrice, totalPrice);
    }
}