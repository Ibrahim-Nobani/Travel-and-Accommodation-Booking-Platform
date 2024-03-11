namespace TravelBookingPlatform.Domain.Enums;

public static class PaymentRefundResponse
{
    public const string Success = "The Refund Succeeded!";
    public const string Failed = "The Refund Process Failed, please try again!";
    public const string Denied = "This Action is Denied, The Transaction is already canceled!";
}
