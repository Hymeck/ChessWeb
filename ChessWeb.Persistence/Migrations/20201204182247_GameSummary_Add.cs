using Microsoft.EntityFrameworkCore.Migrations;

namespace ChessWeb.Persistence.Migrations
{
    public partial class GameSummary_Add : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_GameStatuses_GameStatusId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_GameStatusId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "GameStatusId",
                table: "Games");

            migrationBuilder.CreateTable(
                name: "GameSummaries",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameId = table.Column<long>(type: "bigint", nullable: false),
                    Fen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    StatusId = table.Column<long>(type: "bigint", nullable: true),
                    WhiteUserId = table.Column<long>(type: "bigint", nullable: true),
                    BlackUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastMove = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    ActiveColorId = table.Column<long>(type: "bigint", nullable: true),
                    WinnerId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameSummaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameSummaries_AspNetUsers_BlackUserId",
                        column: x => x.BlackUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GameSummaries_AspNetUsers_WhiteUserId",
                        column: x => x.WhiteUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GameSummaries_AspNetUsers_WinnerId",
                        column: x => x.WinnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GameSummaries_Colors_ActiveColorId",
                        column: x => x.ActiveColorId,
                        principalTable: "Colors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GameSummaries_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameSummaries_GameStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "GameStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameSummaries_ActiveColorId",
                table: "GameSummaries",
                column: "ActiveColorId");

            migrationBuilder.CreateIndex(
                name: "IX_GameSummaries_BlackUserId",
                table: "GameSummaries",
                column: "BlackUserId");

            migrationBuilder.CreateIndex(
                name: "IX_GameSummaries_GameId",
                table: "GameSummaries",
                column: "GameId",
                unique: true);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameSummaries");

            migrationBuilder.AddColumn<long>(
                name: "GameStatusId",
                table: "Games",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Games_GameStatusId",
                table: "Games",
                column: "GameStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_GameStatuses_GameStatusId",
                table: "Games",
                column: "GameStatusId",
                principalTable: "GameStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
