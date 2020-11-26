using Microsoft.EntityFrameworkCore.Migrations;

namespace ChessWeb.Persistence.Migrations
{
    public partial class AsTable_ChessGameInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<long>(
                name: "ChessGameInfoId",
                table: "Game",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ChessGameInfo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HasWhiteKingMoved = table.Column<bool>(type: "bit", nullable: false),
                    HasBlackKingMoved = table.Column<bool>(type: "bit", nullable: false),
                    HasWhiteQueensideRookMoved = table.Column<bool>(type: "bit", nullable: false),
                    HasWhiteKingsideRookMoved = table.Column<bool>(type: "bit", nullable: false),
                    HasBlackQueensideRookMoved = table.Column<bool>(type: "bit", nullable: false),
                    HasBlackKingsideRookMoved = table.Column<bool>(type: "bit", nullable: false),
                    WhiteKingSquare = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    BlackKingSquare = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChessGameInfo", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ChessGameInfo",
                columns: new[] { "Id", "BlackKingSquare", "HasBlackKingMoved", "HasBlackKingsideRookMoved", "HasBlackQueensideRookMoved", "HasWhiteKingMoved", "HasWhiteKingsideRookMoved", "HasWhiteQueensideRookMoved", "WhiteKingSquare" },
                values: new object[] { 1L, "e8", false, false, false, false, false, false, "e1" });

            migrationBuilder.CreateIndex(
                name: "IX_Game_ChessGameInfoId",
                table: "Game",
                column: "ChessGameInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Game_ChessGameInfo_ChessGameInfoId",
                table: "Game",
                column: "ChessGameInfoId",
                principalTable: "ChessGameInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Game_ChessGameInfo_ChessGameInfoId",
                table: "Game");

            migrationBuilder.DropTable(
                name: "ChessGameInfo");

            migrationBuilder.DropIndex(
                name: "IX_Game_ChessGameInfoId",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "ChessGameInfoId",
                table: "Game");

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
    }
}
