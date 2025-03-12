using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace JobFinder.Data.Migrations
{
    /// <inheritdoc />
    public partial class JobSubscriptionNewColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Intership",
                table: "JobsSubscriptions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "JobEngagementId",
                table: "JobsSubscriptions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReccuringTypeId",
                table: "JobsSubscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SearchTerm",
                table: "JobsSubscriptions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "SpecifiedSalary",
                table: "JobsSubscriptions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ReccuringTypes",
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
                    table.PrimaryKey("PK_ReccuringTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ReccuringTypes",
                columns: new[] { "Id", "CreatedOn", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Daily" },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Weekly" },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Monthly" }
                });
            
            migrationBuilder.CreateIndex(
                name: "IX_JobsSubscriptions_JobEngagementId",
                table: "JobsSubscriptions",
                column: "JobEngagementId");

            migrationBuilder.CreateIndex(
                name: "IX_JobsSubscriptions_ReccuringTypeId",
                table: "JobsSubscriptions",
                column: "ReccuringTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobsSubscriptions_JobEngagements_JobEngagementId",
                table: "JobsSubscriptions",
                column: "JobEngagementId",
                principalTable: "JobEngagements",
                principalColumn: "Id");

            migrationBuilder.Sql("Update [JobsSubscriptions] SET [ReccuringTypeId] = 2");

            migrationBuilder.AddForeignKey(
                name: "FK_JobsSubscriptions_ReccuringTypes_ReccuringTypeId",
                table: "JobsSubscriptions",
                column: "ReccuringTypeId",
                principalTable: "ReccuringTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobsSubscriptions_JobEngagements_JobEngagementId",
                table: "JobsSubscriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_JobsSubscriptions_ReccuringTypes_ReccuringTypeId",
                table: "JobsSubscriptions");

            migrationBuilder.DropTable(
                name: "ReccuringTypes");

            migrationBuilder.DropIndex(
                name: "IX_JobsSubscriptions_JobEngagementId",
                table: "JobsSubscriptions");

            migrationBuilder.DropIndex(
                name: "IX_JobsSubscriptions_ReccuringTypeId",
                table: "JobsSubscriptions");

            migrationBuilder.DropColumn(
                name: "Intership",
                table: "JobsSubscriptions");

            migrationBuilder.DropColumn(
                name: "JobEngagementId",
                table: "JobsSubscriptions");

            migrationBuilder.DropColumn(
                name: "ReccuringTypeId",
                table: "JobsSubscriptions");

            migrationBuilder.DropColumn(
                name: "SearchTerm",
                table: "JobsSubscriptions");

            migrationBuilder.DropColumn(
                name: "SpecifiedSalary",
                table: "JobsSubscriptions");
        }
    }
}
