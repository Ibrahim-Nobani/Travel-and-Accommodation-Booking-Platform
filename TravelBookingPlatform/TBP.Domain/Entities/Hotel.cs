using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TravelBookingPlatform.Domain.Entities;

public class Hotel
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; }

    public int? StarRating { get; set; }

    public string Location { get; set; }

    public string ThumbnailImageUrl { get; set; }

    [ForeignKey("City")]
    public int CityId { get; set; }

    public string Owner { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public City City { get; set; }

    public ICollection<Room> Rooms { get; set; }

    public ICollection<Image> Images { get; set; }
}