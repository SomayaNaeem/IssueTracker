using Microsoft.EntityFrameworkCore.Migrations;

namespace IssueTracker.Services.Issues.Infrastructure.Migrations
{
    public partial class update_bug_entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bugs_Participants_ParticipantId",
                table: "Bugs");

            migrationBuilder.DropForeignKey(
                name: "FK_Bugs_Participants_ReporterId",
                table: "Bugs");

            migrationBuilder.DropIndex(
                name: "IX_Bugs_ParticipantId",
                table: "Bugs");

            migrationBuilder.DropIndex(
                name: "IX_Bugs_ReporterId",
                table: "Bugs");

            migrationBuilder.DropColumn(
                name: "ParticipantId",
                table: "Bugs");

            migrationBuilder.AlterColumn<string>(
                name: "ReporterId",
                table: "Bugs",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AssigneeId",
                table: "Bugs",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bugs_AssigneeId",
                table: "Bugs",
                column: "AssigneeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bugs_Participants_AssigneeId",
                table: "Bugs",
                column: "AssigneeId",
                principalTable: "Participants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bugs_Participants_AssigneeId",
                table: "Bugs");

            migrationBuilder.DropIndex(
                name: "IX_Bugs_AssigneeId",
                table: "Bugs");

            migrationBuilder.AlterColumn<string>(
                name: "ReporterId",
                table: "Bugs",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AssigneeId",
                table: "Bugs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParticipantId",
                table: "Bugs",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bugs_ParticipantId",
                table: "Bugs",
                column: "ParticipantId");

            migrationBuilder.CreateIndex(
                name: "IX_Bugs_ReporterId",
                table: "Bugs",
                column: "ReporterId",
                unique: true,
                filter: "[ReporterId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Bugs_Participants_ParticipantId",
                table: "Bugs",
                column: "ParticipantId",
                principalTable: "Participants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bugs_Participants_ReporterId",
                table: "Bugs",
                column: "ReporterId",
                principalTable: "Participants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
