using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFinder.Data.Migrations
{
    /// <inheritdoc />
    public partial class JobAdEntity_LifecycleStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LifecycleStatusId",
                table: "JobAdvertisements",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobAdvertisements_LifecycleStatusId",
                table: "JobAdvertisements",
                column: "LifecycleStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobAdvertisements_LifecycleStatuses_LifecycleStatusId",
                table: "JobAdvertisements",
                column: "LifecycleStatusId",
                principalTable: "LifecycleStatuses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobAdvertisements_LifecycleStatuses_LifecycleStatusId",
                table: "JobAdvertisements");

            migrationBuilder.DropIndex(
                name: "IX_JobAdvertisements_LifecycleStatusId",
                table: "JobAdvertisements");

            migrationBuilder.DropColumn(
                name: "LifecycleStatusId",
                table: "JobAdvertisements");
        }
    }
}
