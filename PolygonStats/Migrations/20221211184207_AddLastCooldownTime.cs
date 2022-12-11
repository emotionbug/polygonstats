using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PolygonStats.Migrations
{
    /// <inheritdoc />
    public partial class AddLastCooldownTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastCooldown",
                table: "Account",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_SessionLogEntry_timestamp",
                table: "SessionLogEntry",
                column: "timestamp");

            migrationBuilder.CreateIndex(
                name: "IX_Session_EndTime",
                table: "Session",
                column: "EndTime");

            migrationBuilder.CreateIndex(
                name: "IX_Session_StartTime",
                table: "Session",
                column: "StartTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SessionLogEntry_timestamp",
                table: "SessionLogEntry");

            migrationBuilder.DropIndex(
                name: "IX_Session_EndTime",
                table: "Session");

            migrationBuilder.DropIndex(
                name: "IX_Session_StartTime",
                table: "Session");

            migrationBuilder.DropColumn(
                name: "LastCooldown",
                table: "Account");
        }
    }
}
