using JobFinder.Data.MigrationHelpers;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFinder.Data.Migrations
{
    /// <inheritdoc />
    public partial class LatestCompanyJobAdsForSubscribersView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            FunctionsHelper.Drop_UDF_GetLatestJobAdsForCompanySubscriptions(migrationBuilder);
            ViewsHelper.Create_View_LatestCompanyJobsForSubscribers(migrationBuilder);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            ViewsHelper.Drop_View_LatestCompanyJobsForSubscribers(migrationBuilder);
            FunctionsHelper.Create_UDF_GetLatestJobAdsForCompanySubscriptions(migrationBuilder);
        }
    }
}
