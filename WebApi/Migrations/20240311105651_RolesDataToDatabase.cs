using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class RolesDataToDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "14b5dde9-f5b9-4c9f-8776-dfbd6cb00756", null, "Editor", "EDITOR" },
                    { "34efaaab-d7b1-4662-8c31-f54e3508ce36", null, "Admin", "ADMIN" },
                    { "7e66a5c8-f339-4cc9-af86-74641029ef0f", null, "User", "USER" }
                });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedTime",
                value: new DateTime(2024, 3, 11, 13, 56, 50, 942, DateTimeKind.Local).AddTicks(701));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedTime",
                value: new DateTime(2024, 3, 11, 13, 56, 50, 942, DateTimeKind.Local).AddTicks(705));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedTime",
                value: new DateTime(2024, 3, 11, 13, 56, 50, 942, DateTimeKind.Local).AddTicks(708));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedTime",
                value: new DateTime(2024, 3, 11, 13, 56, 50, 942, DateTimeKind.Local).AddTicks(710));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "14b5dde9-f5b9-4c9f-8776-dfbd6cb00756");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "34efaaab-d7b1-4662-8c31-f54e3508ce36");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7e66a5c8-f339-4cc9-af86-74641029ef0f");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedTime",
                value: new DateTime(2024, 3, 11, 13, 36, 58, 300, DateTimeKind.Local).AddTicks(4559));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedTime",
                value: new DateTime(2024, 3, 11, 13, 36, 58, 300, DateTimeKind.Local).AddTicks(4563));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedTime",
                value: new DateTime(2024, 3, 11, 13, 36, 58, 300, DateTimeKind.Local).AddTicks(4565));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedTime",
                value: new DateTime(2024, 3, 11, 13, 36, 58, 300, DateTimeKind.Local).AddTicks(4567));
        }
    }
}
