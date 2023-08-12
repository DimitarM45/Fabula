using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fabula.Data.Migrations
{
    public partial class AddedEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false, comment: "First name of user"),
                    LastName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false, comment: "Last name of user"),
                    ProfilePictureUrl = table.Column<string>(type: "nvarchar(2084)", maxLength: 2084, nullable: false, comment: "A url which leads to the user's profile picture"),
                    Bio = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "User bio description"),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "User date of birth"),
                    ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                },
                comment: "Users");

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Id of genre")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false, comment: "Name of genre")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                },
                comment: "Genre of a piece");

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Id of tag")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false, comment: "Name of tag")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                },
                comment: "Tags for better categorisation of literary works");

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id of list"),
                    Title = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false, comment: "Title of list"),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false, comment: "Description of list"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Date of creation"),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Date and time of deletion of the list. Note: A nullable type is used for the purposes of documenting both whether a list has been deleted and also when the operation took place."),
                    hasAdultContent = table.Column<bool>(type: "bit", nullable: false, comment: "Adult content flag of the list"),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id of creator")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lists_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Reading lists");

            migrationBuilder.CreateTable(
                name: "Pieces",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id of the piece"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Title of the piece"),
                    CoverUrl = table.Column<string>(type: "nvarchar(2084)", maxLength: 2084, nullable: false, comment: "A url which leads to the piece's cover art"),
                    Content = table.Column<string>(type: "nvarchar(max)", maxLength: 100000, nullable: false, comment: "The piece itself"),
                    Synopsys = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false, comment: "Synopsys of the piece"),
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id of piece author"),
                    PublishedOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Date of publishing"),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Date and time of deletion of the piece. Note: A nullable type is used for the purposes of documenting both whether a piece has been deleted and also when the operation took place."),
                    hasAdultContent = table.Column<bool>(type: "bit", nullable: false, comment: "Adult content flag of the piece"),
                    ListId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
                name: "UsersFollowedLists",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id of user"),
                    ListId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id of list")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersFollowedLists", x => new { x.UserId, x.ListId });
                    table.ForeignKey(
                        name: "FK_UsersFollowedLists_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsersFollowedLists_Lists_ListId",
                        column: x => x.ListId,
                        principalTable: "Lists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Mapping table for users and the lists they've followed");

            migrationBuilder.CreateTable(
                name: "UsersLikedLists",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id of user"),
                    ListId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id of list")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersLikedLists", x => new { x.UserId, x.ListId });
                    table.ForeignKey(
                        name: "FK_UsersLikedLists_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsersLikedLists_Lists_ListId",
                        column: x => x.ListId,
                        principalTable: "Lists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Mapping table for users and the lists they've liked");

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id of the comment"),
                    Content = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "Content of the comment"),
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id of the comment author"),
                    PieceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id of piece"),
                    PublishedOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Date of publishing"),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Date and time of deletion of the comment. Note: A nullable type is used for the purposes of documenting both whether a comment has been deleted and also when the operation took place.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_Pieces_PieceId",
                        column: x => x.PieceId,
                        principalTable: "Pieces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Comments made by users");

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

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id of rating"),
                    Value = table.Column<byte>(type: "tinyint", nullable: false, comment: "Value of rating"),
                    PublishedOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Date of publishing"),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Date and time of deletion of the rating. Note: A nullable type is used for the purposes of documenting both whether a rating has been deleted and also when the operation took place."),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id of user"),
                    PieceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id of piece")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ratings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ratings_Pieces_PieceId",
                        column: x => x.PieceId,
                        principalTable: "Pieces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Rating of piece");

            migrationBuilder.CreateTable(
                name: "UsersFavoritePieces",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id of user"),
                    PieceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id of piece")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersFavoritePieces", x => new { x.UserId, x.PieceId });
                    table.ForeignKey(
                        name: "FK_UsersFavoritePieces_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsersFavoritePieces_Pieces_PieceId",
                        column: x => x.PieceId,
                        principalTable: "Pieces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Mapping table for users and the pieces they've favorited");

            migrationBuilder.CreateTable(
                name: "UsersLikedComments",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id of user"),
                    CommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id of comment")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersLikedComments", x => new { x.UserId, x.CommentId });
                    table.ForeignKey(
                        name: "FK_UsersLikedComments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsersLikedComments_Comments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Mapping table for users and the comments they've liked");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ApplicationUserId",
                table: "AspNetUsers",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_AuthorId",
                table: "Comments",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PieceId",
                table: "Comments",
                column: "PieceId");

            migrationBuilder.CreateIndex(
                name: "IX_GenrePiece_StoriesId",
                table: "GenrePiece",
                column: "StoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_Lists_CreatorId",
                table: "Lists",
                column: "CreatorId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_PieceId",
                table: "Ratings",
                column: "PieceId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_UserId",
                table: "Ratings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersFavoritePieces_PieceId",
                table: "UsersFavoritePieces",
                column: "PieceId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersFollowedLists_ListId",
                table: "UsersFollowedLists",
                column: "ListId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersLikedComments_CommentId",
                table: "UsersLikedComments",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersLikedLists_ListId",
                table: "UsersLikedLists",
                column: "ListId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "GenrePiece");

            migrationBuilder.DropTable(
                name: "PieceTag");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "UsersFavoritePieces");

            migrationBuilder.DropTable(
                name: "UsersFollowedLists");

            migrationBuilder.DropTable(
                name: "UsersLikedComments");

            migrationBuilder.DropTable(
                name: "UsersLikedLists");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Pieces");

            migrationBuilder.DropTable(
                name: "Lists");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
