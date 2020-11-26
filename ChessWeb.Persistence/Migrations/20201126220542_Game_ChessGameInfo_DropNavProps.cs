using Microsoft.EntityFrameworkCore.Migrations;

namespace ChessWeb.Persistence.Migrations
{
    public partial class Game_ChessGameInfo_DropNavProps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Game_ChessGameInfo_ChessGameInfoId",
                table: "Game");

            migrationBuilder.DropForeignKey(
                name: "FK_Move_Game_GameId",
                table: "Move");

            migrationBuilder.DropForeignKey(
                name: "FK_Move_Player_PlayerId",
                table: "Move");

            migrationBuilder.DropForeignKey(
                name: "FK_Side_Color_ColorId",
                table: "Side");

            migrationBuilder.DropForeignKey(
                name: "FK_Side_Game_GameId",
                table: "Side");

            migrationBuilder.DropForeignKey(
                name: "FK_Side_Player_PlayerId",
                table: "Side");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Side",
                table: "Side");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Player",
                table: "Player");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Move",
                table: "Move");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Game",
                table: "Game");

            migrationBuilder.DropIndex(
                name: "IX_Game_ChessGameInfoId",
                table: "Game");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Color",
                table: "Color");

            migrationBuilder.DropColumn(
                name: "ChessGameInfoId",
                table: "Game");

            migrationBuilder.RenameTable(
                name: "Side",
                newName: "Sides");

            migrationBuilder.RenameTable(
                name: "Player",
                newName: "Players");

            migrationBuilder.RenameTable(
                name: "Move",
                newName: "Moves");

            migrationBuilder.RenameTable(
                name: "Game",
                newName: "Games");

            migrationBuilder.RenameTable(
                name: "Color",
                newName: "Colors");

            migrationBuilder.RenameIndex(
                name: "IX_Side_PlayerId",
                table: "Sides",
                newName: "IX_Sides_PlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_Side_GameId",
                table: "Sides",
                newName: "IX_Sides_GameId");

            migrationBuilder.RenameIndex(
                name: "IX_Side_ColorId",
                table: "Sides",
                newName: "IX_Sides_ColorId");

            migrationBuilder.RenameIndex(
                name: "IX_Player_Nickname",
                table: "Players",
                newName: "IX_Players_Nickname");

            migrationBuilder.RenameIndex(
                name: "IX_Move_PlayerId",
                table: "Moves",
                newName: "IX_Moves_PlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_Move_GameId",
                table: "Moves",
                newName: "IX_Moves_GameId");

            migrationBuilder.RenameIndex(
                name: "IX_Color_ColorType",
                table: "Colors",
                newName: "IX_Colors_ColorType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sides",
                table: "Sides",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Players",
                table: "Players",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Moves",
                table: "Moves",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Games",
                table: "Games",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Colors",
                table: "Colors",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Moves_Games_GameId",
                table: "Moves",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Moves_Players_PlayerId",
                table: "Moves",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sides_Colors_ColorId",
                table: "Sides",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sides_Games_GameId",
                table: "Sides",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sides_Players_PlayerId",
                table: "Sides",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Moves_Games_GameId",
                table: "Moves");

            migrationBuilder.DropForeignKey(
                name: "FK_Moves_Players_PlayerId",
                table: "Moves");

            migrationBuilder.DropForeignKey(
                name: "FK_Sides_Colors_ColorId",
                table: "Sides");

            migrationBuilder.DropForeignKey(
                name: "FK_Sides_Games_GameId",
                table: "Sides");

            migrationBuilder.DropForeignKey(
                name: "FK_Sides_Players_PlayerId",
                table: "Sides");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sides",
                table: "Sides");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Players",
                table: "Players");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Moves",
                table: "Moves");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Games",
                table: "Games");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Colors",
                table: "Colors");

            migrationBuilder.RenameTable(
                name: "Sides",
                newName: "Side");

            migrationBuilder.RenameTable(
                name: "Players",
                newName: "Player");

            migrationBuilder.RenameTable(
                name: "Moves",
                newName: "Move");

            migrationBuilder.RenameTable(
                name: "Games",
                newName: "Game");

            migrationBuilder.RenameTable(
                name: "Colors",
                newName: "Color");

            migrationBuilder.RenameIndex(
                name: "IX_Sides_PlayerId",
                table: "Side",
                newName: "IX_Side_PlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_Sides_GameId",
                table: "Side",
                newName: "IX_Side_GameId");

            migrationBuilder.RenameIndex(
                name: "IX_Sides_ColorId",
                table: "Side",
                newName: "IX_Side_ColorId");

            migrationBuilder.RenameIndex(
                name: "IX_Players_Nickname",
                table: "Player",
                newName: "IX_Player_Nickname");

            migrationBuilder.RenameIndex(
                name: "IX_Moves_PlayerId",
                table: "Move",
                newName: "IX_Move_PlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_Moves_GameId",
                table: "Move",
                newName: "IX_Move_GameId");

            migrationBuilder.RenameIndex(
                name: "IX_Colors_ColorType",
                table: "Color",
                newName: "IX_Color_ColorType");

            migrationBuilder.AddColumn<long>(
                name: "ChessGameInfoId",
                table: "Game",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Side",
                table: "Side",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Player",
                table: "Player",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Move",
                table: "Move",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Game",
                table: "Game",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Color",
                table: "Color",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Move_Game_GameId",
                table: "Move",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Move_Player_PlayerId",
                table: "Move",
                column: "PlayerId",
                principalTable: "Player",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Side_Color_ColorId",
                table: "Side",
                column: "ColorId",
                principalTable: "Color",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Side_Game_GameId",
                table: "Side",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Side_Player_PlayerId",
                table: "Side",
                column: "PlayerId",
                principalTable: "Player",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
