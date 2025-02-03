using JobFinder.Data.MigrationHelpers;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JobFinder.Data.Migrations
{
    public partial class CreateViewsAndFunctions : Migration
    {
        protected override void Up(MigrationBuilder builder)
        {
            ViewsHelper.CreateCompaniesSubscriptionsDataView(builder);
            ViewsHelper.CreateSubscriprionsByJobCategoryAndLocationView(builder);
            FunctionsHelper.CreateGetLatesJobAdsForSubscribersFunction(builder);
        }

        protected override void Down(MigrationBuilder builder)
        {
            ViewsHelper.DropCompaniesSubscriptionsDataView(builder);
            ViewsHelper.DropSubscriprionsByJobCategoryAndLocationView(builder);
            FunctionsHelper.DropGetLatesJobAdsForSubscribersFunction(builder);
        }
    }
}
