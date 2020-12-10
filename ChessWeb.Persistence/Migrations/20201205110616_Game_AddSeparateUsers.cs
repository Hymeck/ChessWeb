using Microsoft.EntityFrameworkCore.Migrations;

namespace ChessWeb.Persistence.Migrations
{
    public partial class Game_AddSeparateUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BlackUserId",
                table: "Games",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WhiteUserId",
                table: "Games",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Games_BlackUserId",
                table: "Games",
                column: "BlackUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_WhiteUserId",
                table: "Games",
                column: "WhiteUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_AspNetUsers_BlackUserId",
                table: "Games",
                column: "BlackUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_AspNetUsers_WhiteUserId",
                table: "Games",
                column: "WhiteUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_AspNetUsers_BlackUserId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_AspNetUsers_WhiteUserId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_BlackUserId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_WhiteUserId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "BlackUserId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "WhiteUserId",
                table: "Games");
        }
    }
}
