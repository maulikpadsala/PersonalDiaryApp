using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonalDiaryAPI.Migrations
{
    public partial class tokenMigration4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Error",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IsSuccess",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Error",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "IsSuccess",
                table: "Events");
        }
    }
}
