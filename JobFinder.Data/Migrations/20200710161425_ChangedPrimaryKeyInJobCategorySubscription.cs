using Microsoft.EntityFrameworkCore.Migrations;

namespace JobFinder.Data.Migrations
{
    public partial class ChangedPrimaryKeyInJobCategorySubscription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_JobCategorySubscriptions",
                table: "JobCategorySubscriptions");

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "JobCategorySubscriptions",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "JobCategorySubscriptions",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobCategorySubscriptions",
                table: "JobCategorySubscriptions",
                column: "Id");

            migrationBuilder.AddUniqueConstraint(
                name: "IX_JobCategorySubscription_TripleAK",
                table: "JobCategorySubscriptions",
                columns: new[] { "UserId", "JobCategoryId", "Location" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_JobCategorySubscriptions",
                table: "JobCategorySubscriptions");

            migrationBuilder.DropUniqueConstraint(
                name: "IX_JobCategorySubscription_TripleAK",
                table: "JobCategorySubscriptions");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "JobCategorySubscriptions");

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "JobCategorySubscriptions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobCategorySubscriptions",
                table: "JobCategorySubscriptions",
                columns: new[] { "UserId", "JobCategoryId" });
        }
    }
}
