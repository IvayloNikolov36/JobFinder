using JobFinder.Data.MigrationHelpers;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFinder.Data.Migrations
{
    /// <inheritdoc />
    public partial class GetJobSubscriptions_ConvertedToFunction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            ViewsHelper.Drop_View_JobSubscriptions(migrationBuilder);
            FunctionsHelper.Create_UDF_GetJobSubscriptions(migrationBuilder);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            ViewsHelper.Create_View_JobSubscriptions(migrationBuilder);
            FunctionsHelper.Drop_UDF_GetJobSubscriptions(migrationBuilder);

        }
    }
}
