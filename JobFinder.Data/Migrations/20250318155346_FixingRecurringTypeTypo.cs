using System;
using JobFinder.Data.MigrationHelpers;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace JobFinder.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixingRecurringTypeTypo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobsSubscriptions_ReccuringTypes_ReccuringTypeId",
                table: "JobsSubscriptions");

            migrationBuilder.DropTable(
                name: "ReccuringTypes");

            migrationBuilder.RenameColumn(
                name: "ReccuringTypeId",
                table: "JobsSubscriptions",
                newName: "RecurringTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_JobsSubscriptions_ReccuringTypeId",
                table: "JobsSubscriptions",
                newName: "IX_JobsSubscriptions_RecurringTypeId");

            migrationBuilder.CreateTable(
                name: "RecurringTypes",
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
                    table.PrimaryKey("PK_RecurringTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "RecurringTypes",
                columns: new[] { "Id", "CreatedOn", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Daily" },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Weekly" },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Monthly" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_JobsSubscriptions_RecurringTypes_RecurringTypeId",
                table: "JobsSubscriptions",
                column: "RecurringTypeId",
                principalTable: "RecurringTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            FunctionsHelper.Create_UDF_GetJobSubscriptions(migrationBuilder);
            FunctionsHelper.Create_UDF_GetLatesJobAdsForSubscribers_v2(migrationBuilder);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobsSubscriptions_RecurringTypes_RecurringTypeId",
                table: "JobsSubscriptions");

            migrationBuilder.DropTable(
                name: "RecurringTypes");

            migrationBuilder.RenameColumn(
                name: "RecurringTypeId",
                table: "JobsSubscriptions",
                newName: "ReccuringTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_JobsSubscriptions_RecurringTypeId",
                table: "JobsSubscriptions",
                newName: "IX_JobsSubscriptions_ReccuringTypeId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_JobsSubscriptions_ReccuringTypes_ReccuringTypeId",
                table: "JobsSubscriptions",
                column: "ReccuringTypeId",
                principalTable: "ReccuringTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
