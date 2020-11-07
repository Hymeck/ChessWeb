using Microsoft.EntityFrameworkCore.Migrations;

namespace ChessWeb.Persistence.Migrations
{
    public partial class Game_Player_Side_Color : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Move",
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
                    table.PrimaryKey("PK_Move", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Move_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Move_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Side",
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
                    table.PrimaryKey("PK_Side", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Side_Color_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Color",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Side_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Side_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Color",
                columns: new[] { "Id", "ColorType" },
                values: new object[,]
                {
                    { 1L, true },
                    { 2L, false }
                });

            migrationBuilder.InsertData(
                table: "Game",
                columns: new[] { "Id", "Fen" },
                values: new object[] { 1L, "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1" });

            migrationBuilder.InsertData(
                table: "Player",
                columns: new[] { "Id", "Email", "Nickname", "Password" },
                values: new object[,]
                {
                    { 1L, "noonimf@gmail.com", "Hymeck", "hymeckpass" },
                    { 2L, "mr.yatson@gmail.com", "Racoty", "racotypass" },
                    { 3L, "vadimyaren@yandex.by", "Yaren", "yarenpass" },
                    { 4L, "some_email@gmail.com", "Someone", "someonepass" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Move_GameId",
                table: "Move",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Move_PlayerId",
                table: "Move",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Side_ColorId",
                table: "Side",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Side_GameId",
                table: "Side",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Side_PlayerId",
                table: "Side",
                column: "PlayerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Move");

            migrationBuilder.DropTable(
                name: "Side");

            migrationBuilder.DeleteData(
                table: "Color",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Color",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Game",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Player",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Player",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Player",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Player",
                keyColumn: "Id",
                keyValue: 4L);
        }
    }
}
