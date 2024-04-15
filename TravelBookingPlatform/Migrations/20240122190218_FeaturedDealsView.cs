using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelBookingPlatform.Migrations
{
    /// <inheritdoc />
    public partial class FeaturedDealsView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            CREATE VIEW FeaturedDealView AS
            SELECT
                fd.Id AS FeaturedDealId,
                r.Id AS RoomId,
                r.Number AS RoomNumber,
                r.ThumbnailImageUrl AS RoomThumbnail,
                h.Name AS HotelName,
                h.Location AS HotelLocation,
                fd.OriginalPrice,
                fd.DiscountedPrice
            FROM FeaturedDeals fd
            JOIN Rooms r ON fd.RoomId = r.Id
            JOIN Hotels h ON r.HotelId = h.Id;
        ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW IF EXISTS FeaturedDealView;");
        }
    }
}
