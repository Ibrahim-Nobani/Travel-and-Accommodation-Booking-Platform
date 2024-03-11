using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelBookingPlatform.Application.DTOs;

public class PaymentTransactionDto
{
    public int Id { get; set; }

    public int BookingId { get; set; }

    public string TransactionId { get; set; }

    public decimal Amount { get; set; }

    public DateTime PaymentDate { get; set; }
}