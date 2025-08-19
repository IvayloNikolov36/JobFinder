using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFinder.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveJobAd_IsActiveColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobAdvertisements_LifecycleStatuses_LifecycleStatusId",
                table: "JobAdvertisements");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "JobAdvertisements");

            migrationBuilder.AlterColumn<int>(
                name: "LifecycleStatusId",
                table: "JobAdvertisements",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_JobAdvertisements_LifecycleStatuses_LifecycleStatusId",
                table: "JobAdvertisements",
                column: "LifecycleStatusId",
                principalTable: "LifecycleStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobAdvertisements_LifecycleStatuses_LifecycleStatusId",
                table: "JobAdvertisements");

            migrationBuilder.AlterColumn<int>(
                name: "LifecycleStatusId",
                table: "JobAdvertisements",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "JobAdvertisements",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddForeignKey(
                name: "FK_JobAdvertisements_LifecycleStatuses_LifecycleStatusId",
                table: "JobAdvertisements",
                column: "LifecycleStatusId",
                principalTable: "LifecycleStatuses",
                principalColumn: "Id");
        }
    }
}
