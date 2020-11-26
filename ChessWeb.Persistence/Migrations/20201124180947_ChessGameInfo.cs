using Microsoft.EntityFrameworkCore.Migrations;

namespace ChessWeb.Persistence.Migrations
{
    public partial class ChessGameInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChessGameInfo_BlackKingSquare",
                table: "Game",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ChessGameInfo_HasBlackKingMoved",
                table: "Game",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ChessGameInfo_HasBlackKingsideRookMoved",
                table: "Game",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ChessGameInfo_HasBlackQueensideRookMoved",
                table: "Game",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ChessGameInfo_HasWhiteKingMoved",
                table: "Game",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ChessGameInfo_HasWhiteKingsideRookMoved",
                table: "Game",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ChessGameInfo_HasWhiteQueensideRookMoved",
                table: "Game",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChessGameInfo_WhiteKingSquare",
                table: "Game",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ChessGameInfo_BlackKingSquare", "ChessGameInfo_WhiteKingSquare" },
                values: new object[] { "e8", "e1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChessGameInfo_BlackKingSquare",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "ChessGameInfo_HasBlackKingMoved",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "ChessGameInfo_HasBlackKingsideRookMoved",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "ChessGameInfo_HasBlackQueensideRookMoved",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "ChessGameInfo_HasWhiteKingMoved",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "ChessGameInfo_HasWhiteKingsideRookMoved",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "ChessGameInfo_HasWhiteQueensideRookMoved",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "ChessGameInfo_WhiteKingSquare",
                table: "Game");
        }
    }
}
