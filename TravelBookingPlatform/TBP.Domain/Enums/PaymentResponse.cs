namespace TravelBookingPlatform.Domain.Enums;

public static class PaymentResponse
{
    public const string Success = "The Transaction Succeeded!";
    public const string Cancelled = "The Transaction Failed, please try again!";
    public const string Done = "The Transaction has already been marked as confirmed!";
}
