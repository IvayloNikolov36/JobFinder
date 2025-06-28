using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFinder.Data.Migrations
{
    /// <inheritdoc />
    public partial class CvPreviewRequestUniqueConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CvPreviewRequests_AnonymousProfiles_AnonymousProfilePreviewedId",
                table: "CvPreviewRequests");

            migrationBuilder.DropIndex(
                name: "IX_CvPreviewRequests_AnonymousProfilePreviewedId",
                table: "CvPreviewRequests");

            migrationBuilder.DropColumn(
                name: "IsAccepted",
                table: "CvPreviewRequests");

            migrationBuilder.RenameColumn(
                name: "AnonymousProfilePreviewedId",
                table: "CvPreviewRequests",
                newName: "AnonymousProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_CvPreviewRequests_AnonymousProfileId_JobAdId",
                table: "CvPreviewRequests",
                columns: new[] { "AnonymousProfileId", "JobAdId" },
                unique: true,
                filter: "[AnonymousProfileId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_CvPreviewRequests_AnonymousProfiles_AnonymousProfileId",
                table: "CvPreviewRequests",
                column: "AnonymousProfileId",
                principalTable: "AnonymousProfiles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CvPreviewRequests_AnonymousProfiles_AnonymousProfileId",
                table: "CvPreviewRequests");

            migrationBuilder.DropIndex(
                name: "IX_CvPreviewRequests_AnonymousProfileId_JobAdId",
                table: "CvPreviewRequests");

            migrationBuilder.RenameColumn(
                name: "AnonymousProfileId",
                table: "CvPreviewRequests",
                newName: "AnonymousProfilePreviewedId");

            migrationBuilder.AddColumn<bool>(
                name: "IsAccepted",
                table: "CvPreviewRequests",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_CvPreviewRequests_AnonymousProfilePreviewedId",
                table: "CvPreviewRequests",
                column: "AnonymousProfilePreviewedId");

            migrationBuilder.AddForeignKey(
                name: "FK_CvPreviewRequests_AnonymousProfiles_AnonymousProfilePreviewedId",
                table: "CvPreviewRequests",
                column: "AnonymousProfilePreviewedId",
                principalTable: "AnonymousProfiles",
                principalColumn: "Id");
        }
    }
}
