using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFinder.Data.Migrations
{
    /// <inheritdoc />
    public partial class CvPreviewRequest_RemoveAnonymousProfileIdColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CvPreviewRequests_AnonymousProfiles_AnonymousProfileId",
                table: "CvPreviewRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_CvPreviewRequests_CurriculumVitaes_CvId",
                table: "CvPreviewRequests");

            migrationBuilder.DropIndex(
                name: "IX_CvPreviewRequests_AnonymousProfileId_JobAdId",
                table: "CvPreviewRequests");

            migrationBuilder.DropIndex(
                name: "IX_CvPreviewRequests_CvId",
                table: "CvPreviewRequests");

            migrationBuilder.DropColumn(
                name: "AnonymousProfileId",
                table: "CvPreviewRequests");

            migrationBuilder.AlterColumn<string>(
                name: "CvId",
                table: "CvPreviewRequests",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CvPreviewRequests_CvId_JobAdId",
                table: "CvPreviewRequests",
                columns: new[] { "CvId", "JobAdId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CvPreviewRequests_CurriculumVitaes_CvId",
                table: "CvPreviewRequests",
                column: "CvId",
                principalTable: "CurriculumVitaes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CvPreviewRequests_CurriculumVitaes_CvId",
                table: "CvPreviewRequests");

            migrationBuilder.DropIndex(
                name: "IX_CvPreviewRequests_CvId_JobAdId",
                table: "CvPreviewRequests");

            migrationBuilder.AlterColumn<string>(
                name: "CvId",
                table: "CvPreviewRequests",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "AnonymousProfileId",
                table: "CvPreviewRequests",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CvPreviewRequests_AnonymousProfileId_JobAdId",
                table: "CvPreviewRequests",
                columns: new[] { "AnonymousProfileId", "JobAdId" },
                unique: true,
                filter: "[AnonymousProfileId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CvPreviewRequests_CvId",
                table: "CvPreviewRequests",
                column: "CvId");

            migrationBuilder.AddForeignKey(
                name: "FK_CvPreviewRequests_AnonymousProfiles_AnonymousProfileId",
                table: "CvPreviewRequests",
                column: "AnonymousProfileId",
                principalTable: "AnonymousProfiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CvPreviewRequests_CurriculumVitaes_CvId",
                table: "CvPreviewRequests",
                column: "CvId",
                principalTable: "CurriculumVitaes",
                principalColumn: "Id");
        }
    }
}
