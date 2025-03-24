using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFinder.Data.Migrations
{
    /// <inheritdoc />
    public partial class JobAdvertisement_CheckConstraint_Salary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "CHK_JobAdvertisements_MinSalary_MaxSalary_Currency",
                table: "JobAdvertisements",
                sql: "(CurrencyId IS NOT NULL AND MinSalary IS NOT NULL AND MaxSalary IS NOT NULL AND MinSalary <= MaxSalary)\r\n                        OR (CurrencyId IS NULL AND MinSalary IS NULL AND MaxSalary IS NULL)\r\n                     ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CHK_JobAdvertisements_MinSalary_MaxSalary_Currency",
                table: "JobAdvertisements");
        }
    }
}
