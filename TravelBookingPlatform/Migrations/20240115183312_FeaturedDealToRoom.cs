using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelBookingPlatform.Migrations
{
    /// <inheritdoc />
    public partial class FeaturedDealToRoom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FeaturedDealId",
                table: "Rooms",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1,
                column: "FeaturedDealId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2,
                column: "FeaturedDealId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3,
                column: "FeaturedDealId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 4,
                column: "FeaturedDealId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 5,
                column: "FeaturedDealId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_FeaturedDealId",
                table: "Rooms",
                column: "FeaturedDealId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_FeaturedDeals_FeaturedDealId",
                table: "Rooms",
                column: "FeaturedDealId",
                principalTable: "FeaturedDeals",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_FeaturedDeals_FeaturedDealId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_FeaturedDealId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "FeaturedDealId",
                table: "Rooms");

            migrationBuilder.AddColumn<int>(
                name: "HotelId",
                table: "FeaturedDeals",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "FeaturedDeals",
                keyColumn: "Id",
                keyValue: 1,
                column: "HotelId",
                value: null);

            migrationBuilder.UpdateData(
                table: "FeaturedDeals",
                keyColumn: "Id",
                keyValue: 2,
                column: "HotelId",
                value: null);

            migrationBuilder.UpdateData(
                table: "FeaturedDeals",
                keyColumn: "Id",
                keyValue: 3,
                column: "HotelId",
                value: null);

            migrationBuilder.UpdateData(
                table: "FeaturedDeals",
                keyColumn: "Id",
                keyValue: 4,
                column: "HotelId",
                value: null);

            migrationBuilder.UpdateData(
                table: "FeaturedDeals",
                keyColumn: "Id",
                keyValue: 5,
                column: "HotelId",
                value: null);
        }
    }
}
