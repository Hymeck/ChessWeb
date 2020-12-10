using Microsoft.EntityFrameworkCore.Migrations;

namespace ChessWeb.Persistence.Migrations
{
    public partial class GameSummary_Remove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameSummaries");

            migrationBuilder.AddColumn<bool>(
                name: "ActiveColor",
                table: "Games",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "BlackUsername",
                table: "Games",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastMove",
                table: "Games",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "Status",
                table: "Games",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<string>(
                name: "WhiteUsername",
                table: "Games",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Winner",
                table: "Games",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActiveColor",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "BlackUsername",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "LastMove",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "WhiteUsername",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Winner",
                table: "Games");

            migrationBuilder.CreateTable(
                name: "GameSummaries",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActiveColor = table.Column<bool>(type: "bit", nullable: false),
                    GameId = table.Column<long>(type: "bigint", nullable: false),
                    LastMove = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    Winner = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameSummaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameSummaries_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameSummaries_GameId",
                table: "GameSummaries",
                column: "GameId",
                unique: true);
        }
    }
}
