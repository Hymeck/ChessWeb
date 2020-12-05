using Microsoft.EntityFrameworkCore.Migrations;

namespace ChessWeb.Persistence.Migrations
{
    public partial class GameSummary_RemoveNavProps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameSummaries_AspNetUsers_BlackUserId",
                table: "GameSummaries");

            migrationBuilder.DropForeignKey(
                name: "FK_GameSummaries_AspNetUsers_WhiteUserId",
                table: "GameSummaries");

            migrationBuilder.DropForeignKey(
                name: "FK_GameSummaries_AspNetUsers_WinnerId",
                table: "GameSummaries");

            migrationBuilder.DropForeignKey(
                name: "FK_GameSummaries_Colors_ActiveColorId",
                table: "GameSummaries");

            migrationBuilder.DropForeignKey(
                name: "FK_GameSummaries_GameStatuses_StatusId",
                table: "GameSummaries");

            migrationBuilder.DropIndex(
                name: "IX_GameSummaries_ActiveColorId",
                table: "GameSummaries");

            migrationBuilder.DropIndex(
                name: "IX_GameSummaries_BlackUserId",
                table: "GameSummaries");

            migrationBuilder.DropIndex(
                name: "IX_GameSummaries_StatusId",
                table: "GameSummaries");

            migrationBuilder.DropIndex(
                name: "IX_GameSummaries_WhiteUserId",
                table: "GameSummaries");

            migrationBuilder.DropIndex(
                name: "IX_GameSummaries_WinnerId",
                table: "GameSummaries");

            migrationBuilder.DropColumn(
                name: "ActiveColorId",
                table: "GameSummaries");

            migrationBuilder.DropColumn(
                name: "BlackUserId",
                table: "GameSummaries");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "GameSummaries");

            migrationBuilder.DropColumn(
                name: "WhiteUserId",
                table: "GameSummaries");

            migrationBuilder.DropColumn(
                name: "WinnerId",
                table: "GameSummaries");

            migrationBuilder.AddColumn<string>(
                name: "ActiveColor",
                table: "GameSummaries",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BlackUser",
                table: "GameSummaries",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "GameSummaries",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WhiteUser",
                table: "GameSummaries",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Winner",
                table: "GameSummaries",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActiveColor",
                table: "GameSummaries");

            migrationBuilder.DropColumn(
                name: "BlackUser",
                table: "GameSummaries");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "GameSummaries");

            migrationBuilder.DropColumn(
                name: "WhiteUser",
                table: "GameSummaries");

            migrationBuilder.DropColumn(
                name: "Winner",
                table: "GameSummaries");

            migrationBuilder.AddColumn<long>(
                name: "ActiveColorId",
                table: "GameSummaries",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "BlackUserId",
                table: "GameSummaries",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "StatusId",
                table: "GameSummaries",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "WhiteUserId",
                table: "GameSummaries",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "WinnerId",
                table: "GameSummaries",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameSummaries_ActiveColorId",
                table: "GameSummaries",
                column: "ActiveColorId");

            migrationBuilder.CreateIndex(
                name: "IX_GameSummaries_BlackUserId",
                table: "GameSummaries",
                column: "BlackUserId");

            migrationBuilder.CreateIndex(
                name: "IX_GameSummaries_StatusId",
                table: "GameSummaries",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_GameSummaries_WhiteUserId",
                table: "GameSummaries",
                column: "WhiteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_GameSummaries_WinnerId",
                table: "GameSummaries",
                column: "WinnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameSummaries_AspNetUsers_BlackUserId",
                table: "GameSummaries",
                column: "BlackUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GameSummaries_AspNetUsers_WhiteUserId",
                table: "GameSummaries",
                column: "WhiteUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GameSummaries_AspNetUsers_WinnerId",
                table: "GameSummaries",
                column: "WinnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GameSummaries_Colors_ActiveColorId",
                table: "GameSummaries",
                column: "ActiveColorId",
                principalTable: "Colors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GameSummaries_GameStatuses_StatusId",
                table: "GameSummaries",
                column: "StatusId",
                principalTable: "GameStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
