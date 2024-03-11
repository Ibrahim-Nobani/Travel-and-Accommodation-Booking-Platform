using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelBookingPlatform.Domain.Entities;

namespace TravelBookingPlatform.Infrastructure.Database.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
       public void Configure(EntityTypeBuilder<User> builder)
       {
              builder.HasKey(u => u.Id);

              builder.Property(u => u.Username)
                     .IsRequired()
                     .HasMaxLength(100);

              builder.Property(u => u.Email)
                     .IsRequired()
                     .HasMaxLength(100);

              builder.HasIndex(u => u.Username)
                     .IsUnique();

              builder.HasIndex(u => u.Email)
                     .IsUnique();

              builder.Property(u => u.PasswordHash)
                     .IsRequired();

              builder.HasOne(u => u.Role)
                     .WithMany(r => r.Users)
                     .HasForeignKey(u => u.RoleId)
                     .IsRequired();

              builder.HasMany(u => u.Bookings)
                     .WithOne(b => b.User)
                     .HasForeignKey(b => b.UserId)
                     .IsRequired();

              builder.HasMany(u => u.VisitedHotels)
                     .WithOne(uv => uv.User)
                     .HasForeignKey(uv => uv.UserId)
                     .IsRequired();
       }
}
