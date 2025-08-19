using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFinder.Data.Migrations
{
    /// <inheritdoc />
    public partial class TransferJobAdData_FromIsActive_ToLifecycleStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
            @$"
                UPDATE [dbo].[JobAdvertisements]
                SET [LifecycleStatusId] = (CASE WHEN [IsActive] = 1 THEN 2 ELSE 3 END)
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
            @$"
                UPDATE [dbo].[JobAdvertisements]
                SET [IsActive] = (CASE WHEN [LifecycleStatusId] = 3 THEN 0 ELSE 1 END)
            ");
        }
    }
}
