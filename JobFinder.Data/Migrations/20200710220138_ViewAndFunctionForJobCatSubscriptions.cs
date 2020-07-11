using JobFinder.Data.MigrationHelpers;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JobFinder.Data.Migrations
{
    public partial class ViewAndFunctionForJobCatSubscriptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            ViewsHelper.CreateSubscriprionsByJobCategoryAndLocationView(migrationBuilder);
            FunctionsHelper.CreateGetLatesJobAdsForSubscribersFunction(migrationBuilder);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            ViewsHelper.DropSubscriprionsByJobCategoryAndLocationView(migrationBuilder);
            FunctionsHelper.DropGetLatesJobAdsForSubscribersFunction(migrationBuilder);
        }
    }
}
