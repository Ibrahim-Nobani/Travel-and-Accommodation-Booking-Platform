using System.ComponentModel.DataAnnotations;
namespace TravelBookingPlatform.Domain.Entities;

public class Role
{
    [Key]
    public int Id { get; set; }

    public string RoleName { get; set; }

    public ICollection<User> Users { get; set; }
}
