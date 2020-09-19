using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IssueTracker.Services.Issues.Infrastructure.Migrations
{
    public partial class add_StoryParticipants_entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stories_Participants_ParticipantId",
                table: "Stories");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Participants_ParticipantId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Participants_ReporterId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_ParticipantId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_ReporterId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Stories_ParticipantId",
                table: "Stories");

            migrationBuilder.DropColumn(
                name: "ParticipantId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "ParticipantId",
                table: "Stories");

            migrationBuilder.AlterColumn<string>(
                name: "ReporterId",
                table: "Tasks",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AssigneeId",
                table: "Tasks",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "StoryParticipants",
                columns: table => new
                {
                    ParticipantId = table.Column<string>(nullable: false),
                    StoryId = table.Column<string>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoryParticipants", x => new { x.StoryId, x.ParticipantId });
                    table.ForeignKey(
                        name: "FK_StoryParticipants_Participants_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "Participants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoryParticipants_Stories_StoryId",
                        column: x => x.StoryId,
                        principalTable: "Stories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_AssigneeId",
                table: "Tasks",
                column: "AssigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_StoryParticipants_ParticipantId",
                table: "StoryParticipants",
                column: "ParticipantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Participants_AssigneeId",
                table: "Tasks",
                column: "AssigneeId",
                principalTable: "Participants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Participants_AssigneeId",
                table: "Tasks");

            migrationBuilder.DropTable(
                name: "StoryParticipants");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_AssigneeId",
                table: "Tasks");

            migrationBuilder.AlterColumn<string>(
                name: "ReporterId",
                table: "Tasks",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AssigneeId",
                table: "Tasks",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParticipantId",
                table: "Tasks",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParticipantId",
                table: "Stories",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ParticipantId",
                table: "Tasks",
                column: "ParticipantId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ReporterId",
                table: "Tasks",
                column: "ReporterId",
                unique: true,
                filter: "[ReporterId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Stories_ParticipantId",
                table: "Stories",
                column: "ParticipantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stories_Participants_ParticipantId",
                table: "Stories",
                column: "ParticipantId",
                principalTable: "Participants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Participants_ParticipantId",
                table: "Tasks",
                column: "ParticipantId",
                principalTable: "Participants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Participants_ReporterId",
                table: "Tasks",
                column: "ReporterId",
                principalTable: "Participants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
