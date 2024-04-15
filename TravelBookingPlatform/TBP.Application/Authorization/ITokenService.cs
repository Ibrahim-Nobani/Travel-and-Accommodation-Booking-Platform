using TravelBookingPlatform.Domain.Entities;
namespace TravelBookingPlatform.Application.Authorization;

public interface ITokenService
{
    Task<string> GenerateTokenAsync(User user);
}