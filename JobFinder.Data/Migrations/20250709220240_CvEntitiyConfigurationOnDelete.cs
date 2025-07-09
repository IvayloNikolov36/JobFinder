using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFinder.Data.Migrations
{
    /// <inheritdoc />
    public partial class CvEntitiyConfigurationOnDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnonymousProfiles_CurriculumVitaes_CvId",
                table: "AnonymousProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseCertificatesInfos_CurriculumVitaes_CvId",
                table: "CourseCertificatesInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_CvPreviewRequests_CurriculumVitaes_CvId",
                table: "CvPreviewRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageInfos_CurriculumVitaes_CvId",
                table: "LanguageInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalInfos_CurriculumVitaes_CvId",
                table: "PersonalInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_SkillsInfos_CurriculumVitaes_CvId",
                table: "SkillsInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkExperienceInfos_CurriculumVitaes_CvId",
                table: "WorkExperienceInfos");

            migrationBuilder.AddForeignKey(
                name: "FK_AnonymousProfiles_CurriculumVitaes_CvId",
                table: "AnonymousProfiles",
                column: "CvId",
                principalTable: "CurriculumVitaes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseCertificatesInfos_CurriculumVitaes_CvId",
                table: "CourseCertificatesInfos",
                column: "CvId",
                principalTable: "CurriculumVitaes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CvPreviewRequests_CurriculumVitaes_CvId",
                table: "CvPreviewRequests",
                column: "CvId",
                principalTable: "CurriculumVitaes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageInfos_CurriculumVitaes_CvId",
                table: "LanguageInfos",
                column: "CvId",
                principalTable: "CurriculumVitaes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalInfos_CurriculumVitaes_CvId",
                table: "PersonalInfos",
                column: "CvId",
                principalTable: "CurriculumVitaes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SkillsInfos_CurriculumVitaes_CvId",
                table: "SkillsInfos",
                column: "CvId",
                principalTable: "CurriculumVitaes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkExperienceInfos_CurriculumVitaes_CvId",
                table: "WorkExperienceInfos",
                column: "CvId",
                principalTable: "CurriculumVitaes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnonymousProfiles_CurriculumVitaes_CvId",
                table: "AnonymousProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseCertificatesInfos_CurriculumVitaes_CvId",
                table: "CourseCertificatesInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_CvPreviewRequests_CurriculumVitaes_CvId",
                table: "CvPreviewRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageInfos_CurriculumVitaes_CvId",
                table: "LanguageInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalInfos_CurriculumVitaes_CvId",
                table: "PersonalInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_SkillsInfos_CurriculumVitaes_CvId",
                table: "SkillsInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkExperienceInfos_CurriculumVitaes_CvId",
                table: "WorkExperienceInfos");

            migrationBuilder.AddForeignKey(
                name: "FK_AnonymousProfiles_CurriculumVitaes_CvId",
                table: "AnonymousProfiles",
                column: "CvId",
                principalTable: "CurriculumVitaes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseCertificatesInfos_CurriculumVitaes_CvId",
                table: "CourseCertificatesInfos",
                column: "CvId",
                principalTable: "CurriculumVitaes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CvPreviewRequests_CurriculumVitaes_CvId",
                table: "CvPreviewRequests",
                column: "CvId",
                principalTable: "CurriculumVitaes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageInfos_CurriculumVitaes_CvId",
                table: "LanguageInfos",
                column: "CvId",
                principalTable: "CurriculumVitaes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalInfos_CurriculumVitaes_CvId",
                table: "PersonalInfos",
                column: "CvId",
                principalTable: "CurriculumVitaes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SkillsInfos_CurriculumVitaes_CvId",
                table: "SkillsInfos",
                column: "CvId",
                principalTable: "CurriculumVitaes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkExperienceInfos_CurriculumVitaes_CvId",
                table: "WorkExperienceInfos",
                column: "CvId",
                principalTable: "CurriculumVitaes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
