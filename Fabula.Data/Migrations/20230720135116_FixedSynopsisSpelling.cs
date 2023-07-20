using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fabula.Data.Migrations
{
    public partial class FixedSynopsisSpelling : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompositionTag_Tag_TagsId",
                table: "CompositionTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tag",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "Synopsys",
                table: "Compositions");

            migrationBuilder.RenameTable(
                name: "Tag",
                newName: "Tags");

            migrationBuilder.AlterTable(
                name: "Tags",
                comment: "Tags for better categorization of literary works",
                oldComment: "Tags for better categorisation of literary works");

            migrationBuilder.AddColumn<string>(
                name: "Synopsis",
                table: "Compositions",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "",
                comment: "Synopsys of the composition");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tags",
                table: "Tags",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompositionTag_Tags_TagsId",
                table: "CompositionTag",
                column: "TagsId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompositionTag_Tags_TagsId",
                table: "CompositionTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "Synopsis",
                table: "Compositions");

            migrationBuilder.RenameTable(
                name: "Tags",
                newName: "Tag");

            migrationBuilder.AlterTable(
                name: "Tag",
                comment: "Tags for better categorisation of literary works",
                oldComment: "Tags for better categorization of literary works");

            migrationBuilder.AddColumn<string>(
                name: "Synopsys",
                table: "Compositions",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                comment: "Synopsys of the composition");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tag",
                table: "Tag",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompositionTag_Tag_TagsId",
                table: "CompositionTag",
                column: "TagsId",
                principalTable: "Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
