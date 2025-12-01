using JobFinder.Data.MigrationHelpers;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFinder.Data.Migrations
{
    /// <inheritdoc />
    public partial class View_LatestCompanyJobAdsForSubscribers_ChangeColumnLogoImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            ViewsHelper.Create_View_LatestCompanyJobAdsForSubscribers(migrationBuilder);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            ViewsHelper.Create_View_LatestCompanyJobsForSubscribers(migrationBuilder);
        }
    }
}
