using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelBookingPlatform.Domain.Entities;

public class PaymentTransaction
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Booking")]
    public int BookingId { get; set; }

    public string TransactionId { get; set; }

    public decimal Amount { get; set; }

    public DateTime PaymentDate { get; set; }

    public Booking Booking { get; set; }
}