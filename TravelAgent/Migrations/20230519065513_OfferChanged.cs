using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAgent.Migrations
{
    /// <inheritdoc />
    public partial class OfferChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Offers");

            migrationBuilder.AddColumn<int>(
                name: "OfferRequestId",
                table: "Offers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Offers_OfferRequestId",
                table: "Offers",
                column: "OfferRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_OfferRequests_OfferRequestId",
                table: "Offers",
                column: "OfferRequestId",
                principalTable: "OfferRequests",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_OfferRequests_OfferRequestId",
                table: "Offers");

            migrationBuilder.DropIndex(
                name: "IX_Offers_OfferRequestId",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "OfferRequestId",
                table: "Offers");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Offers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
