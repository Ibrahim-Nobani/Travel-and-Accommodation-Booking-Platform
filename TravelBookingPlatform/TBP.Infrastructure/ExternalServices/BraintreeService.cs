using Braintree;
using Microsoft.Extensions.Options;
using TravelBookingPlatform.Infrastructure.Interfaces;
using TravelBookingPlatform.Infrastructure.Models;
using TravelBookingPlatform.Infrastructure.Settings;
namespace TravelBookingPlatform.Infrastructure.ExternalServices;

public class BraintreeService : IBraintreeService
{
    private readonly BraintreeGateway _braintreeGateway;

    public BraintreeService(IOptions<BraintreeSettings> braintreeSettings)
    {
        var settings = braintreeSettings.Value;

        _braintreeGateway = new BraintreeGateway
        {
            Environment = Braintree.Environment.SANDBOX,
            MerchantId = settings.MerchantId,
            PublicKey = settings.PublicKey,
            PrivateKey = settings.PrivateKey
        };
    }

    public async Task<TransactionResult> ProcessPayment(string paymentMethodNonce, decimal amount)
    {
        var request = new TransactionRequest
        {
            Amount = amount,
            PaymentMethodNonce = paymentMethodNonce,
            Options = new TransactionOptionsRequest
            {
                SubmitForSettlement = true
            }
        };

        var result = await _braintreeGateway.Transaction.SaleAsync(request);

        return new TransactionResult
        {
            IsSuccess = result.IsSuccess(),
            TransactionId = result.Target?.Id,
            ErrorMessage = result.Message
        };
    }

    public async Task<bool> ProcessRefundAsync(string transactionId, decimal amount)
    {
        var result = await _braintreeGateway.Transaction.RefundAsync(transactionId, amount);

        return result.IsSuccess();
    }
}
