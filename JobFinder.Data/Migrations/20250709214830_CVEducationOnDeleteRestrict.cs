using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFinder.Data.Migrations
{
    /// <inheritdoc />
    public partial class CVEducationOnDeleteRestrict : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EducationInfos_CurriculumVitaes_CvId",
                table: "EducationInfos");

            migrationBuilder.AddForeignKey(
                name: "FK_EducationInfos_CurriculumVitaes_CvId",
                table: "EducationInfos",
                column: "CvId",
                principalTable: "CurriculumVitaes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EducationInfos_CurriculumVitaes_CvId",
                table: "EducationInfos");

            migrationBuilder.AddForeignKey(
                name: "FK_EducationInfos_CurriculumVitaes_CvId",
                table: "EducationInfos",
                column: "CvId",
                principalTable: "CurriculumVitaes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
