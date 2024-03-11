namespace TravelBookingPlatform.Infrastructure.Models;

public class TransactionResult
{
    public bool IsSuccess { get; set; }
    public string? TransactionId { get; set; }
    public string? ErrorMessage { get; set; }
}
