using JobFinder.Data.MigrationHelpers;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JobFinder.Data.Migrations
{
    public partial class CompanySubscriptionsDataView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            ViewsHelper.CreateCompaniesSubscriptionsDataView(migrationBuilder);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            ViewsHelper.DropCompaniesSubscriptionsDataView(migrationBuilder);
        }
    }
}
