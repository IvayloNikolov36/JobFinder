using Microsoft.EntityFrameworkCore.Migrations;

namespace JobFinder.Data.Migrations
{
    public partial class ChangedRelationBtwJobAdsAndPublisher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobAds_AspNetUsers_PublisherId",
                table: "JobAds");

            migrationBuilder.AlterColumn<int>(
                name: "PublisherId",
                table: "JobAds",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_JobAds_Companies_PublisherId",
                table: "JobAds",
                column: "PublisherId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobAds_Companies_PublisherId",
                table: "JobAds");

            migrationBuilder.AlterColumn<string>(
                name: "PublisherId",
                table: "JobAds",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_JobAds_AspNetUsers_PublisherId",
                table: "JobAds",
                column: "PublisherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
