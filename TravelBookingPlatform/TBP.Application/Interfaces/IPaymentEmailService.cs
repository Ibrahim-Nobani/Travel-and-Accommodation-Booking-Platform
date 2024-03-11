using TravelBookingPlatform.Infrastructure.Models;

namespace TravelBookingPlatform.Application.Interfaces;

public interface IPaymentEmailService
{
    Task SendPaymentConfirmationEmailAsync(string recipientEmail, string recipientName, decimal amount, TransactionResult transactionResult);
    Task SendPaymentFailureEmailAsync(string recipientEmail, string recipientName, TransactionResult paymentResult);
}
