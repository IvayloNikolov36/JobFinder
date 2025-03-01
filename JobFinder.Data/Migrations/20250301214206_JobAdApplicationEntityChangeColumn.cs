using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFinder.Data.Migrations
{
    /// <inheritdoc />
    public partial class JobAdApplicationEntityChangeColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPreviewed",
                table: "JobAdsApplications");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstPreviewDate",
                table: "JobAdsApplications");

            migrationBuilder.RenameColumn(
                name: "LatestPreviewDate",
                table: "JobAdsApplications",
                newName: "PreviewDate");

            migrationBuilder.AddColumn<bool>(
                name: "IsPreviewed",
                table: "JobAdsApplications",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
