using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFinder.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserProfilePictureColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CloudImageEntity_AspNetUsers_UserId",
                table: "CloudImageEntity");

            migrationBuilder.DropIndex(
                name: "IX_CloudImageEntity_UserId",
                table: "CloudImageEntity");

            migrationBuilder.AddColumn<int>(
                name: "ProfilePictureId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CloudImageEntity_UserId",
                table: "CloudImageEntity",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CloudImageEntity_AspNetUsers_UserId",
                table: "CloudImageEntity",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CloudImageEntity_AspNetUsers_UserId",
                table: "CloudImageEntity");

            migrationBuilder.DropIndex(
                name: "IX_CloudImageEntity_UserId",
                table: "CloudImageEntity");

            migrationBuilder.DropColumn(
                name: "ProfilePictureId",
                table: "AspNetUsers");

            migrationBuilder.CreateIndex(
                name: "IX_CloudImageEntity_UserId",
                table: "CloudImageEntity",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CloudImageEntity_AspNetUsers_UserId",
                table: "CloudImageEntity",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
