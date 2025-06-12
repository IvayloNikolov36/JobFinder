using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFinder.Data.Migrations
{
    /// <inheritdoc />
    public partial class AnonymousProfileAppearanceCities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnonymousProfileAppearancesCities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnonymousProfileAppearanceId = table.Column<int>(type: "int", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnonymousProfileAppearancesCities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnonymousProfileAppearancesCities_AnonymousProfileAppearances_AnonymousProfileAppearanceId",
                        column: x => x.AnonymousProfileAppearanceId,
                        principalTable: "AnonymousProfileAppearances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnonymousProfileAppearancesCities_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnonymousProfileAppearancesCities_AnonymousProfileAppearanceId",
                table: "AnonymousProfileAppearancesCities",
                column: "AnonymousProfileAppearanceId");

            migrationBuilder.CreateIndex(
                name: "IX_AnonymousProfileAppearancesCities_CityId",
                table: "AnonymousProfileAppearancesCities",
                column: "CityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnonymousProfileAppearancesCities");
        }
    }
}
