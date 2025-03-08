using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFinder.Data.Migrations
{
    /// <inheritdoc />
    public partial class AlterColumJobAdSubscriptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "JobsSubscriptions");

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "JobsSubscriptions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobsSubscriptions_LocationId",
                table: "JobsSubscriptions",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobsSubscriptions_Cities_LocationId",
                table: "JobsSubscriptions",
                column: "LocationId",
                principalTable: "Cities",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobsSubscriptions_Cities_LocationId",
                table: "JobsSubscriptions");

            migrationBuilder.DropIndex(
                name: "IX_JobsSubscriptions_LocationId",
                table: "JobsSubscriptions");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "JobsSubscriptions");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "JobsSubscriptions",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
