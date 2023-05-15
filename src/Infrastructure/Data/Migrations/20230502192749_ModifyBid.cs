using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cegeka.Auction.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ModifyBid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "Bids");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ItemId",
                table: "Bids",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "PublicId",
                table: "Bids",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
