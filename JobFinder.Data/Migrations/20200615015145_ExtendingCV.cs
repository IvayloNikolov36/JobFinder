using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JobFinder.Data.Migrations
{
    public partial class ExtendingCV : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplications_AspNetUsers_CandidateId",
                table: "JobApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_JobApplications_CVs_CurriculumVitaeId",
                table: "JobApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_JobApplications_JobAds_JobAdId",
                table: "JobApplications");

            migrationBuilder.DropTable(
                name: "CVs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobApplications",
                table: "JobApplications");

            migrationBuilder.DropIndex(
                name: "IX_JobApplications_CurriculumVitaeId",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "PostedOn",
                table: "JobAds");

            migrationBuilder.RenameTable(
                name: "JobApplications",
                newName: "JobApplication");

            migrationBuilder.RenameIndex(
                name: "IX_JobApplications_JobAdId",
                table: "JobApplication",
                newName: "IX_JobApplication_JobAdId");

            migrationBuilder.RenameIndex(
                name: "IX_JobApplications_CandidateId",
                table: "JobApplication",
                newName: "IX_JobApplication_CandidateId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "JobAds",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "JobAds",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Companies",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Companies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurriculumVitaeId1",
                table: "JobApplication",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobApplication",
                table: "JobApplication",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CurriculumVitae",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    PictureUrl = table.Column<string>(nullable: false),
                    Data = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurriculumVitae", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CurriculumVitae_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CourseCertificate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CurriculumVitaeId = table.Column<int>(nullable: false),
                    CurriculumVitaeId1 = table.Column<string>(nullable: true),
                    CourseName = table.Column<string>(maxLength: 100, nullable: false),
                    CertificateUrl = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseCertificate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseCertificate_CurriculumVitae_CurriculumVitaeId1",
                        column: x => x.CurriculumVitaeId1,
                        principalTable: "CurriculumVitae",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Education",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CurriculumVitaeId = table.Column<string>(nullable: true),
                    FromDate = table.Column<DateTime>(nullable: false),
                    ToDate = table.Column<DateTime>(nullable: true),
                    Location = table.Column<string>(maxLength: 60, nullable: false),
                    EducationLevel = table.Column<int>(nullable: false),
                    Major = table.Column<string>(maxLength: 50, nullable: true),
                    MainSubjects = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Education", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Education_CurriculumVitae_CurriculumVitaeId",
                        column: x => x.CurriculumVitaeId,
                        principalTable: "CurriculumVitae",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LanguageInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CurriculumVitaeId = table.Column<string>(nullable: true),
                    LanguageType = table.Column<int>(nullable: false),
                    Comprehension = table.Column<int>(nullable: false),
                    Speaking = table.Column<int>(nullable: false),
                    Writing = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LanguageInfo_CurriculumVitae_CurriculumVitaeId",
                        column: x => x.CurriculumVitaeId,
                        principalTable: "CurriculumVitae",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkExperience",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CurriculumVitaeId = table.Column<string>(nullable: true),
                    FromDate = table.Column<DateTime>(nullable: false),
                    ToDate = table.Column<DateTime>(nullable: true),
                    JobTitle = table.Column<string>(maxLength: 50, nullable: false),
                    Organization = table.Column<string>(maxLength: 60, nullable: false),
                    BusinessSector = table.Column<int>(nullable: false),
                    Location = table.Column<string>(maxLength: 60, nullable: false),
                    AditionalDetails = table.Column<string>(maxLength: 3000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkExperience", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkExperience_CurriculumVitae_CurriculumVitaeId",
                        column: x => x.CurriculumVitaeId,
                        principalTable: "CurriculumVitae",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobApplication_CurriculumVitaeId1",
                table: "JobApplication",
                column: "CurriculumVitaeId1");

            migrationBuilder.CreateIndex(
                name: "IX_CourseCertificate_CurriculumVitaeId1",
                table: "CourseCertificate",
                column: "CurriculumVitaeId1");

            migrationBuilder.CreateIndex(
                name: "IX_CurriculumVitae_UserId",
                table: "CurriculumVitae",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Education_CurriculumVitaeId",
                table: "Education",
                column: "CurriculumVitaeId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageInfo_CurriculumVitaeId",
                table: "LanguageInfo",
                column: "CurriculumVitaeId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkExperience_CurriculumVitaeId",
                table: "WorkExperience",
                column: "CurriculumVitaeId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplication_AspNetUsers_CandidateId",
                table: "JobApplication",
                column: "CandidateId",
                principalTable: "AspNetUsers",
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
                name: "FK_JobApplication_JobAds_JobAdId",
                table: "JobApplication",
                column: "JobAdId",
                principalTable: "JobAds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplication_AspNetUsers_CandidateId",
                table: "JobApplication");

            migrationBuilder.DropForeignKey(
                name: "FK_JobApplication_CurriculumVitae_CurriculumVitaeId1",
                table: "JobApplication");

            migrationBuilder.DropForeignKey(
                name: "FK_JobApplication_JobAds_JobAdId",
                table: "JobApplication");

            migrationBuilder.DropTable(
                name: "CourseCertificate");

            migrationBuilder.DropTable(
                name: "Education");

            migrationBuilder.DropTable(
                name: "LanguageInfo");

            migrationBuilder.DropTable(
                name: "WorkExperience");

            migrationBuilder.DropTable(
                name: "CurriculumVitae");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobApplication",
                table: "JobApplication");

            migrationBuilder.DropIndex(
                name: "IX_JobApplication_CurriculumVitaeId1",
                table: "JobApplication");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "JobAds");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "JobAds");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "CurriculumVitaeId1",
                table: "JobApplication");

            migrationBuilder.RenameTable(
                name: "JobApplication",
                newName: "JobApplications");

            migrationBuilder.RenameIndex(
                name: "IX_JobApplication_JobAdId",
                table: "JobApplications",
                newName: "IX_JobApplications_JobAdId");

            migrationBuilder.RenameIndex(
                name: "IX_JobApplication_CandidateId",
                table: "JobApplications",
                newName: "IX_JobApplications_CandidateId");

            migrationBuilder.AddColumn<DateTime>(
                name: "PostedOn",
                table: "JobAds",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobApplications",
                table: "JobApplications",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CVs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UploaderId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CVs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CVs_AspNetUsers_UploaderId",
                        column: x => x.UploaderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_CurriculumVitaeId",
                table: "JobApplications",
                column: "CurriculumVitaeId");

            migrationBuilder.CreateIndex(
                name: "IX_CVs_UploaderId",
                table: "CVs",
                column: "UploaderId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplications_AspNetUsers_CandidateId",
                table: "JobApplications",
                column: "CandidateId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplications_CVs_CurriculumVitaeId",
                table: "JobApplications",
                column: "CurriculumVitaeId",
                principalTable: "CVs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplications_JobAds_JobAdId",
                table: "JobApplications",
                column: "JobAdId",
                principalTable: "JobAds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
