using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFinder.Data.Migrations
{
    /// <inheritdoc />
    public partial class CloudImageColumnsChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CloudImageEntity_AspNetUsers_UploaderId",
                table: "CloudImageEntity");

            migrationBuilder.DropIndex(
                name: "IX_Companies_LogoImageId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Extension",
                table: "CloudImageEntity");

            migrationBuilder.RenameColumn(
                name: "UploaderId",
                table: "CloudImageEntity",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CloudImageEntity_UploaderId",
                table: "CloudImageEntity",
                newName: "IX_CloudImageEntity_UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_LogoImageId",
                table: "Companies",
                column: "LogoImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_CloudImageEntity_AspNetUsers_UserId",
                table: "CloudImageEntity",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CloudImageEntity_AspNetUsers_UserId",
                table: "CloudImageEntity");

            migrationBuilder.DropIndex(
                name: "IX_Companies_LogoImageId",
                table: "Companies");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "CloudImageEntity",
                newName: "UploaderId");

            migrationBuilder.RenameIndex(
                name: "IX_CloudImageEntity_UserId",
                table: "CloudImageEntity",
                newName: "IX_CloudImageEntity_UploaderId");

            migrationBuilder.AddColumn<string>(
                name: "Extension",
                table: "CloudImageEntity",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_LogoImageId",
                table: "Companies",
                column: "LogoImageId",
                unique: true,
                filter: "[LogoImageId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_CloudImageEntity_AspNetUsers_UploaderId",
                table: "CloudImageEntity",
                column: "UploaderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
