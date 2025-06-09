using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFinder.Data.Migrations
{
    /// <inheritdoc />
    public partial class AnonymousProfileAppearanceCV_FK_Removed_AndAP_FK_required : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnonymousProfileAppearances_AnonymousProfiles_AnonymousProfileId",
                table: "AnonymousProfileAppearances");

            migrationBuilder.DropForeignKey(
                name: "FK_AnonymousProfileAppearances_CurriculumVitaes_CurriculumVitaeId",
                table: "AnonymousProfileAppearances");

            migrationBuilder.DropIndex(
                name: "IX_AnonymousProfileAppearances_AnonymousProfileId",
                table: "AnonymousProfileAppearances");

            migrationBuilder.DropIndex(
                name: "IX_AnonymousProfileAppearances_CurriculumVitaeId",
                table: "AnonymousProfileAppearances");

            migrationBuilder.DropColumn(
                name: "CurriculumVitaeId",
                table: "AnonymousProfileAppearances");

            migrationBuilder.AlterColumn<string>(
                name: "AnonymousProfileId",
                table: "AnonymousProfileAppearances",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AnonymousProfileAppearances_AnonymousProfileId",
                table: "AnonymousProfileAppearances",
                column: "AnonymousProfileId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AnonymousProfileAppearances_AnonymousProfiles_AnonymousProfileId",
                table: "AnonymousProfileAppearances",
                column: "AnonymousProfileId",
                principalTable: "AnonymousProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnonymousProfileAppearances_AnonymousProfiles_AnonymousProfileId",
                table: "AnonymousProfileAppearances");

            migrationBuilder.DropIndex(
                name: "IX_AnonymousProfileAppearances_AnonymousProfileId",
                table: "AnonymousProfileAppearances");

            migrationBuilder.AlterColumn<string>(
                name: "AnonymousProfileId",
                table: "AnonymousProfileAppearances",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "CurriculumVitaeId",
                table: "AnonymousProfileAppearances",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AnonymousProfileAppearances_AnonymousProfileId",
                table: "AnonymousProfileAppearances",
                column: "AnonymousProfileId",
                unique: true,
                filter: "[AnonymousProfileId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AnonymousProfileAppearances_CurriculumVitaeId",
                table: "AnonymousProfileAppearances",
                column: "CurriculumVitaeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnonymousProfileAppearances_AnonymousProfiles_AnonymousProfileId",
                table: "AnonymousProfileAppearances",
                column: "AnonymousProfileId",
                principalTable: "AnonymousProfiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AnonymousProfileAppearances_CurriculumVitaes_CurriculumVitaeId",
                table: "AnonymousProfileAppearances",
                column: "CurriculumVitaeId",
                principalTable: "CurriculumVitaes",
                principalColumn: "Id");
        }
    }
}
