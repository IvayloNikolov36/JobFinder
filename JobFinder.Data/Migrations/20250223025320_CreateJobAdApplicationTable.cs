using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFinder.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateJobAdApplicationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobAdsApplications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobAdId = table.Column<int>(type: "int", nullable: false),
                    ApplicantId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CurriculumViateId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AppliedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobAdsApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobAdsApplications_AspNetUsers_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JobAdsApplications_CurriculumVitaes_CurriculumViateId",
                        column: x => x.CurriculumViateId,
                        principalTable: "CurriculumVitaes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JobAdsApplications_JobAdvertisements_JobAdId",
                        column: x => x.JobAdId,
                        principalTable: "JobAdvertisements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobAdsApplications_ApplicantId",
                table: "JobAdsApplications",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_JobAdsApplications_JobAdId",
                table: "JobAdsApplications",
                column: "JobAdId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobAdsApplications");
        }
    }
}
