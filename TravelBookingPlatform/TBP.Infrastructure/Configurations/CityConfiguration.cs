using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelBookingPlatform.Domain.Entities;

namespace TravelBookingPlatform.Infrastructure.Database.Configurations;

public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(c => c.Country)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(c => c.PostOffice)
               .HasMaxLength(100);

        builder.Property(c => c.ThumbnailImageUrl)
               .HasMaxLength(255);

        builder.Property(c => c.CreatedAt)
               .IsRequired()
               .HasDefaultValueSql("GETDATE()");

        builder.Property(c => c.UpdatedAt)
               .IsRequired(false);

        builder.HasMany(c => c.Hotels)
               .WithOne(h => h.City)
               .HasForeignKey(h => h.CityId);
    }
}
