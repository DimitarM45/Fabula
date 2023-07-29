using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fabula.Data.Migrations
{
    public partial class SeededRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("0046a86d-fa13-4d10-b91a-4c28494f6bac"), "806d7eba-122d-4714-a8cd-da0451bcef4b", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("d977df83-8795-46e9-afee-b8a055ccea74"), "686328ea-ecc4-42d6-bf7d-833612b96949", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0046a86d-fa13-4d10-b91a-4c28494f6bac"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d977df83-8795-46e9-afee-b8a055ccea74"));
        }
    }
}
