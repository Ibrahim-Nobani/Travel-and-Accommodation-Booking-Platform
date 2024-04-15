using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TravelBookingPlatform.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Country", "CreatedAt", "DeletedAt", "Name", "PostOffice", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "Country 1", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "City 1", "PostOffice 1", null },
                    { 2, "Country 2", new DateTime(2023, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "City 2", "PostOffice 2", null },
                    { 3, "Country 3", new DateTime(2023, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "City 3", "PostOffice 3", null },
                    { 4, "Country 4", new DateTime(2023, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "City 4", "PostOffice 4", null },
                    { 5, "Country 5", new DateTime(2023, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "City 5", "PostOffice 5", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "PasswordHash", "Username" },
                values: new object[,]
                {
                    { 1, "hashedPassword1", "user1" },
                    { 2, "hashedPassword2", "user2" },
                    { 3, "hashedPassword3", "user3" },
                    { 4, "hashedPassword4", "user4" },
                    { 5, "hashedPassword5", "user5" }
                });

            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "Id", "CityId", "CreatedAt", "DeletedAt", "Location", "Name", "Owner", "StarRating", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Location 1", "Hotel 1", "Owner 1", 3, null },
                    { 2, 2, new DateTime(2023, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Location 2", "Hotel 2", "Owner 2", 4, null },
                    { 3, 3, new DateTime(2023, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Location 3", "Hotel 3", "Owner 3", 5, null },
                    { 4, 4, new DateTime(2023, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Location 4", "Hotel 4", "Owner 4", 3, null },
                    { 5, 5, new DateTime(2023, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Location 5", "Hotel 5", "Owner 5", 4, null }
                });

            migrationBuilder.InsertData(
                table: "TrendingDestinations",
                columns: new[] { "Id", "CityId", "CreatedAt", "DeletedAt", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null },
                    { 2, 2, new DateTime(2023, 9, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null },
                    { 3, 3, new DateTime(2023, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null },
                    { 4, 4, new DateTime(2023, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null },
                    { 5, 5, new DateTime(2023, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null }
                });

            migrationBuilder.InsertData(
                table: "FeaturedDeals",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "DiscountedPrice", "HotelId", "OriginalPrice", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 150m, 1, 200m, null },
                    { 2, new DateTime(2023, 8, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 200m, 2, 250m, null },
                    { 3, new DateTime(2023, 8, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 150m, 3, 180m, null },
                    { 4, new DateTime(2023, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 120m, 4, 150m, null },
                    { 5, new DateTime(2023, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 250m, 5, 300m, null }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "AdultCapacity", "ChildCapacity", "CreatedAt", "DeletedAt", "HotelId", "Number", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 2, 1, new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, 101, null },
                    { 2, 3, 2, new DateTime(2023, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, 102, null },
                    { 3, 2, 1, new DateTime(2023, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2, 201, null },
                    { 4, 4, 2, new DateTime(2023, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2, 202, null },
                    { 5, 2, 1, new DateTime(2023, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3, 301, null }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "CheckInDate", "CheckOutDate", "CreatedAt", "DeletedAt", "RoomId", "Status", "TotalPrice", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, "Confirmed", 150m, null, 1 },
                    { 2, new DateTime(2023, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3, "Confirmed", 200m, null, 2 },
                    { 3, new DateTime(2023, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 5, "Confirmed", 180m, null, 3 },
                    { 4, new DateTime(2023, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2, "Confirmed", 120m, null, 1 },
                    { 5, new DateTime(2023, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 4, "Confirmed", 250m, null, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "FeaturedDeals",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FeaturedDeals",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "FeaturedDeals",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "FeaturedDeals",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "FeaturedDeals",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "TrendingDestinations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TrendingDestinations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TrendingDestinations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TrendingDestinations",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TrendingDestinations",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
