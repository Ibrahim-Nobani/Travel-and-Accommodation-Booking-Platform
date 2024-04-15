using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelBookingPlatform.Domain.Entities;

namespace TravelBookingPlatform.Infrastructure.Database.Configurations;

public class ImageConfiguration : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.ImageUrl)
               .IsRequired()
               .HasMaxLength(255);

        builder.Property(i => i.EntityId)
               .IsRequired();

        builder.Property(i => i.ImageType)
               .IsRequired();

        builder.HasOne(i => i.Hotel)
               .WithMany(h => h.Images)
               .HasForeignKey(i => i.EntityId)
               .HasPrincipalKey(h => h.Id)
               .IsRequired(false);

        builder.HasOne(i => i.Room)
               .WithMany(r => r.Images)
               .HasForeignKey(i => i.EntityId)
               .HasPrincipalKey(r => r.Id)
               .IsRequired(false);
    }
}