namespace Fabula.Data.Migrations;

using Microsoft.EntityFrameworkCore.Migrations;

using System;

#nullable disable

public partial class ChangedBirthdateToRequired : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<DateTime>(
            name: "BirthDate",
            table: "AspNetUsers",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
            comment: "User date of birth",
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldNullable: true,
            oldComment: "User date of birth");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<DateTime>(
            name: "BirthDate",
            table: "AspNetUsers",
            type: "datetime2",
            nullable: true,
            comment: "User date of birth",
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldComment: "User date of birth");
    }
}
