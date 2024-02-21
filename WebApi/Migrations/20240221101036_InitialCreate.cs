using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "CreatedTime", "Price", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 2, 21, 13, 10, 35, 993, DateTimeKind.Local).AddTicks(2637), 60.5m, "Hacigoz ve Karivat" },
                    { 2, new DateTime(2024, 2, 21, 13, 10, 35, 993, DateTimeKind.Local).AddTicks(2642), 150m, "Tufek, Mikrop ve Celik" },
                    { 3, new DateTime(2024, 2, 21, 13, 10, 35, 993, DateTimeKind.Local).AddTicks(2645), 250m, "Devlet" },
                    { 4, new DateTime(2024, 2, 21, 13, 10, 35, 993, DateTimeKind.Local).AddTicks(2647), 45m, "Mesnevi" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
