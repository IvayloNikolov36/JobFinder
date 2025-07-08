using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFinder.Data.Migrations
{
    /// <inheritdoc />
    public partial class CvPreviewRequest_CvIdColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CvId",
                table: "CvPreviewRequests",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CvPreviewRequests_CvId",
                table: "CvPreviewRequests",
                column: "CvId");

            migrationBuilder.AddForeignKey(
                name: "FK_CvPreviewRequests_CurriculumVitaes_CvId",
                table: "CvPreviewRequests",
                column: "CvId",
                principalTable: "CurriculumVitaes",
                principalColumn: "Id");

            migrationBuilder.Sql(@"UPDATE [dbo].[CvPreviewRequests]
                SET CvId = (
                    SELECT CvId FROM [dbo].[AnonymousProfiles] AS ap
                    WHERE ap.[Id] = [AnonymousProfileId]
                )"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CvPreviewRequests_CurriculumVitaes_CvId",
                table: "CvPreviewRequests");

            migrationBuilder.DropIndex(
                name: "IX_CvPreviewRequests_CvId",
                table: "CvPreviewRequests");

            migrationBuilder.DropColumn(
                name: "CvId",
                table: "CvPreviewRequests");
        }
    }
}
