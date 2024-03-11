using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelBookingPlatform.Migrations
{
    /// <inheritdoc />
    public partial class ThumbanilHotelsAndRooms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ThumbnailImageUrl",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ThumbnailImageUrl",
                table: "Hotels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 1,
                column: "ThumbnailImageUrl",
                value: "/images/hotel1_thumbnail.jpg");

            migrationBuilder.UpdateData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 2,
                column: "ThumbnailImageUrl",
                value: "/images/hotel2_thumbnail.jpg");

            migrationBuilder.UpdateData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 3,
                column: "ThumbnailImageUrl",
                value: "/images/hotel3_thumbnail.jpg");

            migrationBuilder.UpdateData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 4,
                column: "ThumbnailImageUrl",
                value: "/images/hotel4_thumbnail.jpg");

            migrationBuilder.UpdateData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 5,
                column: "ThumbnailImageUrl",
                value: "/images/hotel5_thumbnail.jpg");

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1,
                column: "ThumbnailImageUrl",
                value: "/images/room1_thumbnail.jpg");

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2,
                column: "ThumbnailImageUrl",
                value: "/images/room2_thumbnail.jpg");

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3,
                column: "ThumbnailImageUrl",
                value: "/images/room3_thumbnail.jpg");

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 4,
                column: "ThumbnailImageUrl",
                value: "/images/room4_thumbnail.jpg");

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 5,
                column: "ThumbnailImageUrl",
                value: "/images/room5_thumbnail.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThumbnailImageUrl",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "ThumbnailImageUrl",
                table: "Hotels");
        }
    }
}
