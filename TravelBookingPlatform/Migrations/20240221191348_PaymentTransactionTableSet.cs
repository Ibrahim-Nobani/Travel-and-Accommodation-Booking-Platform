using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelBookingPlatform.Migrations
{
    /// <inheritdoc />
    public partial class PaymentTransactionTableSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentTransaction_Bookings_BookingId",
                table: "PaymentTransaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentTransaction",
                table: "PaymentTransaction");

            migrationBuilder.RenameTable(
                name: "PaymentTransaction",
                newName: "PaymentTransactions");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentTransaction_BookingId",
                table: "PaymentTransactions",
                newName: "IX_PaymentTransactions_BookingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentTransactions",
                table: "PaymentTransactions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentTransactions_Bookings_BookingId",
                table: "PaymentTransactions",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentTransactions_Bookings_BookingId",
                table: "PaymentTransactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentTransactions",
                table: "PaymentTransactions");

            migrationBuilder.RenameTable(
                name: "PaymentTransactions",
                newName: "PaymentTransaction");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentTransactions_BookingId",
                table: "PaymentTransaction",
                newName: "IX_PaymentTransaction_BookingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentTransaction",
                table: "PaymentTransaction",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentTransaction_Bookings_BookingId",
                table: "PaymentTransaction",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
