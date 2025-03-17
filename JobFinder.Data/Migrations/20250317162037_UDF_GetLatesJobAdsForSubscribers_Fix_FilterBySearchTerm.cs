using JobFinder.Data.MigrationHelpers;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFinder.Data.Migrations
{
    /// <inheritdoc />
    public partial class UDF_GetLatesJobAdsForSubscribers_Fix_FilterBySearchTerm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            FunctionsHelper.Drop_UDF_GetLatesJobAdsForSubscribers(migrationBuilder);
            FunctionsHelper.Create_UDF_GetLatesJobAdsForSubscribers_v2(migrationBuilder);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            FunctionsHelper.Drop_UDF_GetLatesJobAdsForSubscribers_v2(migrationBuilder);
            FunctionsHelper.Create_UDF_GetLatesJobAdsForSubscribers(migrationBuilder);
        }
    }
}
