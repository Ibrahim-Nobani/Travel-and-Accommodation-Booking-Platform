using TravelBookingPlatform.Infrastructure.Models;
namespace TravelBookingPlatform.Infrastructure.Interfaces;

public interface IBraintreeService
{
    Task<TransactionResult> ProcessPayment(string paymentMethodNonce, decimal amount);
    Task<bool> ProcessRefundAsync(string transactionId, decimal amount);
}
