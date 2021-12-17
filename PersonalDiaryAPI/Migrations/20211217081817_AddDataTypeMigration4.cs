using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonalDiaryAPI.Migrations
{
    public partial class AddDataTypeMigration4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Users_UserModelUserId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_UserModelUserId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "UserModelUserId",
                table: "Events");

            migrationBuilder.CreateIndex(
                name: "IX_Events_UserId",
                table: "Events",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Users_UserId",
                table: "Events",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Users_UserId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_UserId",
                table: "Events");

            migrationBuilder.AddColumn<int>(
                name: "UserModelUserId",
                table: "Events",
                type: "INT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_UserModelUserId",
                table: "Events",
                column: "UserModelUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Users_UserModelUserId",
                table: "Events",
                column: "UserModelUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
