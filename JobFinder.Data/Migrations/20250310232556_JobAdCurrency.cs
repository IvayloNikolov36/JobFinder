using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFinder.Data.Migrations
{
    /// <inheritdoc />
    public partial class JobAdCurrency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "JobAdvertisements",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobAdvertisements_CurrencyId",
                table: "JobAdvertisements",
                column: "CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobAdvertisements_Currencies_CurrencyId",
                table: "JobAdvertisements",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobAdvertisements_Currencies_CurrencyId",
                table: "JobAdvertisements");

            migrationBuilder.DropIndex(
                name: "IX_JobAdvertisements_CurrencyId",
                table: "JobAdvertisements");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "JobAdvertisements");
        }
    }
}
