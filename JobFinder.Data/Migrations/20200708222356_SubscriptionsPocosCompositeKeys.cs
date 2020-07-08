using Microsoft.EntityFrameworkCore.Migrations;

namespace JobFinder.Data.Migrations
{
    public partial class SubscriptionsPocosCompositeKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_JobCategorySubscriptions",
                table: "JobCategorySubscriptions");

            migrationBuilder.DropIndex(
                name: "IX_JobCategorySubscriptions_UserId",
                table: "JobCategorySubscriptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanySubscriptions",
                table: "CompanySubscriptions");

            migrationBuilder.DropIndex(
                name: "IX_CompanySubscriptions_UserId",
                table: "CompanySubscriptions");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "JobCategorySubscriptions");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CompanySubscriptions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobCategorySubscriptions",
                table: "JobCategorySubscriptions",
                columns: new[] { "UserId", "JobCategoryId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanySubscriptions",
                table: "CompanySubscriptions",
                columns: new[] { "UserId", "CompanyId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_JobCategorySubscriptions",
                table: "JobCategorySubscriptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanySubscriptions",
                table: "CompanySubscriptions");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "JobCategorySubscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CompanySubscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobCategorySubscriptions",
                table: "JobCategorySubscriptions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanySubscriptions",
                table: "CompanySubscriptions",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_JobCategorySubscriptions_UserId",
                table: "JobCategorySubscriptions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanySubscriptions_UserId",
                table: "CompanySubscriptions",
                column: "UserId");
        }
    }
}
