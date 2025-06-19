using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFinder.Data.Migrations
{
    /// <inheritdoc />
    public partial class CvFkRenamings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnonymousProfiles_CurriculumVitaes_CurriculumVitaeId",
                table: "AnonymousProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_EducationInfos_CurriculumVitaes_CurriculumVitaeId",
                table: "EducationInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageInfos_CurriculumVitaes_CurriculumVitaeId",
                table: "LanguageInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalInfos_CurriculumVitaes_CurriculumVitaeId",
                table: "PersonalInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_SkillsInfos_CurriculumVitaes_CurriculumVitaeId",
                table: "SkillsInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkExperienceInfos_CurriculumVitaes_CurriculumVitaeId",
                table: "WorkExperienceInfos");

            migrationBuilder.DropIndex(
                name: "IX_AnonymousProfiles_CurriculumVitaeId",
                table: "AnonymousProfiles");

            migrationBuilder.RenameColumn(
                name: "CurriculumVitaeId",
                table: "WorkExperienceInfos",
                newName: "CvId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkExperienceInfos_CurriculumVitaeId",
                table: "WorkExperienceInfos",
                newName: "IX_WorkExperienceInfos_CvId");

            migrationBuilder.RenameColumn(
                name: "CurriculumVitaeId",
                table: "SkillsInfos",
                newName: "CvId");

            migrationBuilder.RenameIndex(
                name: "IX_SkillsInfos_CurriculumVitaeId",
                table: "SkillsInfos",
                newName: "IX_SkillsInfos_CvId");

            migrationBuilder.RenameColumn(
                name: "CurriculumVitaeId",
                table: "PersonalInfos",
                newName: "CvId");

            migrationBuilder.RenameIndex(
                name: "IX_PersonalInfos_CurriculumVitaeId",
                table: "PersonalInfos",
                newName: "IX_PersonalInfos_CvId");

            migrationBuilder.RenameColumn(
                name: "CurriculumVitaeId",
                table: "LanguageInfos",
                newName: "CvId");

            migrationBuilder.RenameIndex(
                name: "IX_LanguageInfos_CurriculumVitaeId",
                table: "LanguageInfos",
                newName: "IX_LanguageInfos_CvId");

            migrationBuilder.RenameColumn(
                name: "CurriculumVitaeId",
                table: "EducationInfos",
                newName: "CvId");

            migrationBuilder.RenameIndex(
                name: "IX_EducationInfos_CurriculumVitaeId",
                table: "EducationInfos",
                newName: "IX_EducationInfos_CvId");

            migrationBuilder.RenameColumn(
                name: "CurriculumVitaeId",
                table: "AnonymousProfiles",
                newName: "CvId");

            migrationBuilder.CreateIndex(
                name: "IX_AnonymousProfiles_CvId",
                table: "AnonymousProfiles",
                column: "CvId",
                unique: true,
                filter: "[CvId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AnonymousProfiles_CurriculumVitaes_CvId",
                table: "AnonymousProfiles",
                column: "CvId",
                principalTable: "CurriculumVitaes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EducationInfos_CurriculumVitaes_CvId",
                table: "EducationInfos",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnonymousProfiles_CurriculumVitaes_CvId",
                table: "AnonymousProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_EducationInfos_CurriculumVitaes_CvId",
                table: "EducationInfos");

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

            migrationBuilder.DropIndex(
                name: "IX_AnonymousProfiles_CvId",
                table: "AnonymousProfiles");

            migrationBuilder.RenameColumn(
                name: "CvId",
                table: "WorkExperienceInfos",
                newName: "CurriculumVitaeId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkExperienceInfos_CvId",
                table: "WorkExperienceInfos",
                newName: "IX_WorkExperienceInfos_CurriculumVitaeId");

            migrationBuilder.RenameColumn(
                name: "CvId",
                table: "SkillsInfos",
                newName: "CurriculumVitaeId");

            migrationBuilder.RenameIndex(
                name: "IX_SkillsInfos_CvId",
                table: "SkillsInfos",
                newName: "IX_SkillsInfos_CurriculumVitaeId");

            migrationBuilder.RenameColumn(
                name: "CvId",
                table: "PersonalInfos",
                newName: "CurriculumVitaeId");

            migrationBuilder.RenameIndex(
                name: "IX_PersonalInfos_CvId",
                table: "PersonalInfos",
                newName: "IX_PersonalInfos_CurriculumVitaeId");

            migrationBuilder.RenameColumn(
                name: "CvId",
                table: "LanguageInfos",
                newName: "CurriculumVitaeId");

            migrationBuilder.RenameIndex(
                name: "IX_LanguageInfos_CvId",
                table: "LanguageInfos",
                newName: "IX_LanguageInfos_CurriculumVitaeId");

            migrationBuilder.RenameColumn(
                name: "CvId",
                table: "EducationInfos",
                newName: "CurriculumVitaeId");

            migrationBuilder.RenameIndex(
                name: "IX_EducationInfos_CvId",
                table: "EducationInfos",
                newName: "IX_EducationInfos_CurriculumVitaeId");

            migrationBuilder.RenameColumn(
                name: "CvId",
                table: "AnonymousProfiles",
                newName: "CurriculumVitaeId");

            migrationBuilder.CreateIndex(
                name: "IX_AnonymousProfiles_CurriculumVitaeId",
                table: "AnonymousProfiles",
                column: "CurriculumVitaeId",
                unique: true,
                filter: "[CurriculumVitaeId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AnonymousProfiles_CurriculumVitaes_CurriculumVitaeId",
                table: "AnonymousProfiles",
                column: "CurriculumVitaeId",
                principalTable: "CurriculumVitaes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EducationInfos_CurriculumVitaes_CurriculumVitaeId",
                table: "EducationInfos",
                column: "CurriculumVitaeId",
                principalTable: "CurriculumVitaes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageInfos_CurriculumVitaes_CurriculumVitaeId",
                table: "LanguageInfos",
                column: "CurriculumVitaeId",
                principalTable: "CurriculumVitaes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalInfos_CurriculumVitaes_CurriculumVitaeId",
                table: "PersonalInfos",
                column: "CurriculumVitaeId",
                principalTable: "CurriculumVitaes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SkillsInfos_CurriculumVitaes_CurriculumVitaeId",
                table: "SkillsInfos",
                column: "CurriculumVitaeId",
                principalTable: "CurriculumVitaes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkExperienceInfos_CurriculumVitaes_CurriculumVitaeId",
                table: "WorkExperienceInfos",
                column: "CurriculumVitaeId",
                principalTable: "CurriculumVitaes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
