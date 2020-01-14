using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JobFinder.Data.Migrations
{
    public partial class JobAdEngagementAndCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecruitmentOffers");

            migrationBuilder.AddColumn<string>(
                name: "CompanyLogo",
                table: "Companies",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "JobCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobEngagements",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobEngagements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobAds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Position = table.Column<string>(maxLength: 90, nullable: false),
                    Desription = table.Column<string>(nullable: false),
                    Location = table.Column<string>(nullable: false),
                    Expiration = table.Column<DateTime>(nullable: false),
                    PostedOn = table.Column<DateTime>(nullable: false),
                    MinSalary = table.Column<int>(nullable: true),
                    MaxSalary = table.Column<int>(nullable: true),
                    JobCategoryId = table.Column<int>(nullable: false),
                    JobEngagementId = table.Column<int>(nullable: false),
                    PublisherId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobAds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobAds_JobCategories_JobCategoryId",
                        column: x => x.JobCategoryId,
                        principalTable: "JobCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobAds_JobEngagements_JobEngagementId",
                        column: x => x.JobEngagementId,
                        principalTable: "JobEngagements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobAds_AspNetUsers_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobAds_JobCategoryId",
                table: "JobAds",
                column: "JobCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_JobAds_JobEngagementId",
                table: "JobAds",
                column: "JobEngagementId");

            migrationBuilder.CreateIndex(
                name: "IX_JobAds_PublisherId",
                table: "JobAds",
                column: "PublisherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobAds");

            migrationBuilder.DropTable(
                name: "JobCategories");

            migrationBuilder.DropTable(
                name: "JobEngagements");

            migrationBuilder.DropColumn(
                name: "CompanyLogo",
                table: "Companies");

            migrationBuilder.CreateTable(
                name: "RecruitmentOffers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Desription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(90)", maxLength: 90, nullable: false),
                    PostedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PublisherId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecruitmentOffers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecruitmentOffers_AspNetUsers_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecruitmentOffers_PublisherId",
                table: "RecruitmentOffers",
                column: "PublisherId");
        }
    }
}
