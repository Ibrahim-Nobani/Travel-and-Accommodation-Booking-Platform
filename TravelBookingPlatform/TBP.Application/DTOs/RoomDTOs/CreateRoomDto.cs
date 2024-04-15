namespace TravelBookingPlatform.Application.DTOs;

public class CreateRoomDto
{
    public int Number { get; set; }

    public decimal Price { get; set; }

    public string ThumbnailImageUrl { get; set; }

    public int AdultCapacity { get; set; }

    public int ChildCapacity { get; set; }

    public bool Availability { get; set; } = true;
}