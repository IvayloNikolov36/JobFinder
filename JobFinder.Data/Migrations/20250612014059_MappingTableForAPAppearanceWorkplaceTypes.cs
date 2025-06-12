using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFinder.Data.Migrations
{
    /// <inheritdoc />
    public partial class MappingTableForAPAppearanceWorkplaceTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnonymousProfileAppearancesWorkplaceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnonymousProfileAppearanceId = table.Column<int>(type: "int", nullable: false),
                    WorkplaceTypeId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnonymousProfileAppearancesWorkplaceTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnonymousProfileAppearancesWorkplaceTypes_AnonymousProfileAppearances_AnonymousProfileAppearanceId",
                        column: x => x.AnonymousProfileAppearanceId,
                        principalTable: "AnonymousProfileAppearances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnonymousProfileAppearancesWorkplaceTypes_WorkplaceTypes_WorkplaceTypeId",
                        column: x => x.WorkplaceTypeId,
                        principalTable: "WorkplaceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnonymousProfileAppearancesWorkplaceTypes_AnonymousProfileAppearanceId",
                table: "AnonymousProfileAppearancesWorkplaceTypes",
                column: "AnonymousProfileAppearanceId");

            migrationBuilder.CreateIndex(
                name: "IX_AnonymousProfileAppearancesWorkplaceTypes_WorkplaceTypeId",
                table: "AnonymousProfileAppearancesWorkplaceTypes",
                column: "WorkplaceTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnonymousProfileAppearancesWorkplaceTypes");
        }
    }
}
