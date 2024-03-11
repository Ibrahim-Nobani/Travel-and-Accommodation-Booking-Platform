using System.Text;
using TravelBookingPlatform.Infrastructure.Interfaces;
using TravelBookingPlatform.Application.DTOs;
using TravelBookingPlatform.Infrastructure.Models;
using TravelBookingPlatform.Application.Interfaces;

namespace TravelBookingPlatform.Application.Services;

public class PaymentEmailService : IPaymentEmailService
{
    private readonly IMailService _mailService;

    public PaymentEmailService(IMailService mailService)
    {
        _mailService = mailService;
    }

    public async Task SendPaymentConfirmationEmailAsync(string recipientEmail, string recipientName, decimal amount, TransactionResult transactionResult)
    {
        var emailData = new MailData
        {
            EmailToId = recipientEmail,
            EmailToName = recipientName,
            EmailSubject = "Payment Confirmation!",
            EmailBody = GeneratePaymentConfirmationEmail(recipientName, amount, transactionResult)
        };

        await _mailService.SendMailAsync(emailData);
    }

    private string GeneratePaymentConfirmationEmail(string recipientName, decimal amount, TransactionResult transactionResult)
    {
        var emailBody = new StringBuilder();

        emailBody.AppendLine($"Dear {recipientName},");
        emailBody.AppendLine();
        emailBody.AppendLine("We're pleased to inform you that your payment has been successfully processed.");
        emailBody.AppendLine();
        emailBody.AppendLine($"Transaction ID: {transactionResult.TransactionId}");
        emailBody.AppendLine($"Amount: {amount}");

        emailBody.AppendLine();
        emailBody.AppendLine("Thank you for your payment!");
        emailBody.AppendLine();
        emailBody.AppendLine("Best regards,");
        emailBody.AppendLine("[Ibrahim]");

        return emailBody.ToString();
    }

    public async Task SendPaymentFailureEmailAsync(string recipientEmail, string recipientName, TransactionResult paymentResult)
    {
        var emailData = new MailData
        {
            EmailToId = recipientEmail,
            EmailToName = recipientName,
            EmailSubject = "Payment Failure!",
            EmailBody = GeneratePaymentFailureEmail(recipientName, paymentResult)
        };

        await _mailService.SendMailAsync(emailData);
    }

    private string GeneratePaymentFailureEmail(string recipientName, TransactionResult paymentResult)
    {
        var emailBody = new StringBuilder();

        emailBody.AppendLine($"Dear {recipientName},");
        emailBody.AppendLine();
        emailBody.AppendLine("We regret to inform you that your payment has failed.");
        emailBody.AppendLine();
        emailBody.AppendLine("Reason for failure:");
        emailBody.AppendLine(paymentResult.ErrorMessage);

        emailBody.AppendLine();
        emailBody.AppendLine("Please try again or contact customer support for assistance.");
        emailBody.AppendLine();
        emailBody.AppendLine("Best regards,");
        emailBody.AppendLine("[Ibrahim]");

        return emailBody.ToString();
    }
}