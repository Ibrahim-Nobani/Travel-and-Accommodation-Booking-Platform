using Microsoft.EntityFrameworkCore;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Application.Interfaces;
using TravelBookingPlatform.Infrastructure.Database;
namespace TravelBookingPlatform.Infrastructure.Repositories;

public class PaymentTransactionRepository : IPaymentTransactionRepository
{
    private readonly TravelBookingPlatformDbContext _dbContext;

    public PaymentTransactionRepository(TravelBookingPlatformDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PaymentTransaction> GetByIdAsync(int paymentTransactionId)
    {
        return await _dbContext.PaymentTransactions.FindAsync(paymentTransactionId);
    }

    public async Task<IEnumerable<PaymentTransaction>> GetAllAsync()
    {
        return await _dbContext.PaymentTransactions.ToListAsync();
    }

    public void AddAsync(PaymentTransaction paymentTransaction)
    {
        _dbContext.PaymentTransactions.Add(paymentTransaction);
    }

    public void UpdateAsync(PaymentTransaction paymentTransaction)
    {
        _dbContext.Update(paymentTransaction);
    }

    public void DeleteAsync(PaymentTransaction paymentTransaction)
    {
        _dbContext.PaymentTransactions.Remove(paymentTransaction);
    }
}