using Microsoft.EntityFrameworkCore.Migrations;

namespace ChessWeb.Persistence.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColorType = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nickname = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChessGameInfo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_ChessGameInfo_Games_Id",
                        column: x => x.Id,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Moves",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameId = table.Column<long>(type: "bigint", nullable: true),
                    PlayerId = table.Column<long>(type: "bigint", nullable: true),
                    Fen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MoveNext = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Moves_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Moves_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sides",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameId = table.Column<long>(type: "bigint", nullable: true),
                    PlayerId = table.Column<long>(type: "bigint", nullable: true),
                    ColorId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sides", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sides_Colors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sides_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sides_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Colors",
                columns: new[] { "Id", "ColorType" },
                values: new object[,]
                {
                    { 1L, true },
                    { 2L, false }
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "Fen" },
                values: new object[] { 1L, "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1" });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "Email", "Nickname", "Password" },
                values: new object[,]
                {
                    { 1L, "noonimf@gmail.com", "Hymeck", "hymeckpass" },
                    { 2L, "mr.yatson@gmail.com", "Racoty", "racotypass" },
                    { 3L, "vadimyaren@yandex.by", "Yaren", "yarenpass" },
                    { 4L, "some_email@gmail.com", "Someone", "someonepass" }
                });

            migrationBuilder.InsertData(
                table: "ChessGameInfo",
                columns: new[] { "Id", "BlackKingSquare", "HasBlackKingMoved", "HasBlackKingsideRookMoved", "HasBlackQueensideRookMoved", "HasWhiteKingMoved", "HasWhiteKingsideRookMoved", "HasWhiteQueensideRookMoved", "WhiteKingSquare" },
                values: new object[] { 1L, "e8", false, false, false, false, false, false, "e1" });

            migrationBuilder.CreateIndex(
                name: "IX_Colors_ColorType",
                table: "Colors",
                column: "ColorType",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Moves_GameId",
                table: "Moves",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Moves_PlayerId",
                table: "Moves",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_Nickname",
                table: "Players",
                column: "Nickname",
                unique: true,
                filter: "[Nickname] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Sides_ColorId",
                table: "Sides",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Sides_GameId",
                table: "Sides",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Sides_PlayerId",
                table: "Sides",
                column: "PlayerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChessGameInfo");

            migrationBuilder.DropTable(
                name: "Moves");

            migrationBuilder.DropTable(
                name: "Sides");

            migrationBuilder.DropTable(
                name: "Colors");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
