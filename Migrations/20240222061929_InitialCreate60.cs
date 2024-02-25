using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayosferIdentity.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate60 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectRequests_Users_RequesterId",
                table: "ProjectRequests");

            migrationBuilder.DropIndex(
                name: "IX_ProjectRequests_RequesterId",
                table: "ProjectRequests");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ProjectRequests");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ProjectRequests");

            migrationBuilder.DropColumn(
                name: "RequesterId",
                table: "ProjectRequests");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "ProjectRequests");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ProjectRequests",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ProjectRequests",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "RequesterId",
                table: "ProjectRequests",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "ProjectRequests",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectRequests_RequesterId",
                table: "ProjectRequests",
                column: "RequesterId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectRequests_Users_RequesterId",
                table: "ProjectRequests",
                column: "RequesterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
