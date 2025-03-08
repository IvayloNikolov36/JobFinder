using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace JobFinder.Data.Migrations
{
    /// <inheritdoc />
    public partial class CititesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CreatedOn", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { 1, DateTime.UtcNow, null, "Sofia" },
                    { 2, DateTime.UtcNow, null, "Plovdiv" },
                    { 3, DateTime.UtcNow, null, "Varna" },
                    { 4, DateTime.UtcNow, null, "Burgas" },
                    { 5, DateTime.UtcNow, null, "Rouse" },
                    { 6, DateTime.UtcNow, null, "Stara Zagora" },
                    { 7, DateTime.UtcNow, null, "Pleven" },
                    { 8, DateTime.UtcNow, null, "Gabrovo" },
                    { 9, DateTime.UtcNow, null, "Veliko Tarnovo" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
