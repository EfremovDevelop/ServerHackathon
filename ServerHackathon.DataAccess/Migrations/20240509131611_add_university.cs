using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ServerHackathon.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class add_university : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Points",
                table: "User",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "Card",
                table: "User",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UniversityId",
                table: "User",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "University",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Initials = table.Column<string>(type: "text", nullable: false),
                    Logo = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_University", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_UniversityId",
                table: "User",
                column: "UniversityId");

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

            migrationBuilder.DropTable(
                name: "University");

            migrationBuilder.DropIndex(
                name: "IX_User_UniversityId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Card",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UniversityId",
                table: "User");

            migrationBuilder.AlterColumn<int>(
                name: "Points",
                table: "User",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }
    }
}
