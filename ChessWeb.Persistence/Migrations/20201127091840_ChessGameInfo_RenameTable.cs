using Microsoft.EntityFrameworkCore.Migrations;

namespace ChessWeb.Persistence.Migrations
{
    public partial class ChessGameInfo_RenameTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChessGameInfo_Games_Id",
                table: "ChessGameInfo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChessGameInfo",
                table: "ChessGameInfo");

            migrationBuilder.RenameTable(
                name: "ChessGameInfo",
                newName: "ChessGameInfos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChessGameInfos",
                table: "ChessGameInfos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChessGameInfos_Games_Id",
                table: "ChessGameInfos",
                column: "Id",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChessGameInfos_Games_Id",
                table: "ChessGameInfos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChessGameInfos",
                table: "ChessGameInfos");

            migrationBuilder.RenameTable(
                name: "ChessGameInfos",
                newName: "ChessGameInfo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChessGameInfo",
                table: "ChessGameInfo",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChessGameInfo_Games_Id",
                table: "ChessGameInfo",
                column: "Id",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
