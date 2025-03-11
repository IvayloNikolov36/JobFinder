using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFinder.Data.Migrations
{
    /// <inheritdoc />
    public partial class SetJobAdsCurrency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder
                .Sql(@"UPDATE JobAdvertisements
                    SET CurrencyId = (SELECT TOP 1 Id FROM Currencies WHERE [Name]='BGN')
                    WHERE MinSalary IS NOT NULL OR MaxSalary IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
