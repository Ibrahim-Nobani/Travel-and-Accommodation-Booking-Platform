using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelBookingPlatform.Migrations
{
    /// <inheritdoc />
    public partial class AddingPriceToRoomRoomToFeaturedDeals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeaturedDeals_Hotels_HotelId",
                table: "FeaturedDeals");
            migrationBuilder.DropIndex(
                name: "IX_FeaturedDeals_HotelId",
                table: "FeaturedDeals");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Rooms",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<int>(
                name: "HotelId",
                table: "FeaturedDeals",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "FeaturedDeals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "FeaturedDeals",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "HotelId", "RoomId" },
                values: new object[] { null, 1 });

            migrationBuilder.UpdateData(
                table: "FeaturedDeals",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "HotelId", "RoomId" },
                values: new object[] { null, 2 });

            migrationBuilder.UpdateData(
                table: "FeaturedDeals",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "HotelId", "RoomId" },
                values: new object[] { null, 3 });

            migrationBuilder.UpdateData(
                table: "FeaturedDeals",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "HotelId", "RoomId" },
                values: new object[] { null, 4 });

            migrationBuilder.UpdateData(
                table: "FeaturedDeals",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "HotelId", "RoomId" },
                values: new object[] { null, 5 });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1,
                column: "Price",
                value: 200m);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2,
                column: "Price",
                value: 150m);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3,
                column: "Price",
                value: 180m);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 4,
                column: "Price",
                value: 150m);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 5,
                column: "Price",
                value: 300m);

            migrationBuilder.CreateIndex(
                name: "IX_FeaturedDeals_RoomId",
                table: "FeaturedDeals",
                column: "RoomId");


            migrationBuilder.AddForeignKey(
                name: "FK_FeaturedDeals_Rooms_RoomId",
                table: "FeaturedDeals",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.DropColumn(
                name: "HotelId",
                table: "FeaturedDeals");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeaturedDeals_Hotels_HotelId",
                table: "FeaturedDeals");

            migrationBuilder.DropForeignKey(
                name: "FK_FeaturedDeals_Rooms_RoomId",
                table: "FeaturedDeals");

            migrationBuilder.DropIndex(
                name: "IX_FeaturedDeals_RoomId",
                table: "FeaturedDeals");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "FeaturedDeals");

            migrationBuilder.AlterColumn<int>(
                name: "HotelId",
                table: "FeaturedDeals",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "FeaturedDeals",
                keyColumn: "Id",
                keyValue: 1,
                column: "HotelId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "FeaturedDeals",
                keyColumn: "Id",
                keyValue: 2,
                column: "HotelId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "FeaturedDeals",
                keyColumn: "Id",
                keyValue: 3,
                column: "HotelId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "FeaturedDeals",
                keyColumn: "Id",
                keyValue: 4,
                column: "HotelId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "FeaturedDeals",
                keyColumn: "Id",
                keyValue: 5,
                column: "HotelId",
                value: 5);

            migrationBuilder.AddForeignKey(
                name: "FK_FeaturedDeals_Hotels_HotelId",
                table: "FeaturedDeals",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddColumn<int>(
                name: "HotelId",
                table: "FeaturedDeals",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
