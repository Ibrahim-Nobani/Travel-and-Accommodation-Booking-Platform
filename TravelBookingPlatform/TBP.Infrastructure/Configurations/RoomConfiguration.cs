using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelBookingPlatform.Domain.Entities;

namespace TravelBookingPlatform.Infrastructure.Database.Configurations;

public class RoomConfiguration : IEntityTypeConfiguration<Room>
{
       public void Configure(EntityTypeBuilder<Room> builder)
       {
              builder.HasKey(r => r.Id);

              builder.Property(r => r.Number)
                     .IsRequired();

              builder.Property(r => r.Price)
                     .HasColumnType("decimal(18, 2)")
                     .IsRequired();

              builder.Property(r => r.AdultCapacity)
                     .IsRequired();

              builder.Property(r => r.ChildCapacity)
                     .IsRequired();

              builder.Property(r => r.ThumbnailImageUrl)
                     .HasMaxLength(255);

              builder.Property(r => r.Availability)
                     .IsRequired()
                      .HasDefaultValue(true);

              builder.Property(r => r.CreatedAt)
                     .IsRequired()
                     .HasDefaultValueSql("GETDATE()");

              builder.Property(r => r.UpdatedAt)
                     .IsRequired(false);

              builder.HasOne(r => r.Hotel)
                     .WithMany(h => h.Rooms)
                     .HasForeignKey(r => r.HotelId);

              builder.HasMany(r => r.Bookings)
                     .WithOne(b => b.Room)
                     .HasForeignKey(b => b.RoomId);

              builder.HasMany(r => r.Images)
                     .WithOne(i => i.Room)
                     .HasForeignKey(i => i.EntityId);
       }
}