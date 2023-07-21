using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fabula.Data.Migrations
{
    public partial class AddedWebsiteUrlAndFavGenres : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Synopsis",
                table: "Compositions",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                comment: "Synopsis of the composition",
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000,
                oldComment: "Synopsys of the composition");

            migrationBuilder.AddColumn<string>(
                name: "WebsiteURL",
                table: "AspNetUsers",
                type: "nvarchar(2084)",
                maxLength: 2084,
                nullable: true,
                comment: "User website url (social media/personal website)");

            migrationBuilder.CreateTable(
                name: "ApplicationUserGenre",
                columns: table => new
                {
                    FavoriteGenresId = table.Column<int>(type: "int", nullable: false),
                    FavoritesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserGenre", x => new { x.FavoriteGenresId, x.FavoritesId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserGenre_AspNetUsers_FavoritesId",
                        column: x => x.FavoritesId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserGenre_Genres_FavoriteGenresId",
                        column: x => x.FavoriteGenresId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserGenre_FavoritesId",
                table: "ApplicationUserGenre",
                column: "FavoritesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserGenre");

            migrationBuilder.DropColumn(
                name: "WebsiteURL",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Synopsis",
                table: "Compositions",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                comment: "Synopsys of the composition",
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000,
                oldComment: "Synopsis of the composition");
        }
    }
}
