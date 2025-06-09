using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFinder.Data.Migrations
{
    /// <inheritdoc />
    public partial class AnonymousProfileEntityCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnonymousProfileActivated",
                table: "CurriculumVitaes");

            migrationBuilder.DropColumn(
                name: "Data",
                table: "CurriculumVitaes");

            migrationBuilder.AddColumn<string>(
                name: "AnonymousProfileId",
                table: "AnonymousProfileAppearances",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AnonymousProfiles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CurriculumVitaeId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnonymousProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnonymousProfiles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AnonymousProfiles_CurriculumVitaes_CurriculumVitaeId",
                        column: x => x.CurriculumVitaeId,
                        principalTable: "CurriculumVitaes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnonymousProfileAppearances_AnonymousProfileId",
                table: "AnonymousProfileAppearances",
                column: "AnonymousProfileId",
                unique: true,
                filter: "[AnonymousProfileId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AnonymousProfiles_CurriculumVitaeId",
                table: "AnonymousProfiles",
                column: "CurriculumVitaeId",
                unique: true,
                filter: "[CurriculumVitaeId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AnonymousProfiles_UserId",
                table: "AnonymousProfiles",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnonymousProfileAppearances_AnonymousProfiles_AnonymousProfileId",
                table: "AnonymousProfileAppearances",
                column: "AnonymousProfileId",
                principalTable: "AnonymousProfiles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnonymousProfileAppearances_AnonymousProfiles_AnonymousProfileId",
                table: "AnonymousProfileAppearances");

            migrationBuilder.DropTable(
                name: "AnonymousProfiles");

            migrationBuilder.DropIndex(
                name: "IX_AnonymousProfileAppearances_AnonymousProfileId",
                table: "AnonymousProfileAppearances");

            migrationBuilder.DropColumn(
                name: "AnonymousProfileId",
                table: "AnonymousProfileAppearances");

            migrationBuilder.AddColumn<bool>(
                name: "AnonymousProfileActivated",
                table: "CurriculumVitaes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<byte[]>(
                name: "Data",
                table: "CurriculumVitaes",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
