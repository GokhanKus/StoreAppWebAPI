using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class SecondCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "CreatedTime", "Price", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 2, 19, 15, 8, 12, 685, DateTimeKind.Local).AddTicks(8823), 60.5m, "Hacigoz ve Karivat" },
                    { 2, new DateTime(2024, 2, 19, 15, 8, 12, 685, DateTimeKind.Local).AddTicks(8830), 150m, "Tufek, Mikrop ve Celik" },
                    { 3, new DateTime(2024, 2, 19, 15, 8, 12, 685, DateTimeKind.Local).AddTicks(8832), 250m, "Devlet" },
                    { 4, new DateTime(2024, 2, 19, 15, 8, 12, 685, DateTimeKind.Local).AddTicks(8835), 45m, "Mesnevi" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
