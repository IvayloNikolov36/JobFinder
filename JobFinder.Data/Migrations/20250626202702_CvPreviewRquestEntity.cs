using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFinder.Data.Migrations
{
    /// <inheritdoc />
    public partial class CvPreviewRquestEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CvPreviewRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnonymousProfilePreviewedId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    JobAdId = table.Column<int>(type: "int", nullable: false),
                    IsAccepted = table.Column<bool>(type: "bit", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AcceptedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CvPreviewRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CvPreviewRequests_AnonymousProfiles_AnonymousProfilePreviewedId",
                        column: x => x.AnonymousProfilePreviewedId,
                        principalTable: "AnonymousProfiles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CvPreviewRequests_JobAdvertisements_JobAdId",
                        column: x => x.JobAdId,
                        principalTable: "JobAdvertisements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CvPreviewRequests_AnonymousProfilePreviewedId",
                table: "CvPreviewRequests",
                column: "AnonymousProfilePreviewedId");

            migrationBuilder.CreateIndex(
                name: "IX_CvPreviewRequests_JobAdId",
                table: "CvPreviewRequests",
                column: "JobAdId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CvPreviewRequests");
        }
    }
}
