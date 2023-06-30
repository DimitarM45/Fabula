using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fabula.Data.Migrations
{
    public partial class ChangedPieceToComposition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Pieces_PieceId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Pieces_PieceId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersFavoritePieces_Pieces_PieceId",
                table: "UsersFavoritePieces");

            migrationBuilder.DropTable(
                name: "GenrePiece");

            migrationBuilder.DropTable(
                name: "PieceTag");

            migrationBuilder.DropTable(
                name: "Pieces");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersFavoritePieces",
                table: "UsersFavoritePieces");

            migrationBuilder.DropIndex(
                name: "IX_UsersFavoritePieces_PieceId",
                table: "UsersFavoritePieces");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_PieceId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Comments_PieceId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "PieceId",
                table: "UsersFavoritePieces");

            migrationBuilder.DropColumn(
                name: "PieceId",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "PieceId",
                table: "Comments");

            migrationBuilder.AlterTable(
                name: "UsersFavoritePieces",
                comment: "Mapping table for users and the compositions they've favorited",
                oldComment: "Mapping table for users and the pieces they've favorited");

            migrationBuilder.AlterTable(
                name: "Ratings",
                comment: "Rating of composition",
                oldComment: "Rating of piece");

            migrationBuilder.AlterTable(
                name: "Genres",
                comment: "Genre of a composition",
                oldComment: "Genre of a piece");

            migrationBuilder.AddColumn<Guid>(
                name: "CompositionId",
                table: "UsersFavoritePieces",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                comment: "Id of composition");

            migrationBuilder.AddColumn<Guid>(
                name: "CompositionId",
                table: "Ratings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                comment: "Id of composition");

            migrationBuilder.AddColumn<Guid>(
                name: "CompositionId",
                table: "Comments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                comment: "Id of composition");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersFavoritePieces",
                table: "UsersFavoritePieces",
                columns: new[] { "UserId", "CompositionId" });

            migrationBuilder.CreateTable(
                name: "Compositions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id of the composition"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Title of the composition"),
                    CoverUrl = table.Column<string>(type: "nvarchar(2084)", maxLength: 2084, nullable: false, comment: "A url which leads to the composition's cover art"),
                    Content = table.Column<string>(type: "nvarchar(max)", maxLength: 100000, nullable: false, comment: "The composition itself"),
                    Synopsys = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false, comment: "Synopsys of the composition"),
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id of composition author"),
                    PublishedOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Date of publishing"),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Date and time of deletion of the composition. Note: A nullable type is used for the purposes of documenting both whether a composition has been deleted and also when the operation took place."),
                    hasAdultContent = table.Column<bool>(type: "bit", nullable: false, comment: "Adult content flag of the composition"),
                    ListId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Compositions_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Compositions_Lists_ListId",
                        column: x => x.ListId,
                        principalTable: "Lists",
                        principalColumn: "Id");
                },
                comment: "User-written compositions");

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
                        name: "FK_CompositionTag_Tag_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersFavoritePieces_CompositionId",
                table: "UsersFavoritePieces",
                column: "CompositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_CompositionId",
                table: "Ratings",
                column: "CompositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CompositionId",
                table: "Comments",
                column: "CompositionId");

            migrationBuilder.CreateIndex(
                name: "IX_CompositionGenre_GenresId",
                table: "CompositionGenre",
                column: "GenresId");

            migrationBuilder.CreateIndex(
                name: "IX_Compositions_AuthorId",
                table: "Compositions",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Compositions_ListId",
                table: "Compositions",
                column: "ListId");

            migrationBuilder.CreateIndex(
                name: "IX_CompositionTag_TagsId",
                table: "CompositionTag",
                column: "TagsId");

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
                name: "FK_UsersFavoritePieces_Compositions_CompositionId",
                table: "UsersFavoritePieces",
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
                name: "FK_UsersFavoritePieces_Compositions_CompositionId",
                table: "UsersFavoritePieces");

            migrationBuilder.DropTable(
                name: "CompositionGenre");

            migrationBuilder.DropTable(
                name: "CompositionTag");

            migrationBuilder.DropTable(
                name: "Compositions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersFavoritePieces",
                table: "UsersFavoritePieces");

            migrationBuilder.DropIndex(
                name: "IX_UsersFavoritePieces_CompositionId",
                table: "UsersFavoritePieces");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_CompositionId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Comments_CompositionId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "CompositionId",
                table: "UsersFavoritePieces");

            migrationBuilder.DropColumn(
                name: "CompositionId",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "CompositionId",
                table: "Comments");

            migrationBuilder.AlterTable(
                name: "UsersFavoritePieces",
                comment: "Mapping table for users and the pieces they've favorited",
                oldComment: "Mapping table for users and the compositions they've favorited");

            migrationBuilder.AlterTable(
                name: "Ratings",
                comment: "Rating of piece",
                oldComment: "Rating of composition");

            migrationBuilder.AlterTable(
                name: "Genres",
                comment: "Genre of a piece",
                oldComment: "Genre of a composition");

            migrationBuilder.AddColumn<Guid>(
                name: "PieceId",
                table: "UsersFavoritePieces",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                comment: "Id of piece");

            migrationBuilder.AddColumn<Guid>(
                name: "PieceId",
                table: "Ratings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                comment: "Id of piece");

            migrationBuilder.AddColumn<Guid>(
                name: "PieceId",
                table: "Comments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                comment: "Id of piece");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersFavoritePieces",
                table: "UsersFavoritePieces",
                columns: new[] { "UserId", "PieceId" });

            migrationBuilder.CreateTable(
                name: "Pieces",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id of the piece"),
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id of piece author"),
                    Content = table.Column<string>(type: "nvarchar(max)", maxLength: 100000, nullable: false, comment: "The piece itself"),
                    CoverUrl = table.Column<string>(type: "nvarchar(2084)", maxLength: 2084, nullable: false, comment: "A url which leads to the piece's cover art"),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Date and time of deletion of the piece. Note: A nullable type is used for the purposes of documenting both whether a piece has been deleted and also when the operation took place."),
                    ListId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PublishedOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Date of publishing"),
                    Synopsys = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false, comment: "Synopsys of the piece"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Title of the piece"),
                    hasAdultContent = table.Column<bool>(type: "bit", nullable: false, comment: "Adult content flag of the piece")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pieces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pieces_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pieces_Lists_ListId",
                        column: x => x.ListId,
                        principalTable: "Lists",
                        principalColumn: "Id");
                },
                comment: "User-written pieces");

            migrationBuilder.CreateTable(
                name: "GenrePiece",
                columns: table => new
                {
                    GenresId = table.Column<int>(type: "int", nullable: false),
                    StoriesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenrePiece", x => new { x.GenresId, x.StoriesId });
                    table.ForeignKey(
                        name: "FK_GenrePiece_Genres_GenresId",
                        column: x => x.GenresId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenrePiece_Pieces_StoriesId",
                        column: x => x.StoriesId,
                        principalTable: "Pieces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PieceTag",
                columns: table => new
                {
                    PiecesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PieceTag", x => new { x.PiecesId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_PieceTag_Pieces_PiecesId",
                        column: x => x.PiecesId,
                        principalTable: "Pieces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PieceTag_Tag_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersFavoritePieces_PieceId",
                table: "UsersFavoritePieces",
                column: "PieceId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_PieceId",
                table: "Ratings",
                column: "PieceId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PieceId",
                table: "Comments",
                column: "PieceId");

            migrationBuilder.CreateIndex(
                name: "IX_GenrePiece_StoriesId",
                table: "GenrePiece",
                column: "StoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_Pieces_AuthorId",
                table: "Pieces",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Pieces_ListId",
                table: "Pieces",
                column: "ListId");

            migrationBuilder.CreateIndex(
                name: "IX_PieceTag_TagsId",
                table: "PieceTag",
                column: "TagsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Pieces_PieceId",
                table: "Comments",
                column: "PieceId",
                principalTable: "Pieces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Pieces_PieceId",
                table: "Ratings",
                column: "PieceId",
                principalTable: "Pieces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersFavoritePieces_Pieces_PieceId",
                table: "UsersFavoritePieces",
                column: "PieceId",
                principalTable: "Pieces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
