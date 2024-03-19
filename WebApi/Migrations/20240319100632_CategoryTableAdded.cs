using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class CategoryTableAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0dda63b8-783e-4bef-b83d-9d9c64480b05");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9daa292a-d4d0-4fa6-9ceb-28c99cacbcf5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e8cfc050-c519-4161-8e88-15caaaa8310e");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "332962f5-3c90-4e02-b272-5618a7d87649", null, "User", "USER" },
                    { "d055cc9c-4c6f-44f2-a011-706f6a834250", null, "Editor", "EDITOR" },
                    { "d50eaf2d-d3d6-4815-8baa-1d0aeb596906", null, "Admin", "ADMIN" }
                });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedTime",
                value: new DateTime(2024, 3, 19, 13, 6, 31, 806, DateTimeKind.Local).AddTicks(4902));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedTime",
                value: new DateTime(2024, 3, 19, 13, 6, 31, 806, DateTimeKind.Local).AddTicks(4906));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedTime",
                value: new DateTime(2024, 3, 19, 13, 6, 31, 806, DateTimeKind.Local).AddTicks(4908));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedTime",
                value: new DateTime(2024, 3, 19, 13, 6, 31, 806, DateTimeKind.Local).AddTicks(4909));

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName", "CreatedTime" },
                values: new object[,]
                {
                    { 1, "Psychology Thriller", new DateTime(2024, 3, 19, 13, 6, 31, 806, DateTimeKind.Local).AddTicks(6538) },
                    { 2, "Adventure", new DateTime(2024, 3, 19, 13, 6, 31, 806, DateTimeKind.Local).AddTicks(6540) },
                    { 3, "History", new DateTime(2024, 3, 19, 13, 6, 31, 806, DateTimeKind.Local).AddTicks(6542) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "332962f5-3c90-4e02-b272-5618a7d87649");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d055cc9c-4c6f-44f2-a011-706f6a834250");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d50eaf2d-d3d6-4815-8baa-1d0aeb596906");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0dda63b8-783e-4bef-b83d-9d9c64480b05", null, "User", "USER" },
                    { "9daa292a-d4d0-4fa6-9ceb-28c99cacbcf5", null, "Admin", "ADMIN" },
                    { "e8cfc050-c519-4161-8e88-15caaaa8310e", null, "Editor", "EDITOR" }
                });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedTime",
                value: new DateTime(2024, 3, 14, 14, 5, 15, 495, DateTimeKind.Local).AddTicks(3568));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedTime",
                value: new DateTime(2024, 3, 14, 14, 5, 15, 495, DateTimeKind.Local).AddTicks(3577));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedTime",
                value: new DateTime(2024, 3, 14, 14, 5, 15, 495, DateTimeKind.Local).AddTicks(3579));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedTime",
                value: new DateTime(2024, 3, 14, 14, 5, 15, 495, DateTimeKind.Local).AddTicks(3582));
        }
    }
}
