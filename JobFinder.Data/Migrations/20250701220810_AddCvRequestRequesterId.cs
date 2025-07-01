using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFinder.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCvRequestRequesterId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RequesterId",
                table: "CvPreviewRequests",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CvPreviewRequests_RequesterId",
                table: "CvPreviewRequests",
                column: "RequesterId");

            migrationBuilder.AddForeignKey(
                name: "FK_CvPreviewRequests_AspNetUsers_RequesterId",
                table: "CvPreviewRequests",
                column: "RequesterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CvPreviewRequests_AspNetUsers_RequesterId",
                table: "CvPreviewRequests");

            migrationBuilder.DropIndex(
                name: "IX_CvPreviewRequests_RequesterId",
                table: "CvPreviewRequests");

            migrationBuilder.DropColumn(
                name: "RequesterId",
                table: "CvPreviewRequests");
        }
    }
}
