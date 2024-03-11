using Microsoft.EntityFrameworkCore;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Application.Interfaces;
using TravelBookingPlatform.Infrastructure.Database;
using TravelBookingPlatform.Application.DTOs;
namespace TravelBookingPlatform.Infrastructure.Repositories;

public class RoomRepository : IRoomRepository
{
    private readonly TravelBookingPlatformDbContext _dbContext;

    public RoomRepository(TravelBookingPlatformDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Room> GetByIdAsync(int roomId)
    {
        return await _dbContext.Rooms.FindAsync(roomId);
    }

    public async Task<IEnumerable<Room>> GetAllAsync()
    {
        return await _dbContext.Rooms.ToListAsync();
    }

    public void AddAsync(Room room)
    {
        _dbContext.Rooms.Add(room);
    }

    public void UpdateAsync(Room room)
    {
        _dbContext.Update(room);
    }

    public void DeleteAsync(Room room)
    {
        _dbContext.Rooms.Remove(room);
    }

    public async Task<IEnumerable<Room>> GetRoomsForHotel(int hotelId)
    {
        return await _dbContext.Rooms.Where(r => r.HotelId == hotelId).ToListAsync();
    }

    public async Task<IEnumerable<Room>> GetAvailableRoomsForHotel(int hotelId)
    {
        return await _dbContext.Rooms
            .Where(r => r.HotelId == hotelId && r.Availability == true)
            .ToListAsync();
    }

    public async Task<IEnumerable<Room>> SearchAsync(SearchRoomCriteria searchRoomCriteria)
    {
        var query = _dbContext.Rooms
                   .AsQueryable();

        if (searchRoomCriteria.Price.HasValue)
        {
            query = query.Where(r => r.Price == searchRoomCriteria.Price);
        }

        query = query.Where(r =>
            r.AdultCapacity == searchRoomCriteria.AdultCapacity &&
            r.ChildCapacity == searchRoomCriteria.ChildCapacity &&
            r.Availability == searchRoomCriteria.Availability);

        query = ApplyDateFilter(query, searchRoomCriteria.CheckInDate, searchRoomCriteria.CheckOutDate);

        return await query.ToListAsync();
    }

    public async Task<bool> CheckIfExistsByIdAsync(int roomId)
    {
        return await _dbContext.Rooms.AnyAsync(r => r.Id == roomId);
    }

    private IQueryable<Room> ApplyDateFilter(IQueryable<Room> query, DateTime checkInDate, DateTime checkOutDate)
    {
        var overlappingRoomIds = _dbContext.Bookings
            .Where(b =>
                (b.CheckInDate < checkOutDate && b.CheckOutDate > checkInDate)
                || (b.CheckInDate >= checkInDate && b.CheckInDate < checkOutDate)
                || (b.CheckOutDate > checkInDate && b.CheckOutDate <= checkOutDate))
            .Select(b => b.RoomId);

        return query.Where(r => !overlappingRoomIds.Contains(r.Id));
    }
}