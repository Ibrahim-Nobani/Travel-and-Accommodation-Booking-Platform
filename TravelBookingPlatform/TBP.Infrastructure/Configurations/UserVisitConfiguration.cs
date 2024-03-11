using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelBookingPlatform.Domain.Entities;

namespace TravelBookingPlatform.Infrastructure.Database.Configurations;

public class UserVisitConfiguration : IEntityTypeConfiguration<UserVisit>
{
    public void Configure(EntityTypeBuilder<UserVisit> builder)
    {
        builder.HasKey(uv => uv.Id);

        builder.Property(uv => uv.VisitDateTime)
               .IsRequired();

        builder.HasOne(uv => uv.User)
               .WithMany(u => u.VisitedHotels)
               .HasForeignKey(uv => uv.UserId)
               .IsRequired();

        builder.HasOne(uv => uv.Hotel)
               .WithMany()
               .HasForeignKey(uv => uv.HotelId)
               .IsRequired();
    }
}
