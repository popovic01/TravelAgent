using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAgent.Migrations
{
    /// <inheritdoc />
    public partial class WishlistConutInOffer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WishlistCount",
                table: "Offers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WishlistCount",
                table: "Offers");
        }
    }
}
