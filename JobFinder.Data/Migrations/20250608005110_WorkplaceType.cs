using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace JobFinder.Data.Migrations
{
    /// <inheritdoc />
    public partial class WorkplaceType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WorkplaceTypeId",
                table: "JobAdvertisements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "WorkplaceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkplaceTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "WorkplaceTypes",
                columns: new[] { "Id", "CreatedOn", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Office" },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Home" },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Hybrid" }
                });

            migrationBuilder.Sql("UPDATE dbo.[JobAdvertisements] SET WorkplaceTypeId = 1");

            migrationBuilder.CreateIndex(
                name: "IX_JobAdvertisements_WorkplaceTypeId",
                table: "JobAdvertisements",
                column: "WorkplaceTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobAdvertisements_WorkplaceTypes_WorkplaceTypeId",
                table: "JobAdvertisements",
                column: "WorkplaceTypeId",
                principalTable: "WorkplaceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobAdvertisements_WorkplaceTypes_WorkplaceTypeId",
                table: "JobAdvertisements");

            migrationBuilder.DropTable(
                name: "WorkplaceTypes");

            migrationBuilder.DropIndex(
                name: "IX_JobAdvertisements_WorkplaceTypeId",
                table: "JobAdvertisements");

            migrationBuilder.DropColumn(
                name: "WorkplaceTypeId",
                table: "JobAdvertisements");
        }
    }
}
