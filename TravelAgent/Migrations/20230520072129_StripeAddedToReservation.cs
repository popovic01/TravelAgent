using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAgent.Migrations
{
    /// <inheritdoc />
    public partial class StripeAddedToReservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaymentIntentId",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SessionId",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentIntentId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "Reservations");
        }
    }
}
