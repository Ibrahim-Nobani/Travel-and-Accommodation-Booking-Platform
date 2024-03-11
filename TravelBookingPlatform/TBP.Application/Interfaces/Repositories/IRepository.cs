namespace TravelBookingPlatform.Application.Interfaces;

public interface IRepository<T>
{
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    void AddAsync(T entity);
    void UpdateAsync(T entity);
    void DeleteAsync(T entity);
}
