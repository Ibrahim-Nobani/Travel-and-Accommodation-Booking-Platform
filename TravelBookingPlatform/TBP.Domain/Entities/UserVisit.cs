using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelBookingPlatform.Domain.Entities;

public class UserVisit
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("User")]
    public int UserId { get; set; }

    [ForeignKey("Hotel")]
    public int HotelId { get; set; }

    public DateTime VisitDateTime { get; set; }

    public User User { get; set; }

    public Hotel Hotel { get; set; }
}