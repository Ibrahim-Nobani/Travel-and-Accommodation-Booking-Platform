namespace TravelBookingPlatform.Application.DTOs;

public class SearchRoomCriteria
{
    public decimal? Price { get; set; }
    public int AdultCapacity { get; set; } = 2;
    public int ChildCapacity { get; set; } = 0;
    public bool Availability { get; set; } = true;
    public DateTime CheckInDate { get; set; } = DateTime.Today;
    public DateTime CheckOutDate { get; set; } = DateTime.Today.AddDays(1);
}
