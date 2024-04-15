using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelBookingPlatform.Domain.Entities;

namespace TravelBookingPlatform.Infrastructure.Database.Configurations;

public class FeaturedDealConfiguration : IEntityTypeConfiguration<FeaturedDeal>
{
       public void Configure(EntityTypeBuilder<FeaturedDeal> builder)
       {
              builder.HasKey(fd => fd.Id);

              builder.Property(fd => fd.OriginalPrice)
                     .HasColumnType("decimal(18, 2)")
                     .IsRequired();

              builder.Property(fd => fd.DiscountedPrice)
                     .HasColumnType("decimal(18, 2)")
                     .IsRequired();

              builder.Property(fd => fd.CreatedAt)
                     .IsRequired()
                     .HasDefaultValueSql("GETDATE()");

              builder.Property(fd => fd.UpdatedAt)
                     .IsRequired(false);
       }
}
