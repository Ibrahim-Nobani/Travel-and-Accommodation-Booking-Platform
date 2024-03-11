using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelBookingPlatform.Domain.Entities;

namespace TravelBookingPlatform.Infrastructure.Database.Configurations;

public class PaymentTransactionConfiguration : IEntityTypeConfiguration<PaymentTransaction>
{
    public void Configure(EntityTypeBuilder<PaymentTransaction> builder)
    {
        builder.HasKey(pt => pt.Id);

        builder.Property(pt => pt.TransactionId)
               .IsRequired();

        builder.Property(pt => pt.Amount)
               .HasColumnType("decimal(18, 2)")
               .IsRequired();

        builder.Property(pt => pt.PaymentDate)
               .IsRequired()
               .HasDefaultValueSql("GETDATE()");
    }
}