using Microsoft.EntityFrameworkCore.Migrations;

namespace IssueTracker.Services.Issues.Infrastructure.Migrations
{
    public partial class update_participant_entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bugs_Participants_AssigneeId",
                table: "Bugs");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Participants_AssigneeId",
                table: "Tasks");

            migrationBuilder.AddForeignKey(
                name: "FK_Bugs_Participants_AssigneeId",
                table: "Bugs",
                column: "AssigneeId",
                principalTable: "Participants",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Participants_AssigneeId",
                table: "Tasks",
                column: "AssigneeId",
                principalTable: "Participants",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bugs_Participants_AssigneeId",
                table: "Bugs");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Participants_AssigneeId",
                table: "Tasks");

            migrationBuilder.AddForeignKey(
                name: "FK_Bugs_Participants_AssigneeId",
                table: "Bugs",
                column: "AssigneeId",
                principalTable: "Participants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Participants_AssigneeId",
                table: "Tasks",
                column: "AssigneeId",
                principalTable: "Participants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
