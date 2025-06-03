using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace JobFinder.Data.Migrations
{
    /// <inheritdoc />
    public partial class AnonymousProfileAppearance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ITSkills");

            migrationBuilder.CreateTable(
                name: "AnonymousProfileAppearanceEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurriculumVitaeId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    RemoteJobPreferenceId = table.Column<int>(type: "int", nullable: false),
                    JobCategoryId = table.Column<int>(type: "int", nullable: false),
                    JobEngagementId = table.Column<int>(type: "int", nullable: false),
                    JobEngagementsId = table.Column<int>(type: "int", nullable: true),
                    PreferredPositions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnonymousProfileAppearanceEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnonymousProfileAppearanceEntity_CurriculumVitaes_CurriculumVitaeId",
                        column: x => x.CurriculumVitaeId,
                        principalTable: "CurriculumVitaes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AnonymousProfileAppearanceEntity_JobCategories_JobCategoryId",
                        column: x => x.JobCategoryId,
                        principalTable: "JobCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnonymousProfileAppearanceEntity_JobEngagements_JobEngagementsId",
                        column: x => x.JobEngagementsId,
                        principalTable: "JobEngagements",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AnonymousProfileAppearanceEntity_RemoteJobPreferences_RemoteJobPreferenceId",
                        column: x => x.RemoteJobPreferenceId,
                        principalTable: "RemoteJobPreferences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SoftSkills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoftSkills", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnonymousProfileAppearancesITAreas",
                columns: table => new
                {
                    AnonymousProfileAppearanceId = table.Column<int>(type: "int", nullable: false),
                    ITAreaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnonymousProfileAppearancesITAreas", x => new { x.AnonymousProfileAppearanceId, x.ITAreaId });
                    table.ForeignKey(
                        name: "FK_AnonymousProfileAppearancesITAreas_AnonymousProfileAppearanceEntity_AnonymousProfileAppearanceId",
                        column: x => x.AnonymousProfileAppearanceId,
                        principalTable: "AnonymousProfileAppearanceEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnonymousProfileAppearancesITAreas_ItAreas_ITAreaId",
                        column: x => x.ITAreaId,
                        principalTable: "ItAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnonymousProfileAppearancesJobEngagements",
                columns: table => new
                {
                    AnonymousProfileAppearanceId = table.Column<int>(type: "int", nullable: false),
                    JobEngagementId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnonymousProfileAppearancesJobEngagements", x => new { x.AnonymousProfileAppearanceId, x.JobEngagementId });
                    table.ForeignKey(
                        name: "FK_AnonymousProfileAppearancesJobEngagements_AnonymousProfileAppearanceEntity_AnonymousProfileAppearanceId",
                        column: x => x.AnonymousProfileAppearanceId,
                        principalTable: "AnonymousProfileAppearanceEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnonymousProfileAppearancesJobEngagements_JobEngagements_JobEngagementId",
                        column: x => x.JobEngagementId,
                        principalTable: "JobEngagements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnonymousProfileAppearancesTechStacks",
                columns: table => new
                {
                    AnonymousProfileAppearanceId = table.Column<int>(type: "int", nullable: false),
                    TechStackId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnonymousProfileAppearancesTechStacks", x => new { x.AnonymousProfileAppearanceId, x.TechStackId });
                    table.ForeignKey(
                        name: "FK_AnonymousProfileAppearancesTechStacks_AnonymousProfileAppearanceEntity_AnonymousProfileAppearanceId",
                        column: x => x.AnonymousProfileAppearanceId,
                        principalTable: "AnonymousProfileAppearanceEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnonymousProfileAppearancesTechStacks_TechStacks_TechStackId",
                        column: x => x.TechStackId,
                        principalTable: "TechStacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnonymousProfileAppearancesSoftSkills",
                columns: table => new
                {
                    AnonymousProfileAppearanceId = table.Column<int>(type: "int", nullable: false),
                    SoftSkillId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnonymousProfileAppearancesSoftSkills", x => new { x.AnonymousProfileAppearanceId, x.SoftSkillId });
                    table.ForeignKey(
                        name: "FK_AnonymousProfileAppearancesSoftSkills_AnonymousProfileAppearanceEntity_AnonymousProfileAppearanceId",
                        column: x => x.AnonymousProfileAppearanceId,
                        principalTable: "AnonymousProfileAppearanceEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnonymousProfileAppearancesSoftSkills_SoftSkills_SoftSkillId",
                        column: x => x.SoftSkillId,
                        principalTable: "SoftSkills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "SoftSkills",
                columns: new[] { "Id", "CreatedOn", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "teamwork" },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "organization skills" },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "critical thinking" },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "creativity" },
                    { 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "adaptability" },
                    { 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "project management" },
                    { 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "leader ship" },
                    { 8, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "presentation skills" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnonymousProfileAppearanceEntity_CurriculumVitaeId",
                table: "AnonymousProfileAppearanceEntity",
                column: "CurriculumVitaeId");

            migrationBuilder.CreateIndex(
                name: "IX_AnonymousProfileAppearanceEntity_JobCategoryId",
                table: "AnonymousProfileAppearanceEntity",
                column: "JobCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AnonymousProfileAppearanceEntity_JobEngagementsId",
                table: "AnonymousProfileAppearanceEntity",
                column: "JobEngagementsId");

            migrationBuilder.CreateIndex(
                name: "IX_AnonymousProfileAppearanceEntity_RemoteJobPreferenceId",
                table: "AnonymousProfileAppearanceEntity",
                column: "RemoteJobPreferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_AnonymousProfileAppearancesITAreas_ITAreaId",
                table: "AnonymousProfileAppearancesITAreas",
                column: "ITAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_AnonymousProfileAppearancesJobEngagements_JobEngagementId",
                table: "AnonymousProfileAppearancesJobEngagements",
                column: "JobEngagementId");

            migrationBuilder.CreateIndex(
                name: "IX_AnonymousProfileAppearancesSoftSkills_SoftSkillId",
                table: "AnonymousProfileAppearancesSoftSkills",
                column: "SoftSkillId");

            migrationBuilder.CreateIndex(
                name: "IX_AnonymousProfileAppearancesTechStacks_TechStackId",
                table: "AnonymousProfileAppearancesTechStacks",
                column: "TechStackId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnonymousProfileAppearancesITAreas");

            migrationBuilder.DropTable(
                name: "AnonymousProfileAppearancesJobEngagements");

            migrationBuilder.DropTable(
                name: "AnonymousProfileAppearancesSoftSkills");

            migrationBuilder.DropTable(
                name: "AnonymousProfileAppearancesTechStacks");

            migrationBuilder.DropTable(
                name: "SoftSkills");

            migrationBuilder.DropTable(
                name: "AnonymousProfileAppearanceEntity");

            migrationBuilder.CreateTable(
                name: "ITSkills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ITSkills", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ITSkills",
                columns: new[] { "Id", "CreatedOn", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "teamwork" },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "organization skills" },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "critical thinking" },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "creativity" },
                    { 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "adaptability" },
                    { 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "project management" },
                    { 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "leader ship" },
                    { 8, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "presentation skills" }
                });
        }
    }
}
