using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAgent.Migrations
{
    /// <inheritdoc />
    public partial class OfferReqLocations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_OfferRequests_OfferRequestId",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Locations_OfferRequestId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "OfferRequestId",
                table: "Locations");

            migrationBuilder.CreateTable(
                name: "OfferRequestLocation",
                columns: table => new
                {
                    OfferReqLocationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    OfferRequestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferRequestLocation", x => x.OfferReqLocationId);
                    table.ForeignKey(
                        name: "FK_OfferRequestLocation_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfferRequestLocation_OfferRequests_OfferRequestId",
                        column: x => x.OfferRequestId,
                        principalTable: "OfferRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OfferRequestLocation_LocationId",
                table: "OfferRequestLocation",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferRequestLocation_OfferRequestId",
                table: "OfferRequestLocation",
                column: "OfferRequestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OfferRequestLocation");

            migrationBuilder.AddColumn<int>(
                name: "OfferRequestId",
                table: "Locations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Locations_OfferRequestId",
                table: "Locations",
                column: "OfferRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_OfferRequests_OfferRequestId",
                table: "Locations",
                column: "OfferRequestId",
                principalTable: "OfferRequests",
                principalColumn: "Id");
        }
    }
}
