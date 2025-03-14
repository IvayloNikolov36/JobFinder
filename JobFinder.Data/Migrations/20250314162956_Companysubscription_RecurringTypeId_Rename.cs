using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace JobFinder.Data.Migrations
{
    /// <inheritdoc />
    public partial class Companysubscription_RecurringTypeId_Rename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanySubscriptions_ReccuringTypes_ReccuringTypeId",
                table: "CompanySubscriptions");

            migrationBuilder.RenameColumn(
                name: "ReccuringTypeId",
                table: "CompanySubscriptions",
                newName: "RecuringTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_CompanySubscriptions_ReccuringTypeId",
                table: "CompanySubscriptions",
                newName: "IX_CompanySubscriptions_RecuringTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanySubscriptions_ReccuringTypes_RecuringTypeId",
                table: "CompanySubscriptions",
                column: "RecuringTypeId",
                principalTable: "ReccuringTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanySubscriptions_ReccuringTypes_RecuringTypeId",
                table: "CompanySubscriptions");

            migrationBuilder.RenameColumn(
                name: "RecuringTypeId",
                table: "CompanySubscriptions",
                newName: "ReccuringTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_CompanySubscriptions_RecuringTypeId",
                table: "CompanySubscriptions",
                newName: "IX_CompanySubscriptions_ReccuringTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanySubscriptions_ReccuringTypes_ReccuringTypeId",
                table: "CompanySubscriptions",
                column: "ReccuringTypeId",
                principalTable: "ReccuringTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
