using Microsoft.EntityFrameworkCore.Migrations;

namespace ChessWeb.Persistence.Migrations
{
    public partial class Color_UniqueColorType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Color_ColorType",
                table: "Color",
                column: "ColorType",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Color_ColorType",
                table: "Color");
        }
    }
}
