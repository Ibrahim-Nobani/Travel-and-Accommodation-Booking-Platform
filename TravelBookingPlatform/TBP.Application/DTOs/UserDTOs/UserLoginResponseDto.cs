namespace TravelBookingPlatform.Application.DTOs;

public class UserLoginResponseDto
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string AccessToken { get; set; } = string.Empty;
}