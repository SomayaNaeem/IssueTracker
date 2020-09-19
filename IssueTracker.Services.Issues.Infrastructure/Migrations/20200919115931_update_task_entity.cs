using Microsoft.EntityFrameworkCore.Migrations;

namespace IssueTracker.Services.Issues.Infrastructure.Migrations
{
    public partial class update_task_entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Stories_StoryId",
                table: "Tasks");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Stories_StoryId",
                table: "Tasks",
                column: "StoryId",
                principalTable: "Stories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Stories_StoryId",
                table: "Tasks");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Stories_StoryId",
                table: "Tasks",
                column: "StoryId",
                principalTable: "Stories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
