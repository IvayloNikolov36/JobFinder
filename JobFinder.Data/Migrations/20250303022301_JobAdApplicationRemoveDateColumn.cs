using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFinder.Data.Migrations
{
    /// <inheritdoc />
    public partial class JobAdApplicationRemoveDateColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstPreviewDate",
                table: "JobAdsApplications");

            migrationBuilder.RenameColumn(
                name: "LatestPreviewDate",
                table: "JobAdsApplications",
                newName: "PreviewDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PreviewDate",
                table: "JobAdsApplications",
                newName: "LatestPreviewDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "FirstPreviewDate",
                table: "JobAdsApplications",
                type: "datetime2",
                nullable: true);
        }
    }
}
