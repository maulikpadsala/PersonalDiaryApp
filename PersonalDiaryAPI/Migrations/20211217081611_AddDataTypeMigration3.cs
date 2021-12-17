using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonalDiaryAPI.Migrations
{
    public partial class AddDataTypeMigration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Events_eventModelEventId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_eventModelEventId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "eventModelEventId",
                table: "Events");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "eventModelEventId",
                table: "Events",
                type: "INT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_eventModelEventId",
                table: "Events",
                column: "eventModelEventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Events_eventModelEventId",
                table: "Events",
                column: "eventModelEventId",
                principalTable: "Events",
                principalColumn: "EventId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
