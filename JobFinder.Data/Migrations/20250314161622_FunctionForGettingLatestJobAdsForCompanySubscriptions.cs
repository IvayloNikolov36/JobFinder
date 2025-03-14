using JobFinder.Data.MigrationHelpers;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFinder.Data.Migrations
{
    /// <inheritdoc />
    public partial class FunctionForGettingLatestJobAdsForCompanySubscriptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReccuringTypeId",
                table: "CompanySubscriptions",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CompanySubscriptions_ReccuringTypeId",
                table: "CompanySubscriptions",
                column: "ReccuringTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanySubscriptions_ReccuringTypes_ReccuringTypeId",
                table: "CompanySubscriptions",
                column: "ReccuringTypeId",
                principalTable: "ReccuringTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.Sql("Update [dbo].[CompanySubscriptions] SET ReccuringTypeId = 1");

            ViewsHelper.DropCompaniesSubscriptionsDataView(migrationBuilder);
            FunctionsHelper.Create_UDF_GetLatestJobAdsForCompanySubscriptions(migrationBuilder);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanySubscriptions_ReccuringTypes_ReccuringTypeId",
                table: "CompanySubscriptions");

            migrationBuilder.DropIndex(
                name: "IX_CompanySubscriptions_ReccuringTypeId",
                table: "CompanySubscriptions");

            migrationBuilder.DropColumn(
                name: "ReccuringTypeId",
                table: "CompanySubscriptions");

            FunctionsHelper.Drop_UDF_GetLatestJobAdsForCompanySubscriptions(migrationBuilder);
            ViewsHelper.CreateCompaniesSubscriptionsDataView(migrationBuilder);
        }
    }
}
