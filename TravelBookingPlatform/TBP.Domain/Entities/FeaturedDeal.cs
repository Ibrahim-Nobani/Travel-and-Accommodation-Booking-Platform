using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TravelBookingPlatform.Domain.Entities;

public class FeaturedDeal
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Room")]
    public int RoomId { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal OriginalPrice { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal DiscountedPrice { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Room Room { get; set; }
}