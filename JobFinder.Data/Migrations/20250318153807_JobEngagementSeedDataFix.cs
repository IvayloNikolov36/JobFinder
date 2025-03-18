using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFinder.Data.Migrations
{
    /// <inheritdoc />
    public partial class JobEngagementSeedDataFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "JobEngagements",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Suitable for students");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "JobEngagements",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "SuitableForStudents");
        }
    }
}
