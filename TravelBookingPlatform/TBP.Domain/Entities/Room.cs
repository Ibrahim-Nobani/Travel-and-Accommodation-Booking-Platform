using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TravelBookingPlatform.Domain.Entities;

public class Room
{
    [Key]
    public int Id { get; set; }

    public int Number { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }

    public int AdultCapacity { get; set; }

    public int ChildCapacity { get; set; }

    public string ThumbnailImageUrl { get; set; }

    public bool Availability { get; set; }

    [ForeignKey("Hotel")]
    public int HotelId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Hotel Hotel { get; set; }

    public ICollection<Booking> Bookings { get; set; }

    public FeaturedDeal FeaturedDeal { get; set; }

    public ICollection<Image> Images { get; set; }
}