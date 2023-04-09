using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAgent.Migrations
{
    /// <inheritdoc />
    public partial class ClientIdInReqOffer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "OfferRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OfferRequests_ClientId",
                table: "OfferRequests",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_OfferRequests_Users_ClientId",
                table: "OfferRequests",
                column: "ClientId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OfferRequests_Users_ClientId",
                table: "OfferRequests");

            migrationBuilder.DropIndex(
                name: "IX_OfferRequests_ClientId",
                table: "OfferRequests");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "OfferRequests");
        }
    }
}
