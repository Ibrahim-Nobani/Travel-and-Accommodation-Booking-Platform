namespace TravelBookingPlatform.Application.DTOs;

public class SearchHotelCriteria
{
    public string? Name { get; set; }

    public int? StarRating { get; set; }

    public string? Location { get; set; }

    public string? CityName { get; set; }

    public string? Owner { get; set; }
}