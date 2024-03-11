using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TravelBookingPlatform.Domain.Entities;

public class User
{
    [Key]
    public int Id { get; set; }

    public string Username { get; set; }

    public string PasswordHash { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    [ForeignKey("Role")]
    public int RoleId { get; set; }

    public Role Role { get; set; }

    public ICollection<Booking> Bookings { get; set; }

    public ICollection<UserVisit> VisitedHotels { get; set; }
}