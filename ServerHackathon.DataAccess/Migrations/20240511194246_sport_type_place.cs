using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerHackathon.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class sport_type_place : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "EventStatus",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Sport" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EventStatus",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
