using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fabula.Data.Migrations
{
    public partial class RemovedTagEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserGenre");

            migrationBuilder.DropTable(
                name: "CompositionTag");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0046a86d-fa13-4d10-b91a-4c28494f6bac"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d977df83-8795-46e9-afee-b8a055ccea74"));

            migrationBuilder.CreateTable(
                name: "UsersFavoriteGenres",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id of user"),
                    GenreId = table.Column<int>(type: "int", nullable: false, comment: "Id of genre")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersFavoriteGenres", x => new { x.UserId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_UsersFavoriteGenres_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsersFavoriteGenres_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Mapping table for users and the genres they've favorited");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("63ae6c73-83c9-4a7e-bc17-0f731374d040"), "1ae5e579-c4d4-4025-b9f2-c03fdff8cad7", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("77a5d16e-2090-4fab-8679-dee143402c4d"), "db920785-869f-427b-9ffd-5268fd9455f2", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_UsersFavoriteGenres_GenreId",
                table: "UsersFavoriteGenres",
                column: "GenreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersFavoriteGenres");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("63ae6c73-83c9-4a7e-bc17-0f731374d040"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("77a5d16e-2090-4fab-8679-dee143402c4d"));

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

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Id of tag")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false, comment: "Name of tag")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                },
                comment: "Tags for better categorization of literary works");

            migrationBuilder.CreateTable(
                name: "CompositionTag",
                columns: table => new
                {
                    CompositionsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompositionTag", x => new { x.CompositionsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_CompositionTag_Compositions_CompositionsId",
                        column: x => x.CompositionsId,
                        principalTable: "Compositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompositionTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("0046a86d-fa13-4d10-b91a-4c28494f6bac"), "806d7eba-122d-4714-a8cd-da0451bcef4b", "Admin", "ADMIN" },
                    { new Guid("d977df83-8795-46e9-afee-b8a055ccea74"), "686328ea-ecc4-42d6-bf7d-833612b96949", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Biography" },
                    { 2, "Memoir" },
                    { 3, "Non-fiction" },
                    { 4, "Self-Help" },
                    { 5, "Travel" },
                    { 6, "Science" },
                    { 7, "Fiction" },
                    { 8, "Satire" },
                    { 9, "LGBTQ+" },
                    { 10, "Graphic Novel" },
                    { 11, "Food" },
                    { 12, "Music" },
                    { 13, "Finance" },
                    { 14, "Technology" },
                    { 15, "Educational" },
                    { 16, "Nature" },
                    { 17, "Religion" },
                    { 18, "Psychology" },
                    { 19, "Anthology" },
                    { 20, "War" },
                    { 21, "Supernatural" },
                    { 22, "Coming of Age" },
                    { 23, "Family" },
                    { 24, "Magic" },
                    { 25, "Inspirational" },
                    { 26, "Suspense" },
                    { 27, "Thrilling" },
                    { 28, "Intrigue" },
                    { 29, "Spiritual" },
                    { 30, "Artistic" },
                    { 31, "Meditative" },
                    { 32, "Environmental" },
                    { 33, "Journal" },
                    { 34, "Guide" },
                    { 35, "Reference" },
                    { 36, "Health" },
                    { 37, "Fitness" },
                    { 38, "Fashion" },
                    { 39, "Cookbook" },
                    { 40, "Crafts" }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 41, "Gardening" },
                    { 42, "Architecture" },
                    { 43, "Design" },
                    { 44, "Parenting" },
                    { 45, "Relationships" },
                    { 46, "Education" },
                    { 47, "Philosophy" },
                    { 48, "Science Fiction" },
                    { 49, "Business" },
                    { 50, "Crime" },
                    { 51, "Historical" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserGenre_FavoritesId",
                table: "ApplicationUserGenre",
                column: "FavoritesId");

            migrationBuilder.CreateIndex(
                name: "IX_CompositionTag_TagsId",
                table: "CompositionTag",
                column: "TagsId");
        }
    }
}
