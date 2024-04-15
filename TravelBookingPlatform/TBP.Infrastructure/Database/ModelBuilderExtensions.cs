using Microsoft.EntityFrameworkCore;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Domain.Enums;
namespace TravelBookingPlatform.Infrastructure.Database;

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>().HasData(
            new Role { Id = 1, RoleName = "User" },
            new Role { Id = 2, RoleName = "Admin" }
        );

        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Username = "user1", PasswordHash = "hashedPassword1", RoleId = 1, Email = "user1@example.com" },
            new User { Id = 2, Username = "user2", PasswordHash = "hashedPassword2", RoleId = 1, Email = "user2@example.com" },
            new User { Id = 3, Username = "user3", PasswordHash = "hashedPassword3", RoleId = 1, Email = "user3@example.com" },
            new User { Id = 4, Username = "user4", PasswordHash = "hashedPassword4", RoleId = 1, Email = "user4@example.com" },
            new User { Id = 5, Username = "user5", PasswordHash = "hashedPassword5", RoleId = 1, Email = "user5@example.com" }
        );

        modelBuilder.Entity<City>().HasData(
            new City { Id = 1, Name = "City 1", Country = "Country 1", PostOffice = "PostOffice 1", ThumbnailImageUrl = "/images/city1_thumbnail.jpg", CreatedAt = new DateTime(2023, 1, 1) },
            new City { Id = 2, Name = "City 2", Country = "Country 2", PostOffice = "PostOffice 2", ThumbnailImageUrl = "/images/city2_thumbnail.jpg", CreatedAt = new DateTime(2023, 1, 2) },
            new City { Id = 3, Name = "City 3", Country = "Country 3", PostOffice = "PostOffice 3", ThumbnailImageUrl = "/images/city3_thumbnail.jpg", CreatedAt = new DateTime(2023, 1, 3) },
            new City { Id = 4, Name = "City 4", Country = "Country 4", PostOffice = "PostOffice 4", ThumbnailImageUrl = "/images/city4_thumbnail.jpg", CreatedAt = new DateTime(2023, 1, 4) },
            new City { Id = 5, Name = "City 5", Country = "Country 5", PostOffice = "PostOffice 5", ThumbnailImageUrl = "/images/city5_thumbnail.jpg", CreatedAt = new DateTime(2023, 1, 5) }
        );

        modelBuilder.Entity<Hotel>().HasData(
            new Hotel { Id = 1, Name = "Hotel 1", StarRating = 3, Location = "Location 1", CityId = 1, Owner = "Owner 1", ThumbnailImageUrl = "/images/hotel1_thumbnail.jpg", CreatedAt = new DateTime(2023, 1, 1) },
            new Hotel { Id = 2, Name = "Hotel 2", StarRating = 4, Location = "Location 2", CityId = 2, Owner = "Owner 2", ThumbnailImageUrl = "/images/hotel2_thumbnail.jpg", CreatedAt = new DateTime(2023, 1, 2) },
            new Hotel { Id = 3, Name = "Hotel 3", StarRating = 5, Location = "Location 3", CityId = 3, Owner = "Owner 3", ThumbnailImageUrl = "/images/hotel3_thumbnail.jpg", CreatedAt = new DateTime(2023, 1, 3) },
            new Hotel { Id = 4, Name = "Hotel 4", StarRating = 3, Location = "Location 4", CityId = 4, Owner = "Owner 4", ThumbnailImageUrl = "/images/hotel4_thumbnail.jpg", CreatedAt = new DateTime(2023, 1, 4) },
            new Hotel { Id = 5, Name = "Hotel 5", StarRating = 4, Location = "Location 5", CityId = 5, Owner = "Owner 5", ThumbnailImageUrl = "/images/hotel5_thumbnail.jpg", CreatedAt = new DateTime(2023, 1, 5) }
        );

        modelBuilder.Entity<Room>().HasData(
            new Room { Id = 1, Number = 101, Price = 200, AdultCapacity = 2, ChildCapacity = 1, HotelId = 1, Availability = true, ThumbnailImageUrl = "/images/room1_thumbnail.jpg", CreatedAt = new DateTime(2023, 2, 1) },
            new Room { Id = 2, Number = 102, Price = 150, AdultCapacity = 3, ChildCapacity = 2, HotelId = 1, Availability = true, ThumbnailImageUrl = "/images/room2_thumbnail.jpg", CreatedAt = new DateTime(2023, 2, 2) },
            new Room { Id = 3, Number = 201, Price = 180, AdultCapacity = 2, ChildCapacity = 1, HotelId = 2, Availability = true, ThumbnailImageUrl = "/images/room3_thumbnail.jpg", CreatedAt = new DateTime(2023, 2, 3) },
            new Room { Id = 4, Number = 202, Price = 150, AdultCapacity = 4, ChildCapacity = 2, HotelId = 2, Availability = true, ThumbnailImageUrl = "/images/room4_thumbnail.jpg", CreatedAt = new DateTime(2023, 2, 4) },
            new Room { Id = 5, Number = 301, Price = 300, AdultCapacity = 2, ChildCapacity = 1, HotelId = 3, Availability = true, ThumbnailImageUrl = "/images/room5_thumbnail.jpg", CreatedAt = new DateTime(2023, 2, 5) }
        );

        modelBuilder.Entity<Booking>().HasData(
            new Booking { Id = 1, UserId = 1, RoomId = 1, CheckInDate = new DateTime(2023, 3, 1), CheckOutDate = new DateTime(2023, 3, 5), TotalPrice = 150, Status = "Confirmed", CreatedAt = new DateTime(2023, 3, 1) },
            new Booking { Id = 2, UserId = 2, RoomId = 3, CheckInDate = new DateTime(2023, 4, 10), CheckOutDate = new DateTime(2023, 4, 15), TotalPrice = 200, Status = "Confirmed", CreatedAt = new DateTime(2023, 4, 10) },
            new Booking { Id = 3, UserId = 3, RoomId = 5, CheckInDate = new DateTime(2023, 5, 20), CheckOutDate = new DateTime(2023, 5, 25), TotalPrice = 180, Status = "Confirmed", CreatedAt = new DateTime(2023, 5, 20) },
            new Booking { Id = 4, UserId = 1, RoomId = 2, CheckInDate = new DateTime(2023, 6, 5), CheckOutDate = new DateTime(2023, 6, 10), TotalPrice = 120, Status = "Confirmed", CreatedAt = new DateTime(2023, 6, 5) },
            new Booking { Id = 5, UserId = 2, RoomId = 4, CheckInDate = new DateTime(2023, 7, 15), CheckOutDate = new DateTime(2023, 7, 20), TotalPrice = 250, Status = "Confirmed", CreatedAt = new DateTime(2023, 7, 15) }
        );

        modelBuilder.Entity<FeaturedDeal>().HasData(
            new FeaturedDeal { Id = 1, RoomId = 1, OriginalPrice = 200, DiscountedPrice = 150, CreatedAt = new DateTime(2023, 8, 1) },
            new FeaturedDeal { Id = 2, RoomId = 2, OriginalPrice = 250, DiscountedPrice = 200, CreatedAt = new DateTime(2023, 8, 2) },
            new FeaturedDeal { Id = 3, RoomId = 3, OriginalPrice = 180, DiscountedPrice = 150, CreatedAt = new DateTime(2023, 8, 3) },
            new FeaturedDeal { Id = 4, RoomId = 4, OriginalPrice = 150, DiscountedPrice = 120, CreatedAt = new DateTime(2023, 8, 4) },
            new FeaturedDeal { Id = 5, RoomId = 5, OriginalPrice = 300, DiscountedPrice = 250, CreatedAt = new DateTime(2023, 8, 5) }
        );

        modelBuilder.Entity<UserVisit>().HasData(
            new UserVisit { Id = 1, UserId = 1, HotelId = 1, VisitDateTime = new DateTime(2023, 8, 1) },
            new UserVisit { Id = 2, UserId = 1, HotelId = 2, VisitDateTime = new DateTime(2023, 8, 1) },
            new UserVisit { Id = 3, UserId = 1, HotelId = 3, VisitDateTime = new DateTime(2023, 8, 1) },
            new UserVisit { Id = 4, UserId = 1, HotelId = 4, VisitDateTime = new DateTime(2023, 8, 1) },
            new UserVisit { Id = 5, UserId = 1, HotelId = 5, VisitDateTime = new DateTime(2023, 8, 1) },
            new UserVisit { Id = 6, UserId = 2, HotelId = 1, VisitDateTime = new DateTime(2023, 8, 1) },
            new UserVisit { Id = 7, UserId = 2, HotelId = 2, VisitDateTime = new DateTime(2023, 8, 1) },
            new UserVisit { Id = 8, UserId = 2, HotelId = 3, VisitDateTime = new DateTime(2023, 8, 1) },
            new UserVisit { Id = 9, UserId = 2, HotelId = 4, VisitDateTime = new DateTime(2023, 8, 1) },
            new UserVisit { Id = 10, UserId = 2, HotelId = 5, VisitDateTime = new DateTime(2023, 8, 1) }
        );


        modelBuilder.Entity<Image>().HasData(
            new Image { Id = 1, ImageUrl = "/images/hotel1_image1.jpg", ImageType = ImageType.Hotel, EntityId = 1 },
            new Image { Id = 2, ImageUrl = "/images/hotel1_image2.jpg", ImageType = ImageType.Hotel, EntityId = 1 },
            new Image { Id = 3, ImageUrl = "/images/hotel1_image3.jpg", ImageType = ImageType.Hotel, EntityId = 2 },
            new Image { Id = 4, ImageUrl = "/images/hotel1_image4.jpg", ImageType = ImageType.Hotel, EntityId = 2 },
            new Image { Id = 5, ImageUrl = "/images/hotel1_image5.jpg", ImageType = ImageType.Hotel, EntityId = 3 },
            new Image { Id = 6, ImageUrl = "/images/hotel1_image6.jpg", ImageType = ImageType.Hotel, EntityId = 3 },
            new Image { Id = 7, ImageUrl = "/images/hotel1_image7.jpg", ImageType = ImageType.Hotel, EntityId = 4 },
            new Image { Id = 8, ImageUrl = "/images/hotel1_image8.jpg", ImageType = ImageType.Hotel, EntityId = 4 },
            new Image { Id = 9, ImageUrl = "/images/hotel1_image9.jpg", ImageType = ImageType.Hotel, EntityId = 5 },
            new Image { Id = 10, ImageUrl = "/images/hotel1_image10.jpg", ImageType = ImageType.Hotel, EntityId = 5 },

            new Image { Id = 11, ImageUrl = "/images/room1_image1.jpg", ImageType = ImageType.Room, EntityId = 1 },
            new Image { Id = 12, ImageUrl = "/images/room1_image2.jpg", ImageType = ImageType.Room, EntityId = 1 },
            new Image { Id = 13, ImageUrl = "/images/room1_image3.jpg", ImageType = ImageType.Room, EntityId = 2 },
            new Image { Id = 14, ImageUrl = "/images/room1_image4.jpg", ImageType = ImageType.Room, EntityId = 2 },
            new Image { Id = 15, ImageUrl = "/images/room1_image5.jpg", ImageType = ImageType.Room, EntityId = 3 },
            new Image { Id = 16, ImageUrl = "/images/room1_image6.jpg", ImageType = ImageType.Room, EntityId = 3 },
            new Image { Id = 17, ImageUrl = "/images/room1_image7.jpg", ImageType = ImageType.Room, EntityId = 4 },
            new Image { Id = 18, ImageUrl = "/images/room1_image8.jpg", ImageType = ImageType.Room, EntityId = 5 }
        );

    }
}
