using Microsoft.EntityFrameworkCore.Migrations;

namespace JobFinder.Data.Migrations
{
    public partial class AddedTwoDbSets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoursesCertificates_CurriculumVitae_CurriculumVitaeId",
                table: "CoursesCertificates");

            migrationBuilder.DropForeignKey(
                name: "FK_CurriculumVitae_AspNetUsers_UserId",
                table: "CurriculumVitae");

            migrationBuilder.DropForeignKey(
                name: "FK_DrivingCategory_Skills_SkillId",
                table: "DrivingCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_Educations_CurriculumVitae_CurriculumVitaeId",
                table: "Educations");

            migrationBuilder.DropForeignKey(
                name: "FK_JobApplication_CurriculumVitae_CurriculumVitaeId1",
                table: "JobApplication");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguagesInfo_CurriculumVitae_CurriculumVitaeId",
                table: "LanguagesInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalDetails_CurriculumVitae_CurriculumVitaeId",
                table: "PersonalDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Skills_CurriculumVitae_CurriculumVitaeId",
                table: "Skills");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkExperiences_CurriculumVitae_CurriculumVitaeId",
                table: "WorkExperiences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DrivingCategory",
                table: "DrivingCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CurriculumVitae",
                table: "CurriculumVitae");

            migrationBuilder.RenameTable(
                name: "DrivingCategory",
                newName: "DrivingCategories");

            migrationBuilder.RenameTable(
                name: "CurriculumVitae",
                newName: "CVs");

            migrationBuilder.RenameIndex(
                name: "IX_DrivingCategory_SkillId",
                table: "DrivingCategories",
                newName: "IX_DrivingCategories_SkillId");

            migrationBuilder.RenameIndex(
                name: "IX_CurriculumVitae_UserId",
                table: "CVs",
                newName: "IX_CVs_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DrivingCategories",
                table: "DrivingCategories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CVs",
                table: "CVs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CoursesCertificates_CVs_CurriculumVitaeId",
                table: "CoursesCertificates",
                column: "CurriculumVitaeId",
                principalTable: "CVs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CVs_AspNetUsers_UserId",
                table: "CVs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DrivingCategories_Skills_SkillId",
                table: "DrivingCategories",
                column: "SkillId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Educations_CVs_CurriculumVitaeId",
                table: "Educations",
                column: "CurriculumVitaeId",
                principalTable: "CVs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplication_CVs_CurriculumVitaeId1",
                table: "JobApplication",
                column: "CurriculumVitaeId1",
                principalTable: "CVs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LanguagesInfo_CVs_CurriculumVitaeId",
                table: "LanguagesInfo",
                column: "CurriculumVitaeId",
                principalTable: "CVs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalDetails_CVs_CurriculumVitaeId",
                table: "PersonalDetails",
                column: "CurriculumVitaeId",
                principalTable: "CVs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_CVs_CurriculumVitaeId",
                table: "Skills",
                column: "CurriculumVitaeId",
                principalTable: "CVs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkExperiences_CVs_CurriculumVitaeId",
                table: "WorkExperiences",
                column: "CurriculumVitaeId",
                principalTable: "CVs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoursesCertificates_CVs_CurriculumVitaeId",
                table: "CoursesCertificates");

            migrationBuilder.DropForeignKey(
                name: "FK_CVs_AspNetUsers_UserId",
                table: "CVs");

            migrationBuilder.DropForeignKey(
                name: "FK_DrivingCategories_Skills_SkillId",
                table: "DrivingCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_Educations_CVs_CurriculumVitaeId",
                table: "Educations");

            migrationBuilder.DropForeignKey(
                name: "FK_JobApplication_CVs_CurriculumVitaeId1",
                table: "JobApplication");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguagesInfo_CVs_CurriculumVitaeId",
                table: "LanguagesInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalDetails_CVs_CurriculumVitaeId",
                table: "PersonalDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Skills_CVs_CurriculumVitaeId",
                table: "Skills");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkExperiences_CVs_CurriculumVitaeId",
                table: "WorkExperiences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DrivingCategories",
                table: "DrivingCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CVs",
                table: "CVs");

            migrationBuilder.RenameTable(
                name: "DrivingCategories",
                newName: "DrivingCategory");

            migrationBuilder.RenameTable(
                name: "CVs",
                newName: "CurriculumVitae");

            migrationBuilder.RenameIndex(
                name: "IX_DrivingCategories_SkillId",
                table: "DrivingCategory",
                newName: "IX_DrivingCategory_SkillId");

            migrationBuilder.RenameIndex(
                name: "IX_CVs_UserId",
                table: "CurriculumVitae",
                newName: "IX_CurriculumVitae_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DrivingCategory",
                table: "DrivingCategory",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CurriculumVitae",
                table: "CurriculumVitae",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CoursesCertificates_CurriculumVitae_CurriculumVitaeId",
                table: "CoursesCertificates",
                column: "CurriculumVitaeId",
                principalTable: "CurriculumVitae",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CurriculumVitae_AspNetUsers_UserId",
                table: "CurriculumVitae",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DrivingCategory_Skills_SkillId",
                table: "DrivingCategory",
                column: "SkillId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Educations_CurriculumVitae_CurriculumVitaeId",
                table: "Educations",
                column: "CurriculumVitaeId",
                principalTable: "CurriculumVitae",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplication_CurriculumVitae_CurriculumVitaeId1",
                table: "JobApplication",
                column: "CurriculumVitaeId1",
                principalTable: "CurriculumVitae",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LanguagesInfo_CurriculumVitae_CurriculumVitaeId",
                table: "LanguagesInfo",
                column: "CurriculumVitaeId",
                principalTable: "CurriculumVitae",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalDetails_CurriculumVitae_CurriculumVitaeId",
                table: "PersonalDetails",
                column: "CurriculumVitaeId",
                principalTable: "CurriculumVitae",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_CurriculumVitae_CurriculumVitaeId",
                table: "Skills",
                column: "CurriculumVitaeId",
                principalTable: "CurriculumVitae",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkExperiences_CurriculumVitae_CurriculumVitaeId",
                table: "WorkExperiences",
                column: "CurriculumVitaeId",
                principalTable: "CurriculumVitae",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
