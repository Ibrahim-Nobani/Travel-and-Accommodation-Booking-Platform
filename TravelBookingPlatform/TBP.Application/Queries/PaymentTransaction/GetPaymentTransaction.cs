using MediatR;
using TravelBookingPlatform.Application.DTOs;
namespace TravelBookingPlatform.Application.Queries;

public class GetPaymentTransactionQuery : IRequest<PaymentTransactionDto>
{
    public int PaymentTransactionId { get; set; }
}