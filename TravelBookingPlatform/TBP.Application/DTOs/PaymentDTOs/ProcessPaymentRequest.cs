namespace TravelBookingPlatform.Application.DTOs;

public class ProcessPaymentRequest
{
    public int BookingId { get; set; }
    public string PaymentMethodNonce { get; set; }
}