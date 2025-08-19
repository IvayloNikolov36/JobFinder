using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFinder.Data.Migrations
{
    /// <inheritdoc />
    public partial class JobAdEntity_LifecycleStatus_OnDeleteBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobAdvertisements_LifecycleStatuses_LifecycleStatusId",
                table: "JobAdvertisements");

            migrationBuilder.AddForeignKey(
                name: "FK_JobAdvertisements_LifecycleStatuses_LifecycleStatusId",
                table: "JobAdvertisements",
                column: "LifecycleStatusId",
                principalTable: "LifecycleStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobAdvertisements_LifecycleStatuses_LifecycleStatusId",
                table: "JobAdvertisements");

            migrationBuilder.AddForeignKey(
                name: "FK_JobAdvertisements_LifecycleStatuses_LifecycleStatusId",
                table: "JobAdvertisements",
                column: "LifecycleStatusId",
                principalTable: "LifecycleStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
