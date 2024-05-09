using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerHackathon.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class change_db1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_University_UniversityId",
                table: "User");

            migrationBuilder.AlterColumn<int>(
                name: "UniversityId",
                table: "User",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_User_University_UniversityId",
                table: "User",
                column: "UniversityId",
                principalTable: "University",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_University_UniversityId",
                table: "User");

            migrationBuilder.AlterColumn<int>(
                name: "UniversityId",
                table: "User",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_User_University_UniversityId",
                table: "User",
                column: "UniversityId",
                principalTable: "University",
                principalColumn: "Id");
        }
    }
}
