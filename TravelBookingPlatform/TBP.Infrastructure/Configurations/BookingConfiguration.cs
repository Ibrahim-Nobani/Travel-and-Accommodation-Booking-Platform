using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelBookingPlatform.Domain.Entities;

namespace TravelBookingPlatform.Infrastructure.Database.Configurations;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
       public void Configure(EntityTypeBuilder<Booking> builder)
       {
              builder.HasKey(b => b.Id);

              builder.Property(b => b.CheckInDate)
                     .IsRequired();

              builder.Property(b => b.CheckOutDate)
                     .IsRequired();

              builder.Property(b => b.TotalPrice)
                     .HasColumnType("decimal(18, 2)")
                     .IsRequired();

              builder.Property(b => b.Status)
                     .HasMaxLength(50)
                     .IsRequired();

              builder.Property(b => b.CreatedAt)
                     .IsRequired()
                     .HasDefaultValueSql("GETDATE()");

              builder.Property(b => b.UpdatedAt)
                     .IsRequired(false);

              builder.HasOne(b => b.User)
                     .WithMany(u => u.Bookings)
                     .HasForeignKey(b => b.UserId)
                     .IsRequired();

              builder.HasOne(b => b.Room)
                     .WithMany(r => r.Bookings)
                     .HasForeignKey(b => b.RoomId)
                     .IsRequired();

              builder.HasOne(b => b.PaymentTransaction)
                     .WithOne(pt => pt.Booking)
                     .HasForeignKey<PaymentTransaction>(pt => pt.BookingId);
       }
}