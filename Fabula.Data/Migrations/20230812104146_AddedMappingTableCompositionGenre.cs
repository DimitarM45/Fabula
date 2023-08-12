using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fabula.Data.Migrations
{
    public partial class AddedMappingTableCompositionGenre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompositionGenre");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("63ae6c73-83c9-4a7e-bc17-0f731374d040"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("77a5d16e-2090-4fab-8679-dee143402c4d"));

            migrationBuilder.CreateTable(
                name: "CompositionsGenres",
                columns: table => new
                {
                    CompositionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id of composition"),
                    GenreId = table.Column<int>(type: "int", nullable: false, comment: "Id of genre")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompositionsGenres", x => new { x.CompositionId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_CompositionsGenres_Compositions_CompositionId",
                        column: x => x.CompositionId,
                        principalTable: "Compositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompositionsGenres_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Mapping table for compositions and their genres");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("0e44b992-d52e-4a14-93f1-f3d0c79e1ffc"), "79e116e1-18ca-4553-aecc-aff86e880fb5", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("6922988f-b782-4299-b845-78c020efffca"), "4cdffa84-04f0-477f-91f8-cbba10a99a96", "User", "USER" });

            migrationBuilder.CreateIndex(
                name: "IX_CompositionsGenres_GenreId",
                table: "CompositionsGenres",
                column: "GenreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompositionsGenres");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0e44b992-d52e-4a14-93f1-f3d0c79e1ffc"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("6922988f-b782-4299-b845-78c020efffca"));

            migrationBuilder.CreateTable(
                name: "CompositionGenre",
                columns: table => new
                {
                    CompositionsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GenresId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompositionGenre", x => new { x.CompositionsId, x.GenresId });
                    table.ForeignKey(
                        name: "FK_CompositionGenre_Compositions_CompositionsId",
                        column: x => x.CompositionsId,
                        principalTable: "Compositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompositionGenre_Genres_GenresId",
                        column: x => x.GenresId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("63ae6c73-83c9-4a7e-bc17-0f731374d040"), "1ae5e579-c4d4-4025-b9f2-c03fdff8cad7", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("77a5d16e-2090-4fab-8679-dee143402c4d"), "db920785-869f-427b-9ffd-5268fd9455f2", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_CompositionGenre_GenresId",
                table: "CompositionGenre",
                column: "GenresId");
        }
    }
}
