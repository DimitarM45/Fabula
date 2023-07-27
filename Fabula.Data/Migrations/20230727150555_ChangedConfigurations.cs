using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fabula.Data.Migrations
{
    public partial class ChangedConfigurations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Compositions_CompositionId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Compositions_CompositionId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersFavoritePieces_AspNetUsers_UserId",
                table: "UsersFavoritePieces");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersFavoritePieces_Compositions_CompositionId",
                table: "UsersFavoritePieces");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersFavoritePieces",
                table: "UsersFavoritePieces");

            migrationBuilder.RenameTable(
                name: "UsersFavoritePieces",
                newName: "UsersFavoriteCompositions");

            migrationBuilder.RenameIndex(
                name: "IX_UsersFavoritePieces_CompositionId",
                table: "UsersFavoriteCompositions",
                newName: "IX_UsersFavoriteCompositions_CompositionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersFavoriteCompositions",
                table: "UsersFavoriteCompositions",
                columns: new[] { "UserId", "CompositionId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Compositions_CompositionId",
                table: "Comments",
                column: "CompositionId",
                principalTable: "Compositions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Compositions_CompositionId",
                table: "Ratings",
                column: "CompositionId",
                principalTable: "Compositions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersFavoriteCompositions_AspNetUsers_UserId",
                table: "UsersFavoriteCompositions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersFavoriteCompositions_Compositions_CompositionId",
                table: "UsersFavoriteCompositions",
                column: "CompositionId",
                principalTable: "Compositions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Compositions_CompositionId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Compositions_CompositionId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersFavoriteCompositions_AspNetUsers_UserId",
                table: "UsersFavoriteCompositions");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersFavoriteCompositions_Compositions_CompositionId",
                table: "UsersFavoriteCompositions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersFavoriteCompositions",
                table: "UsersFavoriteCompositions");

            migrationBuilder.RenameTable(
                name: "UsersFavoriteCompositions",
                newName: "UsersFavoritePieces");

            migrationBuilder.RenameIndex(
                name: "IX_UsersFavoriteCompositions_CompositionId",
                table: "UsersFavoritePieces",
                newName: "IX_UsersFavoritePieces_CompositionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersFavoritePieces",
                table: "UsersFavoritePieces",
                columns: new[] { "UserId", "CompositionId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Compositions_CompositionId",
                table: "Comments",
                column: "CompositionId",
                principalTable: "Compositions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Compositions_CompositionId",
                table: "Ratings",
                column: "CompositionId",
                principalTable: "Compositions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersFavoritePieces_AspNetUsers_UserId",
                table: "UsersFavoritePieces",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersFavoritePieces_Compositions_CompositionId",
                table: "UsersFavoritePieces",
                column: "CompositionId",
                principalTable: "Compositions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
