using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cegeka.Auction.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ModifyBidClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Bids");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Bids",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Bids",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
