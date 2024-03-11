using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelBookingPlatform.Domain.Entities;

namespace TravelBookingPlatform.Infrastructure.Database.Configurations;

public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
{
    public void Configure(EntityTypeBuilder<Hotel> builder)
    {
        builder.HasKey(h => h.Id);

        builder.Property(h => h.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(h => h.StarRating)
               .IsRequired(false);

        builder.Property(h => h.Location)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(h => h.ThumbnailImageUrl)
               .HasMaxLength(255);

        builder.Property(h => h.CityId)
               .IsRequired();

        builder.Property(h => h.Owner)
               .HasMaxLength(100);

        builder.Property(h => h.CreatedAt)
               .IsRequired()
               .HasDefaultValueSql("GETDATE()");

        builder.Property(h => h.UpdatedAt)
               .IsRequired(false);

        builder.HasOne(h => h.City)
               .WithMany(c => c.Hotels)
               .HasForeignKey(h => h.CityId)
               .IsRequired();

        builder.HasMany(h => h.Rooms)
               .WithOne(r => r.Hotel)
               .HasForeignKey(r => r.HotelId);

        builder.HasMany(h => h.Images)
               .WithOne(i => i.Hotel)
               .HasForeignKey(i => i.EntityId);
    }
}