using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFinder.Data.Migrations
{
    /// <inheritdoc />
    public partial class AnonymousProfileInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IncludeInAnonymousProfile",
                table: "WorkExperienceInfos",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IncludeInAnonymousProfile",
                table: "LanguageInfos",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IncludeInAnonymousProfile",
                table: "EducationInfos",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "AnonymousProfileActivated",
                table: "CurriculumVitaes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IncludeInAnonymousProfile",
                table: "CourseCertificatesInfos",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IncludeInAnonymousProfile",
                table: "WorkExperienceInfos");

            migrationBuilder.DropColumn(
                name: "IncludeInAnonymousProfile",
                table: "LanguageInfos");

            migrationBuilder.DropColumn(
                name: "IncludeInAnonymousProfile",
                table: "EducationInfos");

            migrationBuilder.DropColumn(
                name: "AnonymousProfileActivated",
                table: "CurriculumVitaes");

            migrationBuilder.DropColumn(
                name: "IncludeInAnonymousProfile",
                table: "CourseCertificatesInfos");
        }
    }
}
