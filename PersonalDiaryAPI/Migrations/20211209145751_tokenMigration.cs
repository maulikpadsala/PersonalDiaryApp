using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonalDiaryAPI.Migrations
{
    public partial class tokenMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Error",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdentityUserId",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Error",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                table: "Users");
        }
    }
}
