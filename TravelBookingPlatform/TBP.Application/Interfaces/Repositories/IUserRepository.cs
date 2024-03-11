using TravelBookingPlatform.Domain.Entities;
namespace TravelBookingPlatform.Application.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<bool> IsUsernameTakenAsync(string username);
    Task<bool> IsEmailTakenAsync(string email);
    Task<User> GetByUsernameAsync(string username);
}