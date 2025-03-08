using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFinder.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeLocationInJobAdvertisementEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "JobAdvertisements");

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "JobAdvertisements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_JobAdvertisements_LocationId",
                table: "JobAdvertisements",
                column: "LocationId");

            migrationBuilder.Sql("UPDATE dbo.JobAdvertisements SET LocationId = 1");

            migrationBuilder.AddForeignKey(
                name: "FK_JobAdvertisements_Cities_LocationId",
                table: "JobAdvertisements",
                column: "LocationId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobAdvertisements_Cities_LocationId",
                table: "JobAdvertisements");

            migrationBuilder.DropIndex(
                name: "IX_JobAdvertisements_LocationId",
                table: "JobAdvertisements");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "JobAdvertisements");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "JobAdvertisements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
