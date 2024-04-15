using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelBookingPlatform.Migrations
{
    /// <inheritdoc />
    public partial class RecentlyVisitedView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            CREATE VIEW RecentlyVisitedHotels AS
            SELECT
                uv.Id AS UserVisitId,
                uv.UserId,
                uv.HotelId,
                uv.VisitDateTime,
                h.Name AS HotelName,
                h.ThumbnailImageUrl,
                h.CityId,
                c.Name AS City,
                h.StarRating
            FROM UserVisits uv
            JOIN Hotels h ON uv.HotelId = h.Id
            JOIN Cities c ON h.CityId = c.Id;
        ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW IF EXISTS RecentlyVisitedHotels;");
        }
    }
}
