using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFinder.Data.Migrations
{
    /// <inheritdoc />
    public partial class AnonymousProfileAppearanceWorkplaceTypeIdFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WorkplaceTypeId",
                table: "AnonymousProfileAppearances",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "WorkplaceTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "On-site");

            migrationBuilder.UpdateData(
                table: "WorkplaceTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Remote");

            migrationBuilder.CreateIndex(
                name: "IX_AnonymousProfileAppearances_WorkplaceTypeId",
                table: "AnonymousProfileAppearances",
                column: "WorkplaceTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnonymousProfileAppearances_WorkplaceTypes_WorkplaceTypeId",
                table: "AnonymousProfileAppearances",
                column: "WorkplaceTypeId",
                principalTable: "WorkplaceTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnonymousProfileAppearances_WorkplaceTypes_WorkplaceTypeId",
                table: "AnonymousProfileAppearances");

            migrationBuilder.DropIndex(
                name: "IX_AnonymousProfileAppearances_WorkplaceTypeId",
                table: "AnonymousProfileAppearances");

            migrationBuilder.DropColumn(
                name: "WorkplaceTypeId",
                table: "AnonymousProfileAppearances");

            migrationBuilder.UpdateData(
                table: "WorkplaceTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Office");

            migrationBuilder.UpdateData(
                table: "WorkplaceTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Home");
        }
    }
}
