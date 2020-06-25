using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JobFinder.Data.Migrations
{
    public partial class ChangedColumnTypeOnCourseCertificate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseCertificate_CurriculumVitae_CurriculumVitaeId1",
                table: "CourseCertificate");

            migrationBuilder.DropForeignKey(
                name: "FK_Education_CurriculumVitae_CurriculumVitaeId",
                table: "Education");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageInfo_CurriculumVitae_CurriculumVitaeId",
                table: "LanguageInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkExperience_CurriculumVitae_CurriculumVitaeId",
                table: "WorkExperience");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkExperience",
                table: "WorkExperience");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LanguageInfo",
                table: "LanguageInfo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Education",
                table: "Education");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseCertificate",
                table: "CourseCertificate");

            migrationBuilder.DropIndex(
                name: "IX_CourseCertificate_CurriculumVitaeId1",
                table: "CourseCertificate");

            migrationBuilder.DropColumn(
                name: "CurriculumVitaeId1",
                table: "CourseCertificate");

            migrationBuilder.RenameTable(
                name: "WorkExperience",
                newName: "WorkExperiences");

            migrationBuilder.RenameTable(
                name: "LanguageInfo",
                newName: "LanguagesInfo");

            migrationBuilder.RenameTable(
                name: "Education",
                newName: "Educations");

            migrationBuilder.RenameTable(
                name: "CourseCertificate",
                newName: "CoursesCertificates");

            migrationBuilder.RenameIndex(
                name: "IX_WorkExperience_CurriculumVitaeId",
                table: "WorkExperiences",
                newName: "IX_WorkExperiences_CurriculumVitaeId");

            migrationBuilder.RenameIndex(
                name: "IX_LanguageInfo_CurriculumVitaeId",
                table: "LanguagesInfo",
                newName: "IX_LanguagesInfo_CurriculumVitaeId");

            migrationBuilder.RenameIndex(
                name: "IX_Education_CurriculumVitaeId",
                table: "Educations",
                newName: "IX_Educations_CurriculumVitaeId");

            migrationBuilder.AlterColumn<string>(
                name: "CurriculumVitaeId",
                table: "WorkExperiences",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CurriculumVitaeId",
                table: "LanguagesInfo",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CurriculumVitaeId",
                table: "Educations",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CurriculumVitaeId",
                table: "CoursesCertificates",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkExperiences",
                table: "WorkExperiences",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LanguagesInfo",
                table: "LanguagesInfo",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Educations",
                table: "Educations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoursesCertificates",
                table: "CoursesCertificates",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "PersonalDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CurriculumVitaeId = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    MiddleName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Phone = table.Column<string>(nullable: true),
                    Gender = table.Column<int>(nullable: false),
                    Birthdate = table.Column<DateTime>(nullable: false),
                    CitizenShip = table.Column<int>(nullable: false),
                    Country = table.Column<int>(nullable: false),
                    City = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonalDetails_CurriculumVitae_CurriculumVitaeId",
                        column: x => x.CurriculumVitaeId,
                        principalTable: "CurriculumVitae",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CurriculumVitaeId = table.Column<string>(nullable: false),
                    ComputerSkills = table.Column<string>(maxLength: 10000, nullable: true),
                    Skills = table.Column<string>(maxLength: 500, nullable: true),
                    HasManagedPeople = table.Column<bool>(nullable: false),
                    HasDrivingLicense = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skills_CurriculumVitae_CurriculumVitaeId",
                        column: x => x.CurriculumVitaeId,
                        principalTable: "CurriculumVitae",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DrivingCategory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DrivingCategoryTypeId = table.Column<int>(nullable: false),
                    SkillId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrivingCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DrivingCategory_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoursesCertificates_CurriculumVitaeId",
                table: "CoursesCertificates",
                column: "CurriculumVitaeId");

            migrationBuilder.CreateIndex(
                name: "IX_DrivingCategory_SkillId",
                table: "DrivingCategory",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalDetails_CurriculumVitaeId",
                table: "PersonalDetails",
                column: "CurriculumVitaeId");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_CurriculumVitaeId",
                table: "Skills",
                column: "CurriculumVitaeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CoursesCertificates_CurriculumVitae_CurriculumVitaeId",
                table: "CoursesCertificates",
                column: "CurriculumVitaeId",
                principalTable: "CurriculumVitae",
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
                name: "FK_LanguagesInfo_CurriculumVitae_CurriculumVitaeId",
                table: "LanguagesInfo",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoursesCertificates_CurriculumVitae_CurriculumVitaeId",
                table: "CoursesCertificates");

            migrationBuilder.DropForeignKey(
                name: "FK_Educations_CurriculumVitae_CurriculumVitaeId",
                table: "Educations");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguagesInfo_CurriculumVitae_CurriculumVitaeId",
                table: "LanguagesInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkExperiences_CurriculumVitae_CurriculumVitaeId",
                table: "WorkExperiences");

            migrationBuilder.DropTable(
                name: "DrivingCategory");

            migrationBuilder.DropTable(
                name: "PersonalDetails");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkExperiences",
                table: "WorkExperiences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LanguagesInfo",
                table: "LanguagesInfo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Educations",
                table: "Educations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CoursesCertificates",
                table: "CoursesCertificates");

            migrationBuilder.DropIndex(
                name: "IX_CoursesCertificates_CurriculumVitaeId",
                table: "CoursesCertificates");

            migrationBuilder.RenameTable(
                name: "WorkExperiences",
                newName: "WorkExperience");

            migrationBuilder.RenameTable(
                name: "LanguagesInfo",
                newName: "LanguageInfo");

            migrationBuilder.RenameTable(
                name: "Educations",
                newName: "Education");

            migrationBuilder.RenameTable(
                name: "CoursesCertificates",
                newName: "CourseCertificate");

            migrationBuilder.RenameIndex(
                name: "IX_WorkExperiences_CurriculumVitaeId",
                table: "WorkExperience",
                newName: "IX_WorkExperience_CurriculumVitaeId");

            migrationBuilder.RenameIndex(
                name: "IX_LanguagesInfo_CurriculumVitaeId",
                table: "LanguageInfo",
                newName: "IX_LanguageInfo_CurriculumVitaeId");

            migrationBuilder.RenameIndex(
                name: "IX_Educations_CurriculumVitaeId",
                table: "Education",
                newName: "IX_Education_CurriculumVitaeId");

            migrationBuilder.AlterColumn<string>(
                name: "CurriculumVitaeId",
                table: "WorkExperience",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "CurriculumVitaeId",
                table: "LanguageInfo",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "CurriculumVitaeId",
                table: "Education",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "CurriculumVitaeId",
                table: "CourseCertificate",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "CurriculumVitaeId1",
                table: "CourseCertificate",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkExperience",
                table: "WorkExperience",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LanguageInfo",
                table: "LanguageInfo",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Education",
                table: "Education",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseCertificate",
                table: "CourseCertificate",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CourseCertificate_CurriculumVitaeId1",
                table: "CourseCertificate",
                column: "CurriculumVitaeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseCertificate_CurriculumVitae_CurriculumVitaeId1",
                table: "CourseCertificate",
                column: "CurriculumVitaeId1",
                principalTable: "CurriculumVitae",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Education_CurriculumVitae_CurriculumVitaeId",
                table: "Education",
                column: "CurriculumVitaeId",
                principalTable: "CurriculumVitae",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageInfo_CurriculumVitae_CurriculumVitaeId",
                table: "LanguageInfo",
                column: "CurriculumVitaeId",
                principalTable: "CurriculumVitae",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkExperience_CurriculumVitae_CurriculumVitaeId",
                table: "WorkExperience",
                column: "CurriculumVitaeId",
                principalTable: "CurriculumVitae",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
