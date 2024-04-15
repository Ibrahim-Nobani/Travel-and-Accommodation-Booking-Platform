using Microsoft.EntityFrameworkCore;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Application.Interfaces;
using TravelBookingPlatform.Infrastructure.Database;
namespace TravelBookingPlatform.Infrastructure.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly TravelBookingPlatformDbContext _dbContext;

    public RoleRepository(TravelBookingPlatformDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Role> GetByIdAsync(int roleId)
    {
        return await _dbContext.Roles.FindAsync(roleId);
    }

    public async Task<IEnumerable<Role>> GetAllAsync()
    {
        return await _dbContext.Roles.ToListAsync();
    }

    public void AddAsync(Role role)
    {
        _dbContext.Roles.Add(role);
    }

    public void UpdateAsync(Role role)
    {
        _dbContext.Update(role);
    }

    public void DeleteAsync(Role role)
    {
        _dbContext.Roles.Remove(role);
    }
}