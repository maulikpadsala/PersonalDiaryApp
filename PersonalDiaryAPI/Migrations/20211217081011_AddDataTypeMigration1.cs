using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonalDiaryAPI.Migrations
{
    public partial class AddDataTypeMigration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserModelUserId",
                table: "Events",
                type: "INT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "eventModelEventId",
                table: "Events",
                type: "INT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_eventModelEventId",
                table: "Events",
                column: "eventModelEventId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_UserModelUserId",
                table: "Events",
                column: "UserModelUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Events_eventModelEventId",
                table: "Events",
                column: "eventModelEventId",
                principalTable: "Events",
                principalColumn: "EventId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Users_UserModelUserId",
                table: "Events",
                column: "UserModelUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Events_eventModelEventId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Users_UserModelUserId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_eventModelEventId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_UserModelUserId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "UserModelUserId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "eventModelEventId",
                table: "Events");
        }
    }
}
