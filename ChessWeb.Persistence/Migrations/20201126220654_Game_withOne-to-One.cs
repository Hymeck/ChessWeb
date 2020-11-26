using Microsoft.EntityFrameworkCore.Migrations;

namespace ChessWeb.Persistence.Migrations
{
    public partial class Game_withOnetoOne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "ChessGameInfo",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddForeignKey(
                name: "FK_ChessGameInfo_Games_Id",
                table: "ChessGameInfo",
                column: "Id",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChessGameInfo_Games_Id",
                table: "ChessGameInfo");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "ChessGameInfo",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1, 1");
        }
    }
}
