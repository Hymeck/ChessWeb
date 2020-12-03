using Microsoft.EntityFrameworkCore.Migrations;

namespace ChessWeb.Persistence.Migrations
{
    public partial class GameStatus_Add : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "GameStatusId",
                table: "Games",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GameStatuses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameStatuses", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "GameStatuses",
                columns: new[] { "Id", "Status" },
                values: new object[,]
                {
                    { 1L, (byte)0 },
                    { 2L, (byte)1 },
                    { 3L, (byte)2 },
                    { 4L, (byte)3 },
                    { 5L, (byte)4 },
                    { 6L, (byte)5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_GameStatusId",
                table: "Games",
                column: "GameStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_GameStatuses_Status",
                table: "GameStatuses",
                column: "Status",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_GameStatuses_GameStatusId",
                table: "Games",
                column: "GameStatusId",
                principalTable: "GameStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_GameStatuses_GameStatusId",
                table: "Games");

            migrationBuilder.DropTable(
                name: "GameStatuses");

            migrationBuilder.DropIndex(
                name: "IX_Games_GameStatusId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "GameStatusId",
                table: "Games");
        }
    }
}
