using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayosferIdentity.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate66 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResetCode",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ResetCodeExpiration",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResetCode",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ResetCodeExpiration",
                table: "Users");
        }
    }
}
