using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fabula.Data.Migrations
{
    public partial class ChangedSynopsysMaxLength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Synopsys",
                table: "Compositions",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                comment: "Synopsys of the composition",
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000,
                oldComment: "Synopsys of the composition");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Synopsys",
                table: "Compositions",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                comment: "Synopsys of the composition",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldComment: "Synopsys of the composition");
        }
    }
}
