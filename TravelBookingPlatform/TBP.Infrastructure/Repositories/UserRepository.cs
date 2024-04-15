using Microsoft.EntityFrameworkCore;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Application.Interfaces;
using TravelBookingPlatform.Infrastructure.Database;
namespace TravelBookingPlatform.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly TravelBookingPlatformDbContext _dbContext;

    public UserRepository(TravelBookingPlatformDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User> GetByIdAsync(int userId)
    {
        return await _dbContext.Users.FindAsync(userId);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _dbContext.Users.ToListAsync();
    }

    public void AddAsync(User user)
    {
        _dbContext.Users.Add(user);
    }

    public void UpdateAsync(User user)
    {
        _dbContext.Update(user);
    }

    public void DeleteAsync(User user)
    {
        _dbContext.Users.Remove(user);
    }

    public async Task<bool> IsUsernameTakenAsync(string username)
    {
        return await _dbContext.Users.AnyAsync(u => u.Username == username);
    }

    public async Task<bool> IsEmailTakenAsync(string email)
    {
        return await _dbContext.Users.AnyAsync(u => u.Email == email);
    }

    public async Task<User> GetByUsernameAsync(string username)
    {
        return await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Username == username);
    }
}