using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFinder.Data.Migrations
{
    /// <inheritdoc />
    public partial class JobAdvertisementRenamings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobAdvertisementsITAreas_ItAreas_ITAreaId",
                table: "JobAdvertisementsITAreas");

            migrationBuilder.DropForeignKey(
                name: "FK_JobAdvertisementsITAreas_JobAdvertisements_JobAdvertisementId",
                table: "JobAdvertisementsITAreas");

            migrationBuilder.DropForeignKey(
                name: "FK_JobAdvertisementsSoftSkills_JobAdvertisements_JobAdvertisementId",
                table: "JobAdvertisementsSoftSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_JobAdvertisementsSoftSkills_SoftSkills_SoftSkillId",
                table: "JobAdvertisementsSoftSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_JobAdvertisementsTechStacks_JobAdvertisements_JobAdvertisementId",
                table: "JobAdvertisementsTechStacks");

            migrationBuilder.DropForeignKey(
                name: "FK_JobAdvertisementsTechStacks_TechStacks_TechStackId",
                table: "JobAdvertisementsTechStacks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobAdvertisementsTechStacks",
                table: "JobAdvertisementsTechStacks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobAdvertisementsSoftSkills",
                table: "JobAdvertisementsSoftSkills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobAdvertisementsITAreas",
                table: "JobAdvertisementsITAreas");

            migrationBuilder.RenameTable(
                name: "JobAdvertisementsTechStacks",
                newName: "JobAdsTechStacks");

            migrationBuilder.RenameTable(
                name: "JobAdvertisementsSoftSkills",
                newName: "JobAdsSoftSkills");

            migrationBuilder.RenameTable(
                name: "JobAdvertisementsITAreas",
                newName: "JobAdsItAreas");

            migrationBuilder.RenameColumn(
                name: "JobAdvertisementId",
                table: "JobAdsTechStacks",
                newName: "JobAdId");

            migrationBuilder.RenameIndex(
                name: "IX_JobAdvertisementsTechStacks_TechStackId",
                table: "JobAdsTechStacks",
                newName: "IX_JobAdsTechStacks_TechStackId");

            migrationBuilder.RenameColumn(
                name: "JobAdvertisementId",
                table: "JobAdsSoftSkills",
                newName: "JobAdId");

            migrationBuilder.RenameIndex(
                name: "IX_JobAdvertisementsSoftSkills_SoftSkillId",
                table: "JobAdsSoftSkills",
                newName: "IX_JobAdsSoftSkills_SoftSkillId");

            migrationBuilder.RenameColumn(
                name: "ITAreaId",
                table: "JobAdsItAreas",
                newName: "ItAreaId");

            migrationBuilder.RenameColumn(
                name: "JobAdvertisementId",
                table: "JobAdsItAreas",
                newName: "JobAdId");

            migrationBuilder.RenameIndex(
                name: "IX_JobAdvertisementsITAreas_ITAreaId",
                table: "JobAdsItAreas",
                newName: "IX_JobAdsItAreas_ItAreaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobAdsTechStacks",
                table: "JobAdsTechStacks",
                columns: new[] { "JobAdId", "TechStackId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobAdsSoftSkills",
                table: "JobAdsSoftSkills",
                columns: new[] { "JobAdId", "SoftSkillId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobAdsItAreas",
                table: "JobAdsItAreas",
                columns: new[] { "JobAdId", "ItAreaId" });

            migrationBuilder.AddForeignKey(
                name: "FK_JobAdsItAreas_ItAreas_ItAreaId",
                table: "JobAdsItAreas",
                column: "ItAreaId",
                principalTable: "ItAreas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobAdsItAreas_JobAdvertisements_JobAdId",
                table: "JobAdsItAreas",
                column: "JobAdId",
                principalTable: "JobAdvertisements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobAdsSoftSkills_JobAdvertisements_JobAdId",
                table: "JobAdsSoftSkills",
                column: "JobAdId",
                principalTable: "JobAdvertisements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobAdsSoftSkills_SoftSkills_SoftSkillId",
                table: "JobAdsSoftSkills",
                column: "SoftSkillId",
                principalTable: "SoftSkills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobAdsTechStacks_JobAdvertisements_JobAdId",
                table: "JobAdsTechStacks",
                column: "JobAdId",
                principalTable: "JobAdvertisements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobAdsTechStacks_TechStacks_TechStackId",
                table: "JobAdsTechStacks",
                column: "TechStackId",
                principalTable: "TechStacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobAdsItAreas_ItAreas_ItAreaId",
                table: "JobAdsItAreas");

            migrationBuilder.DropForeignKey(
                name: "FK_JobAdsItAreas_JobAdvertisements_JobAdId",
                table: "JobAdsItAreas");

            migrationBuilder.DropForeignKey(
                name: "FK_JobAdsSoftSkills_JobAdvertisements_JobAdId",
                table: "JobAdsSoftSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_JobAdsSoftSkills_SoftSkills_SoftSkillId",
                table: "JobAdsSoftSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_JobAdsTechStacks_JobAdvertisements_JobAdId",
                table: "JobAdsTechStacks");

            migrationBuilder.DropForeignKey(
                name: "FK_JobAdsTechStacks_TechStacks_TechStackId",
                table: "JobAdsTechStacks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobAdsTechStacks",
                table: "JobAdsTechStacks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobAdsSoftSkills",
                table: "JobAdsSoftSkills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobAdsItAreas",
                table: "JobAdsItAreas");

            migrationBuilder.RenameTable(
                name: "JobAdsTechStacks",
                newName: "JobAdvertisementsTechStacks");

            migrationBuilder.RenameTable(
                name: "JobAdsSoftSkills",
                newName: "JobAdvertisementsSoftSkills");

            migrationBuilder.RenameTable(
                name: "JobAdsItAreas",
                newName: "JobAdvertisementsITAreas");

            migrationBuilder.RenameColumn(
                name: "JobAdId",
                table: "JobAdvertisementsTechStacks",
                newName: "JobAdvertisementId");

            migrationBuilder.RenameIndex(
                name: "IX_JobAdsTechStacks_TechStackId",
                table: "JobAdvertisementsTechStacks",
                newName: "IX_JobAdvertisementsTechStacks_TechStackId");

            migrationBuilder.RenameColumn(
                name: "JobAdId",
                table: "JobAdvertisementsSoftSkills",
                newName: "JobAdvertisementId");

            migrationBuilder.RenameIndex(
                name: "IX_JobAdsSoftSkills_SoftSkillId",
                table: "JobAdvertisementsSoftSkills",
                newName: "IX_JobAdvertisementsSoftSkills_SoftSkillId");

            migrationBuilder.RenameColumn(
                name: "ItAreaId",
                table: "JobAdvertisementsITAreas",
                newName: "ITAreaId");

            migrationBuilder.RenameColumn(
                name: "JobAdId",
                table: "JobAdvertisementsITAreas",
                newName: "JobAdvertisementId");

            migrationBuilder.RenameIndex(
                name: "IX_JobAdsItAreas_ItAreaId",
                table: "JobAdvertisementsITAreas",
                newName: "IX_JobAdvertisementsITAreas_ITAreaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobAdvertisementsTechStacks",
                table: "JobAdvertisementsTechStacks",
                columns: new[] { "JobAdvertisementId", "TechStackId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobAdvertisementsSoftSkills",
                table: "JobAdvertisementsSoftSkills",
                columns: new[] { "JobAdvertisementId", "SoftSkillId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobAdvertisementsITAreas",
                table: "JobAdvertisementsITAreas",
                columns: new[] { "JobAdvertisementId", "ITAreaId" });

            migrationBuilder.AddForeignKey(
                name: "FK_JobAdvertisementsITAreas_ItAreas_ITAreaId",
                table: "JobAdvertisementsITAreas",
                column: "ITAreaId",
                principalTable: "ItAreas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobAdvertisementsITAreas_JobAdvertisements_JobAdvertisementId",
                table: "JobAdvertisementsITAreas",
                column: "JobAdvertisementId",
                principalTable: "JobAdvertisements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobAdvertisementsSoftSkills_JobAdvertisements_JobAdvertisementId",
                table: "JobAdvertisementsSoftSkills",
                column: "JobAdvertisementId",
                principalTable: "JobAdvertisements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobAdvertisementsSoftSkills_SoftSkills_SoftSkillId",
                table: "JobAdvertisementsSoftSkills",
                column: "SoftSkillId",
                principalTable: "SoftSkills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobAdvertisementsTechStacks_JobAdvertisements_JobAdvertisementId",
                table: "JobAdvertisementsTechStacks",
                column: "JobAdvertisementId",
                principalTable: "JobAdvertisements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobAdvertisementsTechStacks_TechStacks_TechStackId",
                table: "JobAdvertisementsTechStacks",
                column: "TechStackId",
                principalTable: "TechStacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
