using JobFinder.Data.MigrationHelpers;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFinder.Data.Migrations
{
    /// <inheritdoc />
    public partial class GetJobSubscriptionsDataViewAndFunctionChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Intership",
                table: "JobAdvertisements",
                type: "bit",
                nullable: false,
                defaultValue: false);

            ViewsHelper.DropSubscriprionsByJobCategoryAndLocationView(migrationBuilder);
            FunctionsHelper.DropGetLatesJobAdsForSubscribersFunction(migrationBuilder);

            ViewsHelper.Create_View_JobSubscriptions(migrationBuilder);
            FunctionsHelper.Create_UDF_GetLatesJobAdsForSubscribers(migrationBuilder);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Intership",
                table: "JobAdvertisements");

            ViewsHelper.Drop_View_JobSubscriptions(migrationBuilder);
            FunctionsHelper.Drop_UDF_GetLatesJobAdsForSubscribers(migrationBuilder);

            ViewsHelper.CreateSubscriprionsByJobCategoryAndLocationView(migrationBuilder);
            FunctionsHelper.CreateGetLatesJobAdsForSubscribersFunction(migrationBuilder);
        }
    }
}
