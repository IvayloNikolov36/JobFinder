using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFinder.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCompanySubscriptionRecurringTypeFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanySubscriptions_ReccuringTypes_RecuringTypeId",
                table: "CompanySubscriptions");

            migrationBuilder.DropIndex(
                name: "IX_CompanySubscriptions_RecuringTypeId",
                table: "CompanySubscriptions");

            migrationBuilder.DropColumn(
                name: "RecuringTypeId",
                table: "CompanySubscriptions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RecuringTypeId",
                table: "CompanySubscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CompanySubscriptions_RecuringTypeId",
                table: "CompanySubscriptions",
                column: "RecuringTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanySubscriptions_ReccuringTypes_RecuringTypeId",
                table: "CompanySubscriptions",
                column: "RecuringTypeId",
                principalTable: "ReccuringTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
