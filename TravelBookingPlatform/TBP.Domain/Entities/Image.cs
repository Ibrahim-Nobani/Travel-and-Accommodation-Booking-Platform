using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TravelBookingPlatform.Domain.Enums;
namespace TravelBookingPlatform.Domain.Entities;

public class Image
{
    [Key]
    public int Id { get; set; }

    public string ImageUrl { get; set; }

    public int EntityId { get; set; }

    public ImageType ImageType { get; set; }

    [ForeignKey(nameof(EntityId))]
    [InverseProperty("Images")]
    public Hotel? Hotel { get; set; }

    [ForeignKey(nameof(EntityId))]
    [InverseProperty("Images")]
    public Room? Room { get; set; }
}