using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cegeka.Auction.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class SmallChangesToLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "lon",
                table: "AuctionItems",
                newName: "Lon");

            migrationBuilder.RenameColumn(
                name: "lat",
                table: "AuctionItems",
                newName: "Lat");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Lon",
                table: "AuctionItems",
                newName: "lon");

            migrationBuilder.RenameColumn(
                name: "Lat",
                table: "AuctionItems",
                newName: "lat");
        }
    }
}
