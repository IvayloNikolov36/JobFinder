using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFinder.Data.Migrations
{
    /// <inheritdoc />
    public partial class NewMappingTablesForJobAdvertisement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobAdvertisements_WorkplaceTypes_WorkplaceTypeId",
                table: "JobAdvertisements");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "AnonymousProfileAppearancesTechStacks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "AnonymousProfileAppearancesTechStacks",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "AnonymousProfileAppearancesSoftSkills",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "AnonymousProfileAppearancesSoftSkills",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "AnonymousProfileAppearancesJobEngagements",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "AnonymousProfileAppearancesJobEngagements",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "AnonymousProfileAppearancesITAreas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "AnonymousProfileAppearancesITAreas",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "JobAdvertisementsITAreas",
                columns: table => new
                {
                    JobAdvertisementId = table.Column<int>(type: "int", nullable: false),
                    ITAreaId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobAdvertisementsITAreas", x => new { x.JobAdvertisementId, x.ITAreaId });
                    table.ForeignKey(
                        name: "FK_JobAdvertisementsITAreas_ItAreas_ITAreaId",
                        column: x => x.ITAreaId,
                        principalTable: "ItAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobAdvertisementsITAreas_JobAdvertisements_JobAdvertisementId",
                        column: x => x.JobAdvertisementId,
                        principalTable: "JobAdvertisements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobAdvertisementsSoftSkills",
                columns: table => new
                {
                    JobAdvertisementId = table.Column<int>(type: "int", nullable: false),
                    SoftSkillId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobAdvertisementsSoftSkills", x => new { x.JobAdvertisementId, x.SoftSkillId });
                    table.ForeignKey(
                        name: "FK_JobAdvertisementsSoftSkills_JobAdvertisements_JobAdvertisementId",
                        column: x => x.JobAdvertisementId,
                        principalTable: "JobAdvertisements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobAdvertisementsSoftSkills_SoftSkills_SoftSkillId",
                        column: x => x.SoftSkillId,
                        principalTable: "SoftSkills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobAdvertisementsTechStacks",
                columns: table => new
                {
                    JobAdvertisementId = table.Column<int>(type: "int", nullable: false),
                    TechStackId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobAdvertisementsTechStacks", x => new { x.JobAdvertisementId, x.TechStackId });
                    table.ForeignKey(
                        name: "FK_JobAdvertisementsTechStacks_JobAdvertisements_JobAdvertisementId",
                        column: x => x.JobAdvertisementId,
                        principalTable: "JobAdvertisements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobAdvertisementsTechStacks_TechStacks_TechStackId",
                        column: x => x.TechStackId,
                        principalTable: "TechStacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobAdvertisementsITAreas_ITAreaId",
                table: "JobAdvertisementsITAreas",
                column: "ITAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_JobAdvertisementsSoftSkills_SoftSkillId",
                table: "JobAdvertisementsSoftSkills",
                column: "SoftSkillId");

            migrationBuilder.CreateIndex(
                name: "IX_JobAdvertisementsTechStacks_TechStackId",
                table: "JobAdvertisementsTechStacks",
                column: "TechStackId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobAdvertisements_WorkplaceTypes_WorkplaceTypeId",
                table: "JobAdvertisements",
                column: "WorkplaceTypeId",
                principalTable: "WorkplaceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobAdvertisements_WorkplaceTypes_WorkplaceTypeId",
                table: "JobAdvertisements");

            migrationBuilder.DropTable(
                name: "JobAdvertisementsITAreas");

            migrationBuilder.DropTable(
                name: "JobAdvertisementsSoftSkills");

            migrationBuilder.DropTable(
                name: "JobAdvertisementsTechStacks");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "AnonymousProfileAppearancesTechStacks");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "AnonymousProfileAppearancesTechStacks");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "AnonymousProfileAppearancesSoftSkills");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "AnonymousProfileAppearancesSoftSkills");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "AnonymousProfileAppearancesJobEngagements");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "AnonymousProfileAppearancesJobEngagements");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "AnonymousProfileAppearancesITAreas");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "AnonymousProfileAppearancesITAreas");

            migrationBuilder.AddForeignKey(
                name: "FK_JobAdvertisements_WorkplaceTypes_WorkplaceTypeId",
                table: "JobAdvertisements",
                column: "WorkplaceTypeId",
                principalTable: "WorkplaceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
