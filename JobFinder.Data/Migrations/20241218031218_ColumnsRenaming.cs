using Microsoft.EntityFrameworkCore.Migrations;

namespace JobFinder.Data.Migrations
{
    public partial class ColumnsRenaming : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AditionalDetails",
                table: "WorkExperienceInfos",
                newName: "AdditionalDetails");

            migrationBuilder.RenameColumn(
                name: "Desription",
                table: "JobAdvertisements",
                newName: "Description");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AdditionalDetails",
                table: "WorkExperienceInfos",
                newName: "AditionalDetails");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "JobAdvertisements",
                newName: "Desription");
        }
    }
}
