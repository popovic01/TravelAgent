using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAgent.Migrations
{
    /// <inheritdoc />
    public partial class AddedOfferRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Offers");

            migrationBuilder.AddColumn<int>(
                name: "OfferRequestId",
                table: "Locations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OfferRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaxPrice = table.Column<double>(type: "float", nullable: false),
                    DepartureLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TransportationTypeId = table.Column<int>(type: "int", nullable: false),
                    SpotNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OfferRequests_TransportationTypes_TransportationTypeId",
                        column: x => x.TransportationTypeId,
                        principalTable: "TransportationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Locations_OfferRequestId",
                table: "Locations",
                column: "OfferRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferRequests_TransportationTypeId",
                table: "OfferRequests",
                column: "TransportationTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_OfferRequests_OfferRequestId",
                table: "Locations",
                column: "OfferRequestId",
                principalTable: "OfferRequests",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_OfferRequests_OfferRequestId",
                table: "Locations");

            migrationBuilder.DropTable(
                name: "OfferRequests");

            migrationBuilder.DropIndex(
                name: "IX_Locations_OfferRequestId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "OfferRequestId",
                table: "Locations");

            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "Offers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
