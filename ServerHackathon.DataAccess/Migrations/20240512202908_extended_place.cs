using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerHackathon.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class extended_place : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "WorkFrom",
                table: "Place",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "WorkTo",
                table: "Place",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "minuteStep",
                table: "Place",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkFrom",
                table: "Place");

            migrationBuilder.DropColumn(
                name: "WorkTo",
                table: "Place");

            migrationBuilder.DropColumn(
                name: "minuteStep",
                table: "Place");
        }
    }
}
