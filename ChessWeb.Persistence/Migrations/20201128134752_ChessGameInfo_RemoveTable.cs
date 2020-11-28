using Microsoft.EntityFrameworkCore.Migrations;

namespace ChessWeb.Persistence.Migrations
{
    public partial class ChessGameInfo_RemoveTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChessGameInfos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChessGameInfos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    BlackKingSquare = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    HasBlackKingMoved = table.Column<bool>(type: "bit", nullable: false),
                    HasBlackKingsideRookMoved = table.Column<bool>(type: "bit", nullable: false),
                    HasBlackQueensideRookMoved = table.Column<bool>(type: "bit", nullable: false),
                    HasWhiteKingMoved = table.Column<bool>(type: "bit", nullable: false),
                    HasWhiteKingsideRookMoved = table.Column<bool>(type: "bit", nullable: false),
                    HasWhiteQueensideRookMoved = table.Column<bool>(type: "bit", nullable: false),
                    WhiteKingSquare = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChessGameInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChessGameInfos_Games_Id",
                        column: x => x.Id,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ChessGameInfos",
                columns: new[] { "Id", "BlackKingSquare", "HasBlackKingMoved", "HasBlackKingsideRookMoved", "HasBlackQueensideRookMoved", "HasWhiteKingMoved", "HasWhiteKingsideRookMoved", "HasWhiteQueensideRookMoved", "WhiteKingSquare" },
                values: new object[] { 1L, "e8", false, false, false, false, false, false, "e1" });
        }
    }
}
