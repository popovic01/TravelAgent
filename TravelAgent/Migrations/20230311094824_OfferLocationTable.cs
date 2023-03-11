using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAgent.Migrations
{
    /// <inheritdoc />
    public partial class OfferLocationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocationOffer");

            migrationBuilder.CreateTable(
                name: "OfferLocation",
                columns: table => new
                {
                    OfferLocationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    OfferId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferLocation", x => x.OfferLocationId);
                    table.ForeignKey(
                        name: "FK_OfferLocation_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfferLocation_Offers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OfferLocation_LocationId",
                table: "OfferLocation",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferLocation_OfferId",
                table: "OfferLocation",
                column: "OfferId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OfferLocation");

            migrationBuilder.CreateTable(
                name: "LocationOffer",
                columns: table => new
                {
                    LocationsId = table.Column<int>(type: "int", nullable: false),
                    OffersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationOffer", x => new { x.LocationsId, x.OffersId });
                    table.ForeignKey(
                        name: "FK_LocationOffer_Locations_LocationsId",
                        column: x => x.LocationsId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LocationOffer_Offers_OffersId",
                        column: x => x.OffersId,
                        principalTable: "Offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LocationOffer_OffersId",
                table: "LocationOffer",
                column: "OffersId");
        }
    }
}
