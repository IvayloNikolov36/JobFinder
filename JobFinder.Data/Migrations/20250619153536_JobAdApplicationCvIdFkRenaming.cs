using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFinder.Data.Migrations
{
    /// <inheritdoc />
    public partial class JobAdApplicationCvIdFkRenaming : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobAdsApplications_CurriculumVitaes_CurriculumVitaeId",
                table: "JobAdsApplications");

            migrationBuilder.RenameColumn(
                name: "CurriculumVitaeId",
                table: "JobAdsApplications",
                newName: "CvId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobAdsApplications_CurriculumVitaes_CvId",
                table: "JobAdsApplications",
                column: "CvId",
                principalTable: "CurriculumVitaes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobAdsApplications_CurriculumVitaes_CvId",
                table: "JobAdsApplications");

            migrationBuilder.RenameColumn(
                name: "CvId",
                table: "JobAdsApplications",
                newName: "CurriculumVitaeId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobAdsApplications_CurriculumVitaes_CurriculumVitaeId",
                table: "JobAdsApplications",
                column: "CurriculumVitaeId",
                principalTable: "CurriculumVitaes",
                principalColumn: "Id");
        }
    }
}
