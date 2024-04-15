using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TravelBookingPlatform.Migrations
{
    /// <inheritdoc />
    public partial class ImageTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntityId = table.Column<int>(type: "int", nullable: false),
                    ImageType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Hotels_EntityId",
                        column: x => x.EntityId,
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Images_Rooms_EntityId",
                        column: x => x.EntityId,
                        principalTable: "Rooms",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "EntityId", "ImageType", "ImageUrl" },
                values: new object[,]
                {
                    { 1, 1, 0, "/images/hotel1_image1.jpg" },
                    { 2, 1, 0, "/images/hotel1_image2.jpg" },
                    { 3, 2, 0, "/images/hotel1_image3.jpg" },
                    { 4, 2, 0, "/images/hotel1_image4.jpg" },
                    { 5, 3, 0, "/images/hotel1_image5.jpg" },
                    { 6, 3, 0, "/images/hotel1_image6.jpg" },
                    { 7, 4, 0, "/images/hotel1_image7.jpg" },
                    { 8, 4, 0, "/images/hotel1_image8.jpg" },
                    { 9, 5, 0, "/images/hotel1_image9.jpg" },
                    { 10, 5, 0, "/images/hotel1_image10.jpg" },
                    { 11, 1, 1, "/images/room1_image1.jpg" },
                    { 12, 1, 1, "/images/room1_image2.jpg" },
                    { 13, 2, 1, "/images/room1_image3.jpg" },
                    { 14, 2, 1, "/images/room1_image4.jpg" },
                    { 15, 3, 1, "/images/room1_image5.jpg" },
                    { 16, 3, 1, "/images/room1_image6.jpg" },
                    { 17, 4, 1, "/images/room1_image7.jpg" },
                    { 18, 5, 1, "/images/room1_image8.jpg" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_EntityId",
                table: "Images",
                column: "EntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");
        }
    }
}
