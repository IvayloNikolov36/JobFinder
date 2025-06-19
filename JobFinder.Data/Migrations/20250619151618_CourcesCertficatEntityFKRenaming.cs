using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFinder.Data.Migrations
{
    /// <inheritdoc />
    public partial class CourcesCertficatEntityFKRenaming : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseCertificatesInfos_CurriculumVitaes_CurriculumVitaeId",
                table: "CourseCertificatesInfos");

            migrationBuilder.RenameColumn(
                name: "CurriculumVitaeId",
                table: "CourseCertificatesInfos",
                newName: "CvId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseCertificatesInfos_CurriculumVitaeId",
                table: "CourseCertificatesInfos",
                newName: "IX_CourseCertificatesInfos_CvId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseCertificatesInfos_CurriculumVitaes_CvId",
                table: "CourseCertificatesInfos",
                column: "CvId",
                principalTable: "CurriculumVitaes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseCertificatesInfos_CurriculumVitaes_CvId",
                table: "CourseCertificatesInfos");

            migrationBuilder.RenameColumn(
                name: "CvId",
                table: "CourseCertificatesInfos",
                newName: "CurriculumVitaeId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseCertificatesInfos_CvId",
                table: "CourseCertificatesInfos",
                newName: "IX_CourseCertificatesInfos_CurriculumVitaeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseCertificatesInfos_CurriculumVitaes_CurriculumVitaeId",
                table: "CourseCertificatesInfos",
                column: "CurriculumVitaeId",
                principalTable: "CurriculumVitaes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
