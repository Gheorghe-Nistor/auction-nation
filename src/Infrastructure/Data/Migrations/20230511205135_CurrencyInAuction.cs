using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cegeka.Auction.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class CurrencyInAuction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "AuctionItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "AuctionItems");
        }
    }
}
