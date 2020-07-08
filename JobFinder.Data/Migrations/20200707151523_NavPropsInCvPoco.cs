using Microsoft.EntityFrameworkCore.Migrations;

namespace JobFinder.Data.Migrations
{
    public partial class NavPropsInCvPoco : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Skills_CurriculumVitaeId",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_PersonalDetails_CurriculumVitaeId",
                table: "PersonalDetails");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_CurriculumVitaeId",
                table: "Skills",
                column: "CurriculumVitaeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonalDetails_CurriculumVitaeId",
                table: "PersonalDetails",
                column: "CurriculumVitaeId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Skills_CurriculumVitaeId",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_PersonalDetails_CurriculumVitaeId",
                table: "PersonalDetails");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_CurriculumVitaeId",
                table: "Skills",
                column: "CurriculumVitaeId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalDetails_CurriculumVitaeId",
                table: "PersonalDetails",
                column: "CurriculumVitaeId");
        }
    }
}
