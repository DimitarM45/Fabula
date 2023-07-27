using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fabula.Data.Migrations
{
    public partial class SwitchedToCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Compositions_CompositionId",
                table: "Comments");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Compositions_CompositionId",
                table: "Comments",
                column: "CompositionId",
                principalTable: "Compositions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Compositions_CompositionId",
                table: "Comments");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Compositions_CompositionId",
                table: "Comments",
                column: "CompositionId",
                principalTable: "Compositions",
                principalColumn: "Id");
        }
    }
}
