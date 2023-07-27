using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fabula.Data.Migrations
{
    public partial class SwitchedToCascadeOnRatings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Compositions_CompositionId",
                table: "Ratings");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Compositions_CompositionId",
                table: "Ratings",
                column: "CompositionId",
                principalTable: "Compositions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Compositions_CompositionId",
                table: "Ratings");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Compositions_CompositionId",
                table: "Ratings",
                column: "CompositionId",
                principalTable: "Compositions",
                principalColumn: "Id");
        }
    }
}
