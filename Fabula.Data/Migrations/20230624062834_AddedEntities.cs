using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fabula.Data.Migrations
{
    public partial class AddedEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "AspNetUsers",
                comment: "Users");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "AspNetUsers",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "User bio description");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "",
                comment: "First name of user");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "",
                comment: "Last name of user");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePictureUrl",
                table: "AspNetUsers",
                type: "nvarchar(2084)",
                maxLength: 2084,
                nullable: false,
                defaultValue: "",
                comment: "A url which leads to the user's profile picture");

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Id of the genre")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Name of genre"),
                    PictureUrl = table.Column<string>(type: "nvarchar(2084)", maxLength: 2084, nullable: false, comment: "Picture for genre page")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                },
                comment: "Story genres");

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id of the post"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Title of the post"),
                    Content = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false, comment: "Content of the post"),
                    AuthorId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Id of post author"),
                    PublishedOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Date of publishing"),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Date and time of deletion of the post. Note: A nullable type is used for the purposes of documenting both whether a post has been deleted and also when the operation took place.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Posts made by users");

            migrationBuilder.CreateTable(
                name: "Stories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id of the story"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Title of the story"),
                    CoverUrl = table.Column<string>(type: "nvarchar(2084)", maxLength: 2084, nullable: false, comment: "A url which leads to the story's cover art"),
                    Content = table.Column<string>(type: "nvarchar(max)", maxLength: 100000, nullable: false, comment: "The story itself"),
                    Synopsys = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false, comment: "Synopsys of the story"),
                    AuthorId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Id of story author"),
                    PublishedOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Date of publishing"),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Date and time of deletion of the story. Note: A nullable type is used for the purposes of documenting both whether a story has been deleted and also when the operation took place.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stories_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                },
                comment: "User-written stories");

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id of the comment"),
                    Content = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "Content of the comment"),
                    AuthorId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Id of the comment author"),
                    PublishedOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Date of publishing"),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Date and time of deletion of the comment. Note: A nullable type is used for the purposes of documenting both whether a comment has been deleted and also when the operation took place."),
                    PostId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id");
                },
                comment: "Comments made by users");

            migrationBuilder.CreateTable(
                name: "UsersLikedPosts",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Id of user"),
                    PostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id of post")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersLikedPosts", x => new { x.UserId, x.PostId });
                    table.ForeignKey(
                        name: "FK_UsersLikedPosts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UsersLikedPosts_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Mapping table for users and the posts they've liked");

            migrationBuilder.CreateTable(
                name: "GenreStory",
                columns: table => new
                {
                    GenresId = table.Column<int>(type: "int", nullable: false),
                    StoriesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreStory", x => new { x.GenresId, x.StoriesId });
                    table.ForeignKey(
                        name: "FK_GenreStory_Genres_GenresId",
                        column: x => x.GenresId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenreStory_Stories_StoriesId",
                        column: x => x.StoriesId,
                        principalTable: "Stories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsersLikedStories",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Id of user"),
                    StoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id of story")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersLikedStories", x => new { x.UserId, x.StoryId });
                    table.ForeignKey(
                        name: "FK_UsersLikedStories_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UsersLikedStories_Stories_StoryId",
                        column: x => x.StoryId,
                        principalTable: "Stories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Mapping table for users and the stories they've liked");

            migrationBuilder.CreateTable(
                name: "UsersLikedComments",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Id of user"),
                    CommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id of comment")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersLikedComments", x => new { x.UserId, x.CommentId });
                    table.ForeignKey(
                        name: "FK_UsersLikedComments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UsersLikedComments_Comments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Mapping table for users and the comments they've liked");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ApplicationUserId",
                table: "AspNetUsers",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_AuthorId",
                table: "Comments",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostId",
                table: "Comments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_GenreStory_StoriesId",
                table: "GenreStory",
                column: "StoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_AuthorId",
                table: "Posts",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Stories_AuthorId",
                table: "Stories",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersLikedComments_CommentId",
                table: "UsersLikedComments",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersLikedPosts_PostId",
                table: "UsersLikedPosts",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersLikedStories_StoryId",
                table: "UsersLikedStories",
                column: "StoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_ApplicationUserId",
                table: "AspNetUsers",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_ApplicationUserId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "GenreStory");

            migrationBuilder.DropTable(
                name: "UsersLikedComments");

            migrationBuilder.DropTable(
                name: "UsersLikedPosts");

            migrationBuilder.DropTable(
                name: "UsersLikedStories");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Stories");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ApplicationUserId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Bio",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProfilePictureUrl",
                table: "AspNetUsers");

            migrationBuilder.AlterTable(
                name: "AspNetUsers",
                oldComment: "Users");
        }
    }
}
