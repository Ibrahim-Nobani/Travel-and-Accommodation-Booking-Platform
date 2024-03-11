using Microsoft.EntityFrameworkCore;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Domain.Enums;
using TravelBookingPlatform.Application.Interfaces;
using TravelBookingPlatform.Infrastructure.Database;
namespace TravelBookingPlatform.Infrastructure.Repositories;

public class ImageRepository : IImageRepository
{
    private readonly TravelBookingPlatformDbContext _dbContext;

    public ImageRepository(TravelBookingPlatformDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Image> GetByIdAsync(int imageId)
    {
        return await _dbContext.Images.FindAsync(imageId);
    }

    public async Task<IEnumerable<Image>> GetAllAsync()
    {
        return await _dbContext.Images.ToListAsync();
    }

    public void AddAsync(Image image)
    {
        _dbContext.Images.Add(image);
    }

    public void UpdateAsync(Image image)
    {
        _dbContext.Update(image);
    }

    public void DeleteAsync(Image image)
    {
        _dbContext.Images.Remove(image);
    }

    public async Task<IEnumerable<Image>> GetRoomImagesAsync(int roomId)
    {
        return await _dbContext.Images
            .Where(i => i.ImageType == ImageType.Room && i.EntityId == roomId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Image>> GetHotelImagesAsync(int hotelId)
    {
        return await _dbContext.Images
            .Where(i => i.ImageType == ImageType.Hotel && i.EntityId == hotelId)
            .ToListAsync();
    }
}